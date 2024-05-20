using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Familia
    {
        public string Id { get; set; }
        public IList<Pessoa> Pessoas { get; set; }
        public IList<Renda> Rendas { get; set; }
        public int Status { get; set; }
    }
}
