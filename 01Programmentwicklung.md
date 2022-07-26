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

<br/>
<br/>

### CStunde
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
     //Schleife für die 3 Kategorien Temperatur, Luftfeuchtigkeit und Gas-Qualität
     for(int j = 0; j < 3; j++)
     {
         //Schleife für die spezifischen Werte in den Kategorien, um Mittelwert zu bilden
         for(int i = ++start_min; i < 60; i++) sum = sum + Wert[j][i];
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
public void display(DateTime _now, double _Temp, double _Hum, double _Gas)
{   
    //Display konfigurieren
    using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
    using var driver = new Pcf8574(i2c);
    using var lcd = new Lcd2004(registerSelectPin: 0, 
                    enablePin: 2, 
                    dataPins: new int[] { 4, 5, 6, 7 }, 
                    backlightPin: 3, 
                    backlightBrightness: 0.1f, 
                    readWritePin: 1, 
                    controller: new GpioController(PinNumberingScheme.Logical, driver)); 
                    
    //Ausgabe Arrays
    String[] Line = new String[4];
    Line [0] = " " + _now.ToString("d") + "   " + _now.ToString("HH:mm") + " ";
    Line [1] = "Humidity: " + _Hum + " %";
    Line [2] = "Temperature: " + _Temp + " \u00DFC";
    Line [3] = "Gas Quality: " + _Gas + " %"; 
    
    //Schleife für Bildschirmzeilenausgabe
    for(int i = 0; i < 4; i++)
    {
        //Zeile hat begrenzten Platz für Zeichen, deshalb wird der rest mit Leerzeichen gefüllt
        if(Line[i].Length <= 20)
        {   
            //Part als Hilfsvariable für Leerzeichen als Puffer
            String Part = "";
            //Zeile hat begrenzten Platz für Zeichen, deshalb wird der Rest mit der Anzahl an Leerzeichen gefüllt
            for(int j = 0; j < 20 - Line[i].Length; j++){Part = Part + " ";}
            //Endglied mit dem Inhalt der Leerzeichen an Ausgabezeile hängen
            Line[i] = Line[i] + Part;
        }
        //Zeile auf Display ausgaben
        lcd.Write(Line[i]);
    }
    
    hour = _now.Hour; //Stunde aktualisieren
}
```
Die Methode ```void Display()``` soll lediglich die ganzen Werte auf dem LCD-Display ausgeben. Dieser wird am Anfang der Merhode kofiguriert und Hilfsarrays ```String[] Line = new String[4]``` erstellt, um für die erste Zeile das Datum und die Uhrzeit, und die letzten Zeilen für Temperatur, Luftfeuchtigkeit und Gas-Qualität auszugeben. Die Zeilen für den LCD-Bildschirm umfassen eine bestimmte Anzahl an Zeichen, weshalb die String Arrays ```Line[]``` mittels Stringoperationen benutzt werden müssen. mit der String Methode ```Length()``` wird die Länge des Inhalts des Stringarrayobjektes erfasst und der Rest mit Leerzeichen in ```Part``` gespeichert, um die Zeile vollauszuschöpfen, da sonst ungewollte Zeilenumbrüche auf dem LCD-Display zu sehen wären. Nach erfolgreichem Bearbeiten der ```Line[]``` wird dieses am Ende der Schleife aufbauend auf dem Bildschirm ausgegeben. Am Ende der Methode wird die Stunde immer wieder aktualisiert, um es dem richtigen Stundenobjekt zuzuordnen.

<br/>

#### Warnhinweis
```csharp                                      Usage
    public void GasAlert(double _Gas)
    {
        //Pinnbelegung der physischen Ausgabekomponenten
        int pinBuzzer = 27, pinRed = 22, pinGreen = 23;

        //Konfigurierung
        using var controller = new GpioController();
        controller.OpenPin(pinBuzzer, PinMode.Output);
        controller.OpenPin(pinRed, PinMode.Output);
        controller.OpenPin(pinGreen, PinMode.Output);

        if(_Gas >= 35) //Wenn Gaswert über bedrohliche Grenze steigt
        {
            if(_Gas >= 50) //Wenn Gaswert über kritische Grenze steigt
            {   
                //Buzzer und rote LED geben schnelles Signal aus (Ausgabefrequenz hoch)
                for(int i = 0; i < 6; i++)
                {
                    Thread.Sleep(500); controller.Write(pinBuzzer, PinValue.High);
                    controller.Write(pinRed, PinValue.High);
                    Thread.Sleep(500); controller.Write(pinBuzzer, PinValue.Low);
                    controller.Write(pinRed, PinValue.Low);  
                }
            }
            else
            {
                //Buzzer und gelbe LED geben langsames Signal aus (Ausgabefrequenz niedrig)
                for(int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1000); controller.Write(pinBuzzer, PinValue.High);
                    controller.Write(pinRed, PinValue.High); controller.Write(pinGreen, PinValue.High);
                    Thread.Sleep(1000); controller.Write(pinBuzzer, PinValue.Low);
                    controller.Write(pinRed, PinValue.Low); controller.Write(pinGreen, PinValue.Low);
                }
            }
        }
        else Thread.Sleep(6000); //Aktualiesierung nach 6 Sekunden, um Prozessor zu entlasten

        controller.Write(pinGreen, PinValue.Low); //Grüne LED abschalten, bei erfolgreichem Hochfahren
    }
}
```
Die Methode dient lediglich für die Erfassung eines zu hohen Gaswertes, steigt die Gas-Qualität über 35% so gibt die gelbe LED und der Buzzer ein langsames Signal aus, steigt die Gas-Qualität lediglich über die kritsche Grenze von 50% so leuchtet die rote LED auf und der Buzzer gibt ein schnelles Signal aus, dies soll zum Verlassen des Raumes auffordern. Die überprüfung des Gaswertes erfolgt aller 6 Sekunden, und wird kein bdrohlicher Gas-Wert erfasst so verweilt der Prozess 6 Sekunden lang in der ```else```-Anweisung, um den Prozessor nicht in hoher Frequenz arbeiten zu lassen, da eine Aktualisierung der Werte aller 6 Sekunden völlig ausreicht.

<br/>

#### Werte vergleichen
```csharp                                      Usage
    public void compareValues(Mcp3008 _mcp, Dht22 _dht22, ref double _Temp, ref double _Hum, ref double _Gas, ref bool _warmup)
    {
        if(_warmup)
        {
            //Konfiguration der Pins
            int pinGreen = 23, pinBuzzer = 27;
            using var controller = new GpioController();
            controller.OpenPin(pinGreen, PinMode.Output);
            controller.OpenPin(pinBuzzer, PinMode.Output);

            //Konfiguration des LCD-Bildschirms
            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using var driver = new Pcf8574(i2c);
            using var lcd = new Lcd2004(registerSelectPin: 0, 
                            enablePin: 2, 
                            dataPins: new int[] { 4, 5, 6, 7 }, 
                            backlightPin: 3, 
                            backlightBrightness: 0.1f, 
                            readWritePin: 1, 
                            controller: new GpioController(PinNumberingScheme.Logical, driver));

            //Ausgabe für Sensorenkalibrierung (4 Sekunden lang)
            lcd.Write("  Das Geraet wird   Sensoren kalibriert hochgefahren und die");
            Thread.Sleep(4000);
            
            //Solange der Raspberry hochfährt
            while(_warmup)
            {
                //Überprüfung auf richtige Werte
                if($"{_dht22.Temperature}" != "0 K")
                {
                    //Signal für erfolgreiches Hochfahren (Grüne LED leuchtet auf)
                    controller.Write(pinGreen, PinValue.High); controller.Write(pinBuzzer, PinValue.High);
                    Thread.Sleep(200); controller.Write(pinBuzzer, PinValue.Low);
                    Thread.Sleep(200); controller.Write(pinBuzzer, PinValue.High);
                    Thread.Sleep(200); controller.Write(pinBuzzer, PinValue.Low);

                    //Bildschirmausgabe löschen
                    lcd.Clear();

                    //Ausgabe für erfolgreiches Hochfahren
                    lcd.Write("                       Betriebsbereit      Das Geraet ist                       ");

                    //Abbruch-Anweisung für Schleife
                    break;
                }
            }
        }

        //Werte als String für Stringoperationen
        String Text_Temp = $"{_dht22.Temperature}";
        String Text_Hum = $"{_dht22.Humidity}";

        if(Text_Temp != "0 K") //Vergleich auf falschen Sensorwert
        {
            //Wert soll aus String entnommen werden
            Text_Temp = Text_Temp.Substring(0, Text_Temp.IndexOf(" °C"));
            Text_Hum = Text_Hum.Substring(0, Text_Hum.IndexOf("%")-1);

            //Wenn es keine größeren Differenzen zum vorherigen Wert gibt oder das Gerät am Hochfahren ist, dann ...
            if (Math.Abs(_Hum-Convert.ToDouble(Text_Hum)) < 30 && Math.Abs(_Temp-Convert.ToDouble(Text_Temp)) < 5 || _warmup == true)
            {
                //... sollen die richtigen Werte übernommen werden
                _Temp = Convert.ToDouble(Text_Temp);
                _Hum = Convert.ToDouble(Text_Hum);

                //Hochfahrprozess wird deaktiviert
                _warmup = false;
            }
        }

        //Gas-Wert wird gelesen, da dieser durchgehend richtige Werte ausgibt (Runden auf zwei Stellen nach Komma)
        _Gas = Math.Round(_mcp.Read(0)/ 10.24,2); //Rohwert muss durch 1024 dividiert werden für korrekten Wert (für % und nicht ppm)

    }
