﻿/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Core.Domain;
using Core.Domain.Accounts;
using Core.Dtos;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Extensions;

namespace Service.Controllers.CustomControllers
{
    [RoutePrefix("api/contacts")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CrudApiController<TEntity,TEntityDto> : ApiController where TEntity : BaseEntity<TEntity>, new()
    {
        protected IRepository<TEntity> Repository { get; set; }
        private readonly IComplete _unitOfWork;

        public CrudApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (IComplete)unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ICollection<AccountDto>))]
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var entities = await Repository.GetAll()
                .Include(a => a.Partners)
                .ToListAsync();

            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts.Map<ICollection<AccountDto>>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(AccountDto))]
        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var account = await _repository.GetAsync(id);

            if (account == null)
            {
                return NotFound();
            }
            return Ok(account.Map<AccountDto>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> UpdateAsync(int id, AccountDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var accountInDb = await _repository.GetAsync(id);

            if (accountInDb == null)
            {
                return NotFound();
            }

            _repository.Add(account.Map<Account>());

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateAsync(AccountDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.Add(account.Map<Account>());

            await _unitOfWork.CompleteAsync();

            return Created(new Uri(Request.RequestUri + "/" + account.Id), account);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var account = await _repository.GetAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            _repository.Remove(account);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}*/