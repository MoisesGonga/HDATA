using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaObjectoTransferecia;
using System.Data;

namespace CamadaNegocio
{
    public class PacienteBLL
    {
        AcessoDadosBLL acessoDadosBLL = new AcessoDadosBLL();
        #region Manipulação Dados dos Pacientes

        public PacienteBLL()
        {
            acessoDadosBLL = new AcessoDadosBLL();
        }
        
        public int CadastrarPaciente(Paciente p)
        {

            int idPessoa = -1;
            try
            {
                //CADASTAR PESSOA
                PessoaBLL pessoaBLL = new PessoaBLL();
                idPessoa = Convert.ToInt32(pessoaBLL.CadastrarPessoaFunction(p));
                p.Id_pessoa = idPessoa;
                p.Contacto_.idpessoa = idPessoa;
                p.Endereco_.idpessoa = idPessoa;
                //CADASTRAR CONTACTO ASSOCIANDO-A A PESSOA CADASTRADA
                ContactoBLL contBLL = new ContactoBLL();
               string return_contacto = contBLL.CadastrarContacto(p.Contacto_);

                //CADASTAR ENDEREÇO ASSOCIANDO-A A PESSOA CADASTRADA
                EnderecoBLL endBLL = new EnderecoBLL();
                string return_endereco = endBLL.CadastrarEndereco(p.Endereco_);

                //DADOS DOS PACIENTES
                //select func_cadastrar_paciente(2,'H-2','12-12-2010',null,'DR MALEGAS','24-10-2009','Negra','Agudo','N-22-2009','Ministério da Saude',1);
                //CREATE or replace FUNCTION func_cadastrar_paciente(idpessoa_ INTEGER,identificacao_hp_ varchar,data_entrada_ date,data_saida_ date,medico_enviou_ varchar,data_inicio_hd_ date,
                //raca_ varchar,tipo_insuficiencia_ varchar,nr_term_responsabilidade_ varchar,nome_entidade_responsavel_ varchar ,idproveniencia_ int) RETURNS integer AS
                /*
                idpessoa_ integer,
    identificacao_hp_ character varying,
    data_entrada_ date,
    data_saida_ date,
    medico_enviou_ character varying,
    data_inicio_hd_ date,
    raca_ character varying,
    tipo_insuficiencia_ character varying,
    nr_term_responsabilidade_ character varying,
    nome_entidade_responsavel_ character varying,
    idproveniencia_ integer) 
                 */
                acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idpessoa_", idPessoa);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("data_entrada_", FormatarData(p.Data_Entrada));
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("data_saida_", FormatarData(p.Data_Saida));
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("medico_enviou_", p.Medico_Enviou);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("data_inicio_hd_", FormatarData(p.Data_Inicio_HD));
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("raca_", p.Raca);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("tipo_insuficiencia_", p.TipoInsuficiencia.ToString());
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nr_term_responsabilidade_", p.Nr_Term_Resp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_entidade_responsavel_", p.Nome_Entidade);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idproveniencia_", p.Proveniencia_.Id_Proveniencia);
               object ret_ = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_cadastrar_paciente");
                acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
                return idPessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema detectado na Regra de Negócio no Cadastro de Pacientes..."+ ex.Message);
            }
        }

        private string FormatarData(DateTime data)
        {
            string data_="";
            data_ += data.Year + "-";
           
            if (data.Month + "".Length == 1)
            {
                data_ += "0" + data.Month+"-";
            }
            else
            {
                data_ += data.Month + "-";
            }

            if (data.Day + "".Length == 1)
            {
                data_ += "0" + data.Day;
            }
            else
            {
                data_ += data.Day+"";
            }

            return data_;
        }

