// Message.cs
// Classe servant à gérer les messages affichés durant le déroulement du jeu.
// Programmé par Jean-Sébastien Campeau
// Le 23 février 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimVille
{
    /// <summary>
    /// Définit les messages de succès et d'erreur générés par l'application.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Définit les identificateurs de messages. On utilise un enum pour
        /// éviter d'assigner le même Id à 2 messges différents et pour plus
        /// de clarté dans le code.
        /// </summary>
        public enum Id
        {
            MessageOk,
            ErreurProgrammation,
            ErreurFondInsuffisants,
            ErreurLocationInvalide,
            ErreurTropLoinEnergie,
            DesastreVerglas,
            DesastreSante,
            DesastreTremblementTerre,
            DesastreManifestation,
            DesastreEquipe,
            DesastreEconomique,
            ChargementDriverBd,
            ConnexionBd,
            RequeteBd,
            ChargementOk,
            SauvegardeOk
        };

        /// <summary>
        /// Tableau contenant tous les messages
        /// </summary>
        private static string[] message;

        /// <summary>
        /// Constructeur. Initialise les message.
        /// </summary>
        static Messages()
        {
            message = new string[Enum.GetNames(typeof(Id)).Length];
            message[(int)Id.MessageOk] = "Opération effectuée!";
            message[(int)Id.ErreurProgrammation] = "Erreur de programmation.";
            message[(int)Id.ErreurFondInsuffisants] = "Fonds insuffisants pour réaliser cette opération.";
            message[(int)Id.ErreurLocationInvalide] = "Vous ne pouvez bâtir ce bâtiment à cet endroit.";
            message[(int)Id.ErreurTropLoinEnergie] = "Vous devez avoir une source d'énergie à une distance inférieure à 12.";
            message[(int)Id.DesastreVerglas] = "Tempête de verglas!  Des lignes électriques sont détruites.";
            message[(int)Id.DesastreSante] = "Reforme de la santé!  Des hôpitaux sont fermés.";
            message[(int)Id.DesastreTremblementTerre] = "Tremblement de terre!  Des zones résidentielles sont détruites.";
            message[(int)Id.DesastreManifestation] = "Une manifestation prend le monopole des polices!  Ces postes visés ferment.";
            message[(int)Id.DesastreEquipe] = "Votre équipe sportive locale déménage!  Votre stade est détruit.";
            message[(int)Id.DesastreEconomique] = "Crise économique! Les magasins sont abandonnés.";
        }

        /// <summary>
        /// Permet d'accéder en lecture à un message à partir de son Id.
        /// </summary>
        public static string[] Message
        {
            get { return Messages.message; }
        }
    }
}


