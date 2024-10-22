using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DİŞÇİM
{
    class ConnectionString
    {
        public  SqlConnection GetCon()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=SERHATDEMIR\SQLEXPRESS;Initial Catalog=Dent;Integrated Security=True;Connect Timeout=30";
            return baglanti;
        }
    }
}
