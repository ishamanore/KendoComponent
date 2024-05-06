using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Repository.Interface
{
    public interface IAdminRepo
    {
        List<Trip> FetchAllTrip();
        Trip GetOneTrip(int id);
        void AddTrip(Trip trip);
        void UpdateTrip(Trip trip);
        void DeleteTrip(int id);
    }
}