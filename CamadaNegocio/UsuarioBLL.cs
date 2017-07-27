using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class UsuarioBLL
    {
        BaseAcessoDadosBLL acessodadosBLL;
        public UsuarioBLL()
        {
            acessodadosBLL = new BaseAcessoDadosBLL();
        }

        //public DataRow AutenticarUsuario(string nome_usuario, string senha_usuario)
        //{
        //    //Usuario user = null;

        //    try
        //    {
        //        acessoDadosPostgreSQL.LimparParametros();
        //        acessoDadosPostgreSQL.AdicionarParametro("$1", nome_usuario);
        //        acessoDadosPostgreSQL.AdicionarParametro("$2", senha_usuario);
        //        DataTable DataTableUsuario = acessoDadosPostgreSQL.ExecututarConsulta(CommandType.StoredProcedure, "func_login_usuario");
        //        if (DataTableUsuario != null && DataTableUsuario.Rows.Count > 0)
        //        {
        //            return DataTableUsuario.Rows[0];
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }

        //}//func_login_usuario_md5

        public Usuario AutenticarUsuario_MD5(Usuario user_)
        {
            Usuario user = null;
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$1", user_.NomeUsuario);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$2", user_.PalavraPasse);
                DataTable DataTableUsuario = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.StoredProcedure, "func_login_usuario_md5");
                if (DataTableUsuario != null && DataTableUsuario.Rows.Count > 0)
                {
                    user = new Usuario();
                    Funcionario func = new Funcionario();
                    FuncionarioBLL funcBLL = new FuncionarioBLL();
                    int idfunc = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[0]);
                    func = funcBLL.ConsultarFuncionario(idfunc);
                    user.Funcionario = func;
                    user.NomeUsuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[1]);
                    user.PalavraPasse = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[2]);
                    user.IdUsuario = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[3]);
                    if (Convert.ToString(DataTableUsuario.Rows[0].ItemArray[4]) == UserType.Admin.ToString())
                        user.Perfil_Usuario = UserType.Admin;
                    else if (Convert.ToString(DataTableUsuario.Rows[0].ItemArray[4]) == UserType.Medical.ToString())
                        user.Perfil_Usuario = UserType.Medical;
                    else
                        user.Perfil_Usuario = UserType.Nurse;

                    string str_data_ultimo_acess = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[6]);
                    if (!string.IsNullOrEmpty(str_data_ultimo_acess))
                        user.DataUltimoAcesso = DateTime.Parse(str_data_ultimo_acess);
                    user.SiglaUsuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[7]);
                   
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {

                new Exception("Problema nas Regras da Camada de Negócio do Usuário - AUTENTICAR USUARIO");
            }
            return user;
        }

        public Usuario AutenticarUsuario(Usuario user_)
        {
            Usuario user = null;
            CriptorafiaMD5 criptoMD5 = new CriptorafiaMD5();
            try
            {
                // var novasenha = "123456789";
                // var OUTRAnovasenha = "123456789";
                // var novasenhaCRIPTOGRAFADA = criptoMD5.RetornarMD5(novasenha);
                // Console.WriteLine(novasenhaCRIPTOGRAFADA);
                //Console.WriteLine(criptoMD5.ComparaMD5(OUTRAnovasenha, novasenhaCRIPTOGRAFADA));

                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$1", user_.NomeUsuario);
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$2", user_.PalavraPasse);
                DataTable DataTableUsuario = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.StoredProcedure, "func_login_usuario");
                if (DataTableUsuario != null && DataTableUsuario.Rows.Count > 0)
                {
                    user = new Usuario();
                    Funcionario func = new Funcionario();
                    func.Id_pessoa = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[0]);
                    user.Funcionario = func;
                    user.NomeUsuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[1]);
                    user.PalavraPasse = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[2]);
                    user.IdUsuario = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[3]);
                    if (Convert.ToString(DataTableUsuario.Rows[0].ItemArray[4]) == UserType.Admin.ToString())
                    {
                        user.Perfil_Usuario = UserType.Admin;
                    }
                    else if (Convert.ToString(DataTableUsuario.Rows[0].ItemArray[4]) == UserType.Medical.ToString())
                    {
                        user.Perfil_Usuario = UserType.Medical;
                    }
                    else
                    {
                        user.Perfil_Usuario = UserType.Nurse;
                    }
                }
                else
                {
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {

                new Exception("Problema nas Regras da Camada de Negócio do Usuário - AUTENTICAR USUARIO");
            }
            return user;
        }

        public void CadastrarUsuario(Usuario user)
        {

            try
            {
                this.acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                string query = $"insert into \"Usuario\" values ({user.Funcionario.Id_pessoa},'{user.NomeUsuario}', '{user.PalavraPasse}',default,'{ user.Perfil_Usuario.ToString()}',{BaseHelpBLL.DateToInsert_or_UpdateDatabse(user.DataUltimoAcesso)},{BaseHelpBLL.DateToInsert_or_UpdateDatabse(user.DataCadastro)},'{user.SiglaUsuario}')";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            }
            catch (Exception)
            {

                new Exception("Ocorreu um erro no cadastro do Utilizador");
            }
        }

        public void ActualizarUsuario(Usuario user)
        {
            try
            {
                this.acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                string data_ultimo_acesso = BaseHelpBLL.FormatDateTimeToDataBasePatern(user.DataUltimoAcesso) ;
                string query = $"update \"Usuario\" set nome_usuario = '{user.NomeUsuario}', palavra_passe = '{user.PalavraPasse}',pefil_usuario = '{ user.Perfil_Usuario.ToString()}',ultimo_acesso = '{data_ultimo_acesso}', date_cadastro = {BaseHelpBLL.DateToInsert_or_UpdateDatabse(user.DataCadastro)}, sigla_usuario = '{user.SiglaUsuario}' where idpessoa = {user.Funcionario.Id_pessoa} and id_usuario = {user.IdUsuario} ";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            }
            catch (Exception)
            {

                new Exception("Problema encontrado na actualização dos dados do usuário");
            }
        }

        public void EliminarAcessoVascular(Usuario user)
        {
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                string query = $"delete from \"Usuario\"  where idpessoa = {user.Funcionario.Id_pessoa} and id_usuario = {user.IdUsuario} ";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, query);
            }
            catch (Exception)
            {
                new Exception($"Ocorreu um problema ao eliminar o Utilizador: {user.NomeUsuario} ");
            }
        }

        public List<Usuario> ListarUtilizadores()
        {
            List<Usuario> listautilizadores = new List<Usuario>();
            try
            {
                DataTable DataTableutilizadores = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Usuario\"");
                foreach (DataRow linha in DataTableutilizadores.Rows)
                {
                    Usuario user = new Usuario();
                    user.IdUsuario = Convert.ToInt32(linha["id_usuario"]);
                    user.NomeUsuario = Convert.ToString(linha["nome_usuario"]);
                    user.NomeUsuario = Convert.ToString(linha["pefil_usuario"]);
                    string str_ultimo_acesso = Convert.ToString(linha["ultimo_acesso"]);
                    if (!string.IsNullOrEmpty(str_ultimo_acesso))
                        user.DataUltimoAcesso = DateTime.Parse(str_ultimo_acesso);

                    string str_date_cadastro = Convert.ToString(linha["ultimo_acesso"]);
                    if (!string.IsNullOrEmpty(str_ultimo_acesso))
                        user.DataCadastro = DateTime.Parse(str_ultimo_acesso);

                    listautilizadores.Add(user);
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um problema na listagem dos utilizadores");
            }

            return listautilizadores;
        }

        public DataTable ListarTodosDadosUtilizadores()
        {
            DataTable DataTableutilizadores = null;
            try
            {
                string query = "SELECT * FROM \"Funcionario\",\"Usuario\",dados_pessoais_view WHERE \"Funcionario\".idpessoa = \"Usuario\".idpessoa AND dados_pessoais_view.idpessoa = \"Funcionario\".idpessoa"; 
                 DataTableutilizadores = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, query);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um problema na listagem de todos os dados dos utilizadores");
            }

            return DataTableutilizadores;
        }

        public List<Usuario> ConsultarUtilizadorpeloID(Usuario usuario)
        {
            List<Usuario> listautilizadores = new List<Usuario>();
            try
            {
                DataTable DataTableutilizadores = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Usuario\" Where id_usuario = {usuario.IdUsuario}");
                foreach (DataRow linha in DataTableutilizadores.Rows)
                {
                    Usuario user = new Usuario();
                    user.IdUsuario = Convert.ToInt32(linha["id_usuario"]);
                    user.NomeUsuario = Convert.ToString(linha["nome_usuario"]);
                    user.NomeUsuario = Convert.ToString(linha["pefil_usuario"]);
                    string str_ultimo_acesso = Convert.ToString(linha["ultimo_acesso"]);
                    if (!string.IsNullOrEmpty(str_ultimo_acesso))
                        user.DataUltimoAcesso = DateTime.Parse(str_ultimo_acesso);

                    string str_date_cadastro = Convert.ToString(linha["ultimo_acesso"]);
                    if (!string.IsNullOrEmpty(str_date_cadastro))
                        user.DataCadastro = DateTime.Parse(str_date_cadastro);

                    listautilizadores.Add(user);
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um problema na listagem dos utilizadores");
            }

            return listautilizadores;
        }

        public List<Usuario> ConsultarUtilizadorPeloNome(string nome)
        {
            List<Usuario> listautilizadores = new List<Usuario>();
            try
            {
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Material\" WHERE nome_material ILIKE '%{nome}%'");
                foreach (DataRow linha in dt.Rows)
                {
                    Usuario user = new Usuario();
                    user.IdUsuario = Convert.ToInt32(linha["id_usuario"]);
                    user.NomeUsuario = Convert.ToString(linha["nome_usuario"]);
                    user.NomeUsuario = Convert.ToString(linha["pefil_usuario"]);
                    string str_ultimo_acesso = Convert.ToString(linha["ultimo_acesso"]);
                    if (!string.IsNullOrEmpty(str_ultimo_acesso))
                        user.DataUltimoAcesso = DateTime.Parse(str_ultimo_acesso);

                    string str_date_cadastro = Convert.ToString(linha["ultimo_acesso"]);
                    if (!string.IsNullOrEmpty(str_date_cadastro))
                        user.DataCadastro = DateTime.Parse(str_date_cadastro);

                    listautilizadores.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao consultar o utilizador nome!!!");
            }
            return listautilizadores;
        }
    }
}
