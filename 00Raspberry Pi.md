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
  * Aktivieren VNC-Server (virtual Network Computing) 
  * stellen VNC Resolution auf 1920x1080
  * dann starten wir den Raspberry neu
```
sudo root 
```
* Auf dem Rechner, über den wir auf den Raspberry zugreifen wollen laden wir uns "real VNC viewer" runter und Installieren das Programmm
* wenn alle Konfigurationen stimmen und der Raspberry den VNC-Server gestartet hat, können wir die IPv4 Adresse als Host bei "real VNC viewer" eingeben und nach eingabe des Benutzers und des dazugehörigen Passwortes gelangen wir auf die Grafische oberfläsche des Raspberry Pi 
* damit wir später die Sensoren und GPIOs ansteuern können müssen wir weitere Konfigurationen im "raspi-config" Menü vornehmen
* wir wechseln in der Raspberry Pi Configuration auf die Interfaces und aktivieren folgendes
    * SPI (Serial Peripheral Interface)
    * Serial Port (serielle Schnittstelle, bei denen einzelne Bits zeitgleich nacheinander übertragen werden) 
    * serial Console 
    * 1-Wire (Eindraht-Bus, Die Busteilnehmer werden durch Master gesteuert. Slaves sind mit individuellen Adressen ab Werk ausgestattet und fertig kalibriert)
    * Remote GPIO (Das erlaubt den Remot zugriff auf die Raspberry Pi GPIOs)

# C-Sharp auf dem Raspberry Pi

Das Raspberry Pi OS ist ein Linux basiertes System und .Net ein windows Framework. .Net nutzt man zum entwickeln und Ausführen von C# Programmen. Über Visual Studio ist es schnell und einfach Programme zu entwickeln, kompilieren und auszuführen. Leider ist es nicht ohne probleme möglich C-Sharp Programme auf dem Raspberry Pi auszuführen. Man könnte das Programm auf einen Stick Laden und mit dem Bytecode-Interpreter "MONO" auf dem Raspberry ausführen. Dies ist aber ein sehr aufwendiger vorgang, denn man müsste bei Fehlern das Programm immer wieder neu auf den stick laden und auf dem Raspberry ausführen. Oder man nutzt das Windows IOT Tool. Dies ist aber auch nicht zu empfehlen, weil man damit das Raspberry Pi Os ersetzten müsste. Der Raspberry läuft am besten mit dem Hauseigenen Linux System. Viele Tools wären beim Windows IOT nicht mit dabei und Bugs treten vermehrt auf. Deswegen haben wir uns dafür Entschieden, ".Net" auf dem Raspberry Pi zu installieren und damit gehen wir diesen Problemen aus dem Weg. ".Net" kann man nicht in den Erweiterungen des Raspberry Pi finden, jedoch lässt es sich über die Bash installieren. Der Aufwand ist einmalig zwar höher, erleichtert die spätere Entwicklung des Programms jedoch enorm.


# .Net auf dem Raspberry Pi Installieren und Visual Studio nutzen

* Dieser Befehl wird über Putty auf der Bash eingegeben und Installiert .Net 6 auf dem Raspberry Pi

```
wget -O - https://raw.githubusercontent.com/pjgpetecodes/dotnet5pi/master/install.sh | sudo bash
```
* Danach muss der Raspberry Pi wieder neu gestartet werden bzw REBOOT

```
sudo reboot
```
* Jetzt müssen wir visual Studio auch über Remote-SSH mit dem Raspberry verbinden. Dazu gehen wir auf Visual Studio und Installieren die "Remote-SSH" Erweiterung 
```
ms-vscode-remote.remote-ssh
```
* Als hostname tragen wir **Pi@-IP-Adress-** ein und Visual Studio verbindet sich mit dem Raspberry Pi 
* Auf dem Raspberry Pi erstellen wir einen Ordner, den wir dann über Visual Studio aufrufen können. Das weitere vorgehen ist wie gewohnt. eine C# Datei kann man jetzt erstellen und bearbeiten. Wie man sieht bleibt auch der Befehl gleich. 
```
dotnet new console
```
* Programme sind auf dem Raaspberry jetzt zwar kompilierbar, jedoch bleibt der zugriff auf die GPIOS verwehrt. Deswegen müssen wir ein weiteres Packet hinzufügen, welches wir in den Ordner mit dem Programm speichern. Die Befehle können jetzt über die Bash (via Putty), den VNC-viewer oder Visual Studio eingegeben werden.

```
dotnet add package System.Device.Gpio
```
* Damit wir die Peripherie nutzen können müssen wir in den selben Ordner noch ein weiteres Packet hinterlegen. Dieses Packet würde eigentlich aussreichen um auf die GPIOs direkt zuzugreifen, jedoch gab es dabei öfter Probleme, weshalb wir "System.Device.Gpio" seperat hinterlegt hatten.

```
dotnet add package Iot.Device.Bindings --version 2.1.0-* 
```

# Ansteuern der Sensoren 

* Sensormodelle
   * `MQ-135` (Luftqualitätssensor: Benzol, Alkohol, Rauch)
   * `AM2302` (Temperatur und Luftfeuchtigkeit)
 
* Weitere Komponenten
   * `20x4 LCD Display`
   * `MCP3008` (Analog Digital Wandler)
   * `RGB-LED`
   * `Pegelwandler` (3,3V-5V)
   * `Piezo Buzzer`
   * Steckboard
   * Jumper Kabel


![GPIO-Pi4](/Grafiken/GPIO-Pi4.png "GPIO-PinOut")

## AM2302 

* Der ASM2302 basiert auf der Basis des DHT22 und misst die Luftfeuchtigkeit und die Temperatur. Ausgegeben werden diese werte digital über einen Datenpin

![AM2302](/Grafiken/AM2302.png)



