namespace UrFUCoworkingsMicroservice.Data.Entities
{
    public class CoworkingSettings
    {
        public Guid Id { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly Opening { get; set; }
        public TimeOnly Closing { get; set; }
        public bool IsWorking { get; set; }
        public Guid CoworkingId { get; set; }
        public virtual Coworking Coworking { get; set; } = null!;
    }
}
