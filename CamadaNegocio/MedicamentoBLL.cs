using CamadaObjectoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
   public class MedicamentoBLL
    {

        AcessoDadosBLL acessodadosBLL;

        public MedicamentoBLL()
        {
            acessodadosBLL = new CamadaNegocio.AcessoDadosBLL();
        }

        public List<Medicamento> ListarMedicamentos()
        {
            List<Medicamento> ListMedicamentos = null;
            try
            {
                ListMedicamentos = new List<Medicamento>();
                DataTable DataTableMateriais = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Medicamento\" ");
                foreach (DataRow linha in DataTableMateriais.Rows)
                {
                    Medicamento medicamento = new Medicamento();
                    medicamento.id_medicamento = Convert.ToInt32(linha["id_medicamento"]);
                    medicamento.nome = Convert.ToString(linha["nome"]);
                    medicamento.nome_comercial = Convert.ToString(linha["nome_comercial"]);
                    medicamento.descricao = Convert.ToString(linha["descricao"]);
                    ListMedicamentos.Add(medicamento);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema na Consulta dos Medicamentos...");
            }

            return ListMedicamentos;
        }
        /*
         id_medicamento integer NOT NULL DEFAULT nextval('"Medicamento_id_medicamento_seq"'::regclass),
            nome character varying(30),
            nome_comercial character varying(40),
            descricao character varying(40) NOT NULL,
             
             */

        public int CadastrarMedicamento(Medicamento medicamento)  {
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            string query = $"insert into \"Medicamento\" values (default,'{medicamento.nome}', '{medicamento.nome_comercial}','{medicamento.descricao}')";
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            object rt2 = acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, "select last_value as id_medicamento from \"Medicamento_id_medicamento_seq\"");
            return Convert.ToInt32(rt2);
        }

        public List<Medicamento> ConsultarMedicamentoPeloNome(string nome)
        {
            List<Medicamento> ListaMedicamento = null;
            try
            {
                ListaMedicamento = new List<Medicamento>();
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Medicamento\" WHERE nome ILIKE '%{nome}%'");
                foreach (DataRow linha in dt.Rows)
                {
                    Medicamento medicamento = new Medicamento();
                    medicamento.id_medicamento = Convert.ToInt32(linha["id_medicamento"]);
                    medicamento.nome = Convert.ToString(linha["nome"]);
                    medicamento.nome_comercial = Convert.ToString(linha["nome_comercial"]);
                    medicamento.descricao = Convert.ToString(linha["descricao"]);
                    ListaMedicamento.Add(medicamento);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar o Medicamento pelo nome: ");
            }
            return ListaMedicamento;
        }

        public Medicamento ConsultarMedicamentoPeloID(int id_Medicamento)
        {
            Medicamento medicamento = null;
            try
            {
                medicamento = new Medicamento();
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Medicamento\" WHERE id_medicamento = {id_Medicamento}");
                foreach (DataRow linha in dt.Rows)
                {
                    medicamento.id_medicamento = Convert.ToInt32(linha["id_medicamento"]);
                    medicamento.nome = Convert.ToString(linha["nome"]);
                    medicamento.nome_comercial = Convert.ToString(linha["nome_comercial"]);
                    medicamento.descricao = Convert.ToString(linha["descricao"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar o Medicamento pelo nome: ");
            }
            return medicamento;
        }
    }
}
