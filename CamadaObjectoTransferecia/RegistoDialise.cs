using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamadaObjectoTransferecia
{
    public class RegistoDialise
    {
        /*
          id_maquina integer NOT NULL,
          id_registo_dialise integer NOT NULL DEFAULT nextval('"Registo_dialise_id_registo_dialise_seq"'::regclass),
          data_dialise timestamp without time zone NOT NULL,
          notas_enfermagem text,
          volume_tratado real,
          observ_medicas text,
          justificativo_ausencia character varying(200),
          id_ala integer NOT NULL,
          id_sala integer NOT NULL,
          idpessoa integer NOT NULL,
          idturno integer NOT NULL,
             */
        //public List<Paciente> Pacientes { get; set; }

        public int id_registo_dialise { get; set; }
        public DateTime hora_incio_dialise { get; set; }
        public DateTime hora_fim_dialise { get; set; }
        public DateTime data_dialise { get; set; }
        public string nota_enfermagem { get; set; }
        public string volume_Tratado { get; set; }
        public string observacao_Medica { get; set; }
        public string justificacao_ausencia { get; set; }
        public Sala sala { get; set; }
        public Turno turno { get; set; }
        public Paciente idpessoa { get; set; } 
        public List<Hora_Dialise> Hora_Dialise_ { get; set; }

    }
}