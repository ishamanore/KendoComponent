using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Repository.Interface
{
    public interface IUserRepo
    {
        List<TripNames> GetTripNames(string  type);
        List<string> GetTripDate(int name);

        double GetTripPrice(int name , string date);

        bool seatValidation (RegisterTrip registerTrip);

        bool registerTrip(RegisterTrip registerTrip);

    }
}