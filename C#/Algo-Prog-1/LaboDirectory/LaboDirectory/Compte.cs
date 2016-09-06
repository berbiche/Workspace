using System;
using System.Collections.Generic;
using System.Linq;

namespace LaboDirectory
{
    class Compte
    {
        public struct transaction
        {
            public transaction(DateTime temps, double montant)
            {
                this.temps = temps;
                this.montant = montant;
            }
            private DateTime temps;
            private double montant;

            public DateTime _temps { get { return this.temps; } }
            public double _montant { get { return this.montant; } }
        }
        private List<transaction> livreTransactions = new List<transaction>();
        public List<transaction> _livreTransactions { get { return this.livreTransactions; } set { this.livreTransactions = value; } }
        private double transactionTotal = 0;
        public double _transactionTotal { get { return this.transactionTotal; } }

        public Compte(DateTime temps, double montant)
        {
            transaction newTrans = new transaction(temps, montant);
            livreTransactions.Add(newTrans);
            transactionTotal += montant;
        }
        
        public override string ToString()
        {
            return string.Join("\n", livreTransactions.OrderBy(x=> x._temps).Select(x=> string.Format("{0,-7} {1,16:##.00} $",string.Format("{0}-{1:00}",x._temps.Year,x._temps.Month), x._montant)));
        }
    }
}
