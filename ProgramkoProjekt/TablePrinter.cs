using System.Runtime.ConstrainedExecution;
using System.Text;

namespace ProgramkoProjekt;

/// <summary>
/// Trida pro vypisovani dat ve tvaru tabulek
/// </summary>
public class TablePrinter
{
    private static readonly int COLUMN_WIDTH = 15;

    private static readonly string[] DATA_HEADER =
    {
        Texts.TIME,
        Texts.COST,
        Texts.CATEGORY,
        Texts.DESCRIPTION
    };

    private static readonly string[] CATEGORY_HEADER =
    {
        Texts.CATEGORY,
        Texts.COUNT
    };

    /// <summary>
    /// Vypise dany pocet polozek.
    /// </summary>
    /// <param name="input">Dany pocet polozek s menu prefixem</param>
    /// <param name="expenses">Kolekce vsech vydaju</param>
    /// <param name="orderBy">Sloupec razeni</param>
    /// <param name="orderDir">Smer razeni</param>
    public static void PrintLastExpenses(string input,
                                         IEnumerable<Expense>? expenses,
                                         string orderBy,
                                         string orderDir)
    {
        // kontrola jestli je nactena penezenka
        if (expenses is null)
        {
            Console.WriteLine(Texts.EMPTY_WALLET);
            return;
        }

        // kontrola jestli je zadan pocet polozek
        string[] parts = input.Split(' ');
        if (parts.Length < 2 || !int.TryParse(parts[1], out var value))
        {
            value = expenses.Count(); // vypise se vse
        }

        // vybrani daneho poctu polozek
        var picked = expenses.Take(value); ;
        PrintExpenses(DATA_HEADER, picked, orderBy, orderDir);
    }

    /// <summary>
    /// Vypise polozky, ktere byly zadany dany pocet dnu dozadu
    /// </summary>
    /// <param name="input">Dany pocet dnu dozadu s menu prefixem</param>
    /// <param name="expenses">Kolekce vsech vydaju</param>
    /// <param name="orderBy">Sloupec razeni</param>
    /// <param name="orderDir">Smer razeni</param>
    public static void PrintLastDayExpenses(string input,
                                            IEnumerable<Expense>? expenses,
                                            string orderBy,
                                            string orderDir)
    {
        // kontrola jestli je nactena penezenka
        if (expenses is null)
        {
            Console.WriteLine(Texts.EMPTY_WALLET);
            return;
        }

        // kontrola jestli je zadan pocet dnu zpet
        string[] parts = input.Split(' ');
        if (parts.Length < 2 || !int.TryParse(parts[1], out var value))
        {
            value = 0; // vypisuji dnesek
        }

        var now = DateTime.Now;
        var picked = expenses
            .Where(x => x.Date.Date == DateTime.Today.AddDays(value * -1));
        PrintExpenses(DATA_HEADER, picked, orderBy, orderDir);
    }

    /// <summary>
    /// Vypise polozky podle zadane kategorie
    /// </summary>
    /// <param name="input">Dana kategorie s menu prefixem</param>
    /// <param name="expenses">Kolekce vsech vydaju</param>
    /// <param name="orderBy">Sloupec razeni</param>
    /// <param name="orderDir">Smer razeni</param>
    public static void PrintByCategory(string input,
                                       IEnumerable<Expense>? expenses,
                                       string orderBy,
                                       string orderDir)
    {
        // kontrola jestli je nactena penezenka
        if (expenses is null)
        {
            Console.WriteLine(Texts.EMPTY_WALLET);
            return;
        }

        // kontrola jestli je zadan pocet dnu zpet
        string[] parts = input.Split(' ');
        if (parts.Length < 2)
        {
            Console.WriteLine(Texts.MISSING_CATEGORY);
            return; // nelze nic vypsat
        }

        // spojeni nazvu kategorie
        var category = string.Join(" ", parts.Skip(1));
        var picked = expenses
            .Where(x => x.Category == category);
        PrintExpenses(DATA_HEADER, picked, orderBy, orderDir);
    }

