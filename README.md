# WooliesX Tech Assessment

## Deployed API

https://wooliesxapi20210430055639.azurewebsites.net

## Endpoints
Excercise 1 endpoint: https://wooliesxapi20210430055639.azurewebsites.net/api/answers/user  
Excercise 2 endpoint: https://wooliesxapi20210430055639.azurewebsites.net/api/products/sort?sortOption=Ascending  
Excercise 3 endpoint: https://wooliesxapi20210430055639.azurewebsites.net/api/trolleyTotal  

## Major Packages used

* Mediatr
* FluentValidation
* Automapper
* Serilog
* xUnit
* Moq

## Projects Overview
Based on Clean Architecture   

WooliesX.Domain (.netStandard 2.1 class library)  
Contains the Domain objects for WooliesX API. It's a .netStandard 2.1 project with no dependencies.

WooliesX.Application (.netStandard 2.1 class library)  
Contains application logic. It is dependent on the WooliesX.Domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers.

WooliesX.Infrastructure (.Net Core 3.1 class library)  
Contains classes for accessing external resources such as database connections and caching. It is dependent on the WooliesX.Application layer. These classes are based on interfaces defined within the Application layer.

WooliesX.Web (Asp.Net Core 3.1)  
This is the API layer containing the Controllers. It depends on Application and Infrastructure layers

## References
https://jasontaylor.dev/clean-architecture-getting-started/
