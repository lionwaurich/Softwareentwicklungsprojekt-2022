# Alternative-Prüfung-Softwareentwicklung

| Parameter                | Informationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung**       | Projekt Softwareentwicklung                                                                                                                                                           |
| **Semester**               |   Sommersemester 2022                                                                                                                                                                                        |
| **Hochschule**          | Technische Universität Bergakademie Freiberg                                                                                                                                                      
| **Autoren**              | Lion Waurich (65695) / Caio Marcas Menz (66654)                            

---------------------------------------------------------------------------------
# Wetterstation mittels Raspberry Pi 4 und angeschlossenen peripheren Komponenten

Der Raspberry Pi dient als Recheneinheit, welcher die Daten der Sensoren auswertet.
Die Ausgabe soll optisch und akustisch realisiert werden. Gemessen werden Innentemperatur,
Luftfeuchtigkeit und die Luftqualität mittels eines Gas-Sensors. Die Ausgabe läuft über eine RGB-LED, ein LCD-Display und einem Piezo-Buzzer.
Das Programm wird objektorientiert in C-Sharp über zwei Klassen laufen. Die Sensordaten sollen im 6 Sekunden Takt erfasst und über den LCD-Display ausgegeben werden. Aus diesen Daten wird über jede Stunde der Durchschnitt berechnet und nach 24 Stunden sollen die Sensordaten in einer .txt Datei gespeichert werden. Steigt der Luftqualitätswert über eine kritische Grenze, soll ein optisches und akustisches Signal ausgegeben werden, dies erfolgt in zwei Stufen. 
Die erste Alarmstufe wird ausgelöst, wenn der Luftqualitätswert über 35% "schlechte" Luft steigt, dabei leuchtet die RGB-LED gelb und der Buzzer gibt ein langsames akustisches Signal aus. Die zweite Alarmstufe wird ausgelöst, wenn dieser Wert über 50% steigt. Dann leuchtet die RGB-LED rot und die Frequenz des akustischen Signals wird erhöht. Dies soll Gefahrengrenzwerte signalisieren und und zum Verlassen des Raumes auffordern. Das Programm finden sie [hier](https://github.com/Lion127/Softwareentwicklungsprojekt-2022/blob/main/Wetterstation_komplett/Wetterstation_komplett.cs). Der Code wurde gemeinsam auf Visual Studio geschrieben, da wir immer die Funktionalität des Programmes auf dem Raspberry Pi testen mussten und Visual Studio, Remote mit dem Raspberry Pi verbunden war. Andernfalls hätten wir das Programm zwar auf Github verändern können, jedoch hätten wir es nicht direkt testen können.

* Das ist der Aufbau des Raspberry Pi mit angeschlossener Peripherie

![Aufbau](/Grafiken/Raspberry_Aufbau/Aufbau.jpeg)

* Der erste Start des Programmes:

![Start](/Grafiken/Raspberry_Aufbau/Start.jpeg)

* Nachdem das System hochgefahren ist, leuchtet die LED grün und der Buzzer gibt ein akustisches Signal

![hochgefahren](/Grafiken/Raspberry_Aufbau/hochgefahren.jpeg)

* Jetzt sind alle Sensoren betriebsbereit und die Daten können dem LCD Display entnommen werden
* Video des eines simulierten Gas Alarms gibt es [hier](https://github.com/Lion127/Softwareentwicklungsprojekt-2022/blob/main/Grafiken/AlarmVideo.mp4)

![Ausgabe](/Grafiken/Raspberry_Aufbau/Ausgabe.jpeg)

<br/>

# Verwendete Software

* [Visual Studio](https://code.visualstudio.com/?wt.mc_id=DX_841432)
* [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Raspberry Pi OS version 04.04.2022](https://www.raspberrypi.com/software/)
* [Raspberry Pi Imager](https://www.raspberrypi.com/software/)
* [Putty](https://www.putty.org/)           (SSH zugriff auf die Bash des Raspberry Pi)
* [RealVNC Viewer](https://www.realvnc.com/de/connect/download/viewer/)  (Remote zugriff auf den Raspberry Pi)
* [Fritzing](https://fritzing.org/)        (für die Steckpläne)
* [ClickCharts](https://www.nchsoftware.com/chart/de/index.html)     (für die Klassen Diagramme)

# Quellen

* [Raspberry Pi Forum](https://forum-raspberrypi.de/forum/)
* [Pete Code](https://www.petecodes.co.uk/install-and-use-microsoft-dot-net-5-with-the-raspberry-pi/)
* [Microsoft Tutorials](https://docs.microsoft.com/de-de/dotnet/iot/tutorials/blink-led)
* [Raspberry Pi Tutorials](https://tutorials-raspberrypi.de/raspberry-pi-gas-sensor-mq2-konfigurieren-und-auslesen/)
* [Netzmafia Raspberry Pi](http://www.netzmafia.de/skripten/hardware/RasPi/)
* [Github: Dotnet/Iot](https://github.com/dotnet/iot/issues/416)
