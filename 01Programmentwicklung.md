## Herangehensweise an die Software des Respberry Pi's ##

Die Software wird objektorientiert programmiert und und beinhaltet 2 Klassen. Die Klassen sind CStunden und CTag, wobei CTag ein eindimensionales Array im Umfang von 24 und dem Datentyp CStunden enthält.


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

