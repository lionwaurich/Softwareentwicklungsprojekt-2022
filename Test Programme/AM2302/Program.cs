using System;
using Iot.Device.DHTxx;

using (Dht22 dht = new Dht22(4))

while(true)
{   
    Console.WriteLine($"{dht.Humidity}°C");
    Console.WriteLine($"{dht.Temperature}%");
    Thread.Sleep(1000);
}