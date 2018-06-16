﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Core.Domain.Assets;
using Core.Domain.Portfolios;
using Core.Enums.Domain;
using Core.Interfaces;
using Core.Interfaces.Repositories.Business;
using Infrastructure.AutoMapper;
using Service.Dtos.Asset;
using Service.Dtos.Shared;
using Service.Filters;

namespace Service.Controllers
{
    [RoutePrefix("api/assets")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AssetApiController : BaseApiController
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IBondRepository _bondRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IComplete _unitOfWork;

        public AssetApiController(IUnitOfWork unitOfWork)
        {
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.SerializerSettings.ContractResolver = new AssetContractResolver();

            _unitOfWork = (IComplete)unitOfWork;
            _assetRepository = unitOfWork.Assets;
            _bondRepository = unitOfWork.Bonds;
            _portfolioRepository = unitOfWork.Portfolios;
        }

        [ResponseType(typeof(ICollection<AssetDto>))]
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var assets = await _assetRepository.GetAllAssetsWithPricesAsync();

            if (assets == null)
            {
                return NotFound();
            }

            return Ok(assets.Map<ICollection<AssetDto>>());
        }

        [ResponseType(typeof(AssetDto))]
        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var asset = await _assetRepository.GetAsync(id);

            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset.Map<AssetDto>());
        }

        [ResponseType(typeof(decimal))]
        [HttpPost, Route("{assetId}/portfolios/{portfolioId}/returns")]
        public async Task<IHttpActionResult> GetReturnsForCalculationPeriodAsync(int assetId, int portfolioId, ICollection<DatePeriodDto> calculationPeriods)
        {
            var assetInDb = await _assetRepository.GetAsync(assetId);

            if (assetInDb == null)
            {
                return NotFound();
            }

            var portfolioInDb = await _portfolioRepository.GetAsync(portfolioId);

            if (portfolioInDb == null)
            {
                return NotFound();
            }

            var submittedPeriods = new List<Tuple<DateTime, DateTime>>();
            foreach (var period in calculationPeriods)
            {
                submittedPeriods.Add(new Tuple<DateTime, DateTime>(
                    DateTime.ParseExact(period.DateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(period.DateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ));
            }

            var initialDatetime = submittedPeriods.Min(p => p.Item1).Date;
            var finalDatetime = submittedPeriods.Max(p => p.Item2).Date;

            try
            {
                var periodIncomes = portfolioInDb.Positions
                    .Where(p => p.AssetId == assetId && p.PortfolioId == portfolioId)
                    .Where(p => p.Timestamp >= initialDatetime && p.Timestamp <= finalDatetime)
                    .Select(p => new Tuple<decimal, DateTime>(p.Amount, p.Timestamp))
                    .ToList();

                assetInDb.CalculateReturn(ReturnType.HoldingPeriodReturn, submittedPeriods, periodIncomes);
                var calculatedReturnRate = assetInDb.Returns
                    .Where(r => r.Id == 0)
                    .Select(crr => crr.Rate)
                    .SingleOrDefault();

                _assetRepository.Add(assetInDb);

                return Ok(calculatedReturnRate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ResponseType(typeof(ICollection<AssetDto>))]
        [HttpGet, Route("portfolios/{id}")]
        public async Task<IHttpActionResult> GetByPortfolioAsync(int id)
        {
            var assets = await _assetRepository.GetAllAssetsWithDetailsByPortfolioAsync(id);

            if (assets == null)
            {
                return NotFound();
            }
            
            // TODO: Move the logic to a service
            var assetsDtos = assets.Map<ICollection<AssetDto>>();
            foreach (var assetDto in assetsDtos)
            {
                var lastHoldingPeriodReturn = assets.Select(a => a.Returns
                    .Where(r => r.Type == ReturnType.HoldingPeriodReturn && r.AssetId == assetDto.Id)
                    .OrderByDescending(r => r.CalculatedTime).SingleOrDefault())
                    .Select(r => r?.Rate)
                    .FirstOrDefault();

                var bondAsset = await _bondRepository.GetBondAssetWithCurrencyAsync(assetDto.Id);

                if (bondAsset != null)
                {
                    assetDto.CurrencyCode = bondAsset.Currency.Code;
                }
                assetDto.HoldingPeriodReturnRate = lastHoldingPeriodReturn;
            }

            return Ok(assetsDtos);
        }

        [ResponseType(typeof(AssetPriceDto))]
        [HttpGet, Route("prices")]
        public async Task<IHttpActionResult> GetPricesAsync()
        {
            var assetPrices = await _assetRepository.GetPrices(p => p.Asset != null);

            if (assetPrices == null)
            {
                return NotFound();
            }
            return Ok(assetPrices.Map<ICollection<AssetPriceDto>>());
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> UpdateAsync(int id, AssetDto asset)
        {
            var assetInDb = await _assetRepository.GetAsync(id);

            if (assetInDb == null)
            {
                return NotFound();
            }

            _assetRepository.Add(asset.Map<Asset>());

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPost, Route("")]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateAsync(AssetDto asset)
        {
            _assetRepository.Add(asset.Map<Asset>());

            await _unitOfWork.CompleteAsync();

            return Created(new Uri(Request.RequestUri + "/" + asset.Id), asset);
        }

        [HttpDelete, Route("{id}/delete")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var asset = await _assetRepository.GetAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            _assetRepository.Remove(asset);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