        public int ActualizarPaciente(Paciente p)
        {
            int idPessoa = -1;
            try
            {
                //CADASTAR PESSOA
                PessoaBLL pessoaBLL = new PessoaBLL();
                idPessoa = Convert.ToInt32(pessoaBLL.ActualizarPessoa(p));
                p.Id_pessoa = idPessoa;
                p.Contacto_.idpessoa = idPessoa;
                p.Endereco_.idpessoa = idPessoa;
                //CADASTRAR CONTACTO ASSOCIANDO-A A PESSOA CADASTRADA
                ContactoBLL contBLL = new ContactoBLL();
                string return_contacto = contBLL.ActualizarContacto(p.Contacto_);

                //CADASTAR ENDEREÇO ASSOCIANDO-A A PESSOA CADASTRADA
                EnderecoBLL endBLL = new EnderecoBLL();
                string return_endereco = endBLL.ActualizarEndereco(p.Endereco_);

                acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idpessoa_", idPessoa);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("identificacao_hp_", p.Identificacao_hp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("data_entrada_", FormatarData(p.Data_Entrada));
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("data_saida_", FormatarData(p.Data_Saida));
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("medico_enviou_", p.Medico_Enviou);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("data_inicio_hd_", FormatarData(p.Data_Inicio_HD));
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("raca_", p.Raca);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("tipo_insuficiencia_", p.TipoInsuficiencia.ToString());
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nr_term_responsabilidade_", p.Nr_Term_Resp);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("nome_entidade_responsavel_", p.Nome_Entidade);
                acessoDadosBLL.AcessodadosPostgreSQL.AdicionarParametro("idproveniencia_", p.Proveniencia_.Id_Proveniencia);
                object ret_ = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.StoredProcedure, "func_actualizar_paciente");
                acessoDadosBLL.AcessodadosPostgreSQL.LimparParametros();
            }
            catch (Exception ex)
            {
                throw new Exception("Problema detectado na Regra de Negócio no Cadastro de Pacientes..." + ex.Message);
            }
            return idPessoa;
        }

        public bool EliminarPaciente(Paciente p)
        {
                try
                {
                   acessoDadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, $"delete from \"Pessoa\" where idpessoa = {p.Id_pessoa}");
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao Buscar Paciente pelo ID: " + ex.Message);
                }
         }
            
        

        public Paciente ObterPacientePeloCodigo(int IdPessoa)
        {
            Paciente paciente = new Paciente();
            try
            {
                DataTable dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select * from dados_prontuario where idpessoa = {IdPessoa}");
                if (dt.Rows.Count > 0 )
                {
                    DataRow linha = dt.Rows[0];
                    paciente.Id_pessoa = IdPessoa;
                        paciente.Nome = Convert.ToString(linha["nome"]);
                        paciente.Nome_pai = Convert.ToString(linha["nome_pai"]);
                        paciente.Nome_mae = Convert.ToString(linha["nome_mae"]);
                        paciente.Naturalidade = Convert.ToString(linha["naturalidade"]);
                        paciente.Nacionalidade = Convert.ToString(linha["nacionalidade"]);
                        paciente.Data_nasc = DateTime.Parse(Convert.ToString(linha["datanasc"]));
                        string genero = Convert.ToString(linha["genero"]); paciente.Genero_ = genero.Equals("M") ? EnumGenero.Masculino : EnumGenero.Feminino;
                        string estado_civil_bd = Convert.ToString(linha["estadocivil"]);
                        EnumEstadoCivil estado_civil_ = EnumEstadoCivil.Solteiro;

                    if (estado_civil_bd.Equals(EnumEstadoCivil.Casado.ToString()))
                        {
                            estado_civil_ = EnumEstadoCivil.Casado;
                        }
                        else if (estado_civil_bd.Equals(EnumEstadoCivil.Companheiro.ToString()))
                        {
                            estado_civil_ = EnumEstadoCivil.Companheiro;
                        }
                        else if (estado_civil_bd.Equals(EnumEstadoCivil.Divorciado.ToString()))
                        {
                            estado_civil_ = EnumEstadoCivil.Divorciado;
                        }
                        else if (estado_civil_bd.Equals(EnumEstadoCivil.Separado.ToString()))
                        {
                            estado_civil_ = EnumEstadoCivil.Separado;
                        }
                        else if (estado_civil_bd.Equals(EnumEstadoCivil.Viuvo.ToString()))
                        {
                            estado_civil_ = EnumEstadoCivil.Viuvo;
                        }
                        else if (estado_civil_bd.Equals(EnumEstadoCivil.Divorciado.ToString()))
                        {
                            estado_civil_ = EnumEstadoCivil.Companheiro;
                        }
                        paciente.Estado_civil = estado_civil_;
                        paciente.Num_BI = Convert.ToString(linha["num_bi"]);
                        paciente.Habilitacao_literaria = Convert.ToString(linha["habilitacao_literaria"]);
                        paciente.Contacto_ = new Contacto(
                            Convert.ToString(linha["telefone_1"]),
                            Convert.ToString(linha["email"]),
                            Convert.ToString(linha["telefone_parente"]), 
                            Convert.ToString(linha["nome_parente"]));
                        paciente.Endereco_ = new Endereco(
                            Convert.ToString(linha["pais"]),
                            Convert.ToString(linha["provincia"]),
                            Convert.ToString(linha["municipio"]),
                            Convert.ToString(linha["rua"]));
                        paciente.Identificacao_hp = Convert.ToString(linha["identificacao_hp"]);
                        paciente.Data_Entrada = DateTime.Parse(Convert.ToString(linha["data_entrada"]));
                    string str_data_saida = Convert.ToString(linha["data_saida"]);
                    DateTime dataSaida = DateTime.MinValue;
                         if (string.IsNullOrEmpty(str_data_saida))
                        {
                        paciente.Data_Saida = dataSaida;
                    }
                    else
                    {
                        dataSaida = DateTime.Parse(str_data_saida);
                        paciente.Data_Saida = dataSaida;
                    }
                    paciente.Medico_Enviou = Convert.ToString(linha["medico_enviou"]);
                        paciente.Data_Inicio_HD = DateTime.Parse(Convert.ToString(linha["data_inicio_hd"]));
                        paciente.Raca = Convert.ToString(linha["raca"]);
                        string tipo_insuficiencia = Convert.ToString(linha["tipo_insuficiencia"]);
                        paciente.TipoInsuficiencia = tipo_insuficiencia.Equals(EnumTipoInsuficiencia.Aguda.ToString()) ? EnumTipoInsuficiencia.Aguda : EnumTipoInsuficiencia.Cronica;
                        paciente.Nr_Term_Resp = Convert.ToString(linha["nr_term_responsabilidade"]);
                        paciente.Nome_Entidade = Convert.ToString(linha["nome_entidade_responsavel"]);
                        ProvenienciaBLL pbll = new ProvenienciaBLL();
                        paciente.Proveniencia_ =  pbll.ConsultarProvenienciaPeloID((Convert.ToInt32(linha["idProveniencia"])));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar Paciente pelo nome: " + ex.Message);
            }

            return paciente;

        }

        public DataTable ConsultarPacientePorNome(string nome)
        {
            try
            {
                DataTable dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM dados_prontuario WHERE nome ILIKE '%{nome}%';");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar Paciente pelo nome: ");
            }
        }

        public DataTable ConsultarPacientePorID(string idCentro)
        {
            if (idCentro.Length>0)
            {
            try
            {
                DataTable dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM dados_prontuario WHERE idpessoa = {idCentro} ;");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar Paciente pelo ID: "+ ex.Message);
            }
            }
            return null;
        }

        public DataTable BuscarTodosPaciente()
        {
            DataTable dt = null;
            try
            {
                 dt = acessoDadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, "select * from dados_prontuario order by idpessoa");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar Todos os Pacientes: ");
            }
            return dt;
        }

        #endregion
    }
}
