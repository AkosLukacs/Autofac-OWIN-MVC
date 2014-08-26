Demo for OWIN + MVC Controller + Autofac's InstancePerRequest 'bug'?

`ExampleService` is registered as InstancePerRequest, so one instance of it should be created by a http request.
For web api controllers/actions this works, but for MVC controller, a new instance is created.

Trace output after a request to the API controller (/api/apihome):

    ExampleMiddleware ctor 
    ExampleClass ctor bdfa75e5-cf9c-47b0-85a4-4b9c88f3b62b 
    ExampleService 'bdfa75e5-cf9c-47b0-85a4-4b9c88f3b62b', msg: from ExampleMiddleware 
    ExampleService 'bdfa75e5-cf9c-47b0-85a4-4b9c88f3b62b', msg: from api controller 
    Disposing ExampleService 'bdfa75e5-cf9c-47b0-85a4-4b9c88f3b62b' 

Trace output after a request to MVC controller (/): 

    ExampleMiddleware ctor 
    ExampleClass ctor 90c0b9b7-ec44-41c8-921d-af0f08873ab9 
    ExampleService '90c0b9b7-ec44-41c8-921d-af0f08873ab9', msg: from ExampleMiddleware 
    ExampleClass ctor 6fcfd1aa-e0c2-40de-bd10-b31fa4aa52f1 
    ExampleService '6fcfd1aa-e0c2-40de-bd10-b31fa4aa52f1', msg: from HomeController 
    Disposing ExampleService '6fcfd1aa-e0c2-40de-bd10-b31fa4aa52f1' 
    Disposing ExampleService '90c0b9b7-ec44-41c8-921d-af0f08873ab9' 
