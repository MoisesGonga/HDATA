namespace CamadaObjectoTransferecia
{
    public class Entidade_Responsavel
    {
        public Entidade_Responsavel (int Id, string nome, string nr_term_resp)
        {
            this.Id_Entidade = Id;
            this.Nome = nome;
            this.Nr_Term_Resp = nr_term_resp;
        }
        public Entidade_Responsavel()
        {
            this.Id_Entidade = -1;
           this.Nome = "";
          this.Nr_Term_Resp = "";
        }

        public int Id_Entidade { get; set; }

        public string Nome { get; set; }

        public string Nr_Term_Resp { get; set; }
    }
}