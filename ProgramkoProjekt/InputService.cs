namespace ProgramkoProjekt;

/// <summary>
/// Trida pro zpracovavani vtupu uzivatele
/// </summary>
public class InputService
{
    /// <summary>
    /// Vypise output + ': ' a na stejne radce ocekava vstup.
    /// <para>
    /// Pokud je def <see langword="null"/>, pak se cteni opakuje dokud uzivatel nevlozi neprazdny vstup
    /// </para>
    /// </summary>
    /// <param name="output">Prefix inputu</param>
    /// <param name="def">Defaultni input v pripade, ze uzivatel vlozi prazdny vstup</param>
    /// <returns>Vstup uzivatele</returns>
    public static string InputLine(string output, string? def = null)
    {
        while (true)
        {
            Console.Write(output);
            Console.Write(": ");
            string? input = Console.ReadLine();
            if (input == "") input = null;

            if (def is null &&
                (input is null || input.Length == 0))
            {
                Console.WriteLine(Texts.EMPTY_INPUT);
                continue;
            }

            // tohle je osetreny v predchozi podmince,
            // ale VS to nepochopi a oznacuje mi to jako warning
            return input ?? def!;
        }
    }

    /// <summary>
    /// Stejne jako <see cref="InputLine(string, string?)"/>, 
    /// ale pro integer a bez mozne defaultni hodnoty.
    /// </summary>
    /// <param name="output">Prefix inputu</param>
    /// <returns>Ciselny vstup uzivatele</returns>
    public static int InputLineInt(string output)
    {
        while (true)
        {
            string input = InputLine(output);
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            Console.WriteLine(Texts.INPUT_NOT_NUMBER);
        }
    }

    /// <summary>
    /// Vypise tazajici dialog. Prida za output ' [y/n]: '
    /// </summary>
    /// <param name="output"></param>
    /// <returns>
    /// Pokud uzivatel zada 'y', pak vraci <see langword="true"/>.
    /// Jinak vrací <see langword="false"/>.
    /// </returns>
    public static bool AskLine(string output)
    {
        Console.Write(output);
        Console.Write(" [y/n]: ");
        string? input = Console.ReadLine();
        return input == "y";
    }
}
