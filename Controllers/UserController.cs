using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PercobaanAPI7.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PercobaanAPI7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IEnumerable<Person>> Get()
        {
            await ImportDataFromAPI();
            return await _context.People.ToListAsync();
        }

        private async Task ImportDataFromAPI()
        {
            var client = _httpClientFactory.CreateClient();
            string apiUrl = "https://dummy-user-tan.vercel.app/user";

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

                foreach (var item in data)
                {
                    var existingPerson = await _context.People.FindAsync(item.Id);

                    if (existingPerson == null)
                    {
                        _context.People.Add(item);
                    }
                    else
                    {
                        existingPerson.Name = item.Name;
                        existingPerson.Saldo = item.Saldo;
                        existingPerson.Hutang = item.Hutang;
                    }
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch data from API. Status code: {response.StatusCode}");
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var foundPerson = await _context.People.FindAsync(id);
            if (foundPerson == null)
            {
                return NotFound();
            }
            return Ok(foundPerson);
        }

        [HttpPost("sync", Name = "SyncUserDetails")]
        public async Task<ActionResult> SyncUserDetails()
        {
            await ImportDataFromAPI();
            return Ok("Synchronization complete.");
        }
    }
}
