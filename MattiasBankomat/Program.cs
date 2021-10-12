//Mattias Kokkonen SUT21
using System;
using System.Threading;
using System.IO;
namespace Bankomat
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @".\BankKontoinfo.txt";
            string fileName2 = @".\BankkontoNamn.txt";
            Program P = new Program();

            double[][] KontonArray = new double[5][];

            KontonArray[0] = new double[] { 13000000.00, 3000000.00, 500000.00, 1.00, 635460.00 };
            KontonArray[1] = new double[] { 10650.50, 500.92, 1234567.89, 500.50 };
            KontonArray[2] = new double[] { 50001.11, 2.22, 64852.23 };
            KontonArray[3] = new double[] { 50001.11, 2.22 };
            KontonArray[4] = new double[] { 0.01, 0.02, 0.03, 0.04, 0.05, 0.06 };

            string[][] Kontonamn = new string[5][];

            Kontonamn[0] = new string[] { "GoldPremium konto: ", "Räntekonto: ", "Räntekonto: ", " SparKonto: ", "Vinstkonto: " };
            Kontonamn[1] = new string[] { "Mastercard: ", "StudentKonto ", "Sparkonto: ", "Aktier: " };
            Kontonamn[2] = new string[] { "Lönekonto ", "Sparkonto ", "Fonder: " };
            Kontonamn[3] = new string[] { "kontokort: ", "Sparkonto: " };
            Kontonamn[4] = new string[] { "konto1: ", "konto2: ", "konto3: ", "konto4: ", "konto5: ", "konto6: " };

            if (File.Exists(fileName) && File.Exists(fileName2)) //Laddar in värderna ur en textfil till bank saldona och kontonamnen
            {
                Ladda(KontonArray, Kontonamn);
            }
            else
            {
               Spara(KontonArray, Kontonamn);
                   
            }

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
                string Pincode = Console.ReadLine();
                if (Login(Användare, Pincode))
                {
                    Console.WriteLine("Du är inloggad!");
                }
                else
                {
                    Console.WriteLine("Fel inloggning.");
                    Pincode = "";
                    Användare = "";
                    Thread.Sleep(1500);
                    Tries--;
                    Console.Clear();
                }

                switch (Användare + Pincode)  // Körde med en switch före Login metoden men då kunde man logga in genom att skriva användarnamn och pinkod i användare.
                {
                    case "VONANKA4321":
                        konto(0, Användare, KontonArray, Pincode, Kontonamn);   //Alla användare har ett ID nummer för att det ska funka med rätt array.
                        break;

                    case "MATTIAS0000":
                        konto(1, Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "TOBIAS9034":
                        konto(2, Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "ANAS3535":
                        konto(3, Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    case "ADMIN1234":
                        konto(4, Användare, KontonArray, Pincode, Kontonamn);
                        break;

                    default:
                        
                        break;
                }
            } while (Tries > 0);
            Console.WriteLine("Stänger ner.");
        }
        static bool Login(string Användare, string Pincode)
        {
            if (Användare == "VONANKA" && Pincode == "4321")
            {
                return true;
            }
            if (Användare == "MATTIAS" && Pincode == "0000")
            {
                return true;
            }
            if (Användare == "TOBIAS" && Pincode == "9034")
            {
                return true;
            }
            if (Användare == "ANAS" && Pincode == "3535")
            {
                return true;
            }
            if (Användare == "ADMIN" && Pincode == "1234")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void konto(byte ID, string user, double[][] kontonArray, string Pincode, string[][] kontoNamn)
        {
            bool LogIn = true;
            do
            {
                byte KontoVal = 0;
                bool input = false;
                do {
                Console.Clear();
                Console.WriteLine("Hej " + user + " Ange val.\n1: Se dina konton och saldo\n2:Överföring mellan konton\n3:Ta ut pengar\n4:Logga ut");
                
                
                input = byte.TryParse(Console.ReadLine(), out KontoVal);
                    if (input == false)
                    {
                        Console.WriteLine("Ogiltigt val!");
                        Thread.Sleep(1200);
                    }

                   }while (input == false);

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
                        Thread.Sleep(1200);
                        break;


                }

            } while (LogIn == true);
        }
        public static void Transfer(byte ID,string user, double[][] kontoinfo, string Pincode, string[][] kontoNamn)
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
                    if (FromAccount > kontoinfo[ID].Length-1 | FromAccount < 0)
                    {
                        Console.WriteLine("Det kontot finns inte.");
                    }
                } while (APar == false | (FromAccount > kontoinfo[ID].Length-1 | FromAccount < 0));

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
                    if (Amount <= 0)
                    {
                        Console.WriteLine("Felaktigt summa!");
                    }
                } while (APar == false | Amount > kontoinfo[ID][FromAccount] | Amount <= 0);

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
                Spara(kontoinfo, kontoNamn);
                VisaKonton(ID,user, kontoinfo, kontoNamn);
                Console.WriteLine("Du har överfört "+Amount+ " Från "+kontoNamn[ID][FromAccount] +"till "+kontoNamn[ID][ToAccount]);
                Console.WriteLine("Tryck enter för att fortsätta");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                konto(ID,user, kontoinfo, Pincode, kontoNamn);

            }
        }
        public static void TaUt(byte ID, string user, double[][] kontoinfo, string Pincode, string[][] kontoNamn)
        {
            VisaKonton(ID,user, kontoinfo, kontoNamn);
            double Amount;
            byte FromAccount;
            bool APar;
            string TryPinCode;
            do
            {
                Console.WriteLine("Konto vill du ta ut pengar ifrån?");
                APar = byte.TryParse(Console.ReadLine(), out FromAccount);
                if (APar == false)
                {
                    Console.WriteLine("Felaktigt kontonummer!");
                }
                if (FromAccount > kontoinfo[ID].Length-1 | FromAccount < 0)
                {
                    Console.WriteLine("Det kontot finns inte.");
                }
            } while (APar == false | (FromAccount >kontoinfo[ID].Length-1 | FromAccount < 0));
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


            do
            {
                Console.WriteLine("Skriv in pinkod.");
                TryPinCode = Console.ReadLine();
                if (TryPinCode == Pincode)
                {
                    kontoinfo[ID][FromAccount] = kontoinfo[ID][FromAccount] - Amount;

                    Console.WriteLine("Du har tagit ut " + Amount + "kr från " + kontoNamn[ID][FromAccount] + ": " + kontoinfo[ID][FromAccount]);
                    Spara(kontoinfo, kontoNamn);
                    Console.WriteLine("Tryck enter för att fortsätta");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                }
                else
                {
                    Console.WriteLine("Fel pinkod!");
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            } while (TryPinCode != Pincode);


            konto(ID, user, kontoinfo, Pincode, kontoNamn);
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
      public static void Spara(double[][] KontonArray,string[][] Kontonamn)
        {
            string fileName = @".\BankKontoinfo.txt";
            string fileName2 = @".\BankkontoNamn.txt";

            using (TextWriter sw = File.CreateText(fileName))//Sparar in värderna ur KontoArray till textfil
                {
                    for (int id = 0; id < 5; id++)
                    {
                        sw.WriteLine("|");
                        for (int i = 0; i < KontonArray[id].Length; i++)
                        {
                            sw.WriteLine(KontonArray[id][i]);
                        }
                    }
                }
                using (TextWriter sw = File.CreateText(fileName2))
                {
                    for (int id = 0; id < 5; id++)
                    {
                        sw.WriteLine("|");
                        for (int i = 0; i < Kontonamn[id].Length; i++)
                        {
                            sw.WriteLine(Kontonamn[id][i]);
                        }
                    }
                }
        }
        public static void Ladda(double[][] KontonArray, string[][] Kontonamn)
        {
            string fileName = @".\BankKontoinfo.txt";
            string fileName2 = @".\BankkontoNamn.txt";
            using (StreamReader sr = File.OpenText(fileName))
            {
                int Row = -1;
                byte Col = 0;
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s == "|")
                    {
                        Row++;
                        Col = 0;
                    }
                    else if (s != "|")
                    {
                        KontonArray[Row][Col] = double.Parse(s);
                        Col++;
                    }
                }
            }
            using (StreamReader sr = File.OpenText(fileName2))
            {
                int Row = -1;
                byte Col = 0;
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s == "|")
                    {
                        Row++;
                        Col = 0;
                    }
                    else if (s != "|")
                    {
                        Kontonamn[Row][Col] = s;
                        Col++;
                    }
                }
            }
        }



    }
}
        

    
