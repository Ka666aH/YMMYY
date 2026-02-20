using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Application.Services;
using Infrastructure.Cache;
using Infrastructure.RecipesSource;
using Infrastructure.Translator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Environment.GetEnvironmentVariable("REDIS_ADDRESS") ??
        throw new Exception("Redis address is not set.");
});

builder.Services.AddScoped<ICache, RedisCache>();
builder.Services.AddScoped<ITranslator, LibreTranslator>();
builder.Services.AddScoped<IRecipesSource, Spoonacular>();

builder.Services.AddHttpClient<LibreTranslator>();
builder.Services.AddHttpClient<Spoonacular>();

builder.Services.AddScoped<ITranslatorService, TranslatorService>();
builder.Services.AddScoped<IRecipesSourceService, RecipesSourceService>();

builder.Services.AddScoped<IRecipesFinder, RecipesFinder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "api");
});
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();