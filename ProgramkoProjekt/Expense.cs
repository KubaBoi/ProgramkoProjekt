namespace ProgramkoProjekt;

/// <summary>
/// Record reprezentující záznam výdaje.
/// </summary>
/// <param name="Date">Datum zadani</param>
/// <param name="Cost">Castka vydaje</param>
/// <param name="Category">Kategorie vydaje</param>
/// <param name="Description">Libovolny popis</param>
public record Expense(DateTime Date,
                      int Cost,
                      string Category,
                      string Description)
{
}

