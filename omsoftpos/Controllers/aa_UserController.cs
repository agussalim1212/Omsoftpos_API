using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace omsoftpos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class aa_UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        ClsAkses cla = new ClsAkses();

        public aa_UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get(string cari = "")
        {
            cari = cari.ToString().Trim() != "" ? "where mu_userid = '" + cari.Trim() + "' order by mu_user asc " : "  order by mu_user asc ";
            string sql = @"select * from aa_user " + cari;

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyCon");
            try
            {
              string res =  cla.QuerySql(sql, ref dt, sqlDataSource);
                if (res == "sukses")
                {
                    return new JsonResult(dt);
                }
                else
                {
                    return new JsonResult(res);
                }
            }
            catch
            {
                return new JsonResult("load data gagal");
            }
        }

        [HttpPost]
        public JsonResult Post(Models.aa_user _user)
        {
            string sql = @"insert into aa_user (mu_userid, mu_email, mu_nama, mu_user, mu_password, mu_tambah_id, mu_tgl_tambah
                          ) values (@mu_userid, @mu_email, @mu_nama, @mu_user, @mu_password, @mu_tambah_id, @mu_tgl_tambah) ";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyCon");
            //MySqlDataReader myreader;

            try
            {
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mycmd = new MySqlCommand(sql, myconn))
                    {
                        mycmd.Parameters.AddWithValue("@mu_userid", "USR" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                        mycmd.Parameters.AddWithValue("@mu_email", _user.mu_email);
                        mycmd.Parameters.AddWithValue("@mu_nama", _user.mu_nama);
                        mycmd.Parameters.AddWithValue("@mu_user", _user.mu_user);
                        mycmd.Parameters.AddWithValue("@mu_password", _user.mu_password);
                        mycmd.Parameters.AddWithValue("@mu_tambah_id", _user.mu_tambah_id);
                        mycmd.Parameters.AddWithValue("@mu_tgl_tambah", _user.mu_tgl_tambah);
                        mycmd.ExecuteNonQuery();
                    }
                }

                return new JsonResult("sukses input data");
            } catch (Exception ex)
            {
                return new JsonResult("Gagal input data : " + ex.Message.ToString());
            }
        }

        [HttpPut]
        public JsonResult Put(Models.aa_user _user)
        {
            string sql = @"update aa_user set mu_email = @mu_email,
                                              mu_nama = @mu_nama,
                                              mu_user = @mu_user,
                                              mu_password = @mu_password,
                                              mu_ubah_id = @mu_ubah_id,
                                              mu_tgl_ubah = @mu_tgl_ubah
                                              where mu_userid = @mu_userid ";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyCon");
            //MySqlDataReader myreader;

            try
            {
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mycmd = new MySqlCommand(sql, myconn))
                    {
                        mycmd.Parameters.AddWithValue("@mu_userid", _user.mu_userid);
                        mycmd.Parameters.AddWithValue("@mu_email", _user.mu_email);
                        mycmd.Parameters.AddWithValue("@mu_nama", _user.mu_nama);
                        mycmd.Parameters.AddWithValue("@mu_user", _user.mu_user);
                        mycmd.Parameters.AddWithValue("@mu_password", _user.mu_password);
                        mycmd.Parameters.AddWithValue("@mu_ubah_id", _user.mu_ubah_id);
                        mycmd.Parameters.AddWithValue("@mu_tgl_ubah", _user.mu_tgl_ubah);
                        mycmd.ExecuteNonQuery();
                    }
                }

                return new JsonResult("sukses update data");
            }
            catch (Exception ex)
            {
                return new JsonResult("Gagal update data : " + ex.Message.ToString());
            }
        }

        [HttpDelete]
        public JsonResult PutDelete(Models.aa_user _user)
        {
            string sql = @"update aa_user set mu_recordid = @mu_recordid,
                                              mu_ubah_id = @mu_ubah_id,
                                              mu_tgl_ubah = @mu_tgl_ubah
                                              where mu_userid = @mu_userid ";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyCon");
            //MySqlDataReader myreader;

            try
            {
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mycmd = new MySqlCommand(sql, myconn))
                    {
                        mycmd.Parameters.AddWithValue("@mu_userid", _user.mu_userid);
                        mycmd.Parameters.AddWithValue("@mu_recordid", _user.mu_recordid);
                        mycmd.Parameters.AddWithValue("@mu_ubah_id", _user.mu_ubah_id);
                        mycmd.Parameters.AddWithValue("@mu_tgl_ubah", _user.mu_tgl_ubah);
                        mycmd.ExecuteNonQuery();
                    }
                }

                return new JsonResult("sukses update data");
            }
            catch (Exception ex)
            {
                return new JsonResult("Gagal update data : " + ex.Message.ToString());
            }
        }
    }
}
