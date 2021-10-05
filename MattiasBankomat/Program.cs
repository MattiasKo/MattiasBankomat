﻿//Mattias Kokkonen SUT21
using System;
using System.Threading;
namespace Bankomat
{
    class Program
    {
        static void Main(string[] args)
        {

            Program P = new Program();

            double[][] KontonArray = new double[5][];

            KontonArray[0] = new double[] { 13000000.00, 3000000.00, 500000.00, 1.00, 635460.00 };
            KontonArray[1] = new double[] { 10650.50, 500.92, 1234567.89, 500.50 };
            KontonArray[2] = new double[] { 50001.11, 2.22, 64852.23 };
            KontonArray[3] = new double[] { 50001.11, 2.22 };
            KontonArray[4] = new double[] { 0.01, 0.02, 0.03, 0.04, 0.05, 0.06 };

            string[][] Kontonamn = new string[5][];

            Kontonamn[0] = new string[] { "GoldPremium konto: ", "Räntekonto: ", "Räntekonto: ", " SparKonto: ", "Vinstkonto: " };
            Kontonamn[1] = new string[] { "Mastercard: ", "StudentKonto", "Sparkonto: ", "Aktier: " };
            Kontonamn[2] = new string[] { "Lönekonto ", "Sparkonto ", "Fonder: " };
            Kontonamn[3] = new string[] { "kontokort: ", "Sparkonto: " };
            Kontonamn[4] = new string[] { "konto1: ", "konto2: ", "konto3: ", "konto4: ", "konto5: ", "konto6: " };

            Start(KontonArray, Kontonamn);
        }
            public static void Start(double [][]KontonArray,string[][] Kontonamn)
            {

            
            byte Tries = 3;

            Console.WriteLine("Hej och välkommen till Mattias bankomat!");
            do
            {

                Console.WriteLine("In loggning till bankomaten\n\nSkriv in användarnamn:");
                string Användare = Console.ReadLine().ToUpper();
                Console.WriteLine("Skriv in lösenord");

                int Pincode = int.Parse(Console.ReadLine());


                switch (Användare + Pincode)  //använde en switch case för både användarnamn och pinkod för att logga in. sedan visa kontot som är inloggat. Switch har en default som gör det enkelt om inlogg är fel.
                {
                    case "VONANKA4321":
                        konto(0,Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "MATTIAS0000":
                        konto(1,Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "TOBIAS9034":
                        konto(2,Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "ANAS3535":
                        konto(3,Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "ADMIN1234":
                        konto(4,Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Fel användar namn eller lösenord. Var god vänta... ");
                        Thread.Sleep(1500);
                        Tries--;
                        Console.Clear();
                        break;
                }
            } while (Tries > 0);
            Console.WriteLine("Stänger ner.");
        }
        public static void konto(byte ID, string user, double[][] kontonArray, int Pincode, string[][] kontoNamn)
        {
            bool LogIn = true;
            Console.Clear();
            do
            {

                Console.WriteLine("Hej " + user + " Ange val.\n1: Se dina konton och saldo\n2:Överföring mellan konton\n3:Ta ut pengar\n4:Logga ut");
                byte KontoVal = byte.Parse(Console.ReadLine());
                switch (KontoVal)
                {

                    case 1:
                        {
                            Console.Clear();
                            VisaKonton(ID,user, kontonArray, kontoNamn);
                            Console.WriteLine("Tryck enter för att fortsätta");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                            Console.Clear();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Transfer(ID,user, kontonArray, Pincode, kontoNamn);
                            break;

                        }
                    case 3:
                        {
                            Console.Clear();
                            TaUt(ID,user, kontonArray, Pincode, kontoNamn);
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Start(kontonArray, kontoNamn);
                            break;
                          
                        }
                    default:
                        Console.WriteLine("ogiltigt val.");
                        break;


                }

            } while (LogIn == true);
        }
        public static void Transfer(byte ID,string user, double[][] kontoinfo, int Pincode, string[][] kontoNamn)
        {
            while (true)
            {
                VisaKonton(ID,user, kontoinfo, kontoNamn);

                double Amount;
                byte ToAccount;
                byte FromAccount;
                bool APar;
                do
                {
                    Console.WriteLine("Konto överföring\nVilket konto vill du föra över ifrån?");
                    APar = byte.TryParse(Console.ReadLine(), out FromAccount);
                    if (APar == false)
                    {
                        Console.WriteLine("Felaktigt kontonummer!");
                    }
                    if (FromAccount > kontoinfo[ID].Length | FromAccount < 0)
                    {
                        Console.WriteLine("Det kontot finns inte.");
                    }
                } while (APar == false | (FromAccount > kontoinfo[ID].Length | FromAccount < 0));

                do
                {
                    Console.WriteLine("Hur mycket vill du överföra?");
                    APar = double.TryParse(Console.ReadLine(), out Amount);
                    if (APar == false)
                    {
                        Console.WriteLine("Felaktigt summa!");
                    }
                    if (Amount > kontoinfo[ID][FromAccount])
                    {
                        Console.WriteLine("Inte tillräckligt med pengar i det kontot, Försök igen!");
                    }
                    if (Amount < 1)
                    {
                        Console.WriteLine("Felaktigt summa!");
                    }
                } while (APar == false | Amount > kontoinfo[ID][FromAccount] || Amount < 0);

                do
                {
                    Console.WriteLine("Till vilket konto?");
                    APar = byte.TryParse(Console.ReadLine(), out ToAccount);
                    if (APar == false | ToAccount > kontoinfo[ID].Length | ToAccount < 0)
                    {
                        Console.WriteLine("Felaktigt kontonummer!");
                    }
                } while (APar == false | ToAccount > kontoinfo[ID].Length | ToAccount < 0);

                kontoinfo[ID][FromAccount] = kontoinfo[ID][FromAccount] - Amount;
                kontoinfo[ID][ToAccount] = kontoinfo[ID][ToAccount] + Amount;
                Console.Clear();
                VisaKonton(ID,user, kontoinfo, kontoNamn);
                Console.WriteLine("Tryck enter för att fortsätta");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                Console.Clear();
                konto(ID,user, kontoinfo, Pincode, kontoNamn);

            }
        }
        public static void TaUt(byte ID, string user, double[][] kontoinfo, int Pincode, string[][] kontoNamn)
        {
            VisaKonton(ID,user, kontoinfo, kontoNamn);
            double Amount;
            byte FromAccount;
            bool APar;
            do
            {
                Console.WriteLine("Konto vill du ta ut pengar ifrån?");
                APar = byte.TryParse(Console.ReadLine(), out FromAccount);
                if (APar == false)
                {
                    Console.WriteLine("Felaktigt kontonummer!");
                }
                if (FromAccount > kontoinfo[ID].Length | FromAccount < 0)
                {
                    Console.WriteLine("Det kontot finns inte.");
                }
            } while (APar == false | (FromAccount >kontoinfo[ID].Length | FromAccount < 0));
            do
            {
                Console.WriteLine("Hur mycket vill du ta ut?");
                APar = double.TryParse(Console.ReadLine(), out Amount);
                if (APar == false | Amount < 0)
                {
                    Console.WriteLine("Felaktigt summa!");
                }
                if (Amount > kontoinfo[ID][FromAccount])
                {
                    Console.WriteLine("Inte tillräckligt med pengar i det kontot, Försök igen!");
                }
            } while (Amount > kontoinfo[ID][FromAccount] || Amount < 0);

            int TryPinCode;

            Console.WriteLine("Skriv in pinkod.");
            APar = int.TryParse(Console.ReadLine(), out TryPinCode);
            if (TryPinCode == Pincode)
            {
                kontoinfo[ID][FromAccount] = kontoinfo[ID][FromAccount] - Amount;

                Console.WriteLine("Du har tagit ut " + Amount + "kr från " + kontoNamn[ID][FromAccount] + ": " + kontoinfo[ID][FromAccount]);
                Thread.Sleep(1500);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Fel pinkod!");
                Thread.Sleep(1500);
                Console.Clear();
            }


            konto(ID,user, kontoinfo, Pincode, kontoNamn);
        }
        public static void VisaKonton(byte ID,string user, double[][] kontoinfo, string[][] kontoNamn)
        {

            {
                for (int i = 0; i < kontoinfo[ID].Length; i++)
                {
                    Console.Write(i + ": " + kontoNamn[ID][i] + kontoinfo[ID][i]);
                    Console.WriteLine();
                }
            }
        }
      
    
            

    }
}
        

    
