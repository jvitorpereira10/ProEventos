using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEvents.API.Models;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        public IEnumerable<Event> _events = new Event [] {
               new Event(){
                    EventId = 1,
                    Theme = "Angular 11 e .NET 5",
                    Local = "Quintana-SP",
                    Batch = "1º Lote",
                    ImageURL = "https://media.gazetadopovo.com.br/2021/10/29144929/20171109181552__ndr0597-1920x1024-960x540.jpeg",
                    AmountPeople = Convert.ToInt32("250"),
                    EventDate = DateTime.Now.AddDays(2).ToString()
               },
               new Event(){
                    EventId = 1,
                    Theme = "EF e .NET",
                    Local = "Marília-SP",
                    Batch = "1º Lote",
                    ImageURL = "https://media.gazetadopovo.com.br/2021/10/29144929/20171109181552__ndr0597-1920x1024-960x540.jpeg",
                    AmountPeople = Convert.ToInt32("150"),
                    EventDate = DateTime.Now.AddDays(5).ToString()
                }};

        public EventController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {           
            return _events;
        }
        
        [HttpGet("{id}")]
        public IEnumerable<Event> Get(int id)
        {            
            return _events.Where(e => e.EventId == id);
        }
    }
}