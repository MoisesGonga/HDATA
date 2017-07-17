using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class EscalaBLL
    {
        AcessoDadosBLL acessodadosBLL;

        public EscalaBLL()
        {
            acessodadosBLL = new AcessoDadosBLL();
        }

        public List<Escala> ListarEscala()
        {
            List<Escala> listaEscala = null;
            try
            {
                listaEscala = new List<Escala>();
                DataTable DataTableEscala = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, "select * from \"Escala\" ");
                foreach (DataRow linha in DataTableEscala.Rows)
                {
                    Escala E = new Escala();
                    E.idescala  = Convert.ToInt32(linha["idescala"]);
                    E.nome_escala = Convert.ToString(linha["nome_escala"]);
                    listaEscala.Add(E);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema Encontrado ao Listar a Escala");
            }
            return listaEscala;
        }

        public Escala ObterEscalaPeloID(int idEscala)
        {
            try
            {
                DataTable DataTableEscala = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select * from \"Escala\" Where idescala = {idEscala}");
                foreach (DataRow linha in DataTableEscala.Rows)
                {
                    Escala E = new Escala();
                    E.idescala = Convert.ToInt32(linha["idescala"]);
                    E.nome_escala = Convert.ToString(linha["nome_escala"]);
                    return E;
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema Encontrado ao Obter a Escala pelo ID...");
            }
            return null;
        }
    }
}
