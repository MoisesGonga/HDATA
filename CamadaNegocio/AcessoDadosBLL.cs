using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaAcessoDados;
namespace CamadaNegocio
{
   public class AcessoDadosBLL
    {
        
        private AcessoDadosPostgreSQL acessoDadosPostgreSQL_;

        public AcessoDadosPostgreSQL AcessodadosPostgreSQL
        {
            get {
                if (acessoDadosPostgreSQL_== null)
                {
                    acessoDadosPostgreSQL_ = new AcessoDadosPostgreSQL();
                    return acessoDadosPostgreSQL_;
                }
                return acessoDadosPostgreSQL_;
            }
        }



        //public AcessoDadosPostgreSQL acessodadosPostgreSQL
        //{
        //    get
        //    {
        //        if (acessodadosPostgreSQL_ == null)
        //        {
        //            return new AcessoDadosPostgreSQL();
        //        }

        //            acessodadosPostgreSQL_ = new AcessoDadosPostgreSQL();
        //    }
        //}
    }
}
