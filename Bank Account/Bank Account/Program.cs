using Bank_Library;

var bank = new Bank();

int eingabe;
int anmeldungeingabe;
bool Konto = true;
bool Optionen = true;
bool Programm = false;

string benutzername = default;
string passwort = default;

bank.LoadData();

do 
{

    do
    {
        Konto = true;

        Console.WriteLine("Möchten Sie sich 1) anmelden oder 2) registrieren");
        int.TryParse(Console.ReadLine(), out anmeldungeingabe);

        switch (anmeldungeingabe)
        {
            case 1:
                // Anmelden
                Console.Write("Benutzername: ");
                benutzername = Console.ReadLine();
                Console.Write("Passwort: ");
                passwort = Console.ReadLine();
                if (bank.ExistAccount(benutzername, passwort))
                {
                    Console.WriteLine($"Sie sind nun als {benutzername} angemeldet!     \nWillkommen!!");
                    Konto = false;
                }
                else
                    Console.WriteLine("Account nicht gefunden.");
                break;
            case 2:
                // Registrieren
                Console.Write("Benutzername: ");
                benutzername = Console.ReadLine();
                Console.Write("Passwort: ");
                passwort = Console.ReadLine();
                if (bank.ExistAccount(benutzername, passwort))
                {
                    Console.WriteLine("Account existiert bereits");

                }
                else
                {
                    Console.Write("Passwort bestätigen: ");
                    var passwortbest = Console.ReadLine();
                    if (passwort == passwortbest)
                    {
                        Console.WriteLine("Sie sind jetzt registriert!!     \nWillkommen bei Bank .... !!");
                        Konto = false;
                        bank.AccountHinzu(0, benutzername, passwort);
                    }
                }
                break;
        }
        Console.WriteLine("Drücken Sie eine Taste um fortzufahren....");
        Console.ReadKey();
        Console.Clear();
    } while (Konto);


    do
    {
        Optionen = true;
        //Console.WriteLine("1. Konto erstellen");

        Console.WriteLine("Optionen:");
        Console.WriteLine("1. Kontostand anzeigen");
        Console.WriteLine("2. Geld einzahlen");
        Console.WriteLine("3. Geld abheben");
        Console.WriteLine("4. Konto entfernen");
        Console.WriteLine("5. Zahlungs-Historie anzeigen");
        Console.WriteLine("6. Abmelden");

        int.TryParse(Console.ReadLine(), out eingabe);
       
        switch (eingabe)
        {
            /*
            //case 1: // Konto erstellen

                //Console.Write("Benutzername: ");
                //benutzername = Console.ReadLine();
                //Console.Write("Passwort: ");
                //passwort = Console.ReadLine();
                //Console.Write("Anfangsbetrag: ");
                //if (double.TryParse(Console.ReadLine(), out var anfangsbetrag))
                //{
                //    if (bank.AccountHinzu(anfangsbetrag, benutzername, passwort))
                //        Console.WriteLine("Erstellen erfolgreich.");
                //    else
                //        Console.WriteLine("Konto konnte nicht hinzugefügt werden.");
                //}
                //else
                //    Console.WriteLine("Ungültigeer Anfangsbetrag.");
                //break;
                */

            case 1: // Kontostand anzeigen
                var kontostandcheck = bank.Kontostand(benutzername, passwort);
                Console.WriteLine(kontostandcheck.HasValue ? $"Ihr Kontostand: {kontostandcheck.Value} Euro" : "Kontostand konnte nicht abgerufen werden.");
                break;

            case 2: // Geld einzahlen
                Console.Write("Einzahlungsbetrag: ");
                if (double.TryParse(Console.ReadLine(), out var einzahlenMenge))
                {
                    if (bank.Einzahlen(einzahlenMenge, benutzername, passwort))
                        Console.WriteLine("Einzahlen erfolgreich.");
                    else
                        Console.WriteLine("Einzahlen fehlgeschlagen.");
                }
                else
                    Console.WriteLine("Ungültiger Einzahlungsbetrag.");
                break;

            case 3: // Geld abheben
                Console.Write("Abhebungsbetrag: ");
                if (double.TryParse(Console.ReadLine(), out var abhebenMenge))
                {
                    if (bank.Abheben(abhebenMenge, benutzername, passwort))
                        Console.WriteLine("Abhebung erfolgreich.");
                    else
                        Console.WriteLine("Abhebung fehlgeschlagen.");
                }
                else
                {
                    Console.WriteLine("Ungültiger Abhebungsbetrag.");
                }
                break;

            case 4: // Konto entfernen
                Console.WriteLine("Sind Sie sich sicher? (Ja/Nein)");
                string delant = Console.ReadLine();
                delant = delant.ToLower();
                if (delant == "ja")
                {
                    Console.WriteLine("Geben Sie Ihr Passwort ein");
                    string bestätigung = Console.ReadLine();
                    if (bestätigung == passwort)
                    {
                        kontostandcheck = bank.Kontostand(benutzername, passwort);
                        if (kontostandcheck.Value == 0)
                        {
                        bank.AccountEntf(benutzername, passwort);
                        Console.WriteLine("Ihr Konto wurde gelöscht");
                        Optionen = false;
                        break;
                        }
                        else
                            Console.WriteLine("Sie haben noch Geld/Schulden auf Ihrem Konto | Vorgang abgebrochen");
                    }
                }
                else if (delant == "nein")
                    break;
                else
                {
                    Console.WriteLine("Ungültige Antwort");
                    break;
                }
                break;
            case 5: // Zahlungs-Historie anzeigen
                var historie = bank.AbrufZahlungsHistorie(benutzername, passwort);
                if (historie != null && historie.Count > 0)
                {
                    Console.WriteLine("Ihre Zahlungs-Historie:");
                    foreach (var transaction in historie)
                    {
                        Console.WriteLine(transaction);
                    }
                }
                else
                {
                    Console.WriteLine("Keine Transaktionen gefunden.");
                }
                break;
            case 6: // Ausloggen
                Console.WriteLine("Ausgeloggt");
                benutzername = default;
                passwort = default;
                Optionen = false;
                break;

            default:
                Console.WriteLine("Ungültige Option.");
                break;
        }

        Console.WriteLine("Drücken Sie eine Taste um fortzufahren....");
        Console.ReadKey();
        Console.Clear();
    } while (Optionen);

    Console.WriteLine("Möchten Sie sich (1) Anmelden/Registrieren oder (2) das Programm schließen?");
    int.TryParse(Console.ReadLine(), out int result);
    if (result == 1)
        Programm = true;
    else
        Programm = false;
} while (Programm);

bank.SaveData();