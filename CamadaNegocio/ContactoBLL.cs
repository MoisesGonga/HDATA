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
        AcessoDadosBLL acessoDadosBLL;
        public ContactoBLL()
        {
            acessoDadosBLL = new AcessoDadosBLL();
        }

        public string CadastrarContacto(Contacto contacto)
        {
            //CADASTRO UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            /*
             *idpessoa integer NOT NULL,
                telefone_1 character varying(15),
                email character varying(50),
                telefone_parente character varying(15),
                nome_parente character varying(30), 
             * 
             *
             */
            acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"Insert into \"Contacto\" Values ({contacto.idpessoa},'{contacto.telefone_1}','{contacto.email}','{contacto.telefone_parente}','{contacto.nome_parente}')");
            object obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Contacto\" where idpessoa= {contacto.idpessoa}");
            return Convert.ToString(obj);
        }

        public string AlterarContacto(Contacto contacto)
        {
            //CADASTRO UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Contacto\" set telefone_1 = '{contacto.telefone_1}', email = '{contacto.email}',telefone_parente='{contacto.telefone_parente}', nome_parente='{contacto.nome_parente}' where idpessoa = {contacto.idpessoa}");
            object obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Contacto\" where idpessoa= {contacto.idpessoa}");
            return Convert.ToString(obj);
        }
    }
}
