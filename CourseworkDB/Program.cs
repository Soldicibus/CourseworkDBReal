using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DataContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
/*builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IAdTypesRepository, AdTypesRepository>();
builder.Services.AddTransient<IAdStatusRepository, AdStatusRepository>();
builder.Services.AddTransient<IAdvertisersRepository, AdvertisersRepository>();
builder.Services.AddTransient<IPublishersRepository, PublishersRepository>();
builder.Services.AddTransient<IAdCampaignsRepository, AdCampaignsRepository>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);

app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
