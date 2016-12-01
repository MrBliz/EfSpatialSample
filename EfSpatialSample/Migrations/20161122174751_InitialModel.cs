using Microsoft.EntityFrameworkCore.Migrations;

namespace EfSpatialSample.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "PointsOfInterest",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(nullable: false),
            //         DateAdded = table.Column<DateTime>(nullable: false),
            //         Latitude = table.Column<double>(nullable: false),
            //         Longitude = table.Column<double>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_PointsOfInterest", x => x.Id);
            //     });


            migrationBuilder.Sql($"CREATE TABLE [dbo].[PointsOfInterest]" +
"(" +
" [Id] [uniqueidentifier]  NOT NULL DEFAULT NEWSEQUENTIALID(), " +
"[DateAdded] [datetime2](7)  NOT NULL," +
"[Latitude] [float]  NOT NULL,  " +
"[Longitude] [float]  NOT NULL, " +
"[Location] [geography] NOT NULL " +
") " +
"ALTER TABLE [dbo].[PointsOfInterest] ADD CONSTRAINT PK_PointsOfInterest PRIMARY KEY  ([Id])"
+ "CREATE SPATIAL INDEX SIndx_PointsOfInterest_geography_Location ON PointsOfInterest(Location); "
);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointsOfInterest");
        }
    }
}
