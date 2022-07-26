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

CStunde umfasst 7 Methoden wobei eine davon ein überladener Konstruktor ist.

<br/>

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

<br/>

#### Werte einsetzen
```csharp                                      Usage
public void setValues(double _Temp, double _Hum, double _Gas, int index)
{
    Temp[index] = _Temp; //Temperatur auf Temperatur-Array mit Index speichern
    Hum[index] = _Hum; //Humidity auf Humidity-Array mit Index speichern
    Gas[index] = _Gas; //Gas auf Gas-Array mit Index speichern
}
```
Diese Methode wird benutzt, um das letzte Datum beim Minutenwechsel in das jeweilige Arrayobjekt mit der jeweiligen Minute als Index zu speichern, deswegen umfassen diese Kategorien Arrays mit einer Größe von 60, genauso viel wie eine Stunde an Minuten hat. Diese Minutenwerte werden später benutzt, um den Durchschnitt für diese Stunde zu berechnen.

<br/>

#### Durchschnitt bilden
```csharp                                      Usage
 public void Average()
 {   
     //Array für Werte, um diese als Strings zu verwenden
     double[][] Wert = new double[3][];
     Wert[0] = Temp;
     Wert[1] = Hum;
     Wert[2] = Gas;
     //sum als Variable für Summe und letzendlich Mittelwert
     double sum = 0;
     //Schleife für die 3 Kategorien Temperatur, Luftfeuchtigke
     for(int j = 0; j < 3; j++)
     {
         //Schleife für die spezifischen Werte in den Kategorie
         for(int i = ++start_min; i < 60; i++) sum = sum + Wert
         //Mittelwert bilden in Abhängigkeit der Anfangsminute 
         sum = sum/(60-start_min);
         //Auswahl nach Kategorie
         switch (j)
         {
             case 0: stdAveTemp = Convert.ToInt32(sum); break;
             case 1: stdAveHum = Convert.ToInt32(sum); break;
             case 2: stdAveGas = Convert.ToInt32(sum); break;
         }
         
         //Summe Null setzen für nachfolgende Kategorien
         sum = 0;
     }
 }
```
Die Methode ```void Average()``` soll den Durchschnitt der Minutenarrays von Temperatur, Luftfeuchtigkeit und Gas-Qualität berechnen, deshalb wird ein Jagged-Array benutz um diese Arrays auf eine Dimension zu erweitern, um bei der neuen Dimension, die Kategorien auswählen zu können. Dies ist in den darunterliegenden Schleifen von großem nutzen, da die äußere Schleife, die zu bearbeitende Kategorie auswählt und die innere Schleife erstellt die Summe der Werte und abschließend den Mittelwert.

<br/>

#### Displayausgabe

```csharp                                      Usage
public void display(DateTime _now, double _Temp, double _
{   
    //Display konfigurieren
    using I2cDevice i2c = I2cDevice.Create(new I2cConnect
    using var driver = new Pcf8574(i2c);
    using var lcd = new Lcd2004(registerSelectPin: 0, 
                    enablePin: 2, 
                    dataPins: new int[] { 4, 5, 6, 7 }, 
                    backlightPin: 3, 
                    backlightBrightness: 0.1f, 
                    readWritePin: 1, 
                    controller: new GpioController(PinNum
                    
    //Ausgabe Arrays
    String[] Line = new String[4];
    Line [0] = " " + _now.ToString("d") + "   " + _now.To
    Line [1] = "Humidity: " + _Hum + " %";
    Line [2] = "Temperature: " + _Temp + " \u00DFC";
    Line [3] = "Gas Quality: " + _Gas + " %"; 
    
    //Schleife für Bildschirmzeilenausgabe
    for(int i = 0; i < 4; i++)
    {
        //Zeile hat begrenzten Platz für Zeichen, deshalb
        if(Line[i].Length <= 20)
        {   
            //Part als Hilfsvariable für Leerzeichen als 
            String Part = "";
            //Zeile hat begrenzten Platz für Zeichen, des
            for(int j = 0; j < 20 - Line[i].Length; j++){
            //Endglied mit dem Inhalt der Leerzeichen an 
            Line[i] = Line[i] + Part;
        }
        //Zeile auf Display ausgaben
        lcd.Write(Line[i]);
    }
    
    hour = _now.Hour; //Stunde aktualisieren
}
```
Die Methode ```void Display()``` soll lediglich die ganzen Werte auf dem LCD-Display ausgeben. Dieser wird am Anfang der Merhode kofiguriert und Hilfsarrays ```String[] Line = new String[4]``` erstellt, um für die erste Zeile das Datum und die Uhrzeit, und die letzten Zeilen für Temperatur, Luftfeuchtigkeit und Gas-Qualität auszugeben. Die Zeilen für den LCD-Bildschirm umfassen eine bestimmte Anzahl an Zeichen, weshalb die String Arrays ```Line[]``` mittels Stringoperationen benutzt werden müssen. mit der String Methode ```Length()``` wird die Länge des Inhalts des Stringarrayobjektes erfasst und der Rest mit Leerzeichen in ```Part``` gespeichert, um die Zeile vollauszuschöpfen, da sonst ungewollte Zeilenumbrüche auf dem LCD-Display zu sehen wären. Nach erfolgreichem Bearbeiten der ```Line[]``` wird dieses am Ende der Schleife aufbauend auf dem Bildschirm ausgegeben. Am Ende der Methode wird die Stunde immer wieder aktualisiert, um es dem richtigen Stundenobjekt zuzuordnen.

### CTag ###


```csharp                                      Usage

public tag(DateTime _now, stunden _Stunde) //Überladener Constructor
{
    File_Header = "[ " + _now.ToString("d") + ":\n"; //Header von txt-File soll mit "[" anfangen, für Nachvollziehbarkeit
    Stunde[_now.Hour] = _Stunde; //Stunden-Objekt soll auf eigenen Stundenarray überschrieben werden
}

```

