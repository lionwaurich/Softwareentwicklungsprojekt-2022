using System;
using System.Device.Gpio;
using System.Threading;

int pin = 27;
using var controller = new GpioController();
controller.OpenPin(pin, PinMode.Output);
while (true)
{
    controller.Write(pin, PinValue.High );
    Thread.Sleep(500);
    controller.Write(pin, PinValue.Low );
    Thread.Sleep(500);
}