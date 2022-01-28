using JokeMachine.Authentication;
using JokeMachine.Models;
using JokeMachine.Services;
using JokeMachine.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IServiceCollection services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMvc().AddSessionStateTempDataProvider();
services.AddSession();
services.AddTransient<ApiKeyAuthenticationHandler>();
services.AddTransient<IGetApiKeyQuery, InMemoryGetApiKeyQuery>();
services.AddSingleton<IJokeService, JokeService>();
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
}).AddApiKeySupport(options => { });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
