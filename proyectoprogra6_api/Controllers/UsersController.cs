using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoprogra6_api.Attributes;
using proyectoprogra6_api.Models;
using proyectoprogra6_api.ModelsDTOs;
using proyectoprogra6_api.Tools;

namespace proyectoprogra6_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly proyectoprogra6Context _context;

        public UsersController(proyectoprogra6Context context)
        {
            _context = context;
        }


        //filtra la lista de usuarios
        [HttpGet("Search")]
        public  ActionResult<IEnumerable<UserDTO>> GetUsersSearch(bool active, string search)
        {

            var query = (
                  from u in _context.Users
                  where u.Active == active && (u.Name.Contains(search) || u.Identification.Contains(search) || u.Email.Contains(search))
                  select new UserDTO(
                      u.UserId,
                      u.Name,
                      u.PhoneNumber,
                      u.Address,
                      u.LoginPassword,
                      u.IsAdmin,
                      u.Identification,
                      u.Active,
                      u.Email)
                  ).ToList();

            return query;
        }


        // GET: api/Users
        [HttpGet]
        public  ActionResult<IEnumerable<UserDTO>> GetUsers(bool active)
        {

            var query = (
                  from u in _context.Users
                  where u.Active == active
                  select new UserDTO(
                      u.UserId,
                      u.Name,
                      u.PhoneNumber,
                      u.Address,
                      u.LoginPassword,
                      u.IsAdmin,
                      u.Identification,
                      u.Active,
                      u.Email)
                  ).ToList();

            return query;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO(user);
        }

        // PUT: api/Users/5 actualiza un usuario, este metodo no cambia la contra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user.getNativeModel()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        //actualiza un usaurio, ademas encripta la contraseña porque fue que se cambio
        [HttpPut("password/{id}")]
        public async Task<IActionResult> PutUserChangePassword(int id, UserDTO user)
        {
            
            String EncriptedPassword = new Crypto().EncriptarEnUnSentido(user.LoginPassword);
            user.LoginPassword = EncriptedPassword;

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user.getNativeModel()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users crea un usuario y encripta la contra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO user)
        {
            String EncriptedPassword = new Crypto().EncriptarEnUnSentido(user.LoginPassword);
            user.LoginPassword = EncriptedPassword;

            _context.Users.Add(user.getNativeModel());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5 elimina un usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }





        // my methods 

        //devuelve un usurio segun identificacion y contra
        [HttpGet("LoginUser")]
        //this use query string
        public async Task<ActionResult<UserDTO>> LoginUser(string identification, string password)
        {

            String EncriptedPassword = new Crypto().EncriptarEnUnSentido(password);

            User? user = await _context.Users.SingleOrDefaultAsync(e => e.Identification == identification && e.LoginPassword == EncriptedPassword);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO(user);
        }

        [HttpGet("CheckIdentification")]
        //devuelve un usuario segun una identificacion
        //this use query string
        public async Task<ActionResult<UserDTO>> CheckIdentification(string identification)
        {

            User? user = await _context.Users.SingleOrDefaultAsync(e => e.Identification == identification);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO(user);
        }




        // POST: api/Users crea un usuario y encripta la contra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SaveCode")]
        public async Task<IActionResult> SaveCode(RecoveryCode Code)
        {
            _context.RecoveryCodes.Add(Code);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("HelpResquest")]
        public async Task<IActionResult> HelpResquest(HelpRequest Request)
        {
            _context.HelpRequests.Add(Request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Users/GetHelpResquest
        [HttpGet("GetHelpResquest")]
        public async Task<ActionResult<IEnumerable<HelpRequest>>> GetHelpResquest()
        {
            return await _context.HelpRequests.ToListAsync();
        }

        // DELETE: api/HelpResquest/5
        [HttpDelete("HelpResquest/{id}")]
        public async Task<IActionResult> DeleteHelpResquest(int id)
        {
            var help = await _context.HelpRequests.FindAsync(id);
            if (help == null)
            {
                return NotFound();
            }

            _context.HelpRequests.Remove(help);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }

}
