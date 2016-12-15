
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamadaObjectoTransferecia
{
    public abstract class Pessoa
    {
        public Pessoa()
        {
        }

        public Pessoa(int id_pessoa, string nome, string nome_pai, string nome_mae, string naturalidade, string nacionalidae, DateTime data_nasc, EnumEstadoCivil estado_civil, EnumGenero genero_, string num_bi, string habilitacao_literaria,Contacto contacto, Endereco endereco)
        {
            this.Id_pessoa = id_pessoa;
            this.Nome = nome;
            this.Nome_pai = nome_pai;
            this.Nome_mae = nome_mae;
            this.Naturalidade = naturalidade;
            this.Nacionalidade = nacionalidae;
            this.Data_nasc = data_nasc;
            this.Estado_civil = estado_civil;
            this.Genero_ = genero_;
            this.Num_BI = num_bi;
            this.Habilitacao_literaria = habilitacao_literaria;
            this.Contacto_ = contacto;
            this.Endereco_ = endereco;
        }

        public Pessoa( string nome, string nome_pai, string nome_mae, string naturalidade, string nacionalidae, DateTime data_nasc, EnumEstadoCivil estado_civil, EnumGenero genero_, string num_bi, string habilitacao_literaria, Contacto contacto, Endereco endereco)
        {
            this.Nome = nome;
            this.Nome_pai = nome_pai;
            this.Nome_mae = nome_mae;
            this.Naturalidade = naturalidade;
            this.Nacionalidade = nacionalidae;
            this.Data_nasc = data_nasc;
            this.Estado_civil = estado_civil;
            this.Genero_ = genero_;
            this.Num_BI = num_bi;
            this.Habilitacao_literaria = habilitacao_literaria;
            this.Contacto_ = contacto;
            this.Endereco_ = endereco;
        }

        public int Id_pessoa {get; set; }

        public string Nome { get; set; }

        public string Nome_pai { get; set; }

        public string Nome_mae { get; set; }

        public string Naturalidade { get; set; }

        public string Nacionalidade { get; set; }

        public DateTime Data_nasc { get; set; }

        public EnumEstadoCivil Estado_civil { get; set; }
        
        public EnumGenero Genero_ { get; set; }

        public string Num_BI { get; set; }

        public string Habilitacao_literaria { get; set; }

        public Contacto Contacto_ { get; set; }

        public Endereco Endereco_ { get; set; }

        public int CalcularIdade()
        {
            return DateTime.Now.Date.Year - Data_nasc.Date.Year;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}