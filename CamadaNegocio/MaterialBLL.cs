using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class MaterialBLL
    {
        BaseAcessoDadosBLL acessodadosBLL;

        public MaterialBLL()
        {
            acessodadosBLL = new BaseAcessoDadosBLL();
        }

        public List<Material> ListarMateriais()
        {
            List<Material> Listamateriais = null;
            try
            {
                Listamateriais = new List<Material>();
                DataTable DataTableMateriais = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Material\" ");
                foreach (DataRow linha in DataTableMateriais.Rows)
                {
                    Material material = new Material();
                    material.id_material = Convert.ToInt32(linha["id_material"]);
                    material.nome_material = Convert.ToString(linha["nome_material"]);
                    material.tipo_material = Convert.ToString(linha["tipo_material"]);
                    material.descricao = Convert.ToString(linha["descricao"]);
                    Listamateriais.Add(material);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema na Consulta dos Materiais...");
            }

            return Listamateriais;
        }

        public int CadastrarMaterial(Material material)
        {  
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            string query = $"insert into \"Materias\" values (default,'{material.nome_material}', '{material.descricao}','{material.tipo_material}')";
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            object rt2 = acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, "select last_value as id_material from \"Material_id_material_seq\"");
            return Convert.ToInt32(rt2);
        }
        
        public List<Material> ConsultarMaterialPeloNome(string nome)
        {
            List<Material> listaMaterial = null;
            try
            {
                listaMaterial = new List<Material>();
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Material\" WHERE nome_material ILIKE '%{nome}%'");
                foreach (DataRow linha in dt.Rows)
                {
                    Material material = new Material();
                    material.id_material = Convert.ToInt32(linha["id_material"]);
                    material.nome_material = Convert.ToString(linha["nome_material"]);
                    material.tipo_material  = Convert.ToString(linha["tipo_material"]);
                    material.descricao = Convert.ToString(linha["descricao"]);
                    listaMaterial.Add(material);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar o Material pelo nome: ");
            }
            return listaMaterial;
        }

        public Material Consultar_MaterialPeloID(int id_Material)
        {
            try
            {
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Material\" WHERE id_material = {id_Material}");
                foreach (DataRow linha in dt.Rows)
                {
                    Material material = new Material();
                    material.id_material = Convert.ToInt32(linha["id_material"]);
                    material.nome_material = Convert.ToString(linha["nome_material"]);
                    material.tipo_material = Convert.ToString(linha["tipo_material"]);
                    material.descricao = Convert.ToString(linha["descricao"]);
                    return material;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar o Material pelo ID !!!");
            }
            return null;
        }

    }
}
