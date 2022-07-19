# Alternative-Prüfung-Softwareentwicklung

| Parameter                | Informationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | Projekt Softwareecntwicklung                                                                                                                                                           |
| **Semester**               |   Sommersemester 2022                                                                                                                                                                                        |
| **Hochschule:**          | Technische Universität Bergakademie Freiberg                                                                                                                                                      
| **Autoren**              | Lion Waurich (65695) / Caio Marcas Menz (66654)                            

---------------------------------------------------------------------------------

# Installieren des Raspberry PI Os 

* Die Micro SD karte mittels Raspberry Pi Imager beschreiben 
* Bei der Konfiguration sollte der SSH zugang aktiviert werden, die WLAN Einstellungen und Benutzername/Passwort gewählt werden
> **Achtung** Falls der SSH zugang nicht aktiviert wurde muss eine Datei mit dem Namen "SSH" im "boot" Ordner hinzugefügt werden
* Nach der Installation des OS den Raspberry Starten 
> Falls das WLAN nicht Konfiguriert wurde, muss der Raspberry Pi über ein LAN Kabel mit dem Router verbunden werden
* Die IP-Adresse des Raspberry Pi finden wir in den Router Einstellungen/Informationen unseres Providers 
* Die IPv4-Adresse des Raspberry Pi stellen wir auf statisch
> Man kann auch selber eine IPv4 wählen und sie dem Raspberry zuteilen. Die Einstellung auf Dynamisch lassen ist möglich, jedoch wird davon abgeraten, da die IP des Raspberry pi dann immer neu ermittelt werden muss


# Remote Accsess auf den Raspberry Pi 

* Auf dem Rechner, mit dem wir den Raspberry Ansteuern wollen, müssen wir das Programm "Putty" Installieren
* Die ermittelte IPv4 geben wir als Host Name ein und gelangen so auf die Bash des Raspberry Pi via Secure Shell (SSH) 
> Secure Shell ist ein Netzwerkprotokoll, welches eine möglichkeit gibt, über ein ungesichertes Netzwerk gesichert auf einen Rechner zuzugreifen
* Dort Melden wir uns mit dem Benutzernamen und Passwort an, welches wir zuvor konfiguriert haben
> Standartpasswort/Benutzername falls die Konfiguration vorher nicht vorgenommen wurde : Benutzername = pi Passwort = raspberry
* Dann muss das "Raspberry Pi Software Configuration Tool" aufgerufen werden
```
sudo raspi-config
```
* Der "sudo" Befehl ist wichtig, da der Benutzer "pi" nicht genügend Berechtigungen besitzt. 
> Man kann auch eine gesamte Session als root user nutzen mit dem "su" Befehl (aber nur eine Session, nicht dauerhaft) 
* Im Raspberry Pi Software Configuration Tool nehmen wir dann folgende änderungen vor
  * Aktivieren VNC-Server
  * stellen VNC Resolution auf 1920x1080
  * dann starten wir den Raspberry neu
```
sudo root 
```
* Auf dem Rechner, über den wir auf den Raspberry zugreifen wollen laden wir uns "real VNC viewer" runter und Installieren das Programmm
* wenn alle Konfigurationen stimmen und der Raspberry den VNC-Server gestartet hat, können wir die IPv4 Adresse als Host bei "real VNC viewer" eingeben und nach eingabe des Benutzers und des dazugehörigen Passwortes gelangen wir auf die Grafische oberfläsche des Raspberry Pi 

