//Acre.cs
//Classe Acre
//Programmé par Jean-Philippe Croteau
//Le 25 mars 2013


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimVille
{
    [Serializable]
    class Acre
    {
        public enum TypeTerrain { Champ, Foret, Energie, Police, Hopital, Residentiel, Commercial, Stade, RiviereNordSud, RiviereSudEst, RiviereSudOuest, RiviereNordEst, RiviereNordOuest, Statistiques };

        private TypeTerrain terrain;
        private int sante;
        private int criminalite;
        private int loisirs;
        private int satisfaction;
        private int nbHabitant;

        /// <summary>
        /// constructeur de la classe Acre
        /// </summary>
        public Acre()
        {
            this.terrain = TypeTerrain.Champ;
            this.sante = 0;
            this.criminalite = 0;
            this.loisirs = 0;
            this.satisfaction = 0;
            this.nbHabitant = 0;
        }

        /// <summary>
        /// Propriété du champ terrain
        /// </summary>
        internal TypeTerrain Terrain
        {
            get { return terrain; }
            set { terrain = value; }
        }

        /// <summary>
        /// Indice de santé de l'acre précis
        /// </summary>
        public int Sante
        {
            get { return sante; }
            set { sante = value; }
        }

        /// <summary>
        /// Indice de criminalité de l'acre précis
        /// </summary>
        public int Criminalite
        {
            get { return criminalite; }
            set { criminalite = value; }
        }

        /// <summary>
        /// Indice de loisirs de l'acre précis
        /// </summary>
        public int Loisirs
        {
            get { return loisirs; }
            set { loisirs = value; }
        }

        /// <summary>
        /// Indice de satisfaction de l'acre précis
        /// </summary>
        public int Satisfaction
        {
            get { return satisfaction; }
            set { satisfaction = value; }
        }

        /// <summary>
        /// Nombre d'habitant de l'acre précis
        /// </summary>
        public int NbHabitant
        {
            get { return nbHabitant; }
            set { nbHabitant = value; }
        }
    }
}
