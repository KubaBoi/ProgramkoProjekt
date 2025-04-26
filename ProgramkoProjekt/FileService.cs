using System.IO;

namespace ProgramkoProjekt;

/// <summary>
/// Třída pro načítání a ukládání souborů peneženky.
/// </summary>
public class FileService
{

    /// <summary>
    /// Nacteni souboru s penezenkou
    /// </summary>
    /// <param name="filePath">Cesta k souboru</param>
    /// <param name="walletPath">Reference promenne, do ktere bude v pripade uspechu zapsana cela cesta souboru</param>
    /// <returns>Nactena data, pripade chyby <see langword="null"/></returns>
    public static List<Expense>? LoadWallet(string filePath, ref string? walletPath)
    {
        // pridani suffixu souboru
        if (!filePath.EndsWith(".csv"))
        {
            filePath += ".csv";
        }

        // kontrola jestli soubor existuje
        if (!File.Exists(filePath))
        {
            Console.WriteLine(Texts.FILE_NOT_FOUND);
            return null;
        }

        // inicializace promennych
        List<Expense> resp = [];
        string? line;
        int index = 0;

        try
        {
            // otevreni souboru
            using StreamReader reader = new(filePath);

            // prochazi se vsechny radky
            while ((line = reader.ReadLine()) is not null)
            {
                // rozdeleni radky podle ;
                index++;
                string[] parts = line.Split(";");

                // kontrola jestli radka obsahuje vsechny polozky (jinak chyba)
                if (parts.Length < 4)
                {
                    Console.WriteLine(Texts.FORMAT_ERROR, index);
                    return null;
                }

                // pokus parsovani dateTime pro polozku Date
                if (!DateTime.TryParse(parts[0], out var date))
                {
                    Console.WriteLine(Texts.TIME_FORMAT_ERROR, index, parts[0]);
                    return null;
                }
                // pokud parsovani integer pro polozku cost
                if (!int.TryParse(parts[1], out var cost))
                {
                    Console.WriteLine(Texts.COST_FORMAT_ERROR, index, parts[1]);
                    return null;
                }

                // pridani nove polozky do kolekce
                resp.Add(new Expense(date, cost, parts[2], parts[3]));
            }

            walletPath = ((FileStream)(reader.BaseStream)).Name;
        }
        catch (Exception ex)
        {
            // zachyceni necekanych vyjimek
            Console.WriteLine(Texts.READ_FILE_EXCEPTION, ex.Message);
            return null;
        }

        Console.WriteLine(Texts.WALLET_LOADED, walletPath);
        // serazeni pro defaultni stav razeni
        return [.. resp.OrderByDescending(x => x.Date)];
    }

    /// <summary>
    /// Ulozi data penezenky do souboru
    /// </summary>
    /// <param name="filePath">Cesta k souboru</param>
    /// <param name="expenses">Data k ulozeni</param>
    /// <param name="walletPath">Reference promenne, do ktere bude v pripade uspechu zapsana cela cesta souboru</param>
    public static void ExportWallet(string filePath,
                                    List<Expense>? expenses,
                                    ref string? walletPath)
    {
        // kontrola jestli ukladana ponezenka neni null
        if (expenses is null || expenses.Count == 0)
        {
            Console.WriteLine(Texts.EMPTY_WALLET);
            return;
        }

        // pridani suffixu souboru
        if (!filePath.EndsWith(".csv"))
        {
            filePath += ".csv";
        }

        try
        {
            // otevreni souboru
            using StreamWriter file = new(filePath, false);
            foreach (var exp in expenses)
            {
                // vypsani kazde polozky do souboru jako samostatny radek
                file.WriteLine($"{exp.Date};{exp.Cost};{exp.Category};{exp.Description}");
            }

            walletPath = ((FileStream)(file.BaseStream)).Name;
        }
        catch (Exception ex)
        {
            // zachyceni necekanych vyjimek
            Console.WriteLine(Texts.WRITE_FILE_EXCEPTION, ex.Message);
            return;
        }

        Console.WriteLine(Texts.WALLET_SAVED, walletPath);
    }
}

