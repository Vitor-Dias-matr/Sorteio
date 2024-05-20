using Domain.Infra;
using Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FamiliaRepository : IFamiliaRepository
    {
        public List<Familia> Query()
        {
            try
            {
                var json = File.ReadAllText(@"../Repository/Mock/familias.json", Encoding.GetEncoding("iso-8859-1"));

                var familias = JsonConvert.DeserializeObject<List<Familia>>(json);

                return familias;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
