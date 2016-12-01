using System.Collections.Generic;
using System.Threading.Tasks;
using EfSpatialSample.Models;
using EfSpatialSample.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EfSpatialSample.Models;
using System;
using EfSpatialSample.Commands;

namespace EfSpatialSample.Controllers
{
    [Route("api/[controller]")]
    public class PointsOfInterestController : Controller
    {
        private readonly IDataContext context;
        private readonly ILogger<PointsOfInterestController> logger;
        
        public PointsOfInterestController(IDataContext context, ILogger<PointsOfInterestController> logger)
        {
            this.context = context;
            this.logger = logger;
          
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<PointOfInterest>> Get(double latitude, double longitude, int radius)
        {         
            var query = new GetPointsOfInterest(this.context);

            return await query.Execute(latitude,longitude, radius);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] PointOfInterestPostModel model)
        {

            var poi = new PointOfInterest {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateAdded = DateTime.UtcNow,
                
            }; 

            var command = new AddPointOfInterest(this.context);
            await command.Execute(poi);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}