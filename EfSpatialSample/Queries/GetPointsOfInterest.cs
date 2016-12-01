using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using EfSpatialSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfSpatialSample.Queries
{

    public class GetPointsOfInterest
    {
        private IDataContext context;

        public GetPointsOfInterest(IDataContext context)
        {
            this.context = context;

        }
        
        public async Task<List<PointOfInterest>> Execute(double latitude, double longitude, int radius)
        {
            return await this.context.PointsOfInterest.FromSql(

            "SELECT Id, DateAdded, Latitude, Longitude " +
            "FROM dbo.PointsOfInterest " +
            "WHERE geography::Point(@p0, @p1, 4326).STDistance(Location) <= @p2",
            longitude,
             latitude,
            radius).ToListAsync();
        }
    }
}
