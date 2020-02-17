using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Entities.Base
{
    public abstract class Entity
    {
        [BsonId]
        public string Id { get; set; }

    }
}
