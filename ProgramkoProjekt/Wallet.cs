using System.Threading;

namespace ProgramkoProjekt;

/// <summary>
/// Hlavní třída peněženky, spravuje výdaje i soubory.
/// </summary>
public class Wallet
{
    private List<Expense>? _wallet;
    private string? _walletPath;

    private string orderBy = "0";
    private string orderDir = "1";

    public void MainLoop()
    {
        // prvotni vypsani menu
        Console.WriteLine(Texts.MAIN_MENU);

        while (true)
        {
            Console.WriteLine(Texts.DELIMITER);
            string input = InputService.InputLine(Texts.SELECT_MENU, "h");
            char first = input.ToCharArray()[0];
            Console.WriteLine(Texts.DELIMITER);

            switch (first)
            {
                // help
                case 'h':
                    Console.WriteLine(Texts.MAIN_MENU);
                    break;

                // nacitani/ukladani
                case 'q': // nacist
                    Import();
                    break;
                case 'w': // export
                    Export();
                    break;

                // sprava vydaju
                case 'a': // pridat vydaj
                    AddExpense();
                    break;
                case 's': // poslednich x vydaju
                    TablePrinter.PrintLastExpenses(input, _wallet, orderBy, orderDir);
                    break;
                case 'd': // vydaje den
                    TablePrinter.PrintLastDayExpenses(input, _wallet, orderBy, orderDir);
                    break;
                case 'f': // vydaje kategorie
                    TablePrinter.PrintByCategory(input, _wallet, orderBy, orderDir);
                    break;

                // ovladani
                case 'v':
                    Console.WriteLine(_walletPath);
                    break;
                case 'c': // razeni
                    SetOrder(input);
                    break;
                case 'y': // kategorie
                    TablePrinter.PrintCategories(_wallet);
                    break;
                case 'x': // odejit
                    if (InputService.AskLine(Texts.ASK_EXIT))
                    {
                        return;
                    }
                    break;
                default:
                    Console.WriteLine(Texts.UNKNOWN_COMMAND);
                    break;
            }
            Console.WriteLine();
        }
    }

    private void Import()
    {
        // penezenka je nactena,
        // tak se vypise dialog, jestli uzivatel vazne chce data prepsat
        if (_wallet is not null && !InputService.AskLine(Texts.ASK_RELOAD))
        {
            return;
        }

        var path = InputService.InputLine(Texts.INPUT_FILE);

        // nacteni penezenky, pokud bude penezenka null (nepodari se nacist)
        // pak se pouzije drive nactena
        _wallet = FileService.LoadWallet(path, ref _walletPath) ?? _wallet;
    }

    private void Export()
    {
        var path = _walletPath;

        // pokud penezenka neni nactena ze souboru NEBO
        // pokud je penezenka nactena ze soubrou, tak se vypise dialog, jestli chce 
        // uzivatel vyuzit ten soubor pro ulozeni, aby nemusel zadavat zase cestu
        if (path is null ||
            (path is not null && !InputService.AskLine(string.Format(Texts.ASK_REWRITE, path))))
        {
            // ziskani nove cesty
            path = InputService.InputLine(Texts.INPUT_FILE, _walletPath);
        }

        FileService.ExportWallet(path!, _wallet, ref _walletPath);
    }

    /// <summary>
    /// Pridani noveho zaznamu vydaje
    /// <para>
    /// U kategorie a popisu zmeni pripadne ';' na ',',
    /// aby se nenarusila struktura csv
    /// </para>
    /// </summary>
    private void AddExpense()
    {
        var time = DateTime.Now;
        var cost = InputService.InputLineInt(Texts.COST);
        var category = InputService.InputLine(Texts.CATEGORY)
            .Replace(';', ',');
        var desc = InputService.InputLine(Texts.DESCRIPTION, "")
            .Replace(';', ',');

        _wallet ??= [];
        _wallet.Insert(0, new Expense(time, cost, category, desc));
        Console.WriteLine(Texts.EXPENSE_ADDED);
    }

    /// <summary>
    /// Nastaveni razeni
    /// </summary>
    /// <param name="input">Vstup z klavesnice 'c sloupec smer'</param>
    private void SetOrder(string input)
    {
        var parts = input.Split(' ');
        var ob = parts.Length >= 2 ? parts[1] : "0";
        var od = parts.Length >= 3 ? parts[2] : "1";

        CheckOrderRange(ob, '0', '3', ref orderBy);
        CheckOrderRange(od, '0', '1', ref orderDir);
    }

    /// <summary>
    /// Zkontroluje vstup nastaveni a nastavi osekane hodnoty
    /// </summary>
    /// <param name="inp">Vstup uzivatele</param>
    /// <param name="start">Zacatek moznosti hodnot</param>
    /// <param name="end">Konec moznosti hodnot</param>
    /// <param name="val">Property, ktera se ma nastavit</param>
    private static void CheckOrderRange(string inp, char start, char end, ref string val)
    {
        char c = inp.ToCharArray()[0];
        if (c >= start && c <= end)
        {
            val = c.ToString();
            return;
        }
        Console.WriteLine(Texts.SETTING_ERROR_SET, inp);
    }
}
