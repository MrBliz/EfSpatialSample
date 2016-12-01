using System;

namespace EfSpatialSample.Models
{
    public class PointOfInterest
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateAdded { get; set; }
    }
}