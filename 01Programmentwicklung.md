# Alternative-Prüfung-Softwareentwicklung

| Parameter                | Informationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung**       | Projekt Softwareentwicklung                                                                                                                                                           |
| **Semester**               |   Sommersemester 2022                                                                                                                                                                                        |
| **Hochschule**          | Technische Universität Bergakademie Freiberg                                                                                                                                                      
| **Autoren**              | Lion Waurich (65695) / Caio Marcas Menz (66654)                            

---------------------------------------------------------------------------------
# Chronologie 

## Herangehensweise an die Software des Respberry Pi's ##

Die Software wird objektorientiert programmiert und und beinhaltet 2 Klassen. Die Klassen sind CStunden und CTag, wobei CTag ein eindimensionales Array im Umfang von 24 von dem Datentyp CStunden einschließt. Somit muss CStunden eine Komposition von CTag sein wie man in folgender Grafik entnehmen kann.

## Erstes Klassendiagramm

![erstes Klassendiagramm](https://github.com/Lion127/Softwareentwicklungsprojekt-2022/blob/main/Grafiken/Programm_Entwicklung/erstes_Klassendiagramm.png)

Das folgende Bild zeigt die Grundidee unserer Software für den Raspberry Pi, bei dem 5 Klassen inkludiert sind, wobei die Klasse CJahr die übergeordnete Klasse von allen ist. Die untergeordneten Klassen sind jeweils Kompositionen der nächst übergeordneten Klasse. Die Daten sollten ursprünglich über die ganze Laufzeit des Programms in den Objekten der Klassen gespeichert werden werden, was aber einen unnötig hohen Speicherverbrauch aufweisen und im schlimmsten Fall zu Datenanomalien führen würde. Somit wurde das Klasssendiagramm auf das Nötigste runtergebrochen und letzendlich nur durch Zwei Klassen realisiert. Nämlich CTag und Seiner Komposition CStunden.

## Endgültiges Klassendiagramm
![engültiges Klassendiagramm](https://github.com/Lion127/Softwareentwicklungsprojekt-2022/blob/main/Grafiken/Programm_Entwicklung/endg%C3%BCltiges_Klassendiagramm.png)

Hier zu sehen ist das endgültige Klassendiagramm mit nur zwei Klassen, wobei CStunden nur noch den Durchschnitt der Rohdaten über die jeweilige Stunde in CTag speichert und CTag diese bei Alauf des Tages in eine txt-Datei speichert. Somit wird verhindert, dass die Werte über die Laufzeit lokal im Programm gespeichert werden und im Falle eines Ausfalls des Raspberry Pi's diese nicht verloren gehen. Zudem ist der Code somit auch übersichtlicher, da man ein Jahr genauso mit 365 Tagen realisieren kann ohne noch CWoche, CMonat und CJahr zu implementieren, wie man aus dem ersten Klassendiagramm entnehmen kann.

### CStunde ###
Die Klasse CStunden ist für die aktuelle Ein-/Ausgabe von/auf den peripheren Komponenten auf dem Raspberry Pi zuständig,
diese erstellt für jedes Stundenobjekt die Durchschnittswerte der 3 Attribute Temperatur, Luftfeuchtigkeit und Gas-Qualität bei Anbruch der nächsten Stunde, und speichert diese in seiner übergeordneten Klasse CTag.

CStunde umfasst 7 Methoden wobei eine davon ein überladener Konstruktor ist

#### Überladener Konstruktor
```csharp                                      Usage
public stunden(int _hour, int _min) //Überladener Constructor
{
    hour = _hour; //Aktuelle Stunde auf Attribut speichern
    start_min = _min; //Minute bei dem der Raspberry gestartet hat
}
```
Der überladene Konstruktor soll zu Beginn der Programmdurchführung die Startstunde erfassen, um diese als Startindex für das Stundenarray in CTag nutzen. Somit wird der Stundenbeginn für eine spätere Methode ```void Average()``` von großem Nutzen sein.
Bei der Startminute ist es genauso, diese wird auch beim Programmstart erfasst, um später von dort aus den Durchschnitt der beschriebenen Arrays zu berechnen, ansonsten würden fehlerhafte Werte auftreten, die gegen 0 gehen, da die Arrays mit 0 initialisiert werden.



### CTag ###


```csharp                                      Usage

public tag(DateTime _now, stunden _Stunde) //Überladener Constructor
{
    File_Header = "[ " + _now.ToString("d") + ":\n"; //Header von txt-File soll mit "[" anfangen, für Nachvollziehbarkeit
    Stunde[_now.Hour] = _Stunde; //Stunden-Objekt soll auf eigenen Stundenarray überschrieben werden
}

```

