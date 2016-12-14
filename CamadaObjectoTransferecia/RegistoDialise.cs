using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamadaObjectoTransferecia
{
    public class RegistoDialise
    {
        //public List<Paciente> Pacientes { get; set; }
        public DateTime Data_Dialise { get; set; }
        public string Nota_Enfermagem { get; set; }
        public float Volume_Tratado { get; set; }
        public string Observacao_Medica { get; set; }
        public string Justificacao_Ausencia { get; set; }
        public Sala Sala_ { get; set; }
        public Ala Ala_ { get; set; }
        public Turno Turno_ { get; set; }
        public List<Hora_Dialise> Hora_Dialise_ { get; set; }

    }
}