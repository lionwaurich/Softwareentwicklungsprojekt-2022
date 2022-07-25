using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;

using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
using var driver = new Pcf8574(i2c);
using var lcd = new Lcd2004(registerSelectPin: 0, 
                        enablePin: 2, 
                        dataPins: new int[] { 4, 5, 6, 7 }, 
                        backlightPin: 3, 
                        backlightBrightness: 0.1f,
                        readWritePin: 1, 
                        controller: new GpioController(PinNumberingScheme.Logical, driver));
while (true)
{
    lcd.Clear();
    lcd.SetCursorPosition(0,0);
    lcd.Write("Temperatur:-----\u00DFC");
    Thread.Sleep(200);
}