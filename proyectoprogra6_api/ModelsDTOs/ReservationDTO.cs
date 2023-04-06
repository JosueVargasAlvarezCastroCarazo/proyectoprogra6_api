
using proyectoprogra6_api.Models;

namespace proyectoprogra6_api.ModelsDTOs
{
    public class ReservationDTO
    {


        public ReservationDTO()
        {
            
        }

        public ReservationDTO(int reservationId, string notes, DateTime startDate, DateTime endDate, int userId, int itemId)
        {
            ReservationId = reservationId;
            Notes = notes;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
            ItemId = itemId;
        }

        public ReservationDTO(int reservationId, string notes, DateTime startDate, DateTime endDate, int userId, int itemId,string itemName)
        {
            ReservationId = reservationId;
            Notes = notes;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
            ItemId = itemId;
            ItemName = itemName;
        }

        public ReservationDTO(Reservation reservation)
        {
            ReservationId = reservation.ReservationId;
            Notes = reservation.Notes;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
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
            model.UserId = UserId;
            model.ItemId = ItemId;
            return model;
        }

        public int ReservationId { get; set; }
        public string Notes { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }

    }

}
