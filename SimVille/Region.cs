// Region.cs
// Classe Region
// Programmé par Jean-Philippe Croteau
// Le 25mars 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimVille
{
    [Serializable]
    class Region
    {
        private Acre[,] carte;
        private Economie economieJeu;
        private Random aleatoire;
        private DateTime date;
        private long nombreHabitant;
        private int habitantAcre;
        private Desastres[] desastreNaturel;
        private int probaliterDesastre;
        /// <summary>
        /// Constructeur de la class Region
        /// </summary>
        /// <param name="nombreLignes">Nombre de lignes du tableau de la carte</param>
        /// <param name="nombreColonnes">Nombre de colonnes du tableau de la carte</param>
        public Region(int nombreLignes, int nombreColonnes)
        {
            carte = new Acre[nombreColonnes, nombreLignes];
            aleatoire = new Random();
            for (int i = 0; i < nombreColonnes; i++)
            {
                for (int j = 0; j < nombreLignes; j++)
                {
                    carte[i, j] = new Acre();
                }
            }
            economieJeu = new Economie();
            date = new DateTime();
            nombreHabitant = 0;
            habitantAcre = 0;
            desastreNaturel = new Desastres[6];
            desastreNaturel[(int)Desastres.TypeDesastre.Verglas] = new Desastres(5, Acre.TypeTerrain.Energie);
            desastreNaturel[(int)Desastres.TypeDesastre.ReformeSante] = new Desastres(3, Acre.TypeTerrain.Hopital);
            desastreNaturel[(int)Desastres.TypeDesastre.TremblementTerre] = new Desastres(6, Acre.TypeTerrain.Residentiel);
            desastreNaturel[(int)Desastres.TypeDesastre.Manifestation] = new Desastres(2, Acre.TypeTerrain.Police);
            desastreNaturel[(int)Desastres.TypeDesastre.CriseEconnomique] = new Desastres(4, Acre.TypeTerrain.Commercial);
            desastreNaturel[(int)Desastres.TypeDesastre.Demenagement] = new Desastres(5, Acre.TypeTerrain.Stade);
            probaliterDesastre = 70;
        }

        /// <summary>
        /// Initialise la carte de jeu(remet au point de départ)
        /// </summary>
        public void Initialiser()
        {
            for (int i = 0; i < carte.GetLength(0); i++)
            {
                for (int j = 0; j < carte.GetLength(1); j++)
                {
                    carte[i, j].Terrain = Acre.TypeTerrain.Champ;
                }
            }

            economieJeu.Initialiser();
            InitialierForets();
            InitialierRiviere();
            date = new DateTime(2013, 11, 1);
        }

        /// <summary>
        /// Initialise(place) les forêts
        /// </summary>
        private void InitialierForets()
        {
            int nombreForet = aleatoire.Next(1, 4 + 1);
            for (int i = 0; i < nombreForet; i++)
            {
                int nbAcreX = 0;
                int nbAcreY = 0;
                do
                {
                    nbAcreX = aleatoire.Next(1, 4 + 1);
                    nbAcreY = aleatoire.Next(1, 4 + 1);
                } while (nbAcreX * nbAcreY < 4);

                int positionX = aleatoire.Next(carte.GetLength(0) - nbAcreX + 1);
                int positionY = aleatoire.Next(carte.GetLength(1) - nbAcreY + 1);


                for (int j = positionX; j < positionX + nbAcreX; j++)
                {
                    for (int k = positionY; k < positionY + nbAcreY; k++)
                    {
                        carte[j, k].Terrain = Acre.TypeTerrain.Foret;
                    }

                }

            }
        }

        /// <summary>
        /// Initialise(place) la rivières
        /// </summary>
        private void InitialierRiviere()
        {
            int positionX = aleatoire.Next(carte.GetLength(0));

            for (int i = 0; i < carte.GetLength(1); i++)
            {
                int direction = aleatoire.Next(-1, 1 + 1);
                if (direction == -1)
                {
                    if (positionX >= 0 && positionX < carte.GetLength(0))
                    {
                        carte[positionX, i].Terrain = Acre.TypeTerrain.RiviereNordOuest;
                    }
                    positionX -= 1;
                    if (positionX >= 0 && positionX < carte.GetLength(0))
                    {
                        carte[positionX, i].Terrain = Acre.TypeTerrain.RiviereSudEst;
                    }
                }
                else if (direction == 0)
                {
                    if (positionX >= 0 && positionX < carte.GetLength(0))
                    {
                        carte[positionX, i].Terrain = Acre.TypeTerrain.RiviereNordSud;
                    }
                }
                else
                {
                    if (positionX >= 0 && positionX < carte.GetLength(0))
                    {
                        carte[positionX, i].Terrain = Acre.TypeTerrain.RiviereNordEst;
                    }
                    positionX += 1;
                    if (positionX >= 0 && positionX < carte.GetLength(0))
                    {
                        carte[positionX, i].Terrain = Acre.TypeTerrain.RiviereSudOuest;
                    }

                }
            }
        }

        /// <summary>
        /// Retourne la distance entre 2points donnés
        /// </summary>
        /// <param name="x1">Valeur en X du point 1</param>
        /// <param name="y1">Valeur en Y du point 1</param>
        /// <param name="x2">Valeur en X du point 2</param>
        /// <param name="y2">Valeur en Y du point 2</param>
        /// <returns>Valeur de la distance</returns>
        public int CalculerDistance(int x1, int y1, int x2, int y2)
        {
            int distanceManhattan = 0;
            distanceManhattan = Math.Abs(y2 - y1) + Math.Abs(x2 - x1);
            return distanceManhattan;
        }

        /// <summary>
        /// Retoure la distance entre le point et la batisse la plus proche du type donné
        /// </summary>
        /// <param name="terrain">Terrain recherché</param>
        /// <param name="x">Valeur X du point</param>
        /// <param name="y">Valeur Y du point</param>
        /// <returns>Valeur de la distance</returns>
        public int DistancePointBatiment(Acre.TypeTerrain terrain, int x, int y)
        {
            int distance = int.MaxValue;
            for (int i = 0; i < carte.GetLength(0); i++)
            {
                for (int j = 0; j < carte.GetLength(1); j++)
                {
                    if (carte[i, j].Terrain == terrain)
                    {
                        if (CalculerDistance(i, j, x, y) < distance)
                        {
                            distance = CalculerDistance(i, j, x, y);
                        }
                    }
                }
            }

            return distance;
        }

        /// <summary>
        /// Valide qui est possible ou non de construire à place donné et le type de terrain donné
        /// </summary>
        /// <param name="terrain">Terrain qu'on veut construire</param>
        /// <param name="x">Valeur X du point</param>
        /// <param name="y">Valeur Y du point</param>
        /// <returns>Retourne le message d'erreur conrespondant à la possibilité de construction</returns>
        public Messages.Id ValiderConstruction(Acre.TypeTerrain terrain, int x, int y)
        {
            Messages.Id message = new Messages.Id();
            switch (terrain)
            {
                case Acre.TypeTerrain.Energie:
                    message = ValidationSpecifique(Acre.TypeTerrain.Energie, x, y);
                    break;


                case Acre.TypeTerrain.Police:
                    message = ValidationSpecifique(Acre.TypeTerrain.Police, x, y);
                    break;


                case Acre.TypeTerrain.Hopital:
                    message = ValidationSpecifique(Acre.TypeTerrain.Hopital, x, y);
                    break;


                case Acre.TypeTerrain.Residentiel:
                    message = ValidationSpecifique(Acre.TypeTerrain.Residentiel, x, y);
                    break;


                case Acre.TypeTerrain.Commercial:
                    message = ValidationSpecifique(Acre.TypeTerrain.Commercial, x, y);
                    break;


                case Acre.TypeTerrain.Stade:
                    message = ValidationSpecifique(Acre.TypeTerrain.Stade, x, y);
                    break;
            }
            return message;
        }

        /// <summary>
        /// Valide s'il est possible de construire le type de terrain précis à l'endroit indiquer
        /// <param name="typeTerrain">Le type de terraint qu'on veut construire</param>
        /// <param name="x">Position en X de la construction</param>
        /// <param name="y">Position en Y de la construction</param>
        /// <returns>Retourne un Messages.Id qui indique le résultat de possibilité de construction</returns>
        private Messages.Id ValidationSpecifique(Acre.TypeTerrain typeTerrain, int x, int y)
        {
            Messages.Id message = Messages.Id.MessageOk;
            if (DistancePointBatiment(Acre.TypeTerrain.Energie, x, y) < 12 || typeTerrain == Acre.TypeTerrain.Energie)
            {
                if (carte[x, y].Terrain == Acre.TypeTerrain.Champ)
                {
                    if (economieJeu.MontantBanque >= economieJeu.GetCout(Acre.TypeTerrain.Champ, typeTerrain))
                    {
                        Construire(typeTerrain, x, y);
                        economieJeu.MontantBanque -= economieJeu.GetCout(Acre.TypeTerrain.Champ, typeTerrain);
                    }
                    else
                    {
                        message = Messages.Id.ErreurFondInsuffisants;
                    }
                }
                else if (carte[x, y].Terrain == Acre.TypeTerrain.Foret)
                {
                    if (economieJeu.MontantBanque >= economieJeu.GetCout(Acre.TypeTerrain.Foret, typeTerrain))
                    {
                        Construire(typeTerrain, x, y);
                        economieJeu.MontantBanque -= economieJeu.GetCout(Acre.TypeTerrain.Foret, typeTerrain);
                    }
                    else
                    {
                        message = Messages.Id.ErreurFondInsuffisants;
                    }
                }
                else
                {
                    message = Messages.Id.ErreurLocationInvalide;
                }
            }
            else
            {
                message = Messages.Id.ErreurTropLoinEnergie;
            }


            return message;
        }


        /// <summary>
        /// Constuit le terrain voulu au point voulu
        /// </summary>
        /// <param name="terrain">Terrain qu'on veut construire</param>
        /// <param name="x">X de la case où l'on veut construire</param>
        /// <param name="y">Y de la case où l'on veut construire</param>
        public void Construire(Acre.TypeTerrain terrain, int x, int y)
        {
            carte[x, y].Terrain = terrain;
        }

        /// <summary>
        /// Ajoute un mois aux données du jeu et appelle les fonctions nécessaire pour mettre à jour les données
        /// </summary>
        public Messages.Id AjoutMois()
        {
            Messages.Id message = Messages.Id.MessageOk;
            message = Catastrophe();
            this.CalculerEconomie();
            this.CalculerIndicesEtHabitants();
            this.date = this.date.AddMonths(1);            
            return message;
        }

        /// <summary>
        /// Calcul les indices de chaque case d'habitant.
        /// </summary>
        public void CalculerIndicesEtHabitants()
        {
            for (int i = 0; i < carte.GetLength(0); i++)
            {
                for (int j = 0; j < carte.GetLength(1); j++)
                {
                    if (carte[i, j].Terrain == Acre.TypeTerrain.Residentiel)
                    {
                        if (DistancePointBatiment(Acre.TypeTerrain.Hopital, i, j) <= 20)
                        {
                            carte[i, j].Sante = (20 - DistancePointBatiment(Acre.TypeTerrain.Hopital, i, j)) * 5;
                        }
                        else
                        {
                            carte[i, j].Sante = 0;
                        }
                        if (DistancePointBatiment(Acre.TypeTerrain.Police, i, j) <= 10)
                        {
                            int distance =DistancePointBatiment(Acre.TypeTerrain.Police, i, j) * 10;
                            carte[i, j].Criminalite = distance;
                        }
                        else
                        {
                            carte[i, j].Criminalite = 100;
                        }
                        if (DistancePointBatiment(Acre.TypeTerrain.Stade, i, j) <= 15)
                        {
                            double loisir = ((15 - DistancePointBatiment(Acre.TypeTerrain.Stade, i, j)) * (100.0 / 15.0));
                            carte[i,j].Loisirs = (int)loisir;
                        }
                        else
                        {
                            carte[i, j].Loisirs = 0;
                        }
                        carte[i, j].Satisfaction = (int)(((carte[i, j].Sante + (100 - carte[i, j].Criminalite) + carte[i, j].Loisirs) / 3) - (economieJeu.TauxTaxe * 2));
                        carte[i, j].NbHabitant = carte[i, j].Satisfaction * 10;
                        if (carte[i, j].NbHabitant < 0)
                        {
                            carte[i, j].NbHabitant = 0;
                        }
                    }
                    else
                    {
                        carte[i, j].Criminalite = 0;
                        carte[i, j].Loisirs = 0;
                        carte[i, j].Sante = 0;
                        carte[i, j].Satisfaction = 0;
                        carte[i, j].NbHabitant = 0;
                    }
                }
            }

        }

        /// <summary>
        /// Met à jour toute les données du jeu concernant l'économie
        /// </summary>
        public void CalculerEconomie()
        {
            nombreHabitant = 0;
            int nbCommerce = 0;
            

            for (int i = 0; i < carte.GetLength(0); i++)
            {
                for (int j = 0; j < carte.GetLength(1); j++)
                {
                    if (carte[i, j].Terrain == Acre.TypeTerrain.Residentiel)
                    {
                        nombreHabitant += carte[i, j].NbHabitant;
                    }
                    if (carte[i, j].Terrain == Acre.TypeTerrain.Commercial)
                    {
                        nbCommerce++;
                    }
                }
            }
            
            habitantAcre = (int)(nombreHabitant / (carte.GetLength(0) * carte.GetLength(1)));
            economieJeu.RevenuMoyen = 40000 + (nbCommerce * 1000);
            economieJeu.RevenuVille = (long)(nombreHabitant * economieJeu.RevenuMoyen * (economieJeu.TauxTaxe/100.0)  / 12.0 / 4.0);
            economieJeu.MontantBanque += economieJeu.RevenuVille;
        }

        /// <summary>
        /// Si de façons aléatoire une catastrophe se produit elle construit des champs par dessus les endroits indiqués
        /// </summary>
        /// <returns>Retourne le message de catastrophe</returns>
        public Messages.Id Catastrophe()
        {
            Messages.Id message = Messages.Id.MessageOk;
            aleatoire = new Random();
            if (aleatoire.Next(100 + 1) >= probaliterDesastre)
            {
                int desastreMensuel = aleatoire.Next(desastreNaturel.Length);
                int positionX = aleatoire.Next(carte.GetLength(0) - desastreNaturel[desastreMensuel].Taille);
                int positionY = aleatoire.Next(carte.GetLength(1) - desastreNaturel[desastreMensuel].Taille);
                for (int i = positionX; i < positionX + desastreNaturel[desastreMensuel].Taille; i++)
                {
                    for (int j = positionY; j < positionY + desastreNaturel[desastreMensuel].Taille; j++)
                    {
                        if (carte[i, j].Terrain == desastreNaturel[desastreMensuel].BatimentAffecte)
                        {
                            Construire(Acre.TypeTerrain.Champ, i, j);
                        }
                    }
                }
                switch (desastreMensuel)
                {
                    case (int)Desastres.TypeDesastre.Verglas:
                        message = Messages.Id.DesastreVerglas;
                        break;
                    case (int)Desastres.TypeDesastre.ReformeSante:
                        message = Messages.Id.DesastreSante;
                        break;
                    case (int)Desastres.TypeDesastre.TremblementTerre:
                        message = Messages.Id.DesastreTremblementTerre;
                        break;
                    case (int)Desastres.TypeDesastre.Manifestation:
                        message = Messages.Id.DesastreManifestation;
                        break;
                    case (int)Desastres.TypeDesastre.CriseEconnomique:
                        message = Messages.Id.DesastreEconomique;
                        break;
                    case (int)Desastres.TypeDesastre.Demenagement:
                        message = Messages.Id.DesastreEquipe;
                        break;
                }
            }
            return message;
        }

        /// <summary>
        /// Carte du jeu
        /// </summary>
        public Acre[,] Carte
        {
            get { return carte; }
            set { carte = value; }
        }

        /// <summary>
        /// L'économie de la partie
        /// </summary>
        public Economie EconomieJeu
        {
            get { return economieJeu; }
            set { economieJeu = value; }
        }

        /// <summary>
        /// La date de la ville du jeu
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Nombre d'habitant qui réside dans la ville
        /// </summary>
        public long NombreHabitant
        {
            get { return nombreHabitant; }
            set { nombreHabitant = value; }
        }

        /// <summary>
        /// Moyenne du nombre d'habitant par acre dans la ville
        /// </summary>
        public int HabitantAcre
        {
            get { return habitantAcre; }
            set { habitantAcre = value; }
        }

        /// <summary>
        /// Les désastres qui peuvent se produire dans la ville
        /// </summary>
        public Desastres[] DesastreNaturel
        {
            get { return desastreNaturel; }
            set { desastreNaturel = value; }
        }

        /// <summary>
        /// Probalité qui est une catastrophe
        /// </summary>
        public int ProbaliterDesastre
        {
            get { return probaliterDesastre; }
            set { probaliterDesastre = value; }
        }
    }
}
