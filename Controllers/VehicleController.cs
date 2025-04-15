
using DeliveryAPI.Data;
using DeliveryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DeliveryDbContext _context;

        public VehicleController(DeliveryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
            => await _context.Vehicles.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return vehicle;
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id) return BadRequest();
            _context.Entry(vehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/deliveries")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveriesByVehicle(int id)
        {
            var deliveries = await _context.Deliveries
                .Where(d => d.IdVehicle == id)
                .Include(d => d.Orders)
                .ToListAsync();

            if(deliveries == null || !deliveries.Any())
            {
                return NotFound($"Nenhuma entrega encontrada para o veículo com ID {id}.");
            }

            return Ok(deliveries);
        }

    }
}
