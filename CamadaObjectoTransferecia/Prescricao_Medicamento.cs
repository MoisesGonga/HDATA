using CamadaObjectoTransferencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
    public class Prescricao_Medicamento
    {
        public Prescricao id_prescri_dialise { get; set; }
        public Medicamento id_medicamento { get; set; }
        public string valor_prescrito { get; set; }
    }
}
