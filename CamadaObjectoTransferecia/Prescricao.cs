using CamadaObjectoTransferencia;
using System;
using System.Collections.Generic;

namespace CamadaObjectoTransferecia
{
    public class Prescricao
    {
        public int id_prescricao_dialise { get; set; }
        public string peso_seco { get; set; }
        public string uf_total_max { get; set; }
        public string ektv_prescrito { get; set; }
        public string nr_sessao_semana { get; set; }
        public string nr_hora_sessao { get; set; }
        public string temperatura { get; set; }
        public string debito { get; set; }
        public string glucose { get; set; }
        public string heparina_inicial { get; set; }
        public string heparina_hora { get; set; }
        public string interrupcao_heparina { get; set; }
        public string heparina_bpm { get; set; }
        public DateTime data_prescricao { get; set; }
        public Escala idescala { get; set; }
        public Paciente paciente { get; set; }
        public string tipo_tecnica { get; set; }
    }
}