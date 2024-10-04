using System;
using System.Collections.Generic;
using System.IO;

namespace gestionnaireDeCompteBancaire
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var compteCourant = new CompteCourant("Ashraf Sellak", 102030, 0m);
            var compteEpargne = new CompteEpargne("Basma Haimi", 112233, 0m);

            string choix;

            do
            {
                Console.WriteLine("\nAppuyez sur Entrée pour afficher le menu.");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Veuillez sélectionner une option ci-dessous :");
                Console.WriteLine("[I] Voir les informations sur le titulaire du compte");
                Console.WriteLine("[CS] Compte courant - Consulter le solde");
                Console.WriteLine("[CD] Compte courant - Déposer des fonds");
                Console.WriteLine("[CR] Compte courant - Retirer des fonds");
                Console.WriteLine("[ES] Compte épargne - Consulter le solde");
                Console.WriteLine("[ED] Compte épargne - Déposer des fonds");
                Console.WriteLine("[ER] Compte épargne - Retirer des fonds");
                Console.WriteLine("[TR] Transactions");
                Console.WriteLine("[X] Quitter");
                choix = Console.ReadLine().ToUpper();

                switch (choix)
                {
                    case "I":
                        compteCourant.AfficherInfos();
                        break;
                    case "CS":
                        Console.WriteLine("Solde du compte courant :");
                        compteCourant.AfficherSolde();
                        break;
                    case "CD":
                        Console.WriteLine("Quel montant souhaitez-vous déposer ?");
                        decimal montantDepotCourant;
                        while (!decimal.TryParse(Console.ReadLine(), out montantDepotCourant))
                        {
                            Console.WriteLine("Veuillez entrer un montant valide :");
                        }
                        compteCourant.Deposer(montantDepotCourant);
                        Console.WriteLine($"Vous avez déposé : {montantDepotCourant} $.");
                        break;
                    case "CR":
                        Console.WriteLine("Quel montant souhaitez-vous retirer ?");
                        decimal montantRetraitCourant;
                        while (!decimal.TryParse(Console.ReadLine(), out montantRetraitCourant))
                        {
                            Console.WriteLine("Veuillez entrer un montant valide :");
                        }
                        if (compteCourant.Retirer(montantRetraitCourant))
                        {
                            Console.WriteLine($"Vous avez retiré : {montantRetraitCourant} $.");
                        }
                        break;
                    case "ES":
                        Console.WriteLine("Solde du compte épargne :");
                        compteEpargne.AfficherSolde();
                        break;
                    case "ED":
                        Console.WriteLine("Quel montant souhaitez-vous déposer ?");
                        decimal montantDepotEpargne;
                        while (!decimal.TryParse(Console.ReadLine(), out montantDepotEpargne))
                        {
                            Console.WriteLine("Veuillez entrer un montant valide :");
                        }
                        compteEpargne.Deposer(montantDepotEpargne);
                        Console.WriteLine($"Vous avez déposé : {montantDepotEpargne} $.");
                        break;
                    case "ER":
                        Console.WriteLine("Quel montant souhaitez-vous retirer ?");
                        decimal montantRetraitEpargne;
                        while (!decimal.TryParse(Console.ReadLine(), out montantRetraitEpargne))
                        {
                            Console.WriteLine("Veuillez entrer un montant valide :");
                        }
                        if (compteEpargne.Retirer(montantRetraitEpargne))
                        {
                            Console.WriteLine($"Vous avez retiré : {montantRetraitEpargne} $.");
                        }
                        break;
                    case "TR":
                        Console.WriteLine("");
                        EnregistrerTransactions(compteCourant, compteEpargne);
                        break;
                    case "X":
                        EnregistrerTransactions(compteCourant, compteEpargne);
                        Console.WriteLine("Merci d'avoir utilisé notre service. Au revoir !");
                        break;
                    default:
                        Console.WriteLine("Option non valide. Veuillez réessayer.");
                        break;
                }
            } while (choix != "X");
        }

        class CompteBancaire
        {
            public string Titulaire { get; }
            public int NumeroCompte { get; }
            public decimal Solde { get; protected set; }
            public List<string> Transactions { get; private set; }

            public CompteBancaire(string titulaire, int numeroCompte, decimal soldeInitial)
            {
                Titulaire = titulaire;
                NumeroCompte = numeroCompte;
                Solde = soldeInitial;
                Transactions = new List<string>();
            }

            public void Deposer(decimal montant)
            {
                Solde += montant;
                Transactions.Add($"Depot de {montant} $");
            }

            public virtual bool Retirer(decimal montant)
            {
                if (Solde >= montant)
                {
                    Solde -= montant;
                    Transactions.Add($"Retrait de {montant} $");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Retrait échoué. Solde insuffisant pour retirer {montant} €. Votre solde est de {Solde} $.");
                    return false;
                }
            }

            public void AfficherSolde()
            {
                Console.WriteLine($"Solde: {Solde} $");
            }

            public void AfficherInfos()
            {
                Console.WriteLine($"Titulaire : {Titulaire}");
                Console.WriteLine($"Numero de compte : {NumeroCompte}");
            }
        }

        static void EnregistrerTransactions(CompteBancaire compteCourant, CompteBancaire compteEpargne)
        {
            using (StreamWriter writer = new StreamWriter("transactions.txt"))
            {
                writer.WriteLine("Transactions du compte courant : ");
                foreach (var transaction in compteCourant.Transactions)
                {
                    writer.WriteLine(transaction);
                }
                writer.WriteLine();

                writer.WriteLine("Transactions du compte épargne : ");
                foreach (var transaction in compteEpargne.Transactions)
                {
                    writer.WriteLine(transaction);
                }
            }
        }

        class CompteCourant : CompteBancaire
        {
            public CompteCourant(string titulaire, int numeroCompte, decimal soldeInitial) : base(titulaire, numeroCompte, soldeInitial) { }
        }

        class CompteEpargne : CompteBancaire
        {
            public CompteEpargne(string titulaire, int numeroCompte, decimal soldeInitial) : base(titulaire, numeroCompte, soldeInitial) { }
        }
    }
}
