using CamadaObjectoTransferecia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaNegocio
{
  public  class AcessoVascularBLL
    {
        AcessoDadosBLL acessodadosBLL;

        public AcessoVascularBLL()
        {
            acessodadosBLL = new AcessoDadosBLL();
        }

        public List<AcessoVascular> ListarAcessosVascular(Paciente p)
        {
            List<AcessoVascular> listaAcessoVascular = null;
            try
            {
                listaAcessoVascular = new List<AcessoVascular>();
                DataTable DataTableAcessoVascular = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"SELECT * FROM \"Acesso_vascular\" where idpessoa = {p.Id_pessoa}");
                foreach (DataRow linha in DataTableAcessoVascular.Rows)
                {
                    AcessoVascular acessoVascular = new AcessoVascular();
                    acessoVascular.ID_AcessoVascular = Convert.ToInt32(linha["id_acesso"]);
                    TipoAcessoVascularBLL tipoAcessoBLL = new TipoAcessoVascularBLL();
                    acessoVascular.tipoAcesso = tipoAcessoBLL.ObterAcessoVascular(Convert.ToInt32(linha["id_tipo_acesso"]));
                    string str_Data_av = Convert.ToString(linha["data_realizacao"]);
                    if (string.IsNullOrEmpty(str_Data_av))  {    }
                    else
                    {
                        acessoVascular.Data_Realizacao = DateTime.Parse(str_Data_av);
                    }
                    acessoVascular.Recuperacao_cirugica =  Convert.ToString(linha["recuperacao_cirugica"]);
                    acessoVascular.Director_clinico = Convert.ToString(linha["director_clinico"]);
                    acessoVascular.Clinica_hospital = Convert.ToString(linha["clinica_hospital"]);
                    acessoVascular.Complicacao_av = Convert.ToString(linha["complicacao_av"]);
                    string str_data_falencia = Convert.ToString(linha["data_falencia"]);
                    if (string.IsNullOrEmpty(str_data_falencia)) { }
                    else
                    {
                        acessoVascular.Data_falencia = DateTime.Parse(str_data_falencia);
                    }
                    acessoVascular.MotivoFalencia = Convert.ToString(linha["motivo_falencia"]);
                    acessoVascular.Local_acesso = Convert.ToString(linha["local_acesso"]);
                    acessoVascular.Cirugiao_nefrologista = Convert.ToString(linha["cirugiao_nefrologista"]);
                    PacienteBLL pBLL = new PacienteBLL();
                    acessoVascular.Paciente_ = p;
                    listaAcessoVascular.Add(acessoVascular);
                }
            }
            catch (Exception)
            {
                throw new Exception("Problema na Consulta dos Acessos Vasculares...");
            }

            return listaAcessoVascular;
        }

        private string FormatarData(DateTime data)
        {
            string data_ = "";
            data_ += data.Year + "-";

            if (data.Month + "".Length == 1)
            {
                data_ += "0" + data.Month + "-";
            }
            else
            {

                data_ += data.Month + "-";
            }

            if (data.Day + "".Length == 1)
            {
                data_ += "0" + data.Day;
            }
            else
            {
                data_ += data.Day + "";
            }

            return data_;
        }

        public int CadastrarAcessoVascular(AcessoVascular acessoVascular)
        {
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            string query = $"insert into \"Acesso_vascular\" values (default,{acessoVascular.tipoAcesso.Id_tipo_acesso}, TO_DATE('{FormatarData(acessoVascular.Data_Realizacao)}', 'YYYY-MM-DD'),'{acessoVascular.Recuperacao_cirugica}', '{acessoVascular.Director_clinico}', '{acessoVascular.Clinica_hospital}', '{acessoVascular.Complicacao_av}', TO_DATE('{FormatarData(acessoVascular.Data_falencia)}', 'YYYY-MM-DD'), '{acessoVascular.MotivoFalencia}', {acessoVascular.Paciente_.Id_pessoa}, '{acessoVascular.Local_acesso}', '{acessoVascular.Cirugiao_nefrologista}')";
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            object rt2 = acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, "select last_value as id_acesso from \"Acesso_vascular_id_acesso_seq\"");
            return Convert.ToInt32(rt2);
        }

        public int ActualizarAcessoVascular(AcessoVascular acessoVascular)
        {
            object obj = null;
            //Actualização UTILIZANDO COMANDO DE MANIPULAÇÃO SQL
            try
            {
                string query = $" update \"Acesso_vascular\" set id_tipo_acesso = {acessoVascular.tipoAcesso.Id_tipo_acesso}, data_realizacao = TO_DATE('{FormatarData(acessoVascular.Data_Realizacao)}', 'YYYY-MM-DD'), recuperacao_cirugica='{acessoVascular.Recuperacao_cirugica}', director_clinico='{acessoVascular.Director_clinico}', clinica_hospital = '{acessoVascular.Clinica_hospital}', complicacao_av = '{acessoVascular.Complicacao_av}', data_falencia = TO_DATE('{FormatarData(acessoVascular.Data_falencia)}', 'YYYY-MM-DD'), motivo_falencia = '{acessoVascular.MotivoFalencia}', local_acesso = '{acessoVascular.Local_acesso}', cirugiao_nefrologista = '{acessoVascular.Cirugiao_nefrologista}' where id_acesso = {acessoVascular.ID_AcessoVascular}";
              //  acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text,$" update \"Acesso_vascular\" set id_tipo_acesso = '{acessoVascular.tipoAcesso.Id_tipo_acesso}', data_realizacao = TO_DATE('{FormatarData(acessoVascular.Data_Realizacao)}', 'YYYY-MM-DD'), recuperacao_cirugica='{acessoVascular.Recuperacao_cirugica}', director_clinico='{acessoVascular.Director_clinico}', clinica_hospital = '{acessoVascular.Clinica_hospital}', complicacao_av = '{acessoVascular.Complicacao_av}', data_falencia = TO_DATE('{FormatarData(acessoVascular.Data_falencia)}', 'YYYY-MM-DD'), motivo_falencia = '{acessoVascular.MotivoFalencia}', local_acesso = '{acessoVascular.Local_acesso}', cirugiao_nefrologista = {acessoVascular.Cirugiao_nefrologista} where id_acesso = {acessoVascular.ID_AcessoVascular}");
                acessodadosBLL.AcessodadosPostgreSQL.ExecututarManipulacao(CommandType.Text, query);
            }
            catch (Exception ex)
            {
                throw new Exception("Problema encontrado na Actualização do Acesso Vascular...");
            }
            return acessoVascular.ID_AcessoVascular;
        }

        public AcessoVascular EliminarAcessoVascular(AcessoVascular acessoVascular)
        {
            acessodadosBLL.AcessodadosPostgreSQL.LimparParametros();
            string query = $"delete from \"Acesso_vascular\" where id_acesso = {acessoVascular.ID_AcessoVascular}";
            acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, query);
            return acessoVascular;
        }

        public List<AcessoVascular> ConsultarAcessoVascularPelaData(DateTime dataRealizacao_inicial, DateTime dataRealizacao_final, int idpaciente)
        {
            List<AcessoVascular> listaAcessoVascular = null;
            try
            {
                listaAcessoVascular = new List<AcessoVascular>();
                DataTable DataTableAcessoVascular = acessodadosBLL.AcessodadosPostgreSQL.ExecututarConsulta(CommandType.Text, $"select * from \"Acesso_vascular\" where data_realizacao between TO_DATE('{FormatarData(dataRealizacao_inicial)}', 'YYYY-MM-DD') and TO_DATE('{FormatarData(dataRealizacao_final)}', 'YYYY-MM-DD') and idpessoa = {idpaciente}");
                foreach (DataRow linha in DataTableAcessoVascular.Rows)
                {
                    AcessoVascular acessoVascular = new AcessoVascular();
                    acessoVascular.ID_AcessoVascular = Convert.ToInt32(linha["id_acesso"]);
                    TipoAcessoVascularBLL tipoAcessoBLL = new TipoAcessoVascularBLL();
                    acessoVascular.tipoAcesso = tipoAcessoBLL.ObterAcessoVascular(Convert.ToInt32(linha["id_tipo_acesso"]));
                    string str_Data_av = Convert.ToString(linha["data_realizacao"]);
                    if (string.IsNullOrEmpty(str_Data_av)) { }
                    else
                    {
                        acessoVascular.Data_Realizacao = DateTime.Parse(str_Data_av);
                    }
                    acessoVascular.Recuperacao_cirugica = Convert.ToString(linha["recuperacao_cirugica"]);
                    acessoVascular.Director_clinico = Convert.ToString(linha["director_clinico"]);
                    acessoVascular.Clinica_hospital = Convert.ToString(linha["clinica_hospital"]);
                    acessoVascular.Complicacao_av = Convert.ToString(linha["complicacao_av"]);
                    string str_data_falencia = Convert.ToString(linha["data_falencia"]);
                    if (string.IsNullOrEmpty(str_data_falencia)) { }
                    else
                    {
                        acessoVascular.Data_falencia = DateTime.Parse(str_data_falencia);
                    }
                    acessoVascular.MotivoFalencia = Convert.ToString(linha["motivo_falencia"]);
                    acessoVascular.Local_acesso = Convert.ToString(linha["local_acesso"]);
                    acessoVascular.Cirugiao_nefrologista = Convert.ToString(linha["cirugiao_nefrologista"]);
                    PacienteBLL pBLL = new PacienteBLL();
                    int idpessoa = Convert.ToInt32(linha["idpessoa"]);
                    acessoVascular.Paciente_ = pBLL.ObterPacientePeloCodigo(idpessoa);
                    listaAcessoVascular.Add(acessoVascular);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problema na Consulta dos Acessos Vasculares..."+ex);
            }

            return listaAcessoVascular;
        }
    }
}
