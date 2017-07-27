using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaObjectoTransferecia;
using System.Data;

namespace CamadaNegocio
{
    public class Prescricao_Sal_Mineral_BLL
    {
        BaseAcessoDadosBLL acessodadosBLL;

        public Prescricao_Sal_Mineral_BLL()
        {
            acessodadosBLL = new CamadaNegocio.BaseAcessoDadosBLL();
        }

        public void Cadastrar_Prescricao_Sal_Mineral(Prescricao_Sal_Mineral prescricao_Sal_Mineral, Prescricao prescricao)
        {
            try
            {
                //acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                string query = $"insert into \"Sal_Mineral_Prescricao_dialise\" values ({prescricao_Sal_Mineral.sal_Mineral.id_sal_mineral},{prescricao.id_prescricao_dialise},'{prescricao_Sal_Mineral.valor_prescrito}')";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas ao Inserir os Sais Minerais na Prescrição Nº: "+prescricao.id_prescricao_dialise+ "  "+ex.Message);
            }
        }

       public void Cadastrar_Prescricao_Sal_Mineral(List<Prescricao_Sal_Mineral> List_Prescricao_Sal_Mineral, Prescricao prescricao)
        {
            foreach (Prescricao_Sal_Mineral item in List_Prescricao_Sal_Mineral)
            {
                Cadastrar_Prescricao_Sal_Mineral(item,prescricao);
            }
        }

        public List<Prescricao_Sal_Mineral> Consultar_Prescricao_Sal_Mineral(Prescricao prescricao)
        {
            try
            {
                Sal_MineralBLL sal_MineralBLL = new Sal_MineralBLL();
                List<Prescricao_Sal_Mineral> List_Prescricao_Sal_Mineral = new List<Prescricao_Sal_Mineral>();
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Sal_Mineral_Prescricao_dialise\" WHERE id_prescri_dialise = {prescricao.id_prescricao_dialise}");
                foreach (DataRow linha in dt.Rows)
                {
                    Prescricao_Sal_Mineral prescricao_Sal_Mineral = new Prescricao_Sal_Mineral();
                    prescricao_Sal_Mineral.prescricao = prescricao;
                    prescricao_Sal_Mineral.sal_Mineral  = sal_MineralBLL.Consultar_SalMineralPeloID(Convert.ToInt32(linha["id_tipo_sal_mineral"]));
                    prescricao_Sal_Mineral.valor_prescrito = Convert.ToString(linha["valor_prescrito"]);
                    List_Prescricao_Sal_Mineral.Add(prescricao_Sal_Mineral);
                }
                return List_Prescricao_Sal_Mineral;
            }
            catch 
            {
                throw new Exception($"Erro ao Buscar os Sais Minerais referentes a Prescrição Nº: {prescricao.id_prescricao_dialise}");
            }

        }

        public void Actualizar_Prescricao_Sal_Mineral(Prescricao_Sal_Mineral prescricao_Sal_Mineral)
        {
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Sal_Mineral_Prescricao_dialise\" set valor_prescrito = '{prescricao_Sal_Mineral.valor_prescrito}' where id_prescri_dialise = {prescricao_Sal_Mineral.prescricao.id_prescricao_dialise} and id_tipo_sal_mineral = {prescricao_Sal_Mineral.sal_Mineral.id_sal_mineral}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Actualizar os Sais Minerais referentes a Prescrição Nº: {prescricao_Sal_Mineral.prescricao.id_prescricao_dialise}");
            }
        }

        public void Actualizar_Prescricao_Sal_Mineral(List<Prescricao_Sal_Mineral> List_prescricao_Sal_Mineral)
        {
            try
            {
                foreach (var item in List_prescricao_Sal_Mineral)
                {
                    Actualizar_Prescricao_Sal_Mineral(item);
                }

            }
            catch { }
        }

        public void Eliminar_Prescricao_Sal_Mineral(Prescricao_Sal_Mineral prescricao_Sal_Mineral)
        {
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"delete from \"Sal_Mineral_Prescricao_dialise\" where id_prescri_dialise = {prescricao_Sal_Mineral.prescricao.id_prescricao_dialise} and id_tipo_sal_mineral = {prescricao_Sal_Mineral.sal_Mineral.id_sal_mineral}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Eliminar Sail Mineral referente a Prescrição Nº: {prescricao_Sal_Mineral.prescricao.id_prescricao_dialise}");
            }
        }

    }
}
