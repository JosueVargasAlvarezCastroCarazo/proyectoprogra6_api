
using proyectoprogra6_api.Models;

namespace proyectoprogra6_api.ModelsDTOs
{
    public class ReservationDTO
    {


        public ReservationDTO()
        {
            
        }

        public ReservationDTO(Reservation reservation)
        {
            ReservationId = reservation.ReservationId;
            Notes = reservation.Notes;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            StartTime = reservation.StartTime;
            EndTime = reservation.EndTime;
            UserId = reservation.UserId;
            ItemId = reservation.ItemId;
        }

        public Reservation getNativeModel()
        {
            Reservation model = new Reservation();
            model.ReservationId = ReservationId;
            model.Notes = Notes;
            model.StartDate = StartDate;
            model.EndDate = EndDate;
            model.StartTime = StartTime;
            model.EndTime = EndTime;
            model.UserId = UserId;
            model.ItemId = ItemId;
            return model;
        }

        public int ReservationId { get; set; }
        public string Notes { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

    }

}
