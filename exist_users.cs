using System;
using System.Collections.Generic;
using padvMVC.Models;
using System.Data;

namespace padvMVC.Negocio
{
    public class exist_users : Negocio
    {

        public string AlterarSistemaMedida(long user_id, long system_id)
        {

            try
            {

                conexao.LimparParametros();
                string query = " update exist_users set system_id=" + system_id + " where user_id=" + user_id + "; select 1;";
                string retorno = conexao.ExecutarQuery(CommandType.Text, query).ToString();
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string AlterarConfiguracaoMedida(long measure_config_id, long user_id)
        {

            try
            {

                conexao.LimparParametros();
                string query = "update exist_users set measure_config_id=" + measure_config_id + " where user_id=" + user_id + "; select 1;";
                string retorno = conexao.ExecutarQuery(CommandType.Text, query).ToString();
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string AlterarIdioma(long user_id, long lang_id)
        {

            try
            {

                conexao.LimparParametros();
                if(lang_id ==0 )
                {

                    lang_id = 1;
                }
                string query = " update exist_users set lang_id=" + lang_id + " where user_id=" + user_id + "; select 1;";

                string retorno = conexao.ExecutarQuery(CommandType.Text, query).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string AlterarMoeda(long currency_id, long user_id)
        {

            try
            {

                string query = "update exist_users set currency_id=" + currency_id + " where user_id=" + user_id + "; select 1;";
                string retorno = conexao.ExecutarQuery(CommandType.Text, query).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection ConsultarUsuarios(long id)
        {

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();
                exist_countries countryGetter = new exist_countries();


                string sql = "SELECT * FROM exist_users";
                if (id != 0)
                    sql = sql + " WHERE user_id=" + id;
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                usuarios = GetExist_UsersDtoCollection(tabela);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection ConsultarUsuarios()
        {

            try
            {

                string sql = "SELECT *FROM exist_users";
                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                usuarios = GetExist_UsersDtoCollection(tabela);

                return usuarios;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDto consultarUsuario(string email)
        {

            Exist_UsersDto usuario = new Exist_UsersDto();

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                string sql = "SELECT *FROM exist_users";
                if (email != null)
                    sql = sql + " WHERE user_email='" + email + "'";
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                foreach (DataRow item in tabela.Rows)
                {

                    usuario.user_email = item["user_email"].ToString();
                    usuario.user_company = item["user_company"].ToString();
                    usuario.user_name = item["user_name"].ToString();
                    usuario.user_phone = item["user_phone"].ToString();
                    exist_languages exist_languages = new exist_languages();
                    long lang_id = Convert.ToInt64(item["lang_id"]);
                    var lang_collections = exist_languages.Consultar(lang_id);
                    if (lang_collections.Count > 0)
                    {

                        usuario.lang_id = lang_collections[0];
                    }
                    if (item["user_date_expire"] != DBNull.Value)
                    {

                        usuario.user_date_expire = Convert.ToDateTime(item["user_date_expire"]);
                    }
                    if (item["user_date_register"] != DBNull.Value)
                    {

                        usuario.user_date_register = Convert.ToDateTime(item["user_date_register"]);
                    }
                    if (item["date_answer"] != DBNull.Value)
                    {

                        usuario.date_answer = Convert.ToDateTime(item["date_answer"]);
                    }
                    if (item["treatment"] != DBNull.Value)
                    {

                        usuario.treatment = item["treatment"].ToString();
                    }
                    usuario.currency_id = Convert.ToInt64(item["currency_id"]);
                    usuario.base_calc = Convert.ToInt32(item["base_calc"]);
                    usuario.specie_id = Convert.ToInt64(item["specie_id"]);
                    usuario.dose = Convert.ToInt64(item["dose"]);
                        
                    usuario.user_blocked = Convert.ToBoolean(item["user_blocked"]);
                    usuario.user_id = Convert.ToInt64(item["user_id"]);
                    usuario.user_password = item["user_password"].ToString();
                    usuario.user_country = Convert.ToInt64(item["user_country"]);
                    usuario.user_admin = Convert.ToBoolean(item["user_admin"]);
                    CountriesDto countries = new CountriesDto() { country_id = Convert.ToInt64(item["user_country"]) };

                    if (item["comments"] != DBNull.Value)
                    {

                        usuario.comments = item["comments"].ToString();
                    }
                    if (item["user_accesslevel"] != DBNull.Value)
                    {

                        usuario.user_accesslevel =Convert.ToInt32( item["user_accesslevel"]);
                    }
                    if (item["lang_id"] != DBNull.Value)
                    {

                        usuario.lang_id.lang_id = Convert.ToInt64(item["lang_id"]);
                    }
                    if (item["table_id"] != DBNull.Value)
                    {

                        usuario.table_id = Convert.ToInt64(item["table_id"]);
                    }
                    if (item["measure_config_id"] != DBNull.Value)
                    {

                        usuario.measure_config_id = Convert.ToInt64(item["measure_config_id"]);
                    }
                    if (item["system_id"] != DBNull.Value)
                    {

                        usuario.system_id = Convert.ToInt64(item["system_id"]);
                    }
                    if (item["action"] != DBNull.Value)
                    {

                        usuario._action = Convert.ToInt32(item["action"]);
                    }
                    if (item["last_name"] != DBNull.Value)
                    {

                        usuario.last_name = item["last_name"].ToString();
                    }

                    return usuario;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDto consultarUsuario(long user_id)
        {

            Exist_UsersDto usuario = new Exist_UsersDto();

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                string sql = "SELECT *FROM exist_users";
                if (user_id != 0)
                    sql = sql + " WHERE user_id='" + user_id + "'";
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                foreach (DataRow item in tabela.Rows)
                {

                    usuario.user_email = item["user_email"].ToString();
                    usuario.user_company = item["user_company"].ToString();
                    usuario.user_name = item["user_name"].ToString();
                    usuario.user_phone = item["user_phone"].ToString();
                    exist_languages exist_languages = new exist_languages();
                    long lang_id = Convert.ToInt64(item["lang_id"]);
                    var lang_collections = exist_languages.Consultar(lang_id);
                    if (lang_collections.Count > 0)
                    {

                        usuario.lang_id = lang_collections[0];
                    }
                    if (item["user_date_expire"] != DBNull.Value)
                    {

                        usuario.user_date_expire = Convert.ToDateTime(item["user_date_expire"]);
                    }
                    if (item["user_date_register"] != DBNull.Value)
                    {

                        usuario.user_date_register = Convert.ToDateTime(item["user_date_register"]);
                    }
                    if (item["date_answer"] != DBNull.Value)
                    {

                        usuario.date_answer = Convert.ToDateTime(item["date_answer"]);
                    }
                    if (item["treatment"] != DBNull.Value)
                    {

                        usuario.treatment = item["treatment"].ToString();
                    }
                    usuario.currency_id = Convert.ToInt64(item["currency_id"]);
                    usuario.base_calc = Convert.ToInt32(item["base_calc"]);
                    usuario.specie_id = Convert.ToInt64(item["specie_id"]);
                    usuario.dose = Convert.ToInt64(item["dose"]);

                    usuario.user_blocked = Convert.ToBoolean(item["user_blocked"]);
                    usuario.user_id = Convert.ToInt64(item["user_id"]);
                    usuario.user_password = item["user_password"].ToString();
                    usuario.user_country = Convert.ToInt64(item["user_country"]);
                    CountriesDto countries = new CountriesDto() { country_id = Convert.ToInt64(item["user_country"]) };
                    usuario.user_admin = Convert.ToBoolean(item["user_admin"]);
                    
                    if (item["comments"] != DBNull.Value)
                    {

                        usuario.comments = item["comments"].ToString();
                    }
                    if (item["user_accesslevel"] != DBNull.Value)
                    {

                        usuario.user_accesslevel = Convert.ToInt32(item["user_accesslevel"]);
                    }
                    if (item["lang_id"] != DBNull.Value)
                    {

                        usuario.lang_id.lang_id = Convert.ToInt64(item["lang_id"]);
                    }
                    if (item["table_id"] != DBNull.Value)
                    {

                        usuario.table_id = Convert.ToInt64(item["table_id"]);
                    }
                    if (item["measure_config_id"] != DBNull.Value)
                    {

                        usuario.measure_config_id = Convert.ToInt64(item["measure_config_id"]);
                    }
                    if (item["system_id"] != DBNull.Value)
                    {

                        usuario.system_id = Convert.ToInt64(item["system_id"]);
                    }
                    if (item["action"] != DBNull.Value)
                    {

                        usuario._action = Convert.ToInt32(item["action"]);
                    }
                    if (item["last_name"] != DBNull.Value)
                    {

                        usuario.last_name = item["last_name"].ToString();
                    }
                    return usuario;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool ExistUser(string email)
        {

            string sql = "SELECT * FROM exist_users where user_email='" + email + "'";
            Exist_UsersDto usuario = new Exist_UsersDto();
            conexao.LimparParametros();
            DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
            if (tabela.Rows.Count == 0)
            {

                return false;
            }
            else
            {

                return true;
            }
        }
        public List<string> pegarOsAdms(long region)
        {

            try
            {

                List<string> usuarios = new List<string>();

                string sql = "SELECT * FROM exist_users";
                if (region != 0)
                {

                    sql = sql + " inner join region_for_users on exist_users.user_id = region_for_users.user_id WHERE user_admin = 1  and region_id =" + region;
                }
                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                foreach (DataRow item in tabela.Rows)
                {

                    Exist_UsersDto usuario = new Exist_UsersDto();

                    usuario.user_email = item["user_email"].ToString();
                    usuarios.Add(usuario.user_email);
                }

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection ConsultarAdministradoresPorRegiao(long region_id)
        {

            try
            {

                Exist_UsersDtoCollection collection = new Exist_UsersDtoCollection();
                conexao.LimparParametros();
                string sql = "select u.* from exist_users u ";
                sql = sql + "inner join region_for_users r on u.user_id=r.user_id where r.region_id=" + region_id + "  ";

                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                collection = GetExist_UsersDtoCollection(tabela);

                return collection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Exist_UsersDtoCollection GetExist_UsersDtoCollection(DataTable tabela)
        {

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                foreach (DataRow item in tabela.Rows)
                {

                    Exist_UsersDto usuario = new Exist_UsersDto();
                    usuario.user_id = Convert.ToInt64(item["user_id"]);
                    usuario.user_email = item["user_email"].ToString();
                    usuario.user_company = item["user_company"].ToString();
                    usuario.user_name = item["user_name"].ToString();
                    usuario.user_phone = item["user_phone"].ToString();
                    usuario.dose = Convert.ToInt64(item["dose"]);
                    usuario.user_blocked = Convert.ToBoolean(item["user_blocked"]);
                    usuario.currency_id = Convert.ToInt64(item["currency_id"]);
                    if (item["user_accesslevel"] != DBNull.Value)
                    {

                        usuario.user_accesslevel = Convert.ToInt32(item["user_accesslevel"]);
                    }
                    if (item["user_date_expire"] != DBNull.Value)
                    {

                        usuario.user_date_expire = Convert.ToDateTime(item["user_date_expire"]);
                    }
                    if (item["user_date_register"] != DBNull.Value)
                    {

                        usuario.user_date_register = Convert.ToDateTime(item["user_date_register"]);
                    }
                    if (item["date_answer"] != DBNull.Value)
                    {

                        usuario.date_answer = Convert.ToDateTime(item["date_answer"]);
                    }
                    if (item["last_name"] != DBNull.Value)
                    {

                        usuario.last_name = item["last_name"].ToString();
                    }
                    if (item["treatment"] != DBNull.Value)
                    {

                        usuario.treatment = item["treatment"].ToString();
                    }
                    usuario.user_blocked = Convert.ToBoolean(item["user_blocked"]);
                    exist_countries paises = new exist_countries();
                    long user_country = Convert.ToInt64(item["user_country"]);

                    usuario.user_country = user_country;

                    var collection = paises.Consultar(user_country);
                    if (collection.Count > 0)
                    {

                        usuario.countries = collection[0];
                    }
                    
                    if (item["comments"] != DBNull.Value)
                    {

                        usuario.comments = item["comments"].ToString();
                    }
                    if (item["user_admin"] != DBNull.Value)
                    {

                        usuario.user_admin = Convert.ToBoolean(item["user_admin"]);
                    }
                    usuario.user_password = item["user_password"].ToString();

                    if (tabela.Columns.Contains("chbx"))
                    {

                        usuario.chbx = Convert.ToBoolean(item["chbx"]);
                    }
                    usuarios.Add(usuario);
                }
                return usuarios;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string AtualizaConfiguracoes(Exist_UsersDto model)
        {

            try
            {

                conexao.LimparParametros();
                string query = "update exist_users set system_id=" + model.system_id + ", base_calc=" + model.base_calc + ", dose=" + model.dose + "  where user_id=" + model.user_id + "; select 1;";

                string retorno = conexao.ExecutarQuery(CommandType.Text, query).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string AtualizarTabelaDeAnalise(long usuario_id, long tabela_id)
        {

            try
            {

                conexao.LimparParametros();
                string query = " update exist_users set table_id=" + tabela_id + " where user_id=" + usuario_id + "; select 1;";
                string retorno = conexao.ExecutarQuery(CommandType.Text, query).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection PegarOsAdms(long region)
        {
            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                string sql = "SELECT * FROM exist_users";
                if (region != 0)
                {

                    sql = sql + " inner join region_for_users on exist_users.user_id = region_for_users.user_id WHERE user_admin = 1  and region_id =" + region;
                }

                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<string> pegarOsAdmsPais(long country)
        {

            try
            {

                List<string> usuarios = new List<string>();

                string sql = "SELECT * FROM exist_users";
                if (country != 0)
                {

                    sql = sql + " inner join region_for_users on exist_users.user_id = region_for_users.user_id WHERE user_admin = 1  and country_id =" + country;
                }

                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                foreach (DataRow item in tabela.Rows)
                {

                    Exist_UsersDto usuario = new Exist_UsersDto();

                    usuario.user_email = item["user_email"].ToString();
                    usuarios.Add(usuario.user_email);
                }
                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection ConsultarAdms()
        {

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                string sql = "SELECT * FROM exist_users";
                sql = sql + " WHERE user_admin = 1";

                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                usuarios = GetExist_UsersDtoCollection(tabela);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection ConsultarTodosAdms()
        {

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                string sql = "SELECT * FROM exist_users";
                sql = sql + " WHERE user_admin >=1";
                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                usuarios = GetExist_UsersDtoCollection(tabela);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<string> pegarOsMasters()
        {

            try
            {

                string sql = "select * from exist_users where user_accesslevel =9";
                List<string> usuarios = new List<string>();
                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                foreach (DataRow item in tabela.Rows)
                {

                    Exist_UsersDto usuario = new Exist_UsersDto();

                    usuario.user_email = item["user_email"].ToString();
                    usuarios.Add(usuario.user_email);

                }
                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection PegarOsMasters()
        {

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();
                string sql = "SELECT * FROM exist_users";

                sql = sql + " WHERE user_accesslevel = 9 ";

                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                usuarios = GetExist_UsersDtoCollection(tabela);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public object UnBlockUser(long id)
        {

            Exist_UsersDto user = new Exist_UsersDto();
            try
            {

                string sql = "UPDATE exist_users SET user_blocked =0 where user_id=" + id;
                conexao.LimparParametros();
                var retorno = conexao.ExecutarQuery(CommandType.Text, sql);

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public object license(long id)
        {

            try
            {

                string expire_Data = "";
                string appData = "";

                expire_Data = GetFormatDateForDB(DateTime.Now.AddYears(1),true,true);
                appData = GetFormatDateForDB(DateTime.Now, true);

                string sql = "UPDATE exist_users SET date_answer='" + appData + "',user_date_expire = '" + expire_Data + "',user_blocked = 0 where user_id=" + id;

                conexao.LimparParametros();
                var retorno = conexao.ExecutarQuery(CommandType.Text, sql);

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public object BlockUser(long id)
        {

            try
            {

                string sql = "UPDATE exist_users SET user_blocked =1 where user_id=" + id;

                conexao.LimparParametros();
                var retorno = conexao.ExecutarQuery(CommandType.Text, sql);

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsNewUser(long id)
        {

            Exist_UsersDto usuario = new Exist_UsersDto();
            try
            {

                string sql = "SELECT * FROM exist_users";
                if (id != 0)
                {

                    sql = sql + " where user_id =" + id;
                }
                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                foreach (DataRow item in tabela.Rows)
                {

                    if (item["user_date_expire"] == DBNull.Value)
                    {

                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsNewUser(string email)
        {

            Exist_UsersDto usuario = new Exist_UsersDto();
            try
            {

                string sql = "SELECT * FROM exist_users";
                if (email != null)
                {

                    sql = sql + " where user_email ='" + email + "'";
                }
                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                if (tabela.Rows.Count == 0)
                {

                    return true;
                }
                foreach (DataRow item in tabela.Rows)
                {

                    if (item["user_date_expire"] == DBNull.Value)
                    {

                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string Incluir(Exist_UsersDto Models)
        {

            try
            {

                string senha = Models.user_password;
                DateTime now = new DateTime();
                now = DateTime.Now;
                senha = PwdEncript.criptograph.Criptografar(senha);
                conexao.LimparParametros();
                string date = "";

                date = GetFormatDateForDB(now, false, true);

                string sql = "INSERT INTO exist_users (user_name,user_password,user_company,user_country,user_email,user_phone,user_date_register,treatment) ";
                sql = sql + " VALUES ('" + Models.user_name + "','" + senha + "','" + Models.user_company + "','"
                    + Models.user_country + "','" + Models.user_email + "','" + Models.user_phone + "','" + date + "', ' " + Models.treatment + "'); select 1;";

                var retorno = conexao.ExecutarQuery(CommandType.Text, sql).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string Editar(Exist_UsersDto Models)
        {

            try
            {

                string senha = Models.user_password;
                senha = PwdEncript.criptograph.Criptografar(senha);

                string sql = "";

                sql = "update exist_users SET user_name= '" + Models.user_name + "' " +
    ", user_phone=' " + Models.user_phone+"', last_name= '" +Models.last_name +"' , user_country= '" +Models.user_country+ "' , treatment= '" + Models.treatment + "' , user_email='" + Models.user_email + "', user_password='" + senha +
  "', user_company='" + Models.user_company + "' WHERE user_id='" + Models.user_id + "'; select 1;";

                
                var retorno = conexao.ExecutarQuery(CommandType.Text, sql).ToString();
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public object AlterarDadosADM(Exist_UsersDto Models)
        {

            try
            {

                string senha = Models.user_password;
                senha = PwdEncript.criptograph.Criptografar(senha);

                string sql = "";

                sql = "update exist_users SET user_name='" + Models.user_name + "' " +
    ", user_phone=' " + Models.user_phone + "' , user_email='" + Models.user_email + "', user_password= '" + senha +
  "', user_company='" + Models.user_company + "' WHERE user_id= '" + Models.user_id + "', user_acesslevel= '" + Models.user_accesslevel + "' , treatment= '" + Models.treatment + "' , user_date_expire= '" + Models.user_date_expire +
  "' ,  user_country ='" + Models.user_country + "'";
                ;

                conexao.LimparParametros();
                var retorno = conexao.ExecutarQuery(CommandType.Text, sql);
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public object pegarEmail(string user_email)
        {

            try
            {

                string sql = "SELECT * FROM exist_users WHERE user_email='" + user_email + "'";
                var retorno = conexao.ExecutarConsulta(CommandType.Text, sql);
                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string pegarSenhaViaEmail(string user_email)
        {

            try
            {

                string sql = "SELECT * FROM exist_users WHERE user_email='" + user_email + "'";
                Exist_UsersDto usuario = new Exist_UsersDto();

                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
                foreach (DataRow item in tabela.Rows)
                {

                    usuario.user_password = item["user_password"].ToString();
                }

                return usuario.user_password;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection PegarTodosAdms()
        {

            try
            {

                Exist_UsersDtoCollection usuarios = new Exist_UsersDtoCollection();

                string sql = "SELECT *FROM exist_users WHERE user_admin=1";

                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                usuarios = GetExist_UsersDtoCollection(tabela);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void SetAction2(Exist_UsersDto Models)
        {

            try
            {

                string sql = "";

                sql = "UPDATE exist_users SET action='2' WHERE user_id='" + Models.user_id + "'";

                conexao.LimparParametros();
                conexao.ExecutarQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool ExistEmail(string email)
        {

            string sql = "SELECT * from exist_users where user_email='" + email + "'";

            DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);
            if (tabela == null || tabela.Rows == null || tabela.Rows.Count == 0)
            {

                return false;
            }
            return true;
        }
        public bool Excluir(long id)
        {

            string sql = "";

            sql = "DELETE FROM exist_users WHERE user_id=" + id + "; select 1;";

            if (id == 0)
            {

                return false;
            }
            conexao.LimparParametros();
            var retorno = conexao.ExecutarQuery(CommandType.Text, sql);

            return Convert.ToBoolean(retorno);
        }
        public string AlterarSenha(Exist_UsersDto Models)
        {

            try
            {

                string senha = Models.user_password;
                senha = PwdEncript.criptograph.Criptografar(senha);
                string sql = "";

                sql = "update exist_users SET user_password= '" + senha + "' WHERE user_id= " + Models.user_id + "; select 1;";

                conexao.LimparParametros();
                string retorno = conexao.ExecutarQuery(CommandType.Text, sql).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string AlterarEspecie(long user_id, long specie_id)
        {

            try
            {

                string sql = "";
                sql = "update exist_users SET specie_id= " + specie_id + " WHERE user_id= " + user_id + "; select 1;";

                conexao.LimparParametros();
                string retorno = conexao.ExecutarQuery(CommandType.Text, sql).ToString();

                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Exist_UsersDtoCollection Buscar(string txt)
        {

            try
            {

                string sql = "SELECT * FROM exist_users where user_name like '%" + txt + "%' ";
                Exist_UsersDtoCollection users = new Exist_UsersDtoCollection();
                
                conexao.LimparParametros();
                DataTable tabela = conexao.ExecutarConsulta(CommandType.Text, sql);

                users = GetExist_UsersDtoCollection(tabela);

                return users;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}