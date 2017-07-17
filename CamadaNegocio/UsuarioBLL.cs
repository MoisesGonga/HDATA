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
        AcessoDadosBLL acessodadosBLL;
        public UsuarioBLL()
        {
            acessodadosBLL = new AcessoDadosBLL();
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
                    func.Id_pessoa = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[0]);
                    user.Funcionario = func;
                    user.NomeUsuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[1]);
                    user.PalavraPasse = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[2]);
                    user.IdUsuario = Convert.ToInt32(DataTableUsuario.Rows[0].ItemArray[3]);
                    user.Perfil_Usuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[4]);
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
                acessodadosBLL.AcessodadosPostgreSQL.AdicionarParametro("$2",  user_.PalavraPasse);
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
                    user.Perfil_Usuario = Convert.ToString(DataTableUsuario.Rows[0].ItemArray[4]);
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

    }
}
