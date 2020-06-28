using DisTrack.Data;
using DisTrack.Domain.Interfaces;
using DisTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisTrack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : Controller
    {
        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTrip([FromBody] TripViewModel tripViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTrip = new Trip
                {
                    Departure = tripViewModel.Departure,
                    Destination = tripViewModel.Destination,
                    DepartureTime = tripViewModel.DepartureTime,
                    ArrivalTime = tripViewModel.ArrivalTime,
                    Cost = tripViewModel.Cost,
                    Miles = tripViewModel.Miles
                };

                await _tripService.AddTrip(newTrip);

                return Ok(); 
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
