using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DucksFootie.Entities
{
    [Serializable]
    [DataContract]
    public class Game : IEqualityComparer<Game>, IEquatable<Game>
    {
        [Key]
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Game date")]
        [DataMember]
        public DateTime Date { get; set; }

        [Display(Name = "Players")]
        [DataMember]
        public IEnumerable<GamePlayer> Players { get; set; }

        public bool Equals(Game x, Game y)
        {
            return (x != null && y != null) && x.Date == y.Date;
        }

        public int GetHashCode(Game obj)
        {
            return base.GetHashCode();
        }

        public bool Equals(Game other)
        {
            return Equals(this, other);
        }
    }
}