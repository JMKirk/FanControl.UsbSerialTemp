# FanControl.UsbSerialTemp
[![Download](https://img.shields.io/badge/Download-Plugin-green.svg?style=flat&logo=download)](https://github.com/JMKirk/FanControl.UsbSerialTemp/releases)

Plugin for [FanControl](https://github.com/Rem0o/FanControl.Releases) that provides support for USB/serial based temperature probes. Initial support provides for DS18B20 1–wire sensor connected via USB serial bridge as found in [this product](http://usbtemp.com/).

All props go to [secretformula](https://github.com/secretformula) for making the program.
This update provides support for generic serial-output temperature devices, such as microcontrollers (e.g. Raspberry Pi Pico) that output a plain numeric temperature value over a COM port. It also added automatic fallback when the COM port in the configuration file is unavailable.

## To install

Either
* Download the latest [release](https://github.com/JMKirk/FanControl.UsbSerialTemp/releases)
* Compile the solution.

And then

1. Copy the FanControl.UsbTemp.dll into FanControl's "Plugins" folder
2. Create new file `<Fan Controller Install Dir>/Configurations/FanControl.UsbTemp.json` and copy content of example configuration.
3. Set the 'device_type' to either 'ds18b20' or 'serial'
4. Set COM ports that correspond to your installed temperature sense probe.
5. Open FanControl and enjoy!