using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EfSpatialSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfSpatialSample.Commands
{
    public class AddPointOfInterest
    {
        private readonly IDataContext context;

        public AddPointOfInterest(IDataContext context)
        {
            this.context = context;
        }

        public async Task Execute(PointOfInterest model)
        {

            using (var connection = this.context.Database.GetDbConnection())
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                    "Insert INTO PointsOfInterest (DateAdded, Latitude, Longitude, Location) " +
                    "VALUES (@p0, @p1, @p2, geography::Point(@p1, @p2, 4326))";

                    cmd.Parameters.Add(new SqlParameter("@p0", SqlDbType.DateTime));
                    cmd.Parameters["@p0"].Value = model.DateAdded;

                    cmd.Parameters.Add(new SqlParameter("@p1", SqlDbType.Float));
                    cmd.Parameters["@p1"].Value = model.Latitude;

                    cmd.Parameters.Add(new SqlParameter("@p2", SqlDbType.Float));
                    cmd.Parameters["@p2"].Value = model.Longitude;
                            
                    await cmd.ExecuteScalarAsync();
                }
            }
        }
    }
}
