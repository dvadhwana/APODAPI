using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace azuretestapp.Model
{
    [DataContract]
    public class APOD
    {
        [BsonId]
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string copyright { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string explanation { get; set; }
        [DataMember]
        public string hdurl { get; set; }
        [DataMember]
        public string media_type { get; set; }
        [DataMember]
        public string service_version { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string url { get; set; }
    }
}
