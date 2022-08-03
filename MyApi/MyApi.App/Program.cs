using System.Text.Json;
//using MyApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
// before we "build" the WebApplication, we "set up" things that the middleware will need when they run
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// by default (customizable), for sercurity reasons, this built-in middleware
// only serve files that are inside a folder named wwwroot insinde the project
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


// app.MapGet is part of "minimal api"
//app.MapGet("/", () => "hello world");

//app.MapGet("/map1", async context =>
//{
//    await context.Response.WriteAsync("Hello from map1");
//});

//app.MapGet("/map2", async context =>
//{
//    await context.Response.WriteAsync("Hi there!");
//});


// after you have a WebApplication, you need to construct it's request-processing pipeline using components called "middleware"
// each middleware runs in sequence
// "Use" middleware could be anywhere in the pipeline

//app.UseMiddleware<RequireAuthMiddleware>();
// all of this below is replaced by this ^^
//app.Use(async (context, next) =>
//{
//    if (context.Request.Query["authenticated"] == "true")
//    {
//        // this middleware is done, let the next process take over and do it's thing"
//        await next(context);
//    }
//    else
//    {
//        // don't trigger the next, this middleware will "short-circuit" the pipeline
//        // so we need to set up the response here.

//        context.Response.StatusCode = 401;
//        context.Response.ContentType = "text/plain";
//        await context.Response.WriteAsync("error, not authenticated");
//    }
//});


//app.UseMiddleware<SerializeDataMiddleware>();

//app.Use(async (context, next) =>
//{
//    if (context.Response.ContentType == "text/plain")
//    {
//        context.Response.ContentType = "application/json";
//        context.Response.WriteAsync(JsonSerializer.Serialize(context.Response.Body));
//    }
//    else
//    {
//        await next(context);
//    }
//});




app.Run();
