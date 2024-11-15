namespace UrFUCoworkingsMicroservice.Data.Entities
{
    public class Coworking
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeOnly Opening { get; set; }
        public TimeOnly Closing { get; set; }
        public virtual List<Zone> Zones { get; set; } = null!;
        public virtual List<CoworkingSettings> Settings { get; set; } = null!;
    }
}
