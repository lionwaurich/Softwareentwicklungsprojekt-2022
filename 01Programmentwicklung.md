## Herangehensweise an die Software des Respberry Pi's ##

Die Software soll hauptsächlich durch 2 Klassen implementiert werden. Die Klassen sind CStunden und CTag, wobei CTag ein eindimensionales Array im Umfang von 24 und dem Datentyp CStunden beinhaltet.

Die Klasse CStunden ist für die aktuelle Ein-/Ausgabe von/auf den peripheren Komponenten auf dem Raspberry Pi zuständig,
sdiese erstellt für jedes Stundenobjekt die Durchschnittswerte der 3 Attribute Temperatur, Luftfeuchtigkeit und Gas-Qualität bei Anbruch der nächsten Stunde

