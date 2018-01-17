# Splitbook - Performance & Risk Reporting App

## Description
Initially started as a 2 week DDD challenge Splitbook is a Performance & Risk Reporting corporate app developed as a part of building my personal application portfolio.
This git repository should be treated as work-in-progress rather than the final product. I commit daily so the project changes rapidly.

## Running
The application is configured to run with a local instance of SQL Server 2014 Express, in order to setup the database run
update-database from the nuget commandline to run the migrations. Once the database is set up simply compile and run from Visual Studio 2017.
Username: DemoUser
Password: Secret1#

## Current Features
- Add and Edit Contacts, Partners, Accounts, Portfolios
- List Contacts, Partners, Accounts, Portfolios and Tasks (w/ pagination, sorting and search)
- Close Account, Contact, Partner
- View Portfolio Details i.e Linked Partners and Assets with its prices
- Download Pdf Reports from remote storage
- Log into the system, change password

## Patterns and Practices Used
- Domain Driven Development
- Unit of Work
- Sitemap
- SOLID
- Revealing Module Pattern
- IoC (DI)
- MVC (MVVM Postback)
- Standard Gang of Four: Facade, Singleton, Factory, Decorator

## Api 
The application exposes a standard set of async crud REST services which allow interacting with the application and fetching json serialized data. A service is
exposed for each aggregate root of the domain model. By default the service is hosted on the 60520 port i.e localhost:60520/ running from iis express.

All standard routes are prefixed with /api.
example GET:
localhost:60520/api/contacts/1

## Dependencies
### .NET
MVC 5, Entity Framework 6 (Code First), Web Api 2, AutoMapper, FluentValidation, Ninject, NUnit, log4Net, Identity Framework 2, Owin, Sitemap, Newtonsoft, Attribute Routing

### JavaScript 
jQuery, underscore.js, bootbox.js, datatables, bootstrap, moment.js

### CSS
bootstrap, font-awesome, admin theme bootstrap metro ui

## Tests
The tests should be run with reSharper, the project has low test coverage as TDD falls out of the scope of this exercise.