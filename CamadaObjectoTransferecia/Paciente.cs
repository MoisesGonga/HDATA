
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CamadaObjectoTransferecia
{
    public class Paciente : Pessoa
    {
        public Paciente(int id_pessoa, string nome, string nome_pai, string nome_mae, string naturalidade, string nacionalidae, DateTime data_nasc, EnumEstadoCivil estado_civil, EnumGenero genero_, string num_bi, string habilitacao_literaria, Contacto contacto,Endereco endereco,
            string Identificacao_hp, DateTime Data_Entrada,DateTime Data_Saida, string Medico_Enviou, DateTime Data_Inicio_HD, string Raca, Proveniencia Proveniencia_, string nomeEntidade,string nr_term_respons) :
            base(id_pessoa,  nome,  nome_pai,  nome_mae,  naturalidade,  nacionalidae,  data_nasc,  estado_civil,  genero_,  num_bi, habilitacao_literaria, contacto,endereco)
         {
            this.Identificacao_hp = Identificacao_hp;
            this.Data_Entrada = Data_Entrada;
            this.Data_Saida = Data_Saida;
            this.Medico_Enviou = Medico_Enviou;
            this.Data_Inicio_HD = Data_Inicio_HD;
            this.Raca = Raca;
            this.Proveniencia_ = Proveniencia_;
            this.Nome_Entidade = nomeEntidade;
            this.Nr_Term_Resp = nr_term_respons;
        }

        public Paciente(string nome, string nome_pai, string nome_mae, string naturalidade, string nacionalidae, DateTime data_nasc, EnumEstadoCivil estado_civil, EnumGenero genero_, string num_bi, string habilitacao_literaria, Contacto contacto, Endereco endereco,
            string Identificacao_hp, DateTime Data_Entrada, DateTime Data_Saida, string Medico_Enviou, DateTime Data_Inicio_HD, string Raca, Proveniencia Proveniencia_, string nomeEntidade,string nr_term_respons) :
            base( nome, nome_pai, nome_mae, naturalidade, nacionalidae, data_nasc, estado_civil, genero_, num_bi, habilitacao_literaria, contacto, endereco)
        {
            this.Identificacao_hp = Identificacao_hp;
            this.Data_Entrada = Data_Entrada;
            this.Data_Saida = Data_Saida;
            this.Medico_Enviou = Medico_Enviou;
            this.Data_Inicio_HD = Data_Inicio_HD;
            this.Raca = Raca;
            this.Proveniencia_ = Proveniencia_;
            this.Nome_Entidade = nomeEntidade;
            this.Nr_Term_Resp = nr_term_respons;

        }

        public Paciente()
        {

        }
        // string Identificacao_hp,DateTime Data_Entrada, DateTime Data_Saida, string Medico_Enviou, DateTime Data_Inicio_HD, string Raca, Proveniencia Proveniencia_,Entidade_Responsavel Entidade_Responsavel_

        public string Identificacao_hp { get; set; }
        public string Nome_Entidade { get; set; }

        public string Nr_Term_Resp { get; set; }

        public DateTime Data_Entrada { get; set; }

        public DateTime Data_Saida { get; set; }

        public string Medico_Enviou { get; set; }

        public DateTime Data_Inicio_HD { get; set; }

        public string Raca { get; set; }

        public Proveniencia Proveniencia_ { get; set; }


        public List<Seriologia> Lista_Seriologia { get; set; }

        public List<Seriologia> Seriologia { get; set; }

        public EnumTipoInsuficiencia TipoInsuficiencia { get; set; }

        public override string ToString()
        {
            string insuficiencia = " ";
            if (base.Genero_ == EnumGenero.Masculino)
            {
                if (TipoInsuficiencia ==  EnumTipoInsuficiencia.Aguda)
                {
                    insuficiencia = "Agudo";
                }
                else
                {
                    insuficiencia = "Cronico";
                }
            }
            else
            {
                insuficiencia = TipoInsuficiencia.ToString();
            }
            
            return base.ToString() + " - " + insuficiencia+" "+ Data_Entrada.ToString("d") ;
            
        }
    }
   
}