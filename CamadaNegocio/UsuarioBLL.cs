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

        //}
        public Usuario AutenticarUsuario(Usuario user_)
        {
            Usuario user = null;

            try
            {
                
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
