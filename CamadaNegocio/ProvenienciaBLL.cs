using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;

namespace CamadaNegocio
{
    public class ProvenienciaBLL
    {
        BaseAcessoDadosBLL acessodadosBLL;
        
        #region Manipulação da Tabela Proveniencia
        public ProvenienciaBLL()
        {
            acessodadosBLL = new CamadaNegocio.BaseAcessoDadosBLL();
        }
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

              //  throw;
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
            Proveniencia p =  null;
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

        #endregion
    }
}