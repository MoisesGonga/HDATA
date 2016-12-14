using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamadaObjectoTransferecia
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string PalavraPasse { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}