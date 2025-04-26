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

Hlavní třída p�n�enky, vypisuje menu, p�ij�m� vstupy a spou�t� 
jednotliv� p��kazy.

D�le udr�uje data o na�ten�ch v�daj�ch, cestu k na�ten�mu souboru
a nastaven� �azen�.

### Expense

Datov� `record`, kter� reprezentuje jeden v�daj. 
Obsahuje polo�ky:

- `Date` datum zad�n�
- `Cost` ��stka v�daje
- `Category` kategorie pro shlukov�n� v�daj�
- `Description` kr�tky popis v�daje

### FileService

T��da obsahuje metody `LoadWallet` a `ExportWallet`. 
Slou�� pro na��t�n� a ukl�d�n� dat do soubor�.

Kontroluje existenci souboru v p��pad� na��t�n� a cel� proces �ten�/psan�
je podchycen `try-catch` blokem pro neo�ek�van� v�jimky.

### InputService

T��da obsahuje metody pro na��t�n� vstupu u�ivatele z konzole.

### TablePrinter

Tato t��da obsahuje metody pro vypisov�n� v�daj� na obrazovku
ve form� tabulek.

Umo��uje �azen� nebo filtraci dat.

### Texts

T��da pouze dr�� statick� konstatn� prom�nn� s texty programu.


