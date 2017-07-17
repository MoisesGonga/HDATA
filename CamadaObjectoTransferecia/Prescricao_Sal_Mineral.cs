using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
   public class Prescricao_Sal_Mineral
    {
        public Sal_Mineral sal_Mineral { get; set; }
        public Prescricao prescricao { get; set; }
        public string valor_prescrito { get; set; }
    }
}
