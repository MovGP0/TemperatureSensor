# Temperature Sensor Project

This projects indend to use the Maxim DS18B20 temperature sensor on Windows 10 IoT devices. 

## Planned for this project

There should be two parts to this solution: 

* The first Raspberry Pi reads the data from the serial port and shows the temperature on the screen in real time.
* The second Raspberry Pi emulates the Maxim DS18S20 temperature sensor. 

The project should be developed in an fully test-driven manner. 

The two Rasperries might need to be controlled over the network (via HTTP/REST) in order to run a suite of tests. 

## Help for Libraries

* [xUnit](https://xunit.github.io/)
* [Fluent Assertions](http://www.fluentassertions.com/)
* [Simple Injector](https://simpleinjector.org/index.html)
* [Hypermock](https://github.com/steve-hyperbolic/hyper/tree/master/HyperMock)
* [Reactive Extensions](http://www.introtorx.com/)

## Helpful links 

* [Maxim DS18S20 Datasheet](https://datasheets.maximintegrated.com/en/ds/DS18S20.pdf)
* [Microsoft Blinky Example](https://developer.microsoft.com/en-us/windows/iot/win10/samples/blinky)
* [OneWire protocol implementation](https://electricimp.com/docs/resources/onewire/)
* [1-Wire DS18B20 Sensor on Windows 10 Iot Core/Raspberry Pi 2](https://www.hackster.io/selom/1-wire-ds18b20-sensor-on-windows-10-iot-core-raspberry-pi-2-7d9b67)
