using DisTrack.Commons.Models;
using DisTrack.Data;
using DisTrack.Data.Access.RepositoryInterfaces;
using DisTrack.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DisTrack.Domain.Services
{
    public class TripService : ITripService
    {
        private readonly IDisTrackRepository _repository;
        public TripService(IDisTrackRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TripModel>> GetAllTrips()
        {
            var trips = new List<TripModel>();
            var getAllTrips = _repository.All<Trip>();

            foreach (var trip in getAllTrips)
            {
                trips.Add(new TripModel 
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

            return trips;
        }

        public async Task<TripModel> GetTripById(int id)
        {
            try
            {
                var tripInDb = await _repository.GetSingleAsync<Trip>(x => x.Id == id);

                return new TripModel
                {
                    Departure = tripInDb.Departure,
                    Destination = tripInDb.Destination,
                    ArrivalTime = tripInDb.ArrivalTime,
                    DepartureTime = tripInDb.DepartureTime,
                    Miles = tripInDb.Miles,
                    Cost = tripInDb.Cost
                };
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddTrip(TripModel trip)
        {
            try
            {
                var newTrip = new Trip
                {
                    Departure = trip.Departure,
                    Destination = trip.Destination,
                    DepartureTime = trip.DepartureTime,
                    ArrivalTime = trip.ArrivalTime,
                    Cost = trip.Cost,
                    Miles = trip.Miles
                };

                _repository.Add(newTrip);
                await _repository.CommitAsync();

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTripById(int id)
        {
            try
            {
                var trip = await _repository.GetSingleAsync<Trip>(x => x.Id == id);

                _repository.Delete<Trip>(trip);
                await _repository.CommitAsync();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<TripModel> UpdateTrip(TripModel trip)
        {
            var tripInDb = await _repository.GetSingleAsync<Trip>(x => x.Id == trip.Id, x => x.DepartureTime == trip.DepartureTime);

            tripInDb.Departure = trip.Departure;
            tripInDb.Destination = trip.Destination;
            tripInDb.DepartureTime = trip.DepartureTime;
            tripInDb.ArrivalTime = trip.ArrivalTime;
            tripInDb.Cost = trip.Cost;
            tripInDb.Miles = trip.Miles;

            _repository.Update<Trip>(tripInDb);

            await _repository.CommitAsync();

            return trip;
        }
    }
}
