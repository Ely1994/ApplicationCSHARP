using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using System.Data.SqlClient;
using MySql.Data.MySqlClient;
namespace M2LCSHARP.BDD {
    public class connexion_BDD {
        private string connectionString;
        public MySqlConnection connection { get; set; }
        // un constructeur !
        public connexion_BDD() {
            this.ConnexionBDD();
        }
        // la connexion qui fait tout
        public void ConnexionBDD() {
            /*
            string server = "localhost";
            string port = "3306";
            string database = "appliBD";
            string uid = "root";
            string password = "";
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" 
                + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            */
            
            //connectionString = "Data Source=localhost;Initial Catalog=applibd;User ID=root;Password=";
            string connexionParams = "SERVER=localhost;PORT=3306;DATABASE=applibd;UID=root;PASSWORD=;persistsecurityinfo=True;SslMode=none";
            //this.connection = new SqlConnection(connexionParams);
            this.connection = new MySqlConnection(connexionParams);
        }

        public MySqlConnection getConnection() {
            connectionString = "Data Source=localhost;Initial Catalog=applibd;User ID=root;Password=";
            return this.connection = new MySqlConnection(connectionString);
        }
    }
}