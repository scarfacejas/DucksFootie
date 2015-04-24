using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksFootie.Entities
{
    [Serializable]
    public class GamePlayer : Player
    {
        public Status Playing { get; set; }
    }
}
