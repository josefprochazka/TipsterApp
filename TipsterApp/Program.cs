using Microsoft.OpenApi.Models;
using TipsterApp.Components;
using TipsterApp.Data;
using TipsterApp.Services;

var builder = WebApplication.CreateBuilder(args);

var storageType = builder.Configuration["StorageType"];

if (storageType == "Json")
{
    builder.Services.AddSingleton<ITipStorage, JsonTipStorage>();
}
else
{
    builder.Services.AddSingleton<ITipStorage, InMemoryTipStorage>();
}

// background service registration
builder.Services.AddSingleton<StatsBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<StatsBackgroundService>());

// RESP API
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Zadej 'Bearer <token>' do pole níže.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
