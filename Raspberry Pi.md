# Alternative-Prüfung-Softwareentwicklung

| Parameter                | Informationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | Projekt Softwareecntwicklung                                                                                                                                                           |
| **Semester**               |   Sommersemester 2022                                                                                                                                                                                        |
| **Hochschule:**          | Technische Universität Bergakademie Freiberg                                                                                                                                                      
| **Autoren**              | Lion Waurich (65695) / Caio Marcas Menz (66654)                            

---------------------------------------------------------------------------------

# Installieren des Raspberry PI Os 

* Die SD mittels Raspberry Pi Imager beschreiben 
* Bei der Konfiguration sollte der SSH zugang aktiviert werden, sowie die WLAN Einstellungen 
> **Achtung** Falls der SSH zugang nicht aktiviert wurde muss eine Datei mit dem Namen "SSH" im "boot" Ordner hinzugefügt werden
* Nach der Installation des OS den Raspberry Starten 
> Falls das WLAN nicht Konfiguriert wurde, muss der Raspberry Pi über ein LAN Kabel mit dem Router verbunden werden
* Die IP-Adresse des Raspberry Pi finden wir in den Router Einstellungen/Informationen unseres Providers 
* Die IPv4-Adresse des Raspberry Pi stellen wir auf statisch
> Man kann auch selber eine IPv4 wählen und sie dem Raspberry zuteilen. Die Einstellung auf Dynamisch lassen ist möglich, jedoch muss man bei jeder neuanmeldung die IP-Adresse neu ermitteln (unnötiger Aufwand)


# Remote Accsess auf den Raspberry Pi 

* Auf dem Rechner, mit dem wir den Raspberry Ansteuern wollen, müssen wir das Programm "Putty" Installieren
* Die ermittelte IPv4 geben wir als Host Name ein und gelangen so auf die Bash des Raspberry Pi via Secure Shell (SSH) 
* 
