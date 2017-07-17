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
        public string Perfil_Usuario { get; set; }

        public Usuario() { }

        public Usuario(string nomeUsuario)
        {
            this.NomeUsuario = nomeUsuario;
        }
    }

    
}