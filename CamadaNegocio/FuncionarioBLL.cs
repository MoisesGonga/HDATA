using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class FuncionarioBLL
    {
        BaseAcessoDadosBLL acessodadosBLL;

        public FuncionarioBLL()
        {
            acessodadosBLL = new BaseAcessoDadosBLL();
        }

        public Funcionario ConsultarFuncionario(int idpessoa)
        {
            Funcionario func = null;
            try
            {
                DataTable DataTableFuncionario = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM dados_pessoais_view dview, \"Funcionario\" func WHERE func.idpessoa = {idpessoa}  and dview.idpessoa = {idpessoa}");
                foreach (DataRow linha in DataTableFuncionario.Rows)
                {
                    func = new Funcionario();
                    // DADOS DA TABELA PESSOA
                    func.Id_pessoa = Convert.ToInt32(linha["idpessoa"]);
                    func.Nome = Convert.ToString(linha["nome"]);
                    func.Nome_pai = Convert.ToString("nome_pai");
                    func.Nome_mae = Convert.ToString("nome_mae");
                    func.Naturalidade = Convert.ToString("naturalidade");
                    func.Nacionalidade = Convert.ToString("nacionalidade");

                    string str_Data_nasc = Convert.ToString(linha["datanasc"]);
                    if (!string.IsNullOrEmpty(str_Data_nasc))
                        func.Data_nasc = DateTime.Parse(str_Data_nasc);

                    EnumEstadoCivil estado_civil_ = EnumEstadoCivil.Solteiro;
                    string estado_civil_bd = Convert.ToString("estadocivil");

                    if (estado_civil_bd.Equals(EnumEstadoCivil.Casado.ToString()))
                        estado_civil_ = EnumEstadoCivil.Casado;
                    else if (estado_civil_bd.Equals(EnumEstadoCivil.Companheiro.ToString()))
                        estado_civil_ = EnumEstadoCivil.Companheiro;
                    else if (estado_civil_bd.Equals(EnumEstadoCivil.Divorciado.ToString()))
                        estado_civil_ = EnumEstadoCivil.Divorciado;
                    else if (estado_civil_bd.Equals(EnumEstadoCivil.Separado.ToString()))
                        estado_civil_ = EnumEstadoCivil.Separado;
                    else if (estado_civil_bd.Equals(EnumEstadoCivil.Viuvo.ToString()))
                        estado_civil_ = EnumEstadoCivil.Viuvo;
                    else if (estado_civil_bd.Equals(EnumEstadoCivil.Divorciado.ToString()))
                        estado_civil_ = EnumEstadoCivil.Companheiro;
                    func.Estado_civil = estado_civil_;
                    string genero = Convert.ToString("genero");
                        func.Genero_ =  genero.Equals("M")  ? EnumGenero.Masculino : EnumGenero.Feminino;
                    func.Num_BI = Convert.ToString(linha["num_bi"]);
                    func.Habilitacao_literaria = Convert.ToString(linha["habilitacao_literaria"]);

                    //DADOS DA TABELA - FUNCIONÁRIO
                    func.Id_Empresa = Convert.ToString(linha["id_empresa"]);
                    func.Especialidade = Convert.ToString(linha["id_empresa"]);
                    func.Categoria = Convert.ToString(linha["categoria"]);

                    return func;

                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro na Consulta dos Dados do Funcionário, Por favor informe ao Administrador");
            }

            return func;
        }
    }
}
