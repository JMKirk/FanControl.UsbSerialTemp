using FanControl.Plugins;
using System;
using System.IO.Ports;

namespace FanControl.UsbTemp
{
    internal class UsbTempPluginSensor : IPluginSensor
    {
        private readonly UsbTempSensorConfig _config;
        private ITempSensorDriver _thermometer;
        private SerialThermometer _serialThermometer;
        private float _temp_measurement;

        public string Id => _config.device_type + _config.device_id;
        public string Name => _config.device_id;
        public float? Value => _temp_measurement;

        public UsbTempPluginSensor(UsbTempSensorConfig config)
        {
            _config = config;

            switch (_config.device_type)
            {
                case "ds18b20":
                    _thermometer = new Ds18b20Thermometer();
                    break;

                case "serial":   // ⭐ Added serial support
                    _serialThermometer = new SerialThermometer();
                    _thermometer = _serialThermometer;
                    break;

                default:
                    throw new Exception("Invalid device type");
            }
        }

        public void Open()
        {
            // ⭐ SERIAL DEVICE LOGIC
            if (_config.device_type == "serial")
            {
                // Try configured port first
                _serialThermometer.Open(_config.device_id);
                return;
            }

            // ⭐ ORIGINAL LOGIC FOR DS18B20
            _thermometer.Open(_config.device_id);
        }

        public void Close()
        {
            _thermometer.Close();
        }

        public void Update()
        {
            _temp_measurement = _thermometer.Temperature();
        }
    }
}
