﻿using M2LCSHARP.DATA;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2LCSHARP.BDD {
    public class BDD_Clubs : connexion_BDD {
        public List<club> ReadClub() {


            List<club> liste = new List<club>();

            club club;

            type_club typec;


            using (connection) {
                connection.Open();
                string requete = "SELECT `C_id`,`C_nom`,`C_url`,`C_adresse`,`C_codepostal`,`C_ville`,`C_email`,`C_tel`,'T_libelle',type_club.T_id FROM club INNER JOIN type_club ON club.C_fk_type = type_club.T_id ORDER BY `C_id`";


                MySqlCommand cmd = new MySqlCommand(requete, connection);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        typec = new type_club(Convert.ToInt32(datareader["T_id"]), (string)datareader["T_libelle"]);
                        club = new club((string)datareader["C_nom"], (string)datareader["C_url"], (string)datareader["C_adresse"], (string)datareader["C_codepostal"], (string)datareader["C_ville"], (string)datareader["C_email"], Convert.ToInt32(datareader["C_tel"]), typec);
                        //Convert.ToInt32
                        club.id_club = Convert.ToInt32(datareader["C_id"]);

                        //typec.libelle = (string)datareader["libelle"];
                        //club.type.id_type = (int)datareader["id_type_club"];
                        liste.Add(club);





                    }

                }
            }
            return liste;
        }

        public int Nombredadh(club club) {
            int Nbr = 0;
            using (connection) {
                connection.Open();
                string requete = "SELECT count(A_id) AS Nbr FROM adherent WHERE adherent.A_id=@id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@id", club.id_club);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        Nbr = Convert.ToInt32(datareader["Nbr"]);
                    }
                }
            }

            return Nbr;
        }


        public List<type_club> Liste_Type() {
            List<type_club> Laliste = new List<type_club>();
            type_club type;
            using (connection) {
                connection.Open();
                string requete = "SELECT * FROM type_club";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        type = new type_club(Convert.ToInt32(datareader["T_id"]), (string)datareader["T_libelle"]);
                        Laliste.Add(type);

                    }
                }


            }
            return Laliste;

        }
        public type_club RecupType(string lib) {
            type_club typeclu;
            List<type_club> Liste = new List<type_club>();
            using (connection) {
                connection.Open();
                string requete = "SELECT * FROM type_club WHERE T_libelle=@libelle";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@libelle", lib);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        typeclu = new type_club(Convert.ToInt32(datareader["T_id"]), (string)datareader["T_libelle"]);
                        Liste.Add(typeclu);
                    }

                }

            }
            return Liste[0];

        }
        public void ajouterClub(club UnClub) {
            using (connection) {
                connection.Open();
                string requete = "INSERT INTO `club` (`C_id`, `C_nom`, `C_url`, `C_adresse`, `C_codepostal`, `C_ville`, `C_email`, `C_tel`, `C_fk_type`) VALUES(NULL, @titre,@url,@adresse,@cp,@ville,@mail,@tel,@type)";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@titre", UnClub.Titre_club);
                cmd.Parameters.AddWithValue("@url", UnClub.url_club);
                cmd.Parameters.AddWithValue("@adresse", UnClub.Adresse_club);
                cmd.Parameters.AddWithValue("@cp", UnClub.Code_Postal);
                cmd.Parameters.AddWithValue("@ville", UnClub.Ville);
                cmd.Parameters.AddWithValue("@mail", UnClub.mail_club);

                cmd.Parameters.AddWithValue("@tel", UnClub.telephone_club);
                cmd.Parameters.AddWithValue("@type", UnClub.type.id_type);
                cmd.ExecuteNonQuery();
            }
        }

        public void modifier_club(club UnClub) {
            using (connection) {
                connection.Open();
                string requete = "UPDATE `club` SET `C_nom` = @titre, `C_url` = @url, `C_adresse` = @adresse, `C_codepostal` = @cp, `C_ville` = @ville, `C_email` = @mail, `C_tel` = @tel WHERE `club`.`C_id` = @idc";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@titre", UnClub.Titre_club);
                cmd.Parameters.AddWithValue("@url", UnClub.url_club);
                cmd.Parameters.AddWithValue("@adresse", UnClub.Adresse_club);
                cmd.Parameters.AddWithValue("@cp", UnClub.Code_Postal);
                cmd.Parameters.AddWithValue("@ville", UnClub.Ville);
                cmd.Parameters.AddWithValue("@mail", UnClub.mail_club);

                cmd.Parameters.AddWithValue("@tel", UnClub.telephone_club);
                cmd.Parameters.AddWithValue("@idc", UnClub.id_club);
                cmd.ExecuteNonQuery();

            }
        }
    }
}