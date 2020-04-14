using eDayCar_api.Entities.Identity;
using System.Collections.Generic;

namespace eDayCar_api.Repositories.Abstract
{
    public interface IRequestRepository
    {
        void Add(Request request);
        void Add(IEnumerable<Request> requests);
        List<Request> Get();
        Request Get(string Id);
        IEnumerable<Request> Get(IEnumerable<string> ids);   
        void Update(Request request);
        string Delete(string Id);
    }
}
