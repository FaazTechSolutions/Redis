using Cache.Entity;
using Cache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheController> _logger;
        private readonly CacheContext _context;
        string cacheKey = $"Employee";

        public CacheController(IDistributedCache cache, ILogger<CacheController> logger, CacheContext context)
        {
            _cache = cache;
            _logger = logger;
            _context = context;
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            string cacheKey = $"product_{id}";
            string? cachedData = await _cache.GetStringAsync(cacheKey);

            if (cachedData != null)
            {
                _logger.LogInformation("Cache hit for product {Id}", id);
                return Ok(JsonSerializer.Deserialize<Product>(cachedData));
            }

            // Simulate DB fetch
            var product = new Product { Id = id, Name = "Laptop", Price = 999.99M };

            _logger.LogInformation("Cache miss for product {Id}. Storing in cache...", id);

            var serialized = JsonSerializer.Serialize(product);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return Ok(product);
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployees()
        {
            string? cachedData = await _cache.GetStringAsync(cacheKey);

            if (cachedData != null)
            {
                _logger.LogInformation("Cache hit for Employee");
                return Ok(JsonSerializer.Deserialize<List<Employee>>(cachedData));
            }

            //DB fetch
           var employeeList = _context.Employees.ToList();

            _logger.LogInformation("Cache miss for employee. Storing in cache...");

            var serialized = JsonSerializer.Serialize(employeeList);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return Ok(employeeList);
        }


        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee(Employee emp)
        {
            _logger.LogInformation($"Creating new data");

            _context.Employees.Add(emp);
            _context.SaveChanges();

            _logger.LogInformation($"{cacheKey} cache removed");

            string? cachedData = await _cache.GetStringAsync(cacheKey);

            if (cachedData != null)
            {
                await _cache.RemoveAsync(cacheKey);
            }

            return Ok(emp);
        }

    }
}

