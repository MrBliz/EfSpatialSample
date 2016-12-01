using System.Threading;
using System.Threading.Tasks;
using EfSpatialSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EfSpatialSample
{
    public interface IDataContext
    {
        DbSet<PointOfInterest> PointsOfInterest { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DatabaseFacade Database { get; }

    }

    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public override DatabaseFacade Database
        {
            get { return base.Database; }
        }
    }
}