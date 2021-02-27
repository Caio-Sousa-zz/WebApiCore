using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : MainController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot",
            "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-weather")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("get-all-weather")]
        public ActionResult<IEnumerable<WeatherForecast>> GetAll()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(result);
        }

        [HttpGet("get-all-weather")]
        public ActionResult<IEnumerable<string>> GetResult()
        {
            return new List<string>() { "1", "2", "3" };
        }

        [HttpGet("get")]
        public string Get(int id)
        {
            return "Hello World";
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult Post(Product p)
        {
            if (p.Id == 0)
                return CustomResponse();

            // add no banco

            // return Ok(p); // Mesma coisa mas retorna 200
            return CustomResponse(p);
        }

        [HttpPost("post-teste")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult PostTest(Product p)
        {
            return CreatedAtAction(nameof(Post), p);
        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public ActionResult Put(int id,
                        [FromBody] string value,
                        [FromForm] Product prod)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != prod.Id) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public void Delete(int id,
                           [FromHeader] string header,
                           [FromQuery] string queryString)
        {

        }
    }

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(new
                {
                    success = true,
                    data = result
                });

            return BadRequest(new
            {
                success = false,
                erros = ObterErros()
            });
        }

        public bool ValidOperation()
        {
            // Validações
            return true;
        }

        protected string ObterErros()
        {
            return "";
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}