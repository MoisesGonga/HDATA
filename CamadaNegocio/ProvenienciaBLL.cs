using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;

namespace CamadaNegocio
{
    public class ProvenienciaBLL
    {
        AcessoDadosBLL acessodadosBLL;
        
        #region Manipulação da Tabela Proveniencia
        public ProvenienciaBLL()
        {
            acessodadosBLL = new CamadaNegocio.AcessoDadosBLL();
        }
        public List<Proveniencia> ListarProveniencia()
        {
            List<Proveniencia> listaProveniencia = new List<Proveniencia>();
            DataTable DataTableProveniencia = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, "select \"idProveniencia\" as idproveniencia, nome, descricao from \"Proveniencia\"");
            foreach (DataRow linha in DataTableProveniencia.Rows)
            {
                Proveniencia p = new Proveniencia(Convert.ToInt32(linha["idproveniencia"]), Convert.ToString(linha["nome"]), Convert.ToString(linha["descricao"]));
                listaProveniencia.Add(p);
            }
            return listaProveniencia;
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

        #endregion
    }
}
