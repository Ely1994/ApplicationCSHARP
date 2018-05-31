using M2LCSHARP.DATA;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2LCSHARP.BDD {
    public class BDD_evenement : connexion_BDD {
        /// <summary>
        /// Cette fonction permet de récuperer une liste d'évenement dans la BDD en l'associant à des instances de la classe : evenement
        /// </summary>
        /// <returns>Liste d'événements</returns>
        public List<evenement> ReadEvent() {
            List<evenement> Liste = new List<evenement>();
            club club;
            type_club typec;
            evenement evenement;
            using (connection) {
                connection.Open();
                string requete = "SELECT * FROM evenement JOIN club ON evenement.E_fk_club = club.C_id JOIN type_club ON club.C_fk_type = type_club.T_id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        typec = new type_club(Convert.ToInt32(datareader["T_id"]), (string)datareader["T_libelle"]);
                        club = new club((string)datareader["C_id"], (string)datareader["C_url"], (string)datareader["C_adresse"], (string)datareader["C_codepostal"], (string)datareader["C_ville"], (string)datareader["C_email"], Convert.ToInt32(datareader["C_tel"]), typec);
                        club.id_club = Convert.ToInt32(datareader["C_id"]);
                        evenement = new evenement((string)datareader["E_nom"], (DateTime)datareader["E_datedeb"], (DateTime)datareader["E_datefin"], club);
                        evenement.id_evenement = Convert.ToInt32(datareader["E_id"]);
                        Liste.Add(evenement);
                    }
                }

            }
            return Liste;
        }

        /// <summary>
        /// Cette fonction permet d'ajouter un evenement passé en paramètre dans la base de données
        /// </summary>
        /// <param evenement="evenement"></param>
        public void Ajouter_Evenement(evenement evenement) {
            using (connection) {
                connection.Open();
                string requete = "INSERT INTO `evenement` (`E_id`, `E_nom`, `E_datedeb`, `E_datefin`, `E_fk_club`) VALUES (NULL,@titre,@debut,@fin,@id_club)";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@titre", evenement.Titre_evenement);
                cmd.Parameters.AddWithValue("@debut", evenement.Debut_evenement);
                cmd.Parameters.AddWithValue("@fin", evenement.Fin_evenement);
                cmd.Parameters.AddWithValue("@id_club", evenement.Club.id_club);
                cmd.ExecuteNonQuery();

            }
        }
        public void Modifier_Evenement(evenement evenement) {
            using (connection) {
                connection.Open();
                string requete = "UPDATE `evenement` SET `E_nom` = @titre, `E_datedeb` = @debut, `E_datefin` = @fin WHERE `evenement`.`E_id` =@id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@titre", evenement.Titre_evenement);
                cmd.Parameters.AddWithValue("@debut", evenement.Debut_evenement);
                cmd.Parameters.AddWithValue("@fin", evenement.Fin_evenement);
                cmd.Parameters.AddWithValue("@id", evenement.id_evenement);
                cmd.ExecuteNonQuery();

            }
        }
        public void Supprimer_Evenement(evenement evenement) {
            using (connection) {
                connection.Open();
                string requete = "DELETE FROM `evenement` WHERE `evenement`.`E_id` =@id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@id", evenement.id_evenement);
                cmd.ExecuteNonQuery();
            }
        }
    }
}