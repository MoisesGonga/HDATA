using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
   public class Contacto
    {
        public Contacto(int idpessoa, string telefone_1, string email, string telefone_parente, string nome_parente)
        {
            this.idpessoa = idpessoa;
            this.telefone_1 = telefone_1;
            this.email = email;
            this.telefone_parente = telefone_parente;
            this.nome_parente = nome_parente;
        }

        public Contacto( string telefone_1, string email, string telefone_parente, string nome_parente)
        {
            this.idpessoa = idpessoa;
            this.telefone_1 = telefone_1;
            this.email = email;
            this.telefone_parente = telefone_parente;
            this.nome_parente = nome_parente;
        }
        public Contacto()
        {
            
        }
        public int idpessoa { get; set; } 
        public string telefone_1 { get; set; }
        public string email { get; set; }
        public string telefone_parente { get; set; }
        public string nome_parente { get; set; }
    }
}
