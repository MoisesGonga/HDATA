using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CamadaObjectoTransferecia
{
    public class Funcionario : Pessoa
    {
        public Funcionario(int id_pessoa, string nome, string nome_pai, string nome_mae, string naturalidade, string nacionalidae, DateTime data_nasc, EnumEstadoCivil estado_civil, EnumGenero genero_, string num_bi, string habilitacao_literaria,Contacto contacto,Endereco endereco, string IdEmpresa, string especialidade,string categoria) : base(id_pessoa, nome, nome_pai, nome_mae, naturalidade, nacionalidae, data_nasc, estado_civil, genero_, num_bi, habilitacao_literaria,contacto,endereco)
        {
            this.Id_Empresa = IdEmpresa;
            this.Especialidade = especialidade;
            this.Categoria = categoria;
        }

        public Funcionario()
        {

        }

        public string Id_Empresa { get; set; }
        public string Especialidade { get; set; }
        public string Categoria { get; set; }


    }
}