using System;
using System.Device.Gpio;
using System.Threading;

Console.WriteLine("Blinken");
int Rot = 22;
int Grün = 23;
using var controller = new GpioController();
controller.OpenPin(Rot, PinMode.Output);
controller.OpenPin(Grün, PinMode.Output);
while (true)
{
    controller.Write(Rot, PinValue.Low);
    controller.Write(Grün, PinValue.Low);
    Thread.Sleep(1000);
   
}
