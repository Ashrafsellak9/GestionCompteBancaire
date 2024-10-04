using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompteBancaire
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
        class CompteBancaire
        {
            public string Titulaire
            {
                get;
            }
            public int NumeroCompte {  
                get;
            }
            public decimal Solde
            {
                get;
                protected set;
            }
            public List<string> Transactions
            {
                get;
                private set;
            }
            
            // Constructor
            public CompteBancaire(string titulaire, int numeroCompte, decimal soldeInitial)
            {
                Titulaire = titulaire;
                NumeroCompte = numeroCompte;
                Solde = soldeInitial;
                Transactions = new List<string>();
            }

            // Methods
    }
}
