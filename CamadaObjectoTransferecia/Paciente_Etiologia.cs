using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
    public class Paciente_Etiologia
    {
        public Etiologia idetiologia { get; set; }
        public Paciente idpessoa { get; set; }
        public string observacao { get; set; }
    }
}
