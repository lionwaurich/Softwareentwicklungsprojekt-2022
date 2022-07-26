using System;
using Iot.Device.DHTxx; //Sensor-Bibliothek
using System.Device.Gpio; //Für angeschlossene Komponenten am GPIO
using System.Device.I2c; //Für I2C-Schnittstellen-Erkennung
using System.IO; //Für Dateien
using System.Text; //Für Stringmanipulation
using Iot.Device.CharacterLcd; //Bildschirmausgabe Geräterkennung für LCD (16x2/20x4)
using Iot.Device.Pcx857x; //I2C Controller für LCD-Bildschirm
using System.Device.Spi; //ADC-Bus
using Iot.Device.Adc; //ADC-Modul-Bibliothek

class stunden
{
    //ATTRIBUTE
    public int hour {get; set;} //hour um derzeitige Stunde als Stundenindex zu besitzen
    public int start_min {get; set;}
    private double[] Temp = new double[60]; //Temperatur-Array für jede Minute
    private double[] Hum = new double[60]; //Humidity-Array für jede Minute
    private double[] Gas = new double[60]; //Gas-Array für jede Minute
    private double[] OutTemp = new double[60]; //Außentemperatur-Array für jede Minute
    private int stdAveTemp = 0; //Durchschnitts-Temperatur
    private int stdAveHum = 0; //Durchschnitts-Humidity
    private int stdAveGas = 0; //Durchschnitts-Gas

    //METHODEN
    public stunden(int _hour, int _min) //Überladener Constructor
    {
        hour = _hour; //Aktuelle Stunde auf Attribut speichern
        start_min = _min; //Minute bei dem der Raspberry gestartet hat
    }
    
    public void setValues(double _Temp, double _Hum, double _Gas, int index)
    {
        Temp[index] = _Temp; //Temperatur auf Temperatur-Array mit Index speichern
        Hum[index] = _Hum; //Humidity auf Humidity-Array mit Index speichern
        Gas[index] = _Gas; //Gas auf Gas-Array mit Index speichern
    }

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

    public void GasAlert(double _Hum)
    {
        //Pinnbelegung der physischen Ausgabekomponenten
        int pinBuzzer = 27, pinRed = 22, pinGreen = 23;

        //Konfigurierung
        using var controller = new GpioController();
        controller.OpenPin(pinBuzzer, PinMode.Output);
        controller.OpenPin(pinRed, PinMode.Output);
        controller.OpenPin(pinGreen, PinMode.Output);

        if(_Hum >= 35) //Wenn Gaswert über bedrohliche Grenze steigt
        {
            if(_Hum >= 50) //Wenn Gaswert über kritische Grenze steigt
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

    public void compareValues(Mcp3008 _mcp, Dht22 _dht22, ref double _Temp, ref double _Hum,
    ref double _Gas, ref bool _warmup)
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

    public String save()
    {
        Average(); //Durchschnitt der Werte erstellen

        //Übergabe von fertigem txt-File-Fragment
        return "  bis " + hour + " Uhr:\n" +
        "  Zimmertemperatur: " + stdAveTemp + " °C\n" +
        "  Luftfeuchtigkeit: " + stdAveHum + " %\n" +
        "  Gaskonzentration: " + stdAveGas + " %\n";
    }
}

class tag
{
    public stunden[] Stunde = new stunden[24]; //Stundenobjekte
    public String txt_file = ""; //Spätere txt-File-Kette
    public String File_Header = ""; //Header für txt-File

    public tag(DateTime _now, stunden _Stunde) //Überladener Constructor
    {
        File_Header = "[ " + _now.ToString("d") + ":\n"; //Header von txt-File soll mit "[" anfangen, für Nachvollziehbarkeit

        Stunde[_now.Hour] = _Stunde; //Stunden-Objekt soll auf eigenen Stundenarray überschrieben werden
    }

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
}

class Programm
{
    static void Main(string[] args)
    {
        DateTime now = DateTime.Now; //Aktuelle Uhrzeit und aktuelles Datum
        stunden now_hour = new stunden(now.Hour, now.Minute); //Stunden-Objekt instanziiert
        tag heute = new tag(now, new stunden(now.Hour, now.Minute)); //Tag-Objekt instanziiert

        int Stunde = now.Hour; //Stunde auf aktuelle Stunde setzen
        int minute = now.Minute; //minute auf aktuelle Minute setzen

        //Enable für periodischen Prozess (While-Bedingung); WarmUp für erstmaliges hochfahren des Raspberry Pi
        bool Enable = true, WarmUp = true;

		double temperature = 0, humidity = 0, GasValue = 0; //Variablen auf 0 setzen

        //Konfiguration für ADC-Modul
        var hardwareSpiSettings = new SpiConnectionSettings(0,0);
        using var spi = SpiDevice.Create(hardwareSpiSettings);

        //Periodischer Prozess bis Abbruch erkannt wird
        while(Enable == true)
        {
            using (Dht22 dht = new Dht22(4)) //AM2302-Sensor
            using (Mcp3008 mcp = new Mcp3008(spi)) //ADC-Modul für Gassensor
            
            //Werte auf Fehler vergleichen, wenn keine Fehler, Werte für weiteren Prozess benutzen (CallByReference)
            heute.Stunde[Stunde].compareValues(mcp, dht, ref temperature, ref humidity, ref GasValue, ref WarmUp);

            //Gas-Qualität überprüfen
            heute.Stunde[Stunde].GasAlert(GasValue);

            //Uhrzeit aktualisieren
            now = DateTime.Now;

            //Ausgabe auf LCD-Bildschirm
            heute.Stunde[Stunde].display(now, temperature, humidity, GasValue);

            if(minute != now.Minute) //bei Minutenwechsel
            {
                minute = now.Minute; //Minute auf neue/aktuelle Zeit umändern

                //Werte auf Minuten-Array schreiben, bei index = minute
                heute.Stunde[Stunde].setValues(temperature, humidity, GasValue, minute);

                //Wenn 60 Minuten erreicht sind, soll Durchschnittswert für Stunde ermittelt werden
                if(minute == 0)
                {
                    if(heute.Stunde[Stunde].hour == 0) //Wenn 0 Uhr erreicht ist
                    {
                        heute.save(Stunde); //Endgültige txt-File erstellen

                        heute = new tag(now, new stunden(now.Hour, now.Minute)); //neues Tag-Objekt instanziiert
                    } 
                    else heute.save(Stunde); //txt-File erstellen
                    
                    Stunde = now.Hour; //aktuelle Stunde übernehmen

                    heute.Stunde[Stunde] = new stunden(now.Hour, now.Minute); //neues Stunden-Objekt instanziiert
                }
            }
        }
    }
}