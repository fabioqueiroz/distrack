using DisTrack.Commons.Models;
using DisTrack.Data;
using DisTrack.Domain.Interfaces;
using DisTrack.ViewModels;
using DisTrackProject.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            var tripsInDb = await _tripService.GetAllTrips();

            var trips = new List<TripViewModel>();

            foreach (var trip in tripsInDb)
            {
                trips.Add(new TripViewModel 
                {
                    Id = trip.Id,
                    Departure = trip.Departure,
                    Destination = trip.Destination,
                    ArrivalTime = trip.ArrivalTime,
                    DepartureTime = trip.DepartureTime,
                    Miles = trip.Miles,
                    Cost = trip.Cost
                });
            }
            // TODO: refactor results in separate methods for day/week/month
            var date = trips.OrderBy(x => x.DepartureTime);
            var month = trips.OrderBy(x => x.DepartureTime.Month);
            var week = trips.OrderBy(x => DateManager.GetWeekOfTheYear(x.DepartureTime));

            return Ok(month);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var getTrip = await _tripService.GetTripById(id);

            var trip = new TripViewModel
            {
                Id = getTrip.Id,
                Departure = getTrip.Departure,
                Destination = getTrip.Destination,
                ArrivalTime = getTrip.ArrivalTime,
                DepartureTime = getTrip.DepartureTime,
                Miles = getTrip.Miles,
                Cost = getTrip.Cost
            };

            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTrip([FromBody] TripViewModel tripViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTrip = new TripModel
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

        [HttpPost("UpdateTrip")]
        public async Task<IActionResult> UpdateTrip([FromBody] TripViewModel trip)
        {
            try
            {
                if (!ModelState.IsValid || trip == null)
                {
                    return BadRequest();
                }

                var tripInDb = await _tripService.GetTripById(trip.Id);

                tripInDb.Departure = trip.Departure;
                tripInDb.Destination = trip.Destination;
                tripInDb.DepartureTime = trip.DepartureTime;
                tripInDb.ArrivalTime = trip.ArrivalTime;
                tripInDb.Cost = trip.Cost;
                tripInDb.Miles = trip.Miles;

                await _tripService.UpdateTrip(tripInDb);

                return Ok();
            }
            catch (WebException)
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripById(int id)
        {
            try
            {
                var tripInDb = await _tripService.DeleteTripById(id);
                return Ok();
            }
            catch (WebException)
            {
                return BadRequest(); 
            }

        }
    }
}
