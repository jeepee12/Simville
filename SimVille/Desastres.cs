// Desastre.cs
// Classe désastre
// Programmé par Jean-Philippe Croteau
// Le 23 avril 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimVille
{
    [Serializable]
    class Desastres
    {
        public enum TypeDesastre
        {
            Verglas,
            ReformeSante,
            TremblementTerre,
            Manifestation,
            CriseEconnomique,
            Demenagement
        }
        private int taille;
        private Acre.TypeTerrain batimentAffecte;

        /// <summary>
        /// constructeur de la classe Desastres
        /// </summary>
        /// <param name="taille">nombre de cases de côté</param>
        /// <param name="batimentAffecte">Le type de batiment qui sont détruit par la catastrophe</param>
        public Desastres(int taille, Acre.TypeTerrain batimentAffecte)
        {
            this.batimentAffecte = batimentAffecte;
            this.taille = taille;
        }

        /// <summary>
        /// La  longueur d'un côté
        /// </summary>
        public int Taille
        {
            get { return taille; }
            set { taille = value; }
        }

        /// <summary>
        /// Le type de batiment qui sont détruit
        /// </summary>
        internal Acre.TypeTerrain BatimentAffecte
        {
            get { return batimentAffecte; }
            set { batimentAffecte = value; }
        }
    }
}
