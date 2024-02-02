using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Contribuente contribuente = new Contribuente();
            contribuente.GetAndShowData();
        }

        public class Contribuente
        {
            private string nome, cognome, gender, codiceFiscale, comuneResidenza;
            private int redditoAnnuale;
            private DateOnly dataNascita;

            public string Nome
            {
                get
                {
                    return nome;
                }
                set
                {
                    nome = value;
                }
            }

            public string Cognome
            {
                get
                {
                    return cognome;
                }
                set
                {
                    cognome = value;
                }
            }

            public string Gender
            {
                get
                {
                    return gender;
                }
                set
                {
                    gender = value;
                }
            }

            public string CodiceFiscale
            {
                get
                {
                    return codiceFiscale;
                }
                set
                {
                    codiceFiscale = value;
                }
            }

            public string ComuneResidenza
            {
                get
                {
                    return comuneResidenza;
                }
                set
                {
                    comuneResidenza = value;
                }
            }

            public int RedditoAnnuale
            {
                get
                {
                    return redditoAnnuale;
                }
                set
                {
                    redditoAnnuale = value;
                }
            }

            public DateOnly DataNascita
            {
                get
                {
                    return dataNascita;
                }
                set
                {
                    dataNascita = value;
                }
            }

            public void GetName()
            {
                Console.WriteLine("Scrivi il tuo nome:");
                string input = Console.ReadLine();

                if (input.All(char.IsLetter) && !string.IsNullOrWhiteSpace(input))
                {
                    Nome = input;
                }
                else
                {
                    Console.WriteLine("Scrivi un nome valido \n");
                    GetName();
                }
            }

            public void GetSurname()
            {
                Console.WriteLine("Scrivi il tuo cognome:");
                string input = Console.ReadLine();

                if (input.All(char.IsLetter) && !string.IsNullOrWhiteSpace(input))
                {
                    Cognome = input;
                }
                else
                {
                    Console.WriteLine("Scrivi un cognome valido \n");
                    GetSurname();
                }
            }

            public void GetGender()
            {
                Console.WriteLine("In quale genere ti identifichi? (maschio, femmina, agender, gender fluid...)");
                string input = Console.ReadLine();

                if (input.All(char.IsLetter) && !string.IsNullOrWhiteSpace(input))
                {
                    Gender = input;
                }
                else
                {
                    Console.WriteLine("Scrivi un gender valido (non numerico) \n");
                    GetGender();
                }
            }

            public void GetCodiceFiscale()
            {
                Console.WriteLine("Scrivi il tuo codice fiscale:");
                string input = Console.ReadLine();

                if (input.Length == 16 && Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
                {
                    CodiceFiscale = input.ToUpper();
                }
                else
                {
                    Console.WriteLine("Scrivi un codice fiscale valido \n");
                    GetCodiceFiscale();
                }
            }

            public void GetComuneResidenza()
            {
                Console.WriteLine("Scrivi il tuo comune di residenza:");
                string input = Console.ReadLine();

                if (input.All(char.IsLetter) && !string.IsNullOrWhiteSpace(input))
                {
                    ComuneResidenza = input;
                }
                else
                {
                    Console.WriteLine("Scrivi un comune di residenza valido \n");
                    GetComuneResidenza();
                }
            }

            public void GetRedditoAnnuale()
            {
                Console.WriteLine("Scrivi il tuo reddito annuale:");
                string input = Console.ReadLine();

                bool checkReddito = int.TryParse(input, out int redditoNumber);

                if (checkReddito && redditoNumber >= 0)
                {
                    RedditoAnnuale = redditoNumber;
                }
                else
                {
                    Console.WriteLine("Scrivi una cifra valida (non numeri negativi o lettere): \n");
                    GetRedditoAnnuale();
                }
            }

            public void GetDataNascita()
            {
                Console.WriteLine("Scrivi la tua data di nascita in formato gg/mm/aaaa:");
                string input = Console.ReadLine();

                string formatoData = "dd/MM/yyyy";

                try
                {
                    DateOnly dataInserita = DateOnly.ParseExact(input, formatoData);

                    if (dataInserita > DateOnly.FromDateTime(DateTime.Now))
                    {
                        Console.WriteLine("Inserisci un anno di nascita valido");
                        GetDataNascita();
                        return;
                    };

                    DataNascita = dataInserita;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Formato data non valido. Assicurati di inserire la data nel formato corretto (gg/mm/aaaa).");
                    GetDataNascita();
                }
            }

            public void GetAndShowData()
            {
                GetName();
                Console.Clear();
                GetSurname();
                Console.Clear();
                GetDataNascita();
                Console.Clear();
                GetCodiceFiscale();
                Console.Clear();
                GetGender();
                Console.Clear();
                GetComuneResidenza();
                Console.Clear();
                GetRedditoAnnuale();

                double impostaDovuta = 0;

                if (RedditoAnnuale <= 15000)
                {
                    impostaDovuta = redditoAnnuale * 0.23;
                }
                else if (RedditoAnnuale <= 28000)
                {
                    int eccedenza = RedditoAnnuale - 15000;
                    impostaDovuta = 3450 + (eccedenza * 0.27);
                }
                else if (RedditoAnnuale <= 55000)
                {
                    int eccedenza = RedditoAnnuale - 28000;
                    impostaDovuta = 6960 + (eccedenza * 0.38);
                }
                else if (RedditoAnnuale <= 75000)
                {
                    int eccedenza = RedditoAnnuale - 55000;
                    impostaDovuta = 17220 + (eccedenza * 0.41);
                }
                else if (RedditoAnnuale >= 75001)
                {
                    int eccedenza = RedditoAnnuale - 75000;
                    impostaDovuta = 25420 + (eccedenza * 0.43);
                }
                else
                {
                    Console.WriteLine("Errore nel calcolo dell'imposta, dati forniti non validi");
                }

                Console.Clear();
                Console.WriteLine($"============ CALCOLO DELL'IMPOSTA DA VERSARE ============ \n\nContribuente: {Nome} {Cognome}\nNato il {DataNascita} ({Gender})\nResidente a {ComuneResidenza}\nReddito dichiataro: euro {RedditoAnnuale}\nIMPOSTA DA VERSARE: euro {impostaDovuta}\n\nPremi invio per uscire dall'applicazione");
            }
        }
    }
}
