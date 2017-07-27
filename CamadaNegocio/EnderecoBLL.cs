using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
   public class EnderecoBLL
    {
        BaseAcessoDadosBLL acessoDadosBLL;

        public EnderecoBLL()
        {
            acessoDadosBLL = new BaseAcessoDadosBLL();
        }

        public string CadastrarEndereco(Endereco endereco)
        {
            object obj = null;
            try
            {
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"Insert into \"Endereco\" Values ({endereco.idpessoa},'{endereco.pais}','{endereco.provincia}','{endereco.municipio}','{endereco.rua}')");
                obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Endereco\" where idpessoa= {endereco.idpessoa}");
            }
            catch (Exception ex)
            {
                throw new Exception("Problema encontrado no Cadastro de Endereço...");
            }
            //CADASTRO UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            
           return Convert.ToString(obj);
        }

        public string ActualizarEndereco(Endereco endereco)
        {
            object obj = null;
            //Actualização UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            try
            {
                acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Endereco\" set pais = '{endereco.pais}', provincia = '{endereco.provincia}',municipio='{endereco.municipio}', rua='{endereco.rua}' where idpessoa = {endereco.idpessoa}");
                obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"select idpessoa from \"Endereco\" where idpessoa= {endereco.idpessoa}");
            }
            catch (Exception ex)
            {
                throw new Exception("Problema encontrado na Actualização do Endereço...");
            }
           
            return Convert.ToString(obj);
        }

        public string CadastrarEnderecoFunction(Endereco endereco)
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

        public string ActualizarEnderecoFunction(Endereco endereco)
        {
            //CADASTRO UTILIZANDO FUNÇÃO DO BANCO DE DADOS
            acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idpessoa", endereco.idpessoa);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("pais", endereco.pais);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("provincia", endereco.provincia);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("municipio", endereco.municipio);
            acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("rua", endereco.rua);
            object obj = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_actualizar_endereco");
            return Convert.ToString(obj);
        }
      
    }
}
