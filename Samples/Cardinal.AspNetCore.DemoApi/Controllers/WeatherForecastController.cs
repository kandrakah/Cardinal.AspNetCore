using Cardinal.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore.DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Requisição bem sucedida.</response>
        /// <response code="400">A requisição não pôde ser atendida por uma inconsistência nos parâmetros informados.</response>  
        /// <response code="401">O requisitante não possui autorização para acesso aos parâmetros globais.</response>  
        /// <response code="404">O parâmetro buscado não foi encontrado ou não existe.</response>       
        /// <response code="500">Houve uma falha interna ao tratar a requisição.</response> 
        [HttpGet]
        //[Permission(Method.Get, PermissionValidationType.RequireAuthenticatedOnly)]
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
    }
}
