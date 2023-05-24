using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Notes.API.Data;
using Notes.API.Models.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Event111sController : ControllerBase
    {
        private readonly TableDbContext _tableDbContext;

        public Event111sController(TableDbContext tableDbContext)
        {
            _tableDbContext = tableDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            List<Event111> events = await _tableDbContext.Event111s.ToListAsync();
            return Ok(events);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            Event111 event111 = await _tableDbContext.Event111s.FindAsync(id);

            if (event111 == null)
            {
                return NotFound();
            }

            return Ok(event111);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event111 event111)
        {
            event111.Id = Guid.NewGuid();
            await _tableDbContext.Event111s.AddAsync(event111);
            await _tableDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = event111.Id }, event111);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateEvent(Guid id, Event111 updatedEvent)
        {
            Event111 existingEvent = await _tableDbContext.Event111s.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.StartDate = updatedEvent.StartDate;
            existingEvent.EndDate = updatedEvent.EndDate;
            existingEvent.IsVisable = updatedEvent.IsVisable;

            await _tableDbContext.SaveChangesAsync();

            return Ok(existingEvent);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            Event111 existingEvent = await _tableDbContext.Event111s.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            _tableDbContext.Event111s.Remove(existingEvent);

            await _tableDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
