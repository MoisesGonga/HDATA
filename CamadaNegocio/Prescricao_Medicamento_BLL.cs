using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class Prescricao_Medicamento_BLL
    {
        AcessoDadosBLL acessodadosBLL;

        public Prescricao_Medicamento_BLL()
        {
            acessodadosBLL = new CamadaNegocio.AcessoDadosBLL();
        }

        public bool Cadastrar_Prescricao_Medicamento(Prescricao_Medicamento prescricao_Medicamento, Prescricao prescricao)
        {
            try
            {
               // acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                string query = $"insert into \"Prescricao_dialise_Medicamento\" values ({prescricao.id_prescricao_dialise},{prescricao_Medicamento.id_medicamento.id_medicamento},'{prescricao_Medicamento.valor_prescrito}')";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas ao Inserir Medicamentos na Prescrição Nº: "+ prescricao.id_prescricao_dialise);
            }
          //  return false;
        }

        public void Cadastrar_Prescricao_Medicamento(List<Prescricao_Medicamento> List_prescricao_Medicamento, Prescricao prescricao)
        {
            foreach (Prescricao_Medicamento item in List_prescricao_Medicamento)
            {
                Cadastrar_Prescricao_Medicamento(item,prescricao);
            }
        }

        public List<Prescricao_Medicamento> Consultar_Prescricao_Medicamento(Prescricao prescricao)
        {
            List<Prescricao_Medicamento> List_prescricao_Medicamento = null;
            try
            {
                MedicamentoBLL medicamentoBLL = new MedicamentoBLL();
                List_prescricao_Medicamento = new List<Prescricao_Medicamento>(); ;
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Prescricao_dialise_Medicamento\" WHERE id_prescri_dialise = {prescricao.id_prescricao_dialise}");
                foreach (DataRow linha in dt.Rows)
                {
                    Prescricao_Medicamento prescricao_Medicamento = new Prescricao_Medicamento();
                    prescricao_Medicamento.id_prescri_dialise = prescricao;
                    prescricao_Medicamento.id_medicamento = medicamentoBLL.ConsultarMedicamentoPeloID(Convert.ToInt32(linha["id_medicamento"]));
                    prescricao_Medicamento.valor_prescrito = Convert.ToString(linha["valor_prescrito"]);
                    List_prescricao_Medicamento.Add(prescricao_Medicamento);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Buscar os Medicamentos referentes a Prescrição Nº: {prescricao.id_prescricao_dialise}");
            }
            return List_prescricao_Medicamento;
        }

        public void Actualizar_Prescricao_Medicamento(List<Prescricao_Medicamento> List_prescricao_Medicamento)
        {
            try
            {
                foreach (var item in List_prescricao_Medicamento)
                {
                    Actualizar_Prescricao_Medicamento(item);
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public void Actualizar_Prescricao_Medicamento(Prescricao_Medicamento prescricao_Medicamento)
        {
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Prescricao_dialise_Medicamento\" set valor_prescrito = '{prescricao_Medicamento.valor_prescrito}' where id_prescri_dialise = {prescricao_Medicamento.id_prescri_dialise.id_prescricao_dialise} and id_medicamento = {prescricao_Medicamento.id_medicamento.id_medicamento}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Actualizar os Medicamentos referentes a Prescrição Nº: {prescricao_Medicamento.id_prescri_dialise.id_prescricao_dialise}");
            }
        }

        public void Eliminar_Prescricao_Medicamento(Prescricao_Medicamento prescricao_Medicamento)
        {
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"delete from \"Prescricao_dialise_Medicamento\" where id_prescri_dialise = {prescricao_Medicamento.id_prescri_dialise} and id_medicamento = {prescricao_Medicamento.id_medicamento}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Eliminar Medicamento referente a Prescrição {prescricao_Medicamento.id_prescri_dialise}");
            }
        }

    }
}
