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

namespace proyectoprogra6_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class ItemsController : ControllerBase
    {
        private readonly proyectoprogra6Context _context;

        public ItemsController(proyectoprogra6Context context)
        {
            _context = context;
        }

        // GET: api/Items get items with search
        [HttpGet("Search")]
        public ActionResult<IEnumerable<ItemDTO>> GetItemsSearch(bool active,string search)
        {
            var query = (
                    from u in _context.Items
                    where u.Active == active && (u.ItemName.Contains(search) || u.ItemDescription.Contains(search) || u.Code.Contains(search))
                    select new ItemDTO(
                        u.ItemId,
                        u.ItemName,
                        u.ItemDescription,
                        u.Code,
                        u.Active)
                    ).ToList();

            return query;
        }

        //get active items
        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> GetItems(bool active)
        {
            var query = (
                   from u in _context.Items
                   where u.Active == active
                   select new ItemDTO(
                       u.ItemId,
                       u.ItemName,
                       u.ItemDescription,
                       u.Code,
                       u.Active)
                   ).ToList();

            return query;
        }

        // GET: api/Items/5 get item with one id
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ItemDTO(item);
        }

        // PUT: api/Items/5 update one item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ItemDTO item)
        {
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(item.getNativeModel()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items create a new item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemDTO item)
        {
            _context.Items.Add(item.getNativeModel());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
