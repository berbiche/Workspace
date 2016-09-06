using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace laboPO01v2
{

    class GestionDictionnaire
    {
        #region constructeurs
        //Constructeur par défaut
        public GestionDictionnaire()
        {
            langue = "français";
        }

        //Constructeur surchargée
        public GestionDictionnaire(string lalangue)
        {
            langue = lalangue;
        }
        //Fin constructeurs
        #endregion
        #region méthode statique Special
        public static void Special(ref string Mot, string chaîne)
        {
            Mot.ToUpper();
            Mot = chaîne + Mot + chaîne;
        }

        public static void Special(ref string mot)
        {
            string temp;
            if (mot.Length > 0) //pour éviter des crash
            {
                temp = mot.Substring(0, 1).ToUpper();
                mot = temp + mot.Substring(1).ToLower();//le ToLower est dans le cas où le reste de la chaîne n'était pas déjà en minuscule.
            }
        }
        #endregion
        #region Variables
        public static string TypeDictionnaire;
        public string langue;
        SortedList<string, LeDico> LaLangue = new SortedList<string, LeDico>();
        struct LeDico
        {
            public string mot;
            public char genre;
            public string definition;
        }
        #endregion

        // insère un nom dans la liste
        public void EntrerUnMot(string ParamMot, char ParamGenre, string ParamDefinition, string ParamCle)
        {
            LeDico mot = new LeDico();
            mot.mot = ParamMot; mot.genre = ParamGenre; mot.definition = ParamDefinition;
            LaLangue.Add(ParamCle, mot);
        } // fin EntrerUnNom

        // retourner un nom selon la clé
        public void RetournerUnMot(ref string ParamMot, ref char ParamGenre, ref string ParamDefinition, string ParamCle)
        {
            if (LaLangue.ContainsKey(ParamCle))
            {
                ParamMot = LaLangue.Values.ElementAt(LaLangue.IndexOfKey(ParamCle)).mot;
                ParamGenre = LaLangue.Values.ElementAt(LaLangue.IndexOfKey(ParamCle)).genre;
                ParamDefinition = LaLangue.Values.ElementAt(LaLangue.IndexOfKey(ParamCle)).definition;
            }
            else
                ParamMot = "La clef \"" + ParamCle + "\" est introuvable";
        } // fin RetournerUnMot

    } // fin classe GestionDictionnaire
}
