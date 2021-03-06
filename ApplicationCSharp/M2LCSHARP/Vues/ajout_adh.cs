﻿using M2LCSHARP.BDD;
using M2LCSHARP.DATA;
using M2LCSHARP.DATA_METHODES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2LCSHARP.Vues {
    public partial class ajout_adh : Form {
        public gestion_Adherents GesAdh;
        public gestion_Clubs GesClub;
        BDD_Adhérents bADH = new BDD_Adhérents();


        public ajout_adh(gestion_Adherents gAdhe, gestion_Clubs gClub) {
            InitializeComponent();
            GesAdh = gAdhe;
            GesClub = gClub;
        }

        private void btn_Adh_Valid_Click(object sender, EventArgs e) {
            Random coti = new Random();
            string nom = txt_Nom_Adh.Text;
            string prenom = txt_Prenom_Adh.Text;
            try {
                DateTime naissance = DateTime.Parse(txt_naissance_adh.Text);
                string Cp = txt_Cp_Adh.Text;
                string Ville = txt_Ville_Adh.Text;
                string Adresse = txt_Adr_Adh.Text;

                if (nom.Length != 0 && prenom.Length != 0 && naissance.ToString().Length != 0 && Cp.Length != 0 && Ville.Length != 0 && Adresse.Length != 0) {
                    adherent adhajouté = new adherent(nom, prenom, Cp, Adresse, Ville);
                    adhajouté.DateNaissance = naissance;

                    GesAdh.ajouter_Adherent(adhajouté);

                    bADH.ajouterAdherent(adhajouté);

                    MessageBox.Show("L'adhérent a bien été ajouté", "ajout réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else {
                    MessageBox.Show("Veuillez remplir tous les champs !", "champ(s) manquant(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch { MessageBox.Show("Veuillez entrer une date valide !", "Date", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


        }
        public static void Vidertext(Control parent) {
            foreach (Control control in parent.Controls) {
                if (control.GetType() == typeof(MaskedTextBox)) {
                    ((MaskedTextBox)(control)).Text = string.Empty;
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e) {

            txt_naissance_adh.Text = monthCalendar1.SelectionStart.ToShortDateString();

        }

        private void fermeraj_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void ajout_adh_Load(object sender, EventArgs e) {

        }

        private void txt_naissance_adh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) {

        }
    }
}