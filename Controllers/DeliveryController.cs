
using DeliveryAPI.Data;
using DeliveryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryDbContext _context;

        public DeliveryController(DeliveryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetVehicles()
            => await _context.Deliveries.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null) return NotFound();
            return delivery;
        }

        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDelivery), new { id = delivery.Id }, delivery);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id) return BadRequest();
            _context.Entry(delivery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null) return NotFound();
            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveriesByVehicle(int vehicleId)
        {
            var vehicleExists = await _context.Vehicles.AnyAsync(v => v.Id == vehicleId);

            if (!vehicleExists)
            {
                return NotFound($"Veículo com ID {vehicleId} não encontrado.");
            }

            var deliveries = await _context.Deliveries
                .Where(d => d.IdVehicle == vehicleId)
                .Include(d => d.Orders)
                .ToListAsync();

            if (!deliveries.Any())
            {
                return NotFound($"Nenhuma entrega encontrada para o veículo com ID {vehicleId}.");
            }

            return Ok(deliveries);
        }
    }
}
