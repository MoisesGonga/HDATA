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
        public string SiglaUsuario { get; set; }
        public string PalavraPasse { get; set; }
        public Funcionario Funcionario { get; set; }
        public UserType Perfil_Usuario { get; set; }
        public DateTime DataUltimoAcesso { get; set; }
        public DateTime DataCadastro { get; set; }
        public Usuario() { }

        public Usuario(string nomeUsuario)
        {
            this.NomeUsuario = nomeUsuario;
        }
    }
}