using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
   public class Sal_Mineral
    {
        public int id_sal_mineral { get; set; } 
        public string nome { get; set; }
        public string valor_padrao { get; set; }
        public string tipo_uso { get; set; }
        public string descricao { get; set; }
        public Sal_Mineral()
        {

        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(valor_padrao))
            {
                return $"{nome}";
            }
            return $"{nome} - {valor_padrao}";
        }
    }
}
