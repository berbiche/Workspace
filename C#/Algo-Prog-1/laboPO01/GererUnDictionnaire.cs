using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace laboPO01
{

    class GestionDictionnaire
    {
        // insérer ici la structures, les propriétés et la SortedList
        public static string TypeDictionnaire;
        public string langue;
        SortedList<string, LeDico> LaLangue = new SortedList<string, LeDico>();
        struct LeDico
        {
            public string mot;
            public char genre;
            public string definition;
        }
        // inserer un nom dans la liste
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
