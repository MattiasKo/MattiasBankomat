using System;
using System.Threading;
namespace Bankomat
{
    class Program
    {
        static void Main(string[] args)
        {

            Program P = new Program();
            double[] VonankaKonto = { 13000000.00, 3000000.00, 500000.00, 1.00, 635460.00 };
            double[] MattiasKonto = { 10650.50, 500.92, 1234567.89, 500.50 };
            double[] TobiasKonto = { 50001.11, 2.22, 64852.23 };
            double[] AnasKonto = { 50001.11, 2.22 };
            double[] AdminKonto = { 0.01, 0.02, 0.03, 0.04, 0.05, 0.06 };
            string[] VonankaKontoNamn = { "GoldPremium konto: ", "Räntekonto: ", "Räntekonto: ", " SparKonto: ", "Vinstkonto: " };
            string[] MattiasKontoNamn = { "Mastercard: ", "StudentKonto", "Sparkonto: ", "Aktier: " };
            string[] TobiasKontoNamn = { "Lönekonto ", "Sparkonto ", "Fonder: " };
            string[] AnasKontoNamn = { "kontokort: ", "Sparkonto: " };
            string[] AdminKontoNamn = { "konto1: ", "konto2: ", "konto3: ", "konto4: ", "konto5: ", "konto6: " };
      
            byte Tries = 3;

            Console.WriteLine("Hej och välkommen till Mattias bankomat!");
            do
            {

                Console.WriteLine("In loggning till bankomaten\n\nSkriv in användarnamn:");
                string Användare = Console.ReadLine().ToUpper();
                Console.WriteLine("Skriv in lösenord");

                int Pincode = int.Parse(Console.ReadLine());


                switch (Användare + Pincode)  //använde en switch case för både användarnamn och pinkod för att logga in. sedan visa kontot som är inloggat.
                {
                    case "VONANKA4321":
                        konto(Användare, VonankaKonto, Pincode, VonankaKontoNamn);
                        break;

                    case "MATTIAS0000":
                        konto(Användare, MattiasKonto, Pincode, MattiasKontoNamn);
                        break;

                    case "TOBIAS9034":
                        konto(Användare, TobiasKonto, Pincode, TobiasKontoNamn);
                        break;

                    case "ANAS3535":
                        konto(Användare, AnasKonto, Pincode, AnasKontoNamn);
                        break;

                    case "ADMIN1234":
                        konto(Användare, AdminKonto, Pincode, AdminKontoNamn);
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
        public static void konto(string user, double[] kontoinfo, int Pincode, string[] kontoNamn)
        {
            bool LogIn = true;
            do
            {

                Console.WriteLine("Hej " + user + " Ange val.\n1: Se dina konton och saldo\n2:Överföring mellan konton\n3:Ta ut pengar\n4:Logga ut");
                byte KontoVal = byte.Parse(Console.ReadLine());
                switch (KontoVal)
                {

                    case 1:
                        {
                            Console.Clear();
                            VisaKonton(user, kontoinfo, kontoNamn);
                            Console.WriteLine("Tryck enter för att fortsätta");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                            Console.Clear();
                            break;
                        }
                    case 2:
                        {
                            Transfer(user, kontoinfo, Pincode, kontoNamn);
                            break;

                        }
                    case 3:
                        {
                            TaUt(user, kontoinfo, Pincode, kontoNamn);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Fixa så  man kan logga ut");
                            break;
                          
                        }
                    default:
                        Console.WriteLine("ogiltigt val.");
                        break;


                }

            } while (LogIn == true);
        }
        public static void Transfer(string user, double[] kontoinfo, int Pincode, string[] kontoNamn)
        {
            while (true)
            {
                VisaKonton(user, kontoinfo, kontoNamn);

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
                    if (FromAccount >= kontoinfo.Length | FromAccount < 0)
                    {
                        Console.WriteLine("Det kontot finns inte.");
                    }
                } while (APar == false | (FromAccount >= kontoinfo.Length | FromAccount < 0));

                do
                {
                    Console.WriteLine("Hur mycket vill du överföra?");
                    APar = double.TryParse(Console.ReadLine(), out Amount);
                    if (APar == false)
                    {
                        Console.WriteLine("Felaktigt summa!");
                    }
                    if (Amount > kontoinfo[FromAccount])
                    {
                        Console.WriteLine("Inte tillräckligt med pengar i det kontot, Försök igen!");
                    }
                    if (Amount < 1)
                    {
                        Console.WriteLine("Felaktigt summa!");
                    }
                } while (Amount > kontoinfo[FromAccount] || Amount < 0);

                do
                {
                    Console.WriteLine("Till vilket konto?");
                    APar = byte.TryParse(Console.ReadLine(), out ToAccount);
                    if (APar == false | ToAccount >= kontoinfo.Length | ToAccount < 0)
                    {
                        Console.WriteLine("Felaktigt kontonummer!");
                    }
                } while (APar == false | ToAccount >= kontoinfo.Length | ToAccount < 0);

                kontoinfo[FromAccount] = kontoinfo[FromAccount] - Amount;
                kontoinfo[ToAccount] = kontoinfo[ToAccount] + Amount;
                Console.Clear();
                VisaKonton(user, kontoinfo, kontoNamn);
                Console.WriteLine("Tryck enter för att fortsätta");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                Console.Clear();
                konto(user, kontoinfo, Pincode, kontoNamn);

            }
        }
        public static void TaUt(string user, double[] kontoinfo, int Pincode, string[] kontoNamn)
        {
            VisaKonton(user, kontoinfo, kontoNamn);
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
                if (FromAccount > kontoinfo.Length | FromAccount < 0)
                {
                    Console.WriteLine("Det kontot finns inte.");
                }
            } while (APar == false | (FromAccount >= kontoinfo.Length | FromAccount < 0));
            do
            {
                Console.WriteLine("Hur mycket vill du ta ut?");
                APar = double.TryParse(Console.ReadLine(), out Amount);
                if (APar == false | Amount < 0)
                {
                    Console.WriteLine("Felaktigt summa!");
                }
                if (Amount > kontoinfo[FromAccount])
                {
                    Console.WriteLine("Inte tillräckligt med pengar i det kontot, Försök igen!");
                }
            } while (Amount > kontoinfo[FromAccount] || Amount < 0);

            int TryPinCode;

            Console.WriteLine("Skriv in pinkod.");
            APar = int.TryParse(Console.ReadLine(), out TryPinCode);
            if (TryPinCode == Pincode)
            {
                kontoinfo[FromAccount] = kontoinfo[FromAccount] - Amount;

                Console.WriteLine("Du har tagit ut " + Amount + "kr från " + kontoNamn[FromAccount] + ": " + kontoinfo[FromAccount]);
                Thread.Sleep(1500);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Fel pinkod!");
                Thread.Sleep(1500);
                Console.Clear();
            }


            konto(user, kontoinfo, Pincode, kontoNamn);
        }
        public static void VisaKonton(string user, double[] kontoinfo, string[] kontoNamn)
        {

            {
                for (int i = 0; i < kontoinfo.Length; i++)
                {
                    Console.Write(i + ": " + kontoNamn[i] + kontoinfo[i]);
                    Console.WriteLine();
                }
            }
        }
        public static void Start(string user, double[] kontoinfo)
        {

        }
    
            

    }
}
        

    