    /// <summary>
    /// Vypise tabulku pouzitych kategorii a pocty jejich polozek
    /// </summary>
    /// <param name="expenses">Kolekce vsech vydaju</param>
    public static void PrintCategories(IEnumerable<Expense>? expenses)
    {
        // kontrola jestli je nactena penezenka
        if (expenses is null)
        {
            Console.WriteLine(Texts.EMPTY_WALLET);
            return;
        }

        var categoryCounts = expenses
            .GroupBy(x => x.Category)
            .Select(c => new string[]
            {
                c.Key,
                c.Count().ToString()
            });

        PrintTable(CATEGORY_HEADER, categoryCounts);
    }

    #region Private methods

    /// <summary>
    /// Seradi vybrane polozky
    /// </summary>
    /// <param name="expenses">Vybrane polozky</param>
    /// <param name="orderBy">Sloupec razeni</param>
    /// <param name="orderDir">Smer razeni</param>
    /// <returns>Serazena kolekce</returns>
    private static IEnumerable<Expense> Order(IEnumerable<Expense> expenses,
                                              string orderBy,
                                              string orderDir)
    {
        return orderBy switch
        {
            "0" => orderDir == "0"
            ? expenses.OrderBy(x => x.Date)
            : expenses.OrderByDescending(x => x.Date),

            "1" => orderDir == "0"
            ? expenses.OrderBy(x => x.Cost)
            : expenses.OrderByDescending(x => x.Cost),

            "2" => orderDir == "0"
            ? expenses.OrderBy(x => x.Category)
            : expenses.OrderByDescending(x => x.Category),

            "3" => orderDir == "0"
            ? expenses.OrderBy(x => x.Description)
            : expenses.OrderByDescending(x => x.Description),
            _ => expenses
        };
    }

    /// <summary>
    /// Vypíše tabulku s vybranymi polozkami
    /// </summary>
    /// <param name="headers">Texty hlavicek</param>
    /// <param name="expenses">Vylohy pro vypsani do tabulky</param>
    private static void PrintExpenses(string[] headers,
                                      IEnumerable<Expense> expenses,
                                      string orderBy,
                                      string orderDir)
    {
        expenses = Order(expenses, orderBy, orderDir);
        IEnumerable<string[]> data = expenses
            .Select(x => new string[]
            {
                x.Date.ToString("dd.MM.yyyy"),
                x.Cost.ToString(),
                x.Category,
                x.Description
            });

        PrintTable(headers, data);

        // vypsani souctu castek
        Console.WriteLine(string.Format(Texts.ITEMS_SUM, expenses.Sum(x => x.Cost)));

        string orderByText = headers[int.Parse(orderBy)];
        string orderDirText = orderDir == "1" ? "sestupne" : "vzestupne";

        Console.WriteLine(string.Format(Texts.ORDER_SETTINGS, orderByText, orderDirText));
    }

    /// <summary>
    /// Vypise tabulku s ruznymi daty
    /// </summary>
    /// <param name="headers">Texty hlavicek</param>
    /// <param name="data">Data pro vypsani do tabulky</param>
    private static void PrintTable(string[] headers, IEnumerable<string[]> data)
    {
        // vytvoreni hlavicky
        StringBuilder builder = new StringBuilder();
        foreach (var header in headers)
        {
            builder.Append(header.PadRight(COLUMN_WIDTH));
        }
        builder.AppendLine();
        builder.AppendLine(new string('-', COLUMN_WIDTH * headers.Length));

        // vytvoreni jednotlivych vydaju
        foreach (var d in data)
        {
            foreach (var val in d)
            {
                builder.Append(val.PadRight(COLUMN_WIDTH));
            }
            builder.AppendLine();
        }
        builder.AppendLine();

        // vytvoreni sumarizace dat
        builder.AppendLine(string.Format(Texts.ITEMS_COUNT, data.Count()));

        // vypsani dat
        Console.Write(builder.ToString());
    }

    #endregion
}
