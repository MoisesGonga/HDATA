using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaAcessoDados;
using CamadaObjectoTransferecia;

namespace CamadaNegocio
{
    public class Centro_Hemodialise
    {
        AcessoDadosPostgreSQL acessoDadosPostgreSQL;

        public Centro_Hemodialise()
        {
            acessoDadosPostgreSQL = new AcessoDadosPostgreSQL();
        }



        #region Manipulação Dados dos Pacientes
        //private int CadastrarPessoa(Pessoa p)
        //{

        //    int idPessoa = -1; 
        //    try
        //    {
        //        //--nome,nome_pai,nome_mae,naturalidade,nacionalidade,datanasc,estadocivil,genero,num_bi,habilitacao_literaria
        //        acessoDadosPostgreSQL.LimparParametros();
        //        acessoDadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
        //        acessoDadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
        //        acessoDadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
        //        acessoDadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
        //        acessoDadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
        //        acessoDadosPostgreSQL.AdicionarParametro("datanasc_", p.Data_nasc);
        //        acessoDadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil);
        //        acessoDadosPostgreSQL.AdicionarParametro("genero_", p.Genero_);
        //        acessoDadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
        //        acessoDadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
        //        idPessoa = Convert.ToInt32(acessoDadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_paciente"));
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }

        //    return idPessoa;

        //}

        private int CadastrarPessoa(Funcionario p)
        {

            int idPessoa = -1;
            try
            {
                //--nome,nome_pai,nome_mae,naturalidade,nacionalidade,datanasc,estadocivil,genero,num_bi,habilitacao_literaria
                acessoDadosPostgreSQL.LimparParametros();
                acessoDadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
                acessoDadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
                acessoDadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
                acessoDadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
                acessoDadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
                acessoDadosPostgreSQL.AdicionarParametro("datanasc_", p.Data_nasc);
                acessoDadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil);
                acessoDadosPostgreSQL.AdicionarParametro("genero_", p.Genero_);
                acessoDadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
                acessoDadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
                idPessoa = Convert.ToInt32(acessoDadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_paciente"));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return idPessoa;


        }

        public int CadastrarPaciente(Paciente p)
        {

            int idPessoa = -1;
            try
            {
                //DADOS PESSOAIS
                //--nome,nome_pai,nome_mae,naturalidade,nacionalidade,datanasc,estadocivil,genero,num_bi,habilitacao_literaria
                acessoDadosPostgreSQL.LimparParametros();
                acessoDadosPostgreSQL.AdicionarParametro("nome_", p.Nome);
                acessoDadosPostgreSQL.AdicionarParametro("nome_pai_", p.Nome_pai);
                acessoDadosPostgreSQL.AdicionarParametro("nome_mae_", p.Nome_mae);
                acessoDadosPostgreSQL.AdicionarParametro("naturalidade_", p.Naturalidade);
                acessoDadosPostgreSQL.AdicionarParametro("nacionalidade_", p.Nacionalidade);
                acessoDadosPostgreSQL.AdicionarParametro("datanasc_", p.Data_nasc);
                acessoDadosPostgreSQL.AdicionarParametro("estadocivil_", p.Estado_civil);
                acessoDadosPostgreSQL.AdicionarParametro("genero_", p.Genero_);
                acessoDadosPostgreSQL.AdicionarParametro("num_bi_", p.Num_BI);
                acessoDadosPostgreSQL.AdicionarParametro("habilitacao_literaria_", p.Habilitacao_literaria);
                idPessoa = Convert.ToInt32(acessoDadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_paciente"));
                //DADOS DOS PACIENTES
                acessoDadosPostgreSQL.LimparParametros();
                acessoDadosPostgreSQL.AdicionarParametro("idpessoa_", idPessoa);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                //select func_cadastrar_paciente(2,'H-2','12-12-2010',null,'DR MALEGAS','24-10-2009','Negra','Agudo','N-22-2009','Ministério da Saude',1);
                //CREATE or replace FUNCTION func_cadastrar_paciente(idpessoa_ INTEGER,identificacao_hp_ varchar,data_entrada_ date,data_saida_ date,medico_enviou_ varchar,data_inicio_hd_ date,raca_ varchar,tipo_insuficiencia_ varchar,nr_term_responsabilidade_ varchar,nome_entidade_responsavel_ varchar ,idproveniencia_ int) RETURNS integer AS
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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

        #region Manipulação Dados dos Funcionário


        #endregion



      

        public DataRow AutenticarUsuario(string nome_usuario, string senha_usuario)
        {
            //Usuario user = null;

            try
            {
                acessoDadosPostgreSQL.LimparParametros();
                acessoDadosPostgreSQL.AdicionarParametro("$1", nome_usuario);
                acessoDadosPostgreSQL.AdicionarParametro("$2", senha_usuario);
                DataTable DataTableUsuario = acessoDadosPostgreSQL.ExecututarConsulta(CommandType.StoredProcedure, "func_login_usuario");
                if (DataTableUsuario != null && DataTableUsuario.Rows.Count > 0)
                {
                    return DataTableUsuario.Rows[0];
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public Usuario AutenticarUsuario(Usuario user_)
        {
            Usuario user = null;

            try
            {
                acessoDadosPostgreSQL.LimparParametros();
                acessoDadosPostgreSQL.AdicionarParametro("$1", user_.NomeUsuario);
                acessoDadosPostgreSQL.AdicionarParametro("$2", user_.PalavraPasse);
                DataTable DataTableUsuario = acessoDadosPostgreSQL.ExecututarConsulta(CommandType.StoredProcedure, "func_login_usuario");
                if (DataTableUsuario != null && DataTableUsuario.Rows.Count > 0)
                {
                    user = new Usuario();
                    Funcionario func = new Funcionario();
                    func.Id_pessoa = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[0]);
                    user.Funcionario = func;
                    user.NomeUsuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[1]);
                    user.PalavraPasse = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[2]);
                    user.IdUsuario = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[3]);
                }
                else
                {
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public void CadastrarFuncionario()
        {


        }

        public void CadastrarRegistoHemodialise()
        {

        }

    }

}




