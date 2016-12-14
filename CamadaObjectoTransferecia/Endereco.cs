using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
    public class Endereco
    {
        public Endereco (int idpessoa, string pais, string municipio, string rua)
        {
            this.idpessoa = idpessoa;
            this.pais = pais;
            this.provincia = provincia;
            this.municipio = municipio;
            this.rua = rua;
        }
        public Endereco(string pais, string municipio, string rua)
        {
            this.idpessoa = idpessoa;
            this.pais = pais;
            this.provincia = provincia;
            this.municipio = municipio;
            this.rua = rua;
        }

        public Endereco() { }
         

        public int idpessoa { get; set; }
        public string pais { get; set; }
        public string  provincia { get; set; }
        public string  municipio { get; set; }
        public string rua { get; set; }
    }
}
