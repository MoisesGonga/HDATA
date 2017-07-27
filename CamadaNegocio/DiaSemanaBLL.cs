using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
   public class DiaSemanaBLL
    {
        BaseAcessoDadosBLL acessodadosBLL;

        public DiaSemanaBLL()
        {
            acessodadosBLL = new CamadaNegocio.BaseAcessoDadosBLL();
        }
        public List<DiaSemana> ConsultarDiasSemanaDaEscala(Escala escala)
        {
            List<DiaSemana> ListaDiaSemana = null;
            try
            {
                ListaDiaSemana = new List<DiaSemana>();
                string query = $"SELECT D.id_diasemana, D.nome,D.nome_abrev from \"DiaSemana\" D inner join \"Escala_DiaSemana\" DS on D.id_diasemana=DS.id_diasemana where DS.idescala = '{escala.idescala}' order by D.id_diasemana";
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, query);
                foreach (DataRow linha in dt.Rows)
                {
                    DiaSemana diaSemana = new DiaSemana();
                    diaSemana.Id_DiaSemana = Convert.ToInt32(linha["id_diasemana"]);
                    diaSemana.Nome = Convert.ToString(linha["nome"]);
                    diaSemana.Abrev_DiaSemana = Convert.ToString(linha["nome_abrev"]);
                    ListaDiaSemana.Add(diaSemana);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar os Dias da Semana da Escala..."+ ex);
            }
            return ListaDiaSemana;
        }
    }
}
