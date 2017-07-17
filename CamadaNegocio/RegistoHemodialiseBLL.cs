using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaObjectoTransferecia;
using System.Data;

namespace CamadaNegocio
{
    public class RegistoHemodialiseBLL
    {
        AcessoDadosBLL acessodadosBLL;
        public RegistoHemodialiseBLL()
        {
            acessodadosBLL = new AcessoDadosBLL();
        }

        public DataTable ConsultarRegistoHemodialise(Paciente paciente)
        {
            try
            {
                DataTable DataTableRegistoHemodialise = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select \"idProveniencia\" from \"Registo_dialise\" where idpessoa={paciente.Id_pessoa} order by data_dialise limit 1");
                return DataTableRegistoHemodialise;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataTable ListarPacientesEscalados(DateTime date)
        {
            try
            {
                DayOfWeek diasemana = date.DayOfWeek;
                string strDiaSemana = "";
                if (diasemana.Equals(DayOfWeek.Monday))
                {
                    strDiaSemana = "Seg";
                }
                else if (diasemana.Equals(DayOfWeek.Tuesday))
                {
                    strDiaSemana = "Ter";
                }
                else if (diasemana.Equals(DayOfWeek.Wednesday))
                {
                    strDiaSemana = "Qua";
                }
                else if (diasemana.Equals(DayOfWeek.Thursday))
                {
                    strDiaSemana = "Qui";
                }
                else if (diasemana.Equals(DayOfWeek.Friday))
                {
                    strDiaSemana = "Sex";
                }
                else if (diasemana.Equals(DayOfWeek.Saturday))
                {
                    strDiaSemana = "Sab";
                }
                else if (diasemana.Equals(DayOfWeek.Sunday))
                {
                    strDiaSemana = "Dom";
                }

                string query = "SELECT dp.idpessoa,dp.nome,dp.identificacao_hp,dp.tipo_insuficiencia, dp.data_inicio_hd, av.data_realizacao,tav.nome_acesso,tav.abrev_acesso ,dp.data_entrada,dp.raca,dp.genero FROM ";
                query += "\"Escala\" e, \"DiaSemana\" ds, \"Escala_DiaSemana\" eds, \"Prescricao_dialise\" pd, dados_prontuario dp,\"Acesso_vascular\" av, \"Tipo_Acesso\" tav WHERE ";
                query += "eds.idescala = e.idescala AND eds.id_diasemana = ds.id_diasemana AND pd.idescala = e.idescala AND pd.idpessoa = dp.idpessoa AND ";
                query += "dp.idpessoa = av.idpessoa and av.id_tipo_acesso = tav.id_tipo_acesso and av.id_acesso = (select x.id_acesso from \"Acesso_vascular\" x where x.idpessoa = dp.idpessoa order by x.data_realizacao desc limit 1) AND ";
                query += $"ds.nome_abrev = '{strDiaSemana}' AND pd.estado = '1'";

                DataTable DataTablePaciente = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, query);
                return DataTablePaciente;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao Consultar pacientes escalados");
            }

        }
        /*

        public List<Proveniencia> ListarProveniencia()
        {
            List<Proveniencia> listaProveniencia = null;
            try
            {
                listaProveniencia = new List<Proveniencia>();
                DataTable DataTableProveniencia = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, "select \"idProveniencia\" as idproveniencia, nome, descricao from \"Proveniencia\" order by nome");
                foreach (DataRow linha in DataTableProveniencia.Rows)
                {
                    Proveniencia p = new Proveniencia(Convert.ToInt32(linha["idproveniencia"]), Convert.ToString(linha["nome"]), Convert.ToString(linha["descricao"]));
                    listaProveniencia.Add(p);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listaProveniencia;
        }

        public DataTable ListarProvenienciaDataTable()
        {
            try
            {
                DataTable DataTableProveniencia = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, "select \"idProveniencia\" as idproveniencia, nome, descricao from \"Proveniencia\" order by nome");
                return DataTableProveniencia;

            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }

        public int CadastrarProveniencia(Proveniencia p)
        {
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$1", p.Nome_Proveniencia);
            acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$2", p.Descricao);
            object rt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_proveniencia");
            return Convert.ToInt32(rt);
        }

        public void ActualizarProveniencia(Proveniencia p)
        {
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$1", p.Id_Proveniencia);
            acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$2", p.Nome_Proveniencia);
            acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$3", p.Descricao);
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_actualizar_proveniencia");
        }

        public Proveniencia EliminarProveniencia(Proveniencia p)
        {
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("id_proveniencia_", p.Id_Proveniencia);
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_eliminar_proveniencia");
            return p;
        }

        public Proveniencia ConsultarProvenienciaPeloID(int IdProvenienica)
        {
            Proveniencia p = null;
            try
            {
                DataTable DataTableProveniencia = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select \"idProveniencia\" as idproveniencia, nome, descricao from \"Proveniencia\" where  \"idProveniencia\" = {IdProvenienica}");
                if (DataTableProveniencia.Rows.Count > 0)
                {
                    DataRow linha = DataTableProveniencia.Rows[0];
                    p = new Proveniencia(Convert.ToInt32(linha["idproveniencia"]), Convert.ToString(linha["nome"]), Convert.ToString(linha["descricao"]));
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema na Consulta de Provenienia pelo Código");
            }
            return p;
        }
*/

    }
}
