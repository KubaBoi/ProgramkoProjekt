namespace ProgramkoProjekt;

/// <summary>
/// Trida pro vsechny texty aplikace
/// </summary>
public class Texts
{
    public const string DELIMITER = "<=====================================>\n";

    public const string MAIN_MENU = """
        
        # Penezenka:

        Pomoc                   [h]
        
        Nacist penezenku        [q]
        Ulozit penezenku        [w]

        Pridat vydaj            [a]
        Poslednich x vydaju     [s <x>] (pokud x chybi, pak se vypise vse)
        Vydaje za x dni zpet    [d <x>] (pokud x chybi, pak se vypise dnes)
        Vydaje podle kategorie  [f <kategorie>]

        Cesta nactene penezenky [v]
        Nastavit razeni         [c <razeniPodle> <smer>] (vychozi razeni je '0' a smer '1')
        Vypsat kategorie        [y]
        Odejit                  [x]

        # Razeni                    Smer razeni:
        -----------------------------------------
        '0' podle datumu *          '0' vzestupne
        '1' podle vysky castky      '1' sestupne *
        '2' podle kategorie
        '3' podle popisu

        samotne 'c' bez parametru resetuje nastaveni na vychozi
        """;

    public const string SELECT_MENU = "Zadejte volbu (pro pomoc [h])";
    public const string INPUT_FILE = "Zadejte cestu k souboru";
    public const string EXPENSE_ADDED = "Vydaj pridan";
    public const string WALLET_LOADED = "Penezenka uspesne nactena:\n'{0}'";
    public const string WALLET_SAVED = "Penezenka uspesne ulozena:\n'{0}'";

    public const string ITEMS_COUNT = "Pocet polozek: {0}";
    public const string ITEMS_SUM = "Soucet polozek: {0}";
    public const string ORDER_SETTINGS = "Nastaveni razeni: '{0}' - {1}";

    public const string ASK_RELOAD = """
        Jiz je nactena penezenka, prejete si ji prepsat? 
        Prijdete tak o neulozena data.
        """;
    public const string ASK_REWRITE = """
        Nactena penezenka jiz ma vytvoreni soubor, prejete si export do toho souboru?
        Soubor '{0}' bude prepsan.
        """;
    public const string ASK_EXIT = """
        Opravdu si prejete odejit?
        Prijdete tak o neulozena data.
        """;

    // Varovani
    public const string EMPTY_INPUT = "Prazdne zadani";
    public const string INPUT_NOT_NUMBER = "Zadana hodnota neni cislo";

    // Chyby
    public const string UNKNOWN_COMMAND = "Neznamy prikaz :(";
    public const string FILE_NOT_FOUND = "Soubor neexistuje";
    public const string EMPTY_WALLET = "Neni nactena penezenka";
    public const string FORMAT_ERROR = "Chyba formatu na radce: {0}";
    public const string COST_FORMAT_ERROR = "Na radce: {0} neni hodnota ceny spravna: '{1}'";
    public const string TIME_FORMAT_ERROR = "Na radce: {0} neni hodnota casu spravna: '{1}'";
    public const string MISSING_CATEGORY = "Musite zadat kategorii";
    public const string SETTING_ERROR_SET = "Nepovedlo se nastavit hodnotu '{0}'";

    public const string READ_FILE_EXCEPTION = "Behem nacitani nastala neocekavana chyba:\n{0}";
    public const string WRITE_FILE_EXCEPTION = "Behem ukladani nastala neocekavana chyba:\n{0}";

    // Polozky
    public const string TIME = "Datum";
    public const string COST = "Cena";
    public const string CATEGORY = "Kategorie";
    public const string DESCRIPTION = "Popis";
    public const string COUNT = "Pocet polozek";
}

