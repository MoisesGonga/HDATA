using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
   public class PrescricaoBLL
    {
        BaseAcessoDadosBLL acessoDadosBLL;

        public PrescricaoBLL()
        {
            acessoDadosBLL = new BaseAcessoDadosBLL();
        }

        private string FormatarData(DateTime data)
        {
            string data_ = "";
            data_ += data.Year + "-";

            if (data.Month + "".Length == 1)
            {
                data_ += "0" + data.Month + "-";
            }
            else
            {

                data_ += data.Month + "-";
            }

            if (data.Day + "".Length == 1)
            {
                data_ += "0" + data.Day;
            }
            else
            {
                data_ += data.Day + "";
            }

            return data_;
        }

        public int CadastrarPrescricao(Prescricao prescricao)
        {
            string query = $"insert into \"Prescricao_dialise\" values (default,'{prescricao.peso_seco}','{prescricao.uf_total_max}','{prescricao.ektv_prescrito}',{prescricao.nr_sessao_semana},{prescricao.nr_hora_sessao},'{prescricao.temperatura}','{prescricao.debito}','{prescricao.glucose}','{prescricao.heparina_inicial}','{prescricao.heparina_hora}','{prescricao.interrupcao_heparina}','{prescricao.heparina_bpm}', TO_DATE('{FormatarData(prescricao.data_prescricao)}', 'YYYY-MM-DD'),{prescricao.idescala.idescala}, {prescricao.paciente.Id_pessoa}, '{prescricao.tipo_tecnica}')";
            acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            object rt2 = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, "select last_value as idprescricao_prescri from \"Prescricao_id_prescri_dialise_seq\"");
            return Convert.ToInt32(rt2);

        }

        public int ActualizarPrescricao(Prescricao prescricao)
        {
            //Actualização UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            try
            {
                string query = $" update \"Prescricao_dialise\" set peso_seco = '{prescricao.peso_seco}', uf_total_max = '{prescricao.uf_total_max}', ektv_prescrito = '{prescricao.ektv_prescrito}', nr_sessao_semana = {prescricao.nr_sessao_semana}, nr_hora_sessao = {prescricao.nr_hora_sessao}, temperatura = '{prescricao.temperatura}', debito = '{prescricao.debito}', glucose = '{prescricao.glucose}', heparina_inicial = '{prescricao.heparina_inicial}', heparina_hora = '{prescricao.heparina_hora}', interrupcao_heparina = '{prescricao.interrupcao_heparina}', heparina_bpm = '{prescricao.heparina_bpm}', data_prescricao = TO_DATE('{FormatarData(prescricao.data_prescricao)}'), idescala = {prescricao.idescala.idescala}, tipo_tecnica = '{prescricao.tipo_tecnica}' where id_prescri_dialise = {prescricao.id_prescricao_dialise} and idpessoa = {prescricao.paciente.Id_pessoa}";
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            }
            catch (Exception ex) {
                throw new Exception("Problema encontrado na Actualização da Prescrição!!! "+ex.Message);
            }
            return prescricao.id_prescricao_dialise;
        }

        public bool EliminarPrescricao(Prescricao prescricao)
        {
            try
            {
                string query = $" delete from \"Prescricao_dialise\" where id_prescri_dialise = {prescricao.id_prescricao_dialise}";
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema encontrado ao Eliminar a Prescrição!!!");
            }

        }

        public bool EliminarTodaPrescricao(Prescricao prescricao)
        {
            try
            {
                string query = $" delete from \"Prescricao_dialise\" where idpessoa = {prescricao.paciente.Id_pessoa}";
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema encontrado ao Eliminar Toda a Prescrição!!!");
            }
        }

        public List<Prescricao> ListarPrescricao(Paciente paciente)
        {

            List<Prescricao> List_Prescricao = new List<Prescricao>();
            DataTable dt = null;
            try
            {
                dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select * from \"Prescricao_dialise\" WHERE \"Prescricao_dialise\".idpessoa = {paciente.Id_pessoa}");
                foreach (DataRow linha in dt.Rows)
                {
                    Prescricao prescricao = new Prescricao();
                 
                    prescricao.id_prescricao_dialise = Convert.ToInt32(linha["id_prescri_dialise"]);
                    prescricao.peso_seco = Convert.ToString(linha["peso_seco"]);
                    prescricao.uf_total_max = Convert.ToString(linha["uf_total_max"]);
                    prescricao.ektv_prescrito = Convert.ToString(linha["ektv_prescrito"]);
                    prescricao.nr_sessao_semana = Convert.ToString(linha["nr_sessao_semana"]);
                    prescricao.nr_hora_sessao = Convert.ToString(linha["nr_hora_sessao"]);
                    prescricao.temperatura = Convert.ToString(linha["temperatura"]);
                    prescricao.debito = Convert.ToString(linha["debito"]);
                    prescricao.glucose = Convert.ToString(linha["glucose"]);
                    prescricao.heparina_inicial = Convert.ToString(linha["heparina_inicial"]);
                    prescricao.heparina_hora = Convert.ToString(linha["heparina_hora"]);
                    prescricao.interrupcao_heparina = Convert.ToString(linha["interrupcao_heparina"]);
                    prescricao.heparina_bpm = Convert.ToString(linha["heparina_bpm"]);
                    prescricao.data_prescricao = Convert.ToDateTime(linha["data_prescricao"]);
                    prescricao.tipo_tecnica = Convert.ToString(linha["tipo_tecnica"]);
                    EscalaBLL escalaBll = new EscalaBLL();
                    prescricao.idescala = escalaBll.ObterEscalaPeloID(Convert.ToInt32(linha["idescala"]));
                    List_Prescricao.Add(prescricao);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Listar Prescrição.");
            }
            return List_Prescricao;
        }

        public DataTable ListarPrescricaoDataTable(Paciente paciente)
        {
            DataTable dt = null;
            try
            {
                dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select * from \"Prescricao_dialise\" WHERE \"Prescricao_dialise\".idpessoa = {paciente.Id_pessoa}");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Listar Prescrição.");
            }
            return dt;
        }

        public List<Prescricao_Sal_Mineral> ObterListPrescricao_Sal_Mineral(Prescricao prescricao)
        {
            try
            {
                Prescricao_Sal_Mineral_BLL prescricao_Sal_Mineral_BLL = new Prescricao_Sal_Mineral_BLL();
                return prescricao_Sal_Mineral_BLL.Consultar_Prescricao_Sal_Mineral(prescricao);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao Obter Lista de Sal Mineral - Prescrito!!!");
            }
        }

        public List<Prescricao_Material> ObterListPrescricao_Material(Prescricao prescricao)
        {
            try
            {
                Prescricao_Material_BLL prescricao_Material_BLL = new Prescricao_Material_BLL();
                return prescricao_Material_BLL.Consultar_PrescricaoMaterial(prescricao);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao Obter Lista de Materias - Prescrito");
            }
        }

        public List<Prescricao_Medicamento> ObterListPrescricao_Medicamento(Prescricao prescricao)
        {
            try
            {
                Prescricao_Medicamento_BLL prescricao_Medicamento_BLL = new Prescricao_Medicamento_BLL();
                return prescricao_Medicamento_BLL.Consultar_Prescricao_Medicamento(prescricao);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao Obter Lista de Medicamentos - Prescrito");
            }
        }

        public DataTable ListarPrescricaoActiva()
        {
            DataTable dt = null;
            try
            {
                dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select * from \"Prescricao_dialise\" WHERE \"Prescricao_dialise\".estado = {1}");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Listar Prescrição Activas.");
            }
            return dt;
        }
    }
}
