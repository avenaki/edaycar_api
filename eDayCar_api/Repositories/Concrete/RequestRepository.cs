using eDayCar_api.Entities.Identity;
using eDayCar_api.Repositories.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Repositories.Concrete
{
    public class RequestRepository: IRequestRepository
    {
        private IMongoCollection<Request> Collection { get; }

        public RequestRepository(MongoContext context)
        {
            Collection = context.GetCollection<Request>("Requests");
        }

        public void Add(Request request)
        {
            Collection.InsertOne(request);
        }

        public void Add(IEnumerable<Request> requests)
        {
            Collection.InsertMany(requests);
        }

        List<Request> Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public Request Get(string Id)
        {
            return Collection.Find(i => i.Id == Id ).FirstOrDefault();
        }

        public IEnumerable<Request> Get(IEnumerable<string> ids)
        {
            var filter = Builders<Request>.Filter.In(i => i.Id, ids);
            return Collection.Find(filter).ToList();
        }

        public void Update(Request request)
        {
            var filter = Builders<Request>.Filter.Where(i => i.Id == request.Id);
            Collection.ReplaceOne(filter, request);
        }

        public string Delete(string Id)
        {
            try
            {
                var filter = Builders<Request>.Filter.Where(i => i.Id == Id);
                Collection.DeleteOne(filter);
                return "success";
            }
            catch
            {
                return "fail";
            }
        
        }

        List<Request> IRequestRepository.Get()
        {
            throw new NotImplementedException();
        }
    }
}
