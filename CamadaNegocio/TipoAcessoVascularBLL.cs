using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaObjectoTransferecia;
using System.Data;

namespace CamadaNegocio
{
    public class TipoAcessoVascularBLL
    {
        AcessoDadosBLL acessodadosBLL;
        public TipoAcessoVascularBLL()
        {
            acessodadosBLL = new AcessoDadosBLL();
        }

        public List<TipoAcessoVascular> ListaTipoAcessoVascular()
        {
            List<TipoAcessoVascular> listaTipoAcessoVascular = null;
            try
            {
                listaTipoAcessoVascular = new List<TipoAcessoVascular>();
                DataTable DataTableAcessoVascular = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Tipo_Acesso\"");
                foreach (DataRow linha in DataTableAcessoVascular.Rows)
                {
                    TipoAcessoVascular TipoacessoVascular = new TipoAcessoVascular();
                    TipoacessoVascular.Id_tipo_acesso = Convert.ToInt32(linha["id_tipo_acesso"]);
                    TipoacessoVascular.Nome_acesso = Convert.ToString(linha["nome_acesso"]);
                    TipoacessoVascular.Abrev_acesso = Convert.ToString(linha["abrev_acesso"]);
                    TipoacessoVascular.Descricao = Convert.ToString(linha["descricao"]);
                    listaTipoAcessoVascular.Add(TipoacessoVascular);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema na Consulta dos Tipo Acessos Vasculares...");
            }
            return listaTipoAcessoVascular;
        }

        public TipoAcessoVascular ObterAcessoVascular(int idTipoAcessoVascular)
        {
            TipoAcessoVascular TipoacessoVascular = null;
            try
            {
                DataTable DataTableAcessoVascular = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Tipo_Acesso\" where id_tipo_acesso = {idTipoAcessoVascular}");
                if ( DataTableAcessoVascular.Rows.Count > 0) {
                    DataRow linha = DataTableAcessoVascular.Rows[0];
                    TipoacessoVascular = new TipoAcessoVascular();
                    TipoacessoVascular.Id_tipo_acesso = Convert.ToInt32(linha["id_tipo_acesso"]);
                    TipoacessoVascular.Nome_acesso = Convert.ToString(linha["nome_acesso"]);
                    TipoacessoVascular.Abrev_acesso = Convert.ToString(linha["abrev_acesso"]);
                    TipoacessoVascular.Descricao = Convert.ToString(linha["descricao"]);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema ao Obter o Tipo Acesso Vascular...");
            }
            return TipoacessoVascular;
        }
    }
}
