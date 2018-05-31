﻿using M2LCSHARP.DATA;
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
            connection = getConnection();

            using (connection) {
                connection.Open();
                string requete = "SELECT * FROM adherent LEFT JOIN club ON adherent.A_fk_club = club.C_id LEFT JOIN type_club ON club.C_fk_type = type_club.T_id ORDER BY adherent.A_id";

                MySqlCommand cmd = new MySqlCommand(requete, connection);
                using (MySqlDataReader datareader = cmd.ExecuteReader()) {
                    while (datareader.Read()) {
                        adhérents = new adherent((string)datareader["A_nom"], (string)datareader["A_prenom"], (string)datareader["A_cp"], (string)datareader["A_adresse"], (string)datareader["A_ville"]);

                        adhérents.DateNaissance = (DateTime)datareader["A_datenaiss"];
                        adhérents.Id = (int)datareader["A_id"];


                        if (datareader["id_club"] != System.DBNull.Value && datareader["numero_licence"] != System.DBNull.Value) {
                            type = new type_club(Convert.ToInt32(datareader["id_type_club"]), (string)datareader["libelle"]);
                            club = new club((string)datareader["Titre_club"], (string)datareader["url_club"], (string)datareader["Adresse_club"], (string)datareader["Code_Postal_club"], (string)datareader["Ville_club"], (string)datareader["mail_club"], Convert.ToInt32(datareader["telephone_club"]), type);
                            adhérents.numero_licence = Convert.ToInt32(datareader["numero_licence"]);
                            adhérents.Cotisation = Convert.ToInt32(datareader["cotisation_adherent"]);
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
                string requete = "INSERT INTO `adherent` (`id_adherent`, `numero_licence`, `Nom_adherent`, `Prenom_adherent`, `Date_naissance_adherent`, `Adresse_adherent`, `Code_Postal_adherent`, `Ville_adherent`, `cotisation_adherent`, `id_club`) VALUES (NULL,NULL, @nom, @prenom, @date, @Adresse, @codepostal, @ville,@cotisation, NULL);";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@nom", adhérents.Nom);
                cmd.Parameters.AddWithValue("@prenom", adhérents.Prenom);
                cmd.Parameters.AddWithValue("@date", adhérents.DateNaissance);
                cmd.Parameters.AddWithValue("@adresse", adhérents.Adresse);
                cmd.Parameters.AddWithValue("@codepostal", adhérents.CodePostal);
                cmd.Parameters.AddWithValue("@ville", adhérents.Ville);
                // cmd.Parameters.AddWithValue("@numero", adhérents.numero);
                cmd.Parameters.AddWithValue("@cotisation", adhérents.Cotisation);
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifierAdherent(adherent adhérents) {
            using (connection) {
                connection.Open();
                string requete = "UPDATE `adherent` SET `Nom_adherent` = @nom, `Prenom_adherent` = @prenom, `Date_naissance_adherent` = @date, `Adresse_adherent` = @adresse, `Code_Postal_adherent` = @codepostal, `Ville_adherent` = @ville, `numero_licence` = @numero, `cotisation_adherent`=@coti WHERE `adherent`.`id_adherent` = @id";
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
                string requete = "DELETE FROM `adherent` WHERE `adherent`.`id_adherent` = @id";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@id", adhérents.Id);
                cmd.ExecuteNonQuery();


            }
        }

        public void AjouterUnClub(club Unclub, adherent UnAdh) {
            using (connection) {
                connection.Open();
                string requete = "UPDATE `adherent` SET `id_club` = @idC WHERE `adherent`.`id_adherent`= @idA";
                MySqlCommand cmd = new MySqlCommand(requete, connection);
                cmd.Parameters.AddWithValue("@idA", UnAdh.Id);
                cmd.Parameters.AddWithValue("@idC", Unclub.id_club);
                cmd.ExecuteNonQuery();
            }
        }

    }
}