```
Die Methode sieht zwar kompliziert aus, doch sie dient jediglich zur Ausfilterung von flaschen Sensordaten. Werden flasche Werte erfasst oder eine zu hohe Differenz zum vorherigen Wert erkannt, so wird der vorherige Wert übernommen. Wird ein richtiger Wert erfasst, so wird der Wert mittels ```CallByReference``` überschrieben. 

<br/>

#### Speicherung der Daten
```csharp                                      Usage
    public String save()
    {
        Average(); //Durchschnitt der Werte erstellen

        //Übergabe von fertigem txt-File-Fragment
        return "  bis " + hour + " Uhr:\n" +
        "  Zimmertemperatur: " + stdAveTemp + " °C\n" +
        "  Luftfeuchtigkeit: " + stdAveHum + " %\n" +
        "  Gaskonzentration: " + stdAveGas + " %\n";
    }
```
Die Methode erstellt beim Aufruf eine Stringkette, welche die Durchschnittswerte der laufenden Stunde beinhaltet.

<br/>
<br/>

### CTag

<br/>

#### Überladener Konstruktor
```csharp                                      Usage
public tag(DateTime _now, stunden _Stunde) //Überladener Constructor
{
    File_Header = "[ " + _now.ToString("d") + ":\n"; //Header von txt-File soll mit "[" anfangen, für Nachvollziehbarkeit
    Stunde[_now.Hour] = _Stunde; //Stunden-Objekt soll auf eigenen Stundenarray überschrieben werden
}

