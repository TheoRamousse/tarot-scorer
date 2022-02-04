using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarotDTO
{
    public class SessionDTO : IDTO
    {
        public long Id { get;  set; }
        public string Name { get;  set; }
        public DateTime? StartingTime { get;  set; }
        public DateTime? EndingTime { get;  set; }
        public IEnumerable<PlayerDTO> Players { get;  set; }
    }
}
