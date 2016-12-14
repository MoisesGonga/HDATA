﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
    public class AcessoVascular
    {
        public int ID_AcessoVascular { get; set; }
        public TipoAcesso tipoAcesso { get; set; }
        public DateTime Data_av { get; set; }
        public string Recuperacao_cirugica { get; set; }
        public string Director_clinico { get; set; }
        public string Complicacao_av { get; set; }
        public DateTime Data_falencia { get; set; }
        public String MotivoFalencia { get; set; }
        public Paciente Paciente_ { get; set; }
    }
}
