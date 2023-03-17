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

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

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

        // POST: api/Users
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

        // DELETE: api/Users/5
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








    }

}
