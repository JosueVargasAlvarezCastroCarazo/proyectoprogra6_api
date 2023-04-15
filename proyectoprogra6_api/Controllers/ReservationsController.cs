using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoprogra6_api.Attributes;
using proyectoprogra6_api.Models;
using proyectoprogra6_api.ModelsDTOs;

namespace proyectoprogra6_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class ReservationsController : ControllerBase
    {
        private readonly proyectoprogra6Context _context;

        public ReservationsController(proyectoprogra6Context context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }


        //busca reservas segun fechas y un id de un usuario
        [HttpGet("Search")]
        public ActionResult<IEnumerable<ReservationDTO>> GetReservationsDates(int UserId, string Start, string End)
        {

            DateTime StartDate = DateTime.ParseExact(Start, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact(End, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            var query = new List<ReservationDTO>();

            
            if (UserId == 0)
            {
                query = (
                        from u in _context.Reservations
                        orderby u.StartDate descending
                        join i in _context.Items on u.ItemId equals i.ItemId
                        where u.StartDate >= StartDate && u.EndDate <= EndDate
                        select new ReservationDTO(
                            u.ReservationId,
                            u.Notes,
                            u.StartDate,
                            u.EndDate,
                            u.UserId,
                            u.ItemId,
                            i.ItemName
                            )
                        ).ToList();
            }
            else
            {
                query = (
                        from u in _context.Reservations
                        orderby u.StartDate descending
                        join i in _context.Items on u.ItemId equals i.ItemId
                        where u.StartDate >= StartDate && u.EndDate <= EndDate && u.UserId == UserId
                        select new ReservationDTO(
                            u.ReservationId,
                            u.Notes,
                            u.StartDate,
                            u.EndDate,
                            u.UserId,
                            u.ItemId,
                            i.ItemName
                            )
                        ).ToList();
            }
            return query;
        }

        //busca reservas segun un item de una reserva
        [HttpGet("SearchByItemId")]
        public ActionResult<IEnumerable<ReservationDTO>> SearchByItemId(int ItemId)
        {

            var query = new List<ReservationDTO>();

            query = (
                    from u in _context.Reservations
                    orderby u.StartDate descending
                    join i in _context.Items on u.ItemId equals i.ItemId
                    where i.ItemId == ItemId
                    select new ReservationDTO(
                        u.ReservationId,
                        u.Notes,
                        u.StartDate,
                        u.EndDate,
                        u.UserId,
                        u.ItemId,
                        i.ItemName
                        )
                    ).ToList();
           
            return query;
        }

        // GET: api/Reservations/5 obtiene una reserva segun un id
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5 actualiza una reserva
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservation)
        {


            var query = (
                   from u in _context.Reservations
                   where u.ItemId == reservation.ItemId && ((reservation.StartDate >= u.StartDate && reservation.StartDate <= u.EndDate) || (reservation.EndDate <= u.EndDate && reservation.EndDate >= u.StartDate))
                   select new ReservationDTO(
                       u.ReservationId,
                       u.Notes,
                       u.StartDate,
                       u.EndDate,
                       u.UserId,
                       u.ItemId
                       )
                   ).ToList();


            if (query.Count > 0 && query.First().ReservationId != id)
            {
                return NotFound();
            }
            else
            {

                if (id != reservation.ReservationId)
                {
                    return BadRequest();
                }

                _context.Entry(reservation.getNativeModel()).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
        }

        // POST: api/Reservations crea una nueva reserva
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationDTO reservation)
        {


            var query = (
                    from u in _context.Reservations
                    where u.ItemId == reservation.ItemId && ( (reservation.StartDate >= u.StartDate && reservation.StartDate <= u.EndDate) || (reservation.EndDate <= u.EndDate && reservation.EndDate >= u.StartDate))
                    select new ReservationDTO(
                        u.ReservationId,
                        u.Notes,
                        u.StartDate,
                        u.EndDate,
                        u.UserId,
                        u.ItemId
                        )
                    ).ToList();


            if (query.Count > 0)
            {
                return NotFound();
            }
            else
            {
                _context.Reservations.Add(reservation.getNativeModel());
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetReservation", new { id = reservation.ReservationId }, reservation);
            }

            
        }

        // DELETE: api/Reservations/5 elimina una reserva
        //en este caso si se elimina de verdad
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
