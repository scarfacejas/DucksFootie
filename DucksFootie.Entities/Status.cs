using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucksFootie.Entities
{
    [Serializable]
    public enum Status
    {
        Invited = 0,
        Tentative,
        Accepeted,
        Declined
    }
}
