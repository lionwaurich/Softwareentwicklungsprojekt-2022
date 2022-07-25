# Alternative-Prüfung-Softwareentwicklung

| Parameter                | Informationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung**       | Projekt Softwareecntwicklung                                                                                                                                                           |
| **Semester**               |   Sommersemester 2022                                                                                                                                                                                        |
| **Hochschule**          | Technische Universität Bergakademie Freiberg                                                                                                                                                      
| **Autoren**              | Lion Waurich (65695) / Caio Marcas Menz (66654)                            

---------------------------------------------------------------------------------
# Wetterstation mittels Raspberry Pi 4 und angeschlossenen peripheren Komponenten

Der Raspberry Pi dient als Recheneinheit, welcher die Daten der Sensoren auswertet.
Die Ausgabe soll optisch und akustisch realisiert werden. Gemessen werden Innentemperatur, Außentemperatur,
Luftfeuchtigkeit und die Luftqualität mittels eines Gas-Sensors. Die Ausgabe läuft über eine RGB-LED, ein LCD-Display und einem Piezo-Buzzer.
Das Programm wird objektorientiert in C-Sharp über zwei Klassen laufen. Die Sensordaten sollen im 10 Sekunden Takt erfasst und über jede Stunde der Durchschnitt berechnet werden. Nach 24 Stunden sollen die Sensordaten in einer .txt Datei gespeichert werden. Steigt der Luftqualitätswert über eine Kritische Grenze, soll ein optisches und akustisches Signal ausgegeben werden.


# Verwendete Software

* Visual Studio
* .Net 6
* Raspberry Pi OS version 04.04.2022
* Putty
* RealVNC Viewer
* Fritzing    (für die Steckpläne)
* ClickCharts (für die Klassen Diagramme)
