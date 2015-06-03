using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DucksFootie.Entities
{
    [Serializable]
    [DataContract]
    public class Player : IEqualityComparer<Player>, IEquatable<Player>
    {
        public Player()
        {
            Active = true;
        }

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(this, (Player)obj);
        }

        public bool Equals(Player x, Player y)
        {
            return (x == null || y == null) ? false : x.UserId == y.UserId;
        }

        public bool Equals(Player other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(Player obj)
        {
            return base.GetHashCode();
        }
    }
}
