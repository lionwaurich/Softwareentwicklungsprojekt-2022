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
Die erste Alarmstufe wird ausgelöst, wenn der Luftqualitätswert über 35% "schlechte" Luft steigt, dabei leuchtet die RGB-LED gelb und der Buzzer gibt ein langsames akustisches Signal aus. Die zweite Alarmstufe wird ausgelöst, wenn dieser Wert über 50% steigt. Dann leuchtet die RGB-LED rot und die Frequenz des akustischen Signals wird erhöht. Dies soll Gefahrengrenzwerte signalisieren und und zum Verlassen des Raumes auffordern. 


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
*
