using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class ContactoBLL
    {
        BaseAcessoDadosBLL acessoDadosBLL;
        public ContactoBLL()
        {
            acessoDadosBLL = new BaseAcessoDadosBLL();
        }

        public string CadastrarContacto(Contacto contacto)
        {
            object obj = null;
            try
            {
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"Insert into \"Contacto\" Values ({contacto.idpessoa},'{contacto.telefone_1}','{contacto.email}','{contacto.telefone_parente}','{contacto.nome_parente}')");
               obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Contacto\" where idpessoa= {contacto.idpessoa}");
            }
            catch (Exception)
            {
                throw new Exception("Problema encontrado no Cadastro do Contacto...");
            }
            return Convert.ToString(obj);
        }

        public string ActualizarContacto(Contacto contacto)
        {
            object obj = null;
            //ACTUALIZAR CONTACTO - COMANDO DE MANIPULAÇÃO SQL
            try
            {
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Contacto\" set telefone_1 = '{contacto.telefone_1}', email = '{contacto.email}',telefone_parente='{contacto.telefone_parente}', nome_parente='{contacto.nome_parente}' where idpessoa = {contacto.idpessoa}");
                obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Contacto\" where idpessoa= {contacto.idpessoa}");
            }
            catch (Exception)
            {
                throw new Exception("Problema encontrado na Actualização do Contancto...");
            }
            return Convert.ToString(obj);
        }
    }
}
