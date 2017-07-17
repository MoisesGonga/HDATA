using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class Sal_MineralBLL
    {
        AcessoDadosBLL acessodadosBLL;

        public Sal_MineralBLL()
        {
            acessodadosBLL = new CamadaNegocio.AcessoDadosBLL();
        }

        public List<Sal_Mineral> ListarSal_Mineral()
        {
            List<Sal_Mineral> ListSal_Mineral = null;
            try
            {
                ListSal_Mineral = new List<Sal_Mineral>();
                DataTable DataTableMateriais = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Sal_Mineral\" ");
                foreach (DataRow linha in DataTableMateriais.Rows)
                {
                    Sal_Mineral sal_Mineral = new Sal_Mineral();
                    sal_Mineral.id_sal_mineral = Convert.ToInt32(linha["id_sal_mineral"]);
                    sal_Mineral.nome = Convert.ToString(linha["nome"]);
                    sal_Mineral.valor_padrao = Convert.ToString(linha["valor_padrao"]);
                    sal_Mineral.descricao = Convert.ToString(linha["descricao"]);
                    sal_Mineral.tipo_uso = Convert.ToString(linha["tipo_uso"]);
                    ListSal_Mineral.Add(sal_Mineral);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema na Consulta dos Sais Minerais...");
            }
            return ListSal_Mineral;
        }

        public int CadastraSalMineral(Sal_Mineral sal_Mineral)
        {
          
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            string query = $"insert into \"Sal_Mineral\" values (default,'{sal_Mineral.nome}', '{sal_Mineral.valor_padrao}','{sal_Mineral.descricao}', '{sal_Mineral.tipo_uso}')";
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            object rt2 = acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, "select last_value as id_sal_mineral from \"Sal_Mineral_id_tipo_sal_mineral_seq\"");
            return Convert.ToInt32(rt2);
        }

        public List<Sal_Mineral> ConsultarSalMineralPeloNome(string nome)
        {
            List<Sal_Mineral> listaSal_Mineral = null;
            try
            {
                listaSal_Mineral = new List<Sal_Mineral>();
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Sal_Mineral\" WHERE nome ILIKE '%{nome}%' order by nome");
                foreach (DataRow linha in dt.Rows)
                {
                    Sal_Mineral sal_Mineral = new Sal_Mineral();
                    sal_Mineral.id_sal_mineral = Convert.ToInt32(linha["id_sal_mineral"]);
                    sal_Mineral.nome = Convert.ToString(linha["nome"]);
                    sal_Mineral.valor_padrao = Convert.ToString(linha["valor_padrao"]);
                    sal_Mineral.descricao = Convert.ToString(linha["descricao"]);
                    sal_Mineral.tipo_uso = Convert.ToString(linha["tipo_uso"]);
                    listaSal_Mineral.Add(sal_Mineral);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar o Sal Mineral pelo nome: ");
            }
            return listaSal_Mineral;
        }

        public  Sal_Mineral Consultar_SalMineralPeloID(int id_SalMineral)
        {
            Sal_Mineral sal_Mineral = null;
            try
            {
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Sal_Mineral\" WHERE id_sal_mineral = {id_SalMineral}");
                foreach (DataRow linha in dt.Rows)
                {
                    sal_Mineral = new Sal_Mineral();
                    sal_Mineral.id_sal_mineral = Convert.ToInt32(linha["id_sal_mineral"]);
                    sal_Mineral.nome = Convert.ToString(linha["nome"]);
                    sal_Mineral.valor_padrao = Convert.ToString(linha["valor_padrao"]);
                    sal_Mineral.descricao = Convert.ToString(linha["descricao"]);
                    sal_Mineral.tipo_uso = Convert.ToString(linha["tipo_uso"]);
                    return sal_Mineral;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar o Sal Mineral pelo ID...");
            }
            return null;
        }
    }
}
