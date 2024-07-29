using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign3.Data;
using Assign3.Models;

namespace Assign3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carts = await _context.Carts.ToListAsync();
            return Ok(carts);
        }

        // GET: Cart/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // POST: Cart
        [HttpPost]
        public async Task<IActionResult> CreateCart(Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
            }
            // Extract validation errors
            var errors = ModelState
                .Where(m => m.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(errors);
        }

        // PUT: Cart/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(cart).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new { message = $"{errors}" });
        }


        // POST: Cart/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(m => m.Id == id);

            if (cart == null)
            {
                return NotFound(new { message = $"Cart with ID {id} not found." });
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Cart with ID {id} deleted successfully." });
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