```
Der überladene Konstruktor erstellt bei Instanzierung ein Header für die txt-Datei mit dem aktuellen Datum, um die späteren Daten dem Datum zuordnen zu können. Zudem wird das Stundenobjekt übernommen und auf das Stundenarray mit der aktuellen Stunde als Index überschrieben. Somit baut sich mit jeder Stunde ein gefülltes Stunden Array auf.

<br/>

#### Speicherung der Daten in txt-Datei
```csharp                                      Usage
    public void save(int _stunde)
    {
        if(Stunde[_stunde].hour != 0) //Solange es nicht O Uhr ist
        {
            //txt-File baut sich auf den gespeicherten txt-File-Fargmenten auf
            txt_file = txt_file + Stunde[_stunde].save();  
        }
        else //Wenn O Uhr, wird Stundenzusammenfassung mit Abschlussklammer "]" versehen
        {
            //endgültige txt-File wird mit "]" beendet, für Übersichtlichkeit
            txt_file = File_Header + txt_file + Stunde[_stunde].save() + "]";

            try //Txt-File erstellen und speichern
            {
                //Pfad für txt-Datei
                StreamWriter sw = new StreamWriter(@"/home/pi/Wetterstation/Wetterstation_Daten.txt");

                sw.WriteLine(txt_file); //Speichern der txt_File auf dem Pfad, mittels StreamWriter

                sw.Close(); //Speichern abbrechen
            }
            catch(Exception e){Console.WriteLine("Exception: " + e.Message);} //Ausgabe für Speichertransfer
            finally{Console.WriteLine("Excecuting finally block.");} //Ausgabe für erfolgreichem Speichern
        }
    }
```    
Die Methode überprüft die Uhrzeit und baut die Stringkette solange aus den einzelnen Stundenarrays auf bis das Ende des Tages erreicht wird. Wird dieses Erfasst so schließt die Methode die Stringkette mit einem "]" ab und speichert die Stringkette auf dem Pfad der txt-Datei auf dem Raspberry Pi.
    
    
