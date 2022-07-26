# Alternative-Prüfung-Softwareentwicklung

| Parameter                | Informationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung**       | Projekt Softwareecntwicklung                                                                                                                                                           |
| **Semester**               |   Sommersemester 2022                                                                                                                                                                                        |
| **Hochschule**          | Technische Universität Bergakademie Freiberg                                                                                                                                                      
| **Autoren**              | Lion Waurich (65695) / Caio Marcas Menz (66654)                            

---------------------------------------------------------------------------------
# Chronologie 

## Herangehensweise an die Software des Respberry Pi's ##

Die Software wird objektorientiert programmiert und und beinhaltet 2 Klassen. Die Klassen sind CStunden und CTag, wobei CTag ein eindimensionales Array im Umfang von 24 von dem Datentyp CStunden einschließt. Somit muss CStunden eine Komposition von CTag sein wie man in folgender Grafik entnehmen kann.

![Klassendiagramm](https://github.com/Lion127/Softwareentwicklungsprojekt-2022/blob/main/Grafiken/Programm_Entwicklung/Klassendiagramm.png)


### CStunde ###
Die Klasse CStunden ist für die aktuelle Ein-/Ausgabe von/auf den peripheren Komponenten auf dem Raspberry Pi zuständig,
diese erstellt für jedes Stundenobjekt die Durchschnittswerte der 3 Attribute Temperatur, Luftfeuchtigkeit und Gas-Qualität bei Anbruch der nächsten Stunde.

### CTag ###

```csharp                                      Usage

public tag(DateTime _now, stunden _Stunde) //Überladener Constructor
{
    File_Header = "[ " + _now.ToString("d") + ":\n"; //Header von txt-File soll mit "[" anfangen, für Nachvollziehbarkeit
    Stunde[_now.Hour] = _Stunde; //Stunden-Objekt soll auf eigenen Stundenarray überschrieben werden
}

```

