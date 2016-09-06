using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    class Sudoku
    {
        private int[,] sudoku, sudokuReponse = new int[9,9];
        public int[,] _sudoku { get { return this.sudoku; } }
        public int[,] _sudokuReponse { get { return this.sudokuReponse; } }
        static Random aleatoire = new Random();
        public Sudoku()
        {
            this.sudoku = nouveau();
            string e = "";
            for (int i = 0; i < 81; i++)
            {
                e += sudoku[i / 9, i % 9] != 0 ? sudoku[i / 9, i % 9].ToString() : ".";
            }
            var abc = LinqSudokuSolver.search(LinqSudokuSolver.parse_grid(e));
            for (int i = 0; i < 81; i++)
                sudokuReponse[i / 9, i % 9] = int.Parse(abc.ElementAt(i).Value);
        }
        static int[,] nouveau()
        {
            int[,] abc = new int[9, 9], ghi = new int[9, 9];
            generer(abc, 0);
            for (int i = 0; i < 55; i++)
                abc[aleatoire.Next(0, 9), aleatoire.Next(0, 9)] = 0;
            return abc;
        }
        static bool absentSurLigne(int[,] carre, int ligne, int nombre)
        {
            for (int colonne = 0; colonne < 9; colonne++)
                if (carre[ligne, colonne] == nombre)
                    return false;
            return true;
        }

        static bool absentSurColonne(int[,] carre, int colonne, int nombre)
        {
            for (int ligne = 0; ligne < 9; ligne++)
                if (carre[ligne, colonne] == nombre)
                    return false;
            return true;
        }

        static bool absentSurBloc(int[,] grille, int i, int j, int nombre)
        {
            int ligne = i - (i % 3), colonne = j - (j % 3); //pour déterminer le carré
            for (i = ligne; i < ligne + 3; i++)
                for (j = colonne; j < colonne + 3; j++)
                    if (grille[i, j] == nombre)
                        return false;
            return true;
        }

        static bool generer(int[,] carre, int position)
        {
            if (position == 9 * 9)
                return true;

            int i = position / 9, j = position % 9;

            if (carre[i, j] != 0)
                return generer(carre, position + 1);

            for (int k = 0; k < 9; k++)
            {
                int nombre = aleatoire.Next(0, 10);
                if (absentSurLigne(carre, i, nombre) && absentSurColonne(carre, j, nombre) && absentSurBloc(carre, i, j, nombre))
                {
                    carre[i, j] = nombre;

                    if (generer(carre, position + 1))
                        return true;
                }
            }
            carre[i, j] = 0;

            return false;
        }
    }

    /// <summary>
    /// Ported from http://norvig.com/sudo.py by Richard Birkby, June 2007.
    /// See http://norvig.com/sudoku.html
    /// Also, https://bugzilla.mozilla.org/attachment.cgi?id=266577 - Javascript1.8 version
    /// </summary>
    static class LinqSudokuSolver
    {
        // Throughout this program we have:
        //   r is a row,    e.g. 'A'
        //   c is a column, e.g. '3'
        //   s is a square, e.g. 'A3'
        //   d is a digit,  e.g. '9'
        //   u is a unit,   e.g. ['A1','B1','C1','D1','E1','F1','G1','H1','I1']
        //   g is a grid,   e.g. 81 non-blank chars, e.g. starting with '.18...7...
        //   values is a dict of possible values, e.g. {'A1':'123489', 'A2':'8', ...}
        static string rows = "ABCDEFGHI";
        static string cols = "123456789";
        static string digits = "123456789";
        static string[] squares = cross(rows, cols);
        static Dictionary<string, IEnumerable<string>> peers;
        static Dictionary<string, IGrouping<string, string[]>> units;

        static string[] cross(string A, string B)
        {
            return (from a in A from b in B select "" + a + b).ToArray();
        }
        static LinqSudokuSolver()
        {
            var unitlist = ((from c in cols select cross(rows, c.ToString()))
                               .Concat(from r in rows select cross(r.ToString(), cols))
                               .Concat(from rs in (new[] { "ABC", "DEF", "GHI" }) from cs in (new[] { "123", "456", "789" }) select cross(rs, cs)));
            units = (from s in squares from u in unitlist where u.Contains(s) group u by s into g select g).ToDictionary(g => g.Key);
            peers = (from s in squares from u in units[s] from s2 in u where s2 != s group s2 by s into g select g).ToDictionary(g => g.Key, g => g.Distinct());

        }
        static string[][] zip(string[] A, string[] B)
        {
            var n = A.Length < B.Length ? A.Length : B.Length;
            string[][] sd = new string[n][];
            for (var i = 0; i < n; i++)
            {
                sd[i] = new string[] { A[i].ToString(), B[i].ToString() };
            }
            return sd;
        }
        /// <summary>Given a string of 81 digits (or . or 0 or -), return a dict of {cell:values}</summary>
        public static Dictionary<string, string> parse_grid(string grid)
        {
            var grid2 = from c in grid where "0.-123456789".Contains(c) select c;
            var values = squares.ToDictionary(s => s, s => digits); //To start, every square can be any digit

            foreach (var sd in zip(squares, (from s in grid select s.ToString()).ToArray()))
            {
                var s = sd[0];
                var d = sd[1];

                if (digits.Contains(d) && assign(values, s, d) == null)
                {
                    return null;
                }
            }
            return values;
        }
        /// <summary>Using depth-first search and propagation, try all possible values.</summary>
        public static Dictionary<string, string> search(Dictionary<string, string> values)
        {
            if (values == null)
            {
                return null; // Failed earlier
            }
            if (all(from s in squares select values[s].Length == 1 ? "" : null))
            {
                return values; // Solved!
            }

            // Chose the unfilled square s with the fewest possibilities
            var s2 = (from s in squares where values[s].Length > 1 orderby values[s].Length ascending select s).First();

            return some(from d in values[s2]
                        select search(assign(new Dictionary<string, string>(values), s2, d.ToString())));
        }
        /// <summary>Eliminate all the other values (except d) from values[s] and propagate.</summary>
        static Dictionary<string, string> assign(Dictionary<string, string> values, string s, string d)
        {
            if (all(
                    from d2 in values[s]
                    where d2.ToString() != d
                    select eliminate(values, s, d2.ToString())))
            {
                return values;
            }
            return null;
        }
        /// <summary>Eliminate d from values[s]; propagate when values or places &lt;= 2.</summary>
        static Dictionary<string, string> eliminate(Dictionary<string, string> values, string s, string d)
        {
            if (!values[s].Contains(d))
            {
                return values;
            }
            values[s] = values[s].Replace(d, "");
            if (values[s].Length == 0)
            {
                return null; //Contradiction: removed last value
            }
            else if (values[s].Length == 1)
            {
                //If there is only one value (d2) left in square, remove it from peers
                var d2 = values[s];
                if (!all(from s2 in peers[s] select eliminate(values, s2, d2)))
                {
                    return null;
                }
            }
            //Now check the places where d appears in the units of s
            foreach (var u in units[s])
            {
                var dplaces = from s2 in u where values[s2].Contains(d) select s2;
                if (dplaces.Count() == 0)
                {
                    return null;
                }
                else if (dplaces.Count() == 1)
                {
                    // d can only be in one place in unit; assign it there
                    if (assign(values, dplaces.First(), d) == null)
                    {
                        return null;
                    }
                }
            }
            return values;
        }
        static bool all<T>(IEnumerable<T> seq)
        {
            foreach (var e in seq)
            {
                if (e == null) return false;
            }
            return true;
        }
        static T some<T>(IEnumerable<T> seq)
        {
            foreach (var e in seq)
            {
                if (e != null) return e;
            }
            return default(T);
        }
    }
}
