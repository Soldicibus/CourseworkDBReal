using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CourseworkDB.Data.Models;

public class DataContext : DbContext
{
    private readonly string _connectionString;
    public DataContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("default");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Advertiser> Advertisers { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<AdType> AdTypes { get; set; }
    public DbSet<Ad> Ads { get; set; }
    public DbSet<AdGroup> AdGroups { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<AdStatus> AdStatuses { get; set; }
    public DbSet<AdCampaign> AdCampaigns { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        optionsBuilder.UseMySQL(_connectionString);
    }
}
