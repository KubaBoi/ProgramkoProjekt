# Projekt pro PROGRAMOVÁNÍ (programy INFO ETE15E, SYI ETE56E, TF ETE28E )

Jakub Anderle, xandj017@studenti.czu.cz

# Peněženka

## Popis

Program pro správu finančních výdajů. 

Umožňuje:

- přidávat výdaje
- ukládat/načítat soubory s výdaji
- vypisovat výdaje podle kategorie nebo dne kdy byly zadány, ve formátu tabulek
- řadit vypisované výdaje

Generované soubory s uloženými výdaji jsou ve formátu CSV 
bez hlavičky a s rozdělením pomocí znaku `;`.

## Funkcionality

- `h` - vypíše hlavní menu programu
- `q` - načte peněženku ze souboru
	- pokud již nějaká data načtená jsou, pak se uživatele zeptá jestli chce pokračovat
	- zeptá se uživatele na cestu k souboru (lze zadat i relativní cestu)
- `w` - uloží data do souboru
	- pokud již proběhlo načtení dat, pak se uživatele zeptá jestli chce uložit nová data do starého souboru (pokud ano, tak je přeskočen následující krok)
	- zeptá se uživatele na cestu kam uložit soubor (lze zadat i relativní cestu)
- `a` - vytvoření nového záznamu výdaje
	- postupně se uživatele zeptá na položky výdaje
- `s <x>` - vypíše posledních `x` záznamů (posledních podle nastaveného řazení)
	- pokud není `x` zadáno, pak se vypše vše
- `d <x>` - vypíše výdaje pro jeden den
	- `x` je o kolik dnů zpět (1 = včera, 2 = předevčírem, ...)
	- pokud není `x` vyplněno, tak vypíše dnešní výdaje
- `f <kategorie>` - vypíše výdaje s danou kategorií
- `v` - vypíše cestu k poslednímu načtenému souboru
- `c <razeniPodle> <smer>` - nastavení řazení dat
	- `razeniPodle` - může nabývat hodnot 0 až 4
		- 0 - Date (výchozí)
		- 1 - Cost
		- 2 - Category
		- 3 - Description
	- `smer` - může nabývat hodnot 0 až 1
		- 0 - vzestupně
		- 1 - sestupně (výchozí)
	- samotný příkaz `c` bez parametrů nastaví výchozí řazení
- `y` - vypíše tabulku s použitými kategoriemi a počty záznamů pro každou kategorii
- `x` - zavření programu
	- zeptá se uživatele jestli vážně chce odejít
## Popis tříd

Program je rozdělen na několik funkčních tříd:

### Wallet

Hlavní třída pěnženky, vypisuje menu, přijímá vstupy a spouští 
jednotlivé příkazy.

Dále udržuje data o načtených výdajích, cestu k načtenému souboru
a nastavení řazení.

### Expense

Datové `record`, který reprezentuje jeden výdaj. 
Obsahuje položky:

- `Date` datum zadání
- `Cost` částka výdaje
- `Category` kategorie pro shlukování výdajů
- `Description` krátký popis výdaje

### FileService

Třída obsahuje metody `LoadWallet` a `ExportWallet`. 
Slouží pro načítání a ukládání dat do souborů.

Kontroluje existenci souboru v případě načítání a celý proces čtení/psaní
je podchycen `try-catch` blokem pro neočekávané výjimky.

### InputService

Třída obsahuje metody pro načítání vstupu uživatele z konzole.

### TablePrinter

Tato třída obsahuje metody pro vypisování výdajů na obrazovku
ve formě tabulek.

Umožňuje řazení nebo filtraci dat.

### Texts

Třída pouze poskytuje statické konstatní proměnné s texty programu.


