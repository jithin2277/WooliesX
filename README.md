# WooliesX Tech Assessment

## Deployed API

https://wooliesxapi20210430055639.azurewebsites.net

## Major Packages used

* Mediatr
* Moq
* FluentValidation
* Automapper
* Serilog
* xUnit

## Patterns followed

* Mediator pattern

## Projects Overview

WooliesX.Domain (.netStandard 2.1 class library)  
Contains the Domain objects for WooliesX API. It's a .netStandard 2.1 project with no dependencies.

WooliesX.Application (.netStandard 2.1 class library)  
Contains application logic. It is dependent on the WooliesX.Domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers.

WooliesX.Infrastructure (.Net Core 3.1 class library)  
Contains classes for accessing external resources such as database connections and caching. It is dependent on the WooliesX.Application layer. These classes are based on interfaces defined within the Application layer.

WooliesX.Web (Asp.Net Core 3.1)  
This is the API layer containing the Controllers. It depends on Application and Infrastructure layers



