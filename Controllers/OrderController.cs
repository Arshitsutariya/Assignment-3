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
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // GET: Order/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
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

        // PUT: Order/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(order).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new { message = $"{errors}" });
        }


        // POST: Order/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Order with ID {id} deleted successfully." });
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
