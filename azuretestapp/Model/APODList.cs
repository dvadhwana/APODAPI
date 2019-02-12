using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace azuretestapp.Model
{
    [DataContract]
    public class APODList
    {
        [DataMember]
        public List<APOD> APODS { get; set; }
    }
}
