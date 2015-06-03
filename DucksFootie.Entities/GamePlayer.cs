using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DucksFootie.Entities
{
    [Serializable]
    [DataContract]
    public class GamePlayer : Player
    {
        [DataMember]
        public Status Playing { get; set; }
    }
}
