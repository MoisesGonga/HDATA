using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
   class EnderecoBLL
    {
        AcessoDadosBLL acessoDadosBLL;
        public EnderecoBLL()
        {
            acessoDadosBLL = new AcessoDadosBLL();
        }

        public string CadastrarEndereco(Endereco endereco)
        {
            //CADASTRO UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"Insert into \"Endereco\" Values ({endereco.idpessoa},'{endereco.pais}','{endereco.provincia}','{endereco.municipio}','{endereco.rua}')");
           object obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Endereco\" where idpessoa= {endereco.idpessoa}");
           return Convert.ToString(obj);
        }

        public string CadastrarEnderecox(Endereco endereco)
        {
            //CADASTRO UTILIZANDO FUNÇÃO DO BANCO DE DADOS
            acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idpessoa", endereco.idpessoa);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("pais", endereco.pais);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("provincia", endereco.provincia);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("municipio", endereco.municipio);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("rua", endereco.rua);
            object obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType. StoredProcedure, "func_cadastrar_endereco");
            return Convert.ToString(obj);
        }

        public string AlterarEndereco(Endereco endereco)
        {
            //CADASTRO UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Endereco\" set pais = '{endereco.pais}', provincia = '{endereco.provincia}',municipio='{endereco.municipio}', rua='{endereco.rua}' where idpessoa = {endereco.idpessoa}");
            object obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Endereco\" where idpessoa= {endereco.idpessoa}");
            return Convert.ToString(obj);
        }
    }
}
