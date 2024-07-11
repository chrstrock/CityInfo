using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly CityInfoContext _context;

        public PointOfInterestController(CityInfoContext context)
        {
            _context = context;
        }

        // GET: api/PointOfInterest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterest>>> GetPointsOfInterest()
        {
            return await _context.PointsOfInterest.ToListAsync();
        }

        // GET: api/PointOfInterest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PointOfInterest>> GetPointOfInterest(int id)
        {
            var pointOfInterest = await _context.PointsOfInterest.FindAsync(id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return pointOfInterest;
        }

        // PUT: api/PointOfInterest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointOfInterest(int id, PointOfInterest pointOfInterest)
        {
            if (id != pointOfInterest.Id)
            {
                return BadRequest();
            }

            _context.Entry(pointOfInterest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointOfInterestExists(id))
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

        // POST: api/PointOfInterest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PointOfInterest>> PostPointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Add(pointOfInterest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPointOfInterest", new { id = pointOfInterest.Id }, pointOfInterest);
        }

        // DELETE: api/PointOfInterest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointOfInterest(int id)
        {
            var pointOfInterest = await _context.PointsOfInterest.FindAsync(id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            _context.PointsOfInterest.Remove(pointOfInterest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointOfInterestExists(int id)
        {
            return _context.PointsOfInterest.Any(e => e.Id == id);
        }
    }
}
