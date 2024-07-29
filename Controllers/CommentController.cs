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
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comment
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }

        // GET: Comment/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: Comment
        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
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

        // PUT: Comment/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(comment).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new { message = $"{errors}" });
        }


        // POST: Comment/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);

            if (comment == null)
            {
                return NotFound(new { message = $"Comment with ID {id} not found." });
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Comment with ID {id} deleted successfully." });
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
