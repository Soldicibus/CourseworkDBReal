using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdStatusRepository, AdStatusRepository>();
builder.Services.AddScoped<IAdTypeRepository, AdTypeRepository>();
builder.Services.AddScoped<IAdvertisersRepository, AdvertisersRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IAdCampaignsRepository, AdCampaignsRepository>();
builder.Services.AddTransient<IAdGroupsRepository, AdGroupsRepository>();
builder.Services.AddTransient<IAdRepository, AdRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DataContext>();
/*builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();*/

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
