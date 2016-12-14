using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaObjectoTransferecia
{
    public class Seriologia
    {
        public int Id_Seriologia { get; set; }
        public string Nome_Seriologia { get; set; }
        public string Descricao { get; set; }

       

        public Seriologia(int idseriologia, string nome_seriologia, string descricao)
        {
            this.Id_Seriologia = idseriologia;
            this.Nome_Seriologia = nome_seriologia;
            this.Descricao = descricao;
        }

        public Seriologia()
        {

        }
    }
}
