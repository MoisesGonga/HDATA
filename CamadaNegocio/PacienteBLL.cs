using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaObjectoTransferecia;
using System.Data;

namespace CamadaNegocio
{
    public class PacienteBLL
    {
        AcessoDadosBLL acessoDadosBLL;
        #region Manipulação Dados dos Pacientes
        
        public int CadastrarPaciente(Paciente p)
        {

            int idPessoa = -1;
            try
            {
                //CADASTAR PESSOA
                PessoaBLL pessoaBLL = new PessoaBLL();
                idPessoa = Convert.ToInt32(pessoaBLL.CadastrarPessoa(p));
                
                //CADASTRAR CONTACTO ASSOCIANDO-A A PESSOA CADASTRADA
                ContactoBLL contBLL = new ContactoBLL();
                contBLL.CadastrarContacto(p.Contacto_);

                //CADASTAR ENDEREÇO ASSOCIANDO-A A PESSOA CADASTRADA
                EnderecoBLL endBLL = new EnderecoBLL();
                endBLL.CadastrarEndereco(p.Endereco_);

                //DADOS DOS PACIENTES
                //select func_cadastrar_paciente(2,'H-2','12-12-2010',null,'DR MALEGAS','24-10-2009','Negra','Agudo','N-22-2009','Ministério da Saude',1);
                //CREATE or replace FUNCTION func_cadastrar_paciente(idpessoa_ INTEGER,identificacao_hp_ varchar,data_entrada_ date,data_saida_ date,medico_enviou_ varchar,data_inicio_hd_ date,raca_ varchar,tipo_insuficiencia_ varchar,nr_term_responsabilidade_ varchar,nome_entidade_responsavel_ varchar ,idproveniencia_ int) RETURNS integer AS
                acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idpessoa_", idPessoa);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
            }
            catch (Exception ex)
            {

                throw new Exception("Problema detectado na Regra de Negócio no Cadastro de Pacientes...");
            }
            return idPessoa;
        }

        public void ActualizarPaciente()
        {

        }

        public bool EliminarPaciente()
        {
            bool val = false;




            return val;
        }

        public DataTable ConsultarPacientePorNome(string nome)
        {

            return new DataTable();
        }


        #endregion
    }
}
