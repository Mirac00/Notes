using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Notes.API.Data;
using Notes.API.Models.Entities;

namespace Notes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly TableDbContext TableDbContext;

        public NotesController(TableDbContext TableDbContext)
        {
            this.TableDbContext = TableDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await TableDbContext.Notes.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNoteById")]
        public async Task<IActionResult> GetNoteById([FromRoute] Guid id)
        {
            var note = await TableDbContext.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            await TableDbContext.Notes.AddAsync(note);
            await TableDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note updatedNote)
        {
            var existingNote = await TableDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.IsVisable = updatedNote.IsVisable;

            await TableDbContext.SaveChangesAsync();

            return Ok(existingNote);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var existingNote = await TableDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            TableDbContext.Notes.Remove(existingNote);

            await TableDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
