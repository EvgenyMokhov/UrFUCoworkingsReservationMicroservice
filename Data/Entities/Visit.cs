using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UrFUCoworkingsMicroservice.Data.Entities
{
    [Index(nameof(Id))]
    [Index(nameof(UserId), nameof(ReservationId))]
    public class Visit
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        [Key]
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; } = null!;
    }
}
