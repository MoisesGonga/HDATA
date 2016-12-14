using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Npgsql;
using NpgsqlTypes;

namespace CamadaAcessoDados
{
    public class AcessoDadosPostgreSQL
    {


        private NpgsqlConnection CriarConexao()
        {
            return new NpgsqlConnection(CamadaAcessoDados.Properties.Settings.Default.StringConexao);
        }

        //Manipular Parametros no PostgreSql
        private NpgsqlParameterCollection npgsqlParameterCollection = new NpgsqlCommand().Parameters;

        public void LimparParametros()
        {
            npgsqlParameterCollection.Clear();
        }

        public void AdicionarParametro(string nomeParametro, object valorParamestro)
        {
            npgsqlParameterCollection.Add(new NpgsqlParameter(nomeParametro, valorParamestro));
        }

        public object ExecututarManipulacao(CommandType commandType,string nomeProcedimentoOuStringSQL)
        {
            try
            {
                //Criar Conexão
                NpgsqlConnection npgsqlconnection = CriarConexao();

                //Abrir Conexão
                npgsqlconnection.Open();
                NpgsqlCommand npgsqlCommand = npgsqlconnection.CreateCommand();
                //Tipo de Comando (Se é um procedimento ou uma String SQL)

                npgsqlCommand.CommandType = commandType;
                npgsqlCommand.CommandText = nomeProcedimentoOuStringSQL;
                npgsqlCommand.CommandTimeout = 7200;//Duas Horas (EM SEGUNDOS)

                //Adicionar os Parametros no Commando

                foreach (NpgsqlParameter npgsqlParameter in npgsqlParameterCollection)
                {
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter(npgsqlParameter.ParameterName, npgsqlParameter.Value));
                }

                //npgsqlconnection.Close();

                return npgsqlCommand.ExecuteScalar();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Verificar os Seguintes Problemas na Camada de AcessoDadosPostgreSQL Executar Manipulação: {ex.Message }");
            }
        }

        

        public DataTable ExecututarConsulta(CommandType commandType, string nomeProcedimentoOuStringSQL)
        {
           
            try
            {
                NpgsqlConnection npgsqlConnection = CriarConexao();
                npgsqlConnection.Open();
                NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
                //Tipo de Comando (Se é um procedimento ou uma String SQL)

                npgsqlCommand.CommandType = commandType;
                npgsqlCommand.CommandText = nomeProcedimentoOuStringSQL;
                npgsqlCommand.CommandTimeout = 7500;// tempo (EM SEGUNDOS)

                //Adicionar os Parametros no Commando

                foreach (NpgsqlParameter npgsqlParameter in npgsqlParameterCollection)
                {
                    npgsqlCommand.Parameters.Add(new NpgsqlParameter(npgsqlParameter.ParameterName, npgsqlParameter.Value));
                }

                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                //Data Table  Tabela onde vai ser colocado os ddos que vem do banco de dados
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                npgsqlConnection.Close();
                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception($"Verificar os Seguintes Problemas na Camada de AcessoDadosPostgreSQL Executar Consulta: {ex.Message }");
            }
        }



    }
}
