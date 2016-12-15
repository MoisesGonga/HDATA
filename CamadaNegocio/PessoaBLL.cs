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

        public int CadastrarPessoaX(Pessoa p)
        {
            int idPessoa = -1;
            try
            {
                //CADASTRAR PESSOA SEM USAR FUNÇÃO
                //--nome,nome_pai,nome_mae,naturalidade,nacionalidade,datanasc,estadocivil,genero,num_bi,habilitacao_literaria

                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("datanasc_", p.Data_nasc.ToShortDateString());
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil.ToString());
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("genero_", p.Genero_.ToString());
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
                //select last_value into idpessoa_ as idproveniencia from "Pessoa_idpessoa_seq";
                idPessoa = Convert.ToInt32(acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_paciente"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return idPessoa;
        }

        public int CadastrarPessoa(Pessoa p)
        {
            int idPessoa = -1;
            try
            {
                //DADOS PESSOAIS
                //--nome,nome_pai,nome_mae,naturalidade,nacionalidade,datanasc,estadocivil,genero,num_bi,habilitacao_literaria
                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("datanasc_", p.Data_nasc.Date);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil.ToString());
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("genero_", p.Genero_.ToString());
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
                idPessoa = Convert.ToInt32(acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_pessoa"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return idPessoa;
        }

        }

}
