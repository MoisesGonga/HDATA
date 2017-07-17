using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class PessoaBLL
    {
        AcessoDadosBLL acessodadosBLL;

        public PessoaBLL()
        {
            acessodadosBLL = new AcessoDadosBLL();
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

        public int CadastrarPessoaFunction(Pessoa p)
        {
            int idPessoa = -1;
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
                string data_nascimento = FormatarData(p.Data_nasc);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("datanasc_", data_nascimento);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil.ToString());
                string genero = p.Genero_ == EnumGenero.Masculino ? "M" : "F";
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("genero_", genero);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
                //select last_value into idpessoa_ as idproveniencia from "Pessoa_idpessoa_seq";
                idPessoa = Convert.ToInt32(acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_pessoa"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return idPessoa;
        }

        public int ActualizarPessoaFunction(Pessoa p)
        {
            int idPessoa = -1;
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                //CREATE or replace FUNCTION func_actualizar_pessoa (in idpessoa_ integer, in nome_ varchar, in nome_pai_ varchar, in nome_mae_ varchar, in naturalidade_ varchar, in nacionalidade_ varchar, in datanasc_ varchar,in estadocivil_ varchar, in genero_ varchar, in num_bi_ varchar, in habilitacao_literaria_ varchar) RETURNS integer AS
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idpessoa_", p.Id_pessoa);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
                string data_nascimento = FormatarData(p.Data_nasc);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("datanasc_", data_nascimento);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil.ToString());
                string genero = p.Genero_ == EnumGenero.Masculino ? "M" : "F";
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("genero_", genero);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
                
                //select last_value into idpessoa_ as idproveniencia from "Pessoa_idpessoa_seq";
                idPessoa = Convert.ToInt32(acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_actualizar_pessoa"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return idPessoa;
        }

        public int ActualizarPessoa(Pessoa p)
        {
            try
            {
                string data_nascimento = FormatarData(p.Data_nasc);
                string genero = p.Genero_ == EnumGenero.Masculino ? "M" : "F";
                string query = $"update \"Pessoa\" set nome = '{p.Nome}', nome_pai = '{p.Nome_pai}', nome_mae = '{p.Nome_mae}',naturalidade = '{p.Naturalidade}',nacionalidade = '{p.Nacionalidade}', datanasc = TO_DATE('{data_nascimento}', 'YYYY-MM-DD'), estadocivil = '{p.Estado_civil.ToString()}', genero = '{genero}', num_bi = '{ p.Num_BI}', habilitacao_literaria = '{p.Habilitacao_literaria}' where idpessoa = {p.Id_pessoa}";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text,query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p.Id_pessoa;
        }
    }
}