using M2LCSHARP.DATA;
using M2LCSHARP.DATA_METHODES;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2LCSHARP.BDD {

    public class BDD_Adhérents : connexion_BDD {
        BDD_Clubs Club = new BDD_Clubs();
        public List<club> ClubAdh() {

            List<club> Listeclub = Club.ReadClub();
            return Listeclub;
        }

        public List<adherent> Readadherent() {
            List<adherent> liste = new List<adherent>();

            adherent adhérents;
            club club;
            type_club type;
            //connexion_BDD connect = new connexion_BDD();
            //connection = getConnection();

            using (connection) {
                connection.Open();
                string requete = "SELECT * FROM adherent LEFT JOIN club ON adherent.A_fk_club = club.C_id LEFT JOIN type_club ON club.C_fk_type = type_club.T_id ORDER BY adherent.A_id";

                MySqlCommand cmd = new MySqlCommand(requete, connection);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        adhérents = new adherent((string)datareader["A_nom"], (string)datareader["A_prenom"], (string)datareader["A_cp"], (string)datareader["A_adresse"], (string)datareader["A_ville"]);
                        adhérents.DateNaissance = (DateTime)datareader["A_datenaiss"];
                        adhérents.Id = (int)datareader["A_id"];

                        if (datareader["A_fk_club"] != System.DBNull.Value && datareader["A_licence"] != System.DBNull.Value) {
                            type = new type_club(Convert.ToInt32(datareader["T_id"]), (string)datareader["T_libelle"]);
                            club = new club((string)datareader["C_nom"], (string)datareader["C_url"], (string)datareader["C_adresse"], (string)datareader["C_codepostal"], (string)datareader["C_ville"], (string)datareader["C_email"], Convert.ToInt32(datareader["C_tel"]), type);
                            adhérents.numero_licence = Convert.ToInt32(datareader["A_licence"]);
                            adhérents.Cotisation = Convert.ToInt32(datareader["A_cotisation"]);
                            adhérents.club = club;
                        }
                        liste.Add(adhérents);

                    }
                }
            }
            return liste;
        }
        public void ajouterAdherent(adherent adhérents) {
            using (connection) {
                connection.Open();
                string requete = "INSERT INTO `adherent` ('A_id', `A_licence`, `A_nom`, `A_prenom`, `A_datenaiss`, A_adresse`, `A_cp`, `A_ville`, `A_cotisation`, A_fk_evenement, A_fk_club) VALUES (NULL,@nom, @prenom, @sexe, @date, @Adresse, @codepostal, @ville,@cotisation, NULL, NULL);";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@nom", adhérents.Nom);
                cmd.Parameters.AddWithValue("@prenom", adhérents.Prenom);
                cmd.Parameters.AddWithValue("@date", adhérents.DateNaissance);
                cmd.Parameters.AddWithValue("@adresse", adhérents.Adresse);
                cmd.Parameters.AddWithValue("@codepostal", adhérents.CodePostal);
                cmd.Parameters.AddWithValue("@ville", adhérents.Ville);
                cmd.Parameters.AddWithValue("@cotisation", adhérents.Cotisation);
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifierAdherent(adherent adhérents) {
            using (connection) {
                connection.Open();
                string requete = "UPDATE `adherent` SET `A_nom` = @nom, `A_prenom` = @prenom, `A_datenaiss` = @date, `A_adresse` = @adresse, `A_cp` = @codepostal, `A_ville` = @ville, `A_licence` = @numero, `A_cotisation`=@coti WHERE `adherent`.`A_id` = @id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@nom", adhérents.Nom);
                cmd.Parameters.AddWithValue("@prenom", adhérents.Prenom);
                cmd.Parameters.AddWithValue("@date", adhérents.DateNaissance);
                cmd.Parameters.AddWithValue("@adresse", adhérents.Adresse);
                cmd.Parameters.AddWithValue("@codepostal", adhérents.CodePostal);
                cmd.Parameters.AddWithValue("@ville", adhérents.Ville);
                cmd.Parameters.AddWithValue("@id", adhérents.Id);
                cmd.Parameters.AddWithValue("@numero", adhérents.numero_licence);
                cmd.Parameters.AddWithValue("@coti", adhérents.Cotisation);

                cmd.ExecuteNonQuery();
            }
        }

        public void SupprimerAdherent(adherent adhérents) {
            using (connection) {
                connection.Open();
                string requete = "DELETE FROM `adherent` WHERE `adherent`.`A_id` = @id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@id", adhérents.Id);
                cmd.ExecuteNonQuery();


            }
        }

        public void AjouterUnClub(club Unclub, adherent UnAdh) {
            using (connection) {
                connection.Open();
                string requete = "UPDATE `adherent` SET `C_id` = @idC WHERE `adherent`.`A_id`= @idA";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@idA", UnAdh.Id);
                cmd.Parameters.AddWithValue("@idC", Unclub.id_club);
                cmd.ExecuteNonQuery();
            }
        }

    }
}