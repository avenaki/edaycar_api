using eDayCar.Domain.Entities.Identity;
using eDayCar.Domain.Entities.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Repositories
{
    public interface ITripRepository
    {
        void Add(Trip trip);
        void Add(IEnumerable<Trip> trips);
        List<Trip> Get();
        Trip Get(string id);
        IEnumerable<Trip> Get(IEnumerable<string> ids);
        void Update(Trip trip);
        string Delete(string id);
        IEnumerable<Trip> Get(TripSearchFilter filter);
    }
}
