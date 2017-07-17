namespace CamadaObjectoTransferecia
{
    public class TipoAcessoVascular
    {
        public TipoAcessoVascular(int Id_tipo_acesso_)
        {
            this.Id_tipo_acesso  = Id_tipo_acesso_;
        }

        public TipoAcessoVascular()
        {
        }

        public int Id_tipo_acesso { get; set; }
        public string Nome_acesso { get; set; }
        public string Abrev_acesso { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return $"{this.Nome_acesso} - {this.Abrev_acesso} ";
        }
    }
}