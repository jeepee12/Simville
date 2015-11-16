// Economie.cs
// Classe Economie
// Programmé par Jean-Philippe Croteau
// Le 25 mars 2013
      

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimVille
{
    [Serializable]
    class Economie
    {
        private long montantBanque;
        private double tauxTaxe;
        private int[,] prixConstruction;
        private int revenuMoyen;
        private long revenuVille;


        /// <summary>
        /// Constructeur de la classe Economie
        /// </summary>
        public Economie()
        {
            montantBanque = 0;
            prixConstruction = new int[2, 8];
            prixConstruction[(int)Acre.TypeTerrain.Foret, (int)Acre.TypeTerrain.Energie] = 70000;
            prixConstruction[(int)Acre.TypeTerrain.Foret, (int)Acre.TypeTerrain.Police] = 45000;
            prixConstruction[(int)Acre.TypeTerrain.Foret, (int)Acre.TypeTerrain.Hopital] = 100000;
            prixConstruction[(int)Acre.TypeTerrain.Foret, (int)Acre.TypeTerrain.Residentiel] = 50000;
            prixConstruction[(int)Acre.TypeTerrain.Foret, (int)Acre.TypeTerrain.Commercial] = 80000;
            prixConstruction[(int)Acre.TypeTerrain.Foret, (int)Acre.TypeTerrain.Stade] = 90000;
            prixConstruction[(int)Acre.TypeTerrain.Champ, (int)Acre.TypeTerrain.Energie] = 60000;
            prixConstruction[(int)Acre.TypeTerrain.Champ, (int)Acre.TypeTerrain.Police] = 35000;
            prixConstruction[(int)Acre.TypeTerrain.Champ, (int)Acre.TypeTerrain.Hopital] = 90000;
            prixConstruction[(int)Acre.TypeTerrain.Champ, (int)Acre.TypeTerrain.Residentiel] = 40000;
            prixConstruction[(int)Acre.TypeTerrain.Champ, (int)Acre.TypeTerrain.Commercial] = 65000;
            prixConstruction[(int)Acre.TypeTerrain.Champ, (int)Acre.TypeTerrain.Stade] = 75000;
            tauxTaxe = 0;
            revenuMoyen = 0;
            revenuVille = 0;
        }

        /// <summary>
        /// Initialise les valeurs des champs de la classe Economie
        /// </summary>
        public void Initialiser()
        {
            montantBanque = 500000;
            tauxTaxe = 5;
        }

        /// <summary>
        /// Calcul le cout pour construire à l'endroit indiqué
        /// </summary>
        /// <param name="typeTerrain">Si le lieu est une forêt ou un champ</param>
        /// <param name="typeBatiment">La batisse qu'on veut construire</param>
        /// <returns>Retourne le cout de la construction</returns>
        public int GetCout(Acre.TypeTerrain typeTerrain, Acre.TypeTerrain typeBatiment)
        {
            int cout = 0;
            cout = prixConstruction[(int)typeTerrain, (int)typeBatiment];

            return cout;
        }

        /// <summary>
        /// Montant en banque du joueur
        /// </summary>
        public long MontantBanque
        {
            get { return montantBanque; }
            set { montantBanque = value; }
        }

        /// <summary>
        /// Taux de taxe de la ville
        /// </summary>
        public double TauxTaxe
        {
            get { return tauxTaxe; }
            set { tauxTaxe = value; }
        }

        /// <summary>
        /// Revenu moyen des habitants par acre
        /// </summary>
        public int RevenuMoyen
        {
            get { return revenuMoyen; }
            set { revenuMoyen = value; }
        }

        /// <summary>
        /// Revenu de la ville par mois
        /// </summary>
        public long RevenuVille
        {
            get { return revenuVille; }
            set { revenuVille = value; }
        }
    }
}
