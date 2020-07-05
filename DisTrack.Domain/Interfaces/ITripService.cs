using DisTrack.Commons.Models;
using DisTrack.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisTrack.Domain.Interfaces
{
    public interface ITripService
    {
        Task<List<TripModel>> GetAllTrips();
        Task<TripModel> GetTripById(int id);
        Task<bool> AddTrip(TripModel trip);
        Task<TripModel> UpdateTrip(TripModel trip);
        Task<bool> DeleteTripById(int id);
    }
}
