using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
    public class Prescricao_Material_BLL
    {
        AcessoDadosBLL acessodadosBLL;

        public Prescricao_Material_BLL()
        {
            acessodadosBLL = new CamadaNegocio.AcessoDadosBLL();
        }

        public bool Cadastrar_PrescricaoMaterial(Prescricao_Material prescricao_Material, Prescricao prescricao)
        {
            try
            {
               // acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
                string query = $"insert into \"Material_Prescricao_dialise\" values ({prescricao_Material.id_material.id_material},{prescricao.id_prescricao_dialise})";
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas ao Inserir os Materiais na Prescrição Nº: "+prescricao.id_prescricao_dialise );
            }
            
        }

        public void Cadastrar_PrescricaoMaterial(List<Prescricao_Material> List_prescricao_Material, Prescricao prescricao)
        {
            foreach (Prescricao_Material item in List_prescricao_Material)
            {
                Cadastrar_PrescricaoMaterial(item,prescricao);
            }
        }

        public List<Prescricao_Material> Consultar_PrescricaoMaterial(Prescricao prescricao)
        {
            try
            {
                MaterialBLL materialBLL = new MaterialBLL();
                List<Prescricao_Material> List_prescricao_Material = new List<Prescricao_Material>();
                DataTable dt = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Material_Prescricao_dialise\" WHERE id_prescri_dialise = {prescricao.id_prescricao_dialise}");
                foreach (DataRow linha in dt.Rows)
                {
                    Prescricao_Material prescricao_Material  = new Prescricao_Material();
                    prescricao_Material.id_prescricao_dialise = prescricao;
                    prescricao_Material.id_material = materialBLL.Consultar_MaterialPeloID(Convert.ToInt32(linha["id_material"]));
                    List_prescricao_Material.Add(prescricao_Material);
                }
                return List_prescricao_Material;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Buscar os Materiais referentes a Prescrição {prescricao.id_prescricao_dialise}");
            }
           
        }

        public void Actualizar_PrescricaoMaterial(Prescricao_Material prescricao_Material)
        {
            try
            {
                List<Prescricao_Material> List_prescricao_Material = new List<Prescricao_Material>(); ;
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"update \"Material_Prescricao_dialise\" set id_material = {prescricao_Material.id_material}  where id_prescri_dialise = {prescricao_Material.id_prescricao_dialise}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Actualizar os Materiais referentes a Prescrição {prescricao_Material.id_prescricao_dialise}");
            }
        }

        public void Actualizar_PrescricaoMaterial(List<Prescricao_Material> List_prescricao_Material)
        {
            try
            {
                foreach (var item in List_prescricao_Material)
                {
                    Actualizar_PrescricaoMaterial(item);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Eliminar_PrescricaoMaterial(Prescricao_Material prescricao_Material)
        {
            try
            {
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacaoSQL($"delete from \"Material_Prescricao_dialise\" where id_prescri_dialise = {prescricao_Material.id_prescricao_dialise} and id_material = {prescricao_Material.id_material}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Eliminar Material referente a Prescrição {prescricao_Material.id_prescricao_dialise}");
            }
        }

    }
}
