using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace omsoftpos.Controllers
{
    public class ClsAkses
    {
        public MySqlCommand cmd;
        public MySqlDataAdapter da;
        public MySqlConnection Conn;

        private readonly IConfiguration _configuration;

        public ClsAkses(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClsAkses()
        {
        }

        public bool OpenDB(string sqlDataSource)
        {
            try
            {
                //string sqlDataSource = _configuration.GetConnectionString("MyCon");
                Conn = new MySqlConnection(sqlDataSource);
                if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public void CloseKoneksi()
        {
            try
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open) { Conn.Close(); }
                }
            }
            catch 
            {
            }
        }

        //untuk query menampilkan data
        public string QuerySql(string sql, ref DataTable dt, string koneksi)
        {
            if (!OpenDB(koneksi))
            {
                return "Koneksi gagal";
            }

            MySqlDataReader myreader;
            try
            {
                cmd = new MySqlCommand(sql, Conn);
                //da = new MySqlDataAdapter(cmd);
                //da.Fill(dt);
                //return dt;

                using (MySqlCommand mycmd = new MySqlCommand(sql, Conn))
                {
                    myreader = mycmd.ExecuteReader();
                    dt.Load(myreader);

                    myreader.Close();
                    return "sukses";
                }
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message.ToString();
            }
            finally
            {
                CloseKoneksi();
            }
        }
    }
}
