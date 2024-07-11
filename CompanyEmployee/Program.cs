using CompanyEmployee.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;


var builder = WebApplication.CreateBuilder(args);



LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigurePosgreSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CompanyEmployee.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

var logger= app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionsHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//    app.UseDeveloperExceptionPage();
//else
//    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders= ForwardedHeaders.All
});
app.UseCors("CorsPolicy");



app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    Console.WriteLine($"logic before executing the next delegate");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate");
//});

//app.Run(async context =>
//{
//    Console.WriteLine($"writing the response to the client in the run method.");
//    await context.Response.WriteAsync("Hello From middleware components");
//});

app.MapControllers();

app.Run();
