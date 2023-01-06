using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelos.Models
{
    public class Aerolinea
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Destination { get; set; } = null!;
        public string Flight { get; set; } = null!;
        public int Gate { get; set; }
        public string Remarks { get; set; } = null!;
        public int? Count { get; set; }
    }
}
