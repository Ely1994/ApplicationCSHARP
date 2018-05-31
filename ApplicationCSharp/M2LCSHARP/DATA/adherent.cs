﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2LCSHARP.DATA {
    public class adherent {
        public adherent(string nom, string prenom, string sexe, string codePostal, string adresse, string ville) {


            Nom = nom;
            Prenom = prenom;
            Sexe = sexe;
            CodePostal = codePostal;
            Adresse = adresse;
            Ville = ville;
        }

        public int Id { get; set; }
        public int numero_licence { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string CodePostal { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public DateTime DateNaissance { get; set; }
        public int Cotisation { get; set; }
        public club club { get; set; }

        public void AdhAjoutClub(club club) {
            this.club = club;
        }
    }



}