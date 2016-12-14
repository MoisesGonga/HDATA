namespace CamadaObjectoTransferecia
{
    public class Proveniencia
    {
        public Proveniencia (int id_proveniencia, string nome_proveniencia, string descricao)
        {
            this.Id_Proveniencia = id_proveniencia;
            this.Nome_Proveniencia = nome_proveniencia;
            this.Descricao = descricao;
        }

        public Proveniencia(string nome_proveniencia, string descricao)
        {
            this.Nome_Proveniencia = nome_proveniencia;
            this.Descricao = descricao;
        }

        public Proveniencia()
        {

        }
        public Proveniencia(int id_proveniencia)
        {
            this.Id_Proveniencia = id_proveniencia;
        }

        public int Id_Proveniencia { get; set; }
        public string Nome_Proveniencia { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return Nome_Proveniencia;
        }
    }
}