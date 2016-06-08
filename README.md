# Selly

## Live demo: http://selly.nbi.ninja

### The code is pretty clear and self explanatory.
Basic flow: 
1.) POST: Client -> Controller action -> BusinessLogic Core -> DataLayer.Extensions repositories -> DataLayer base repositories -> EF context -> Database
2.) GET: Database -> EF Context -> DataLayer base repositories -> DataLayer.Extensions repositories -> BusinessLogic Core -> Controller action -> Client

### Between the BLL and DAL layers, models are validated and translated using AutoMapper. There are 4 model layers used for validating and encapsulating the entire flow of the application. Grouping them by type, we get:
1.) View Models (Website.Models, BLL.Models) -> These are the ones with which the user interacts
2.) Logic Models (DataLayer.Models) -> Used within the application to parse logic and interact with the database.

By using the Response type (from BLL) with OK status code instead of the classic HttpResponses with different http status codes, we enable easy transition to other client types, like mobile devices and gadgets, by offering this easy way of parsing and validating responses.

# Some of the Frameworks, Patterns and Workflows
- N-Tier setup of the project
- Entity Framework (datalayer)
- ASP.NET MVC WebApi (website project)
- SQL Server Express 2014
- Repository and Unit of work pattern (datalayer and datalayer.extensions)
- Proper IOC for repositories, easily configurable (datalayer.extensions -> unit of work)
- Factory pattern (BLL.Models.Response)
- AngularJS, JQuery, Bootstrap (for UI)
- Microsoft testing module, FluentAssertions and Autofixture for Unit Tests and Integration Tests
- ITextSharp for writing documents
- NLog for logging purposes
And more on the road
