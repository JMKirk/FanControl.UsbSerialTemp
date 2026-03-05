using System;
using System.IO.Ports;

namespace FanControl.UsbTemp
{
    internal class SerialThermometer : ITempSensorDriver
    {
        private SerialPort _port;
        private string _configuredPort;

        public void Open(string deviceId)
        {
            _configuredPort = deviceId;

            // Try configured port first
            if (TryOpenPort(deviceId))
                return;

            // Try all ports
            foreach (var port in SerialPort.GetPortNames())
            {
                if (TryOpenPort(port))
                    return;
            }

            // If nothing works, leave _port null
            // FanControl will still load the plugin
        }

        private bool TryOpenPort(string portName)
        {
            try
            {
                var p = new SerialPort(portName, 115200)
                {
                    NewLine = "\n",
                    ReadTimeout = 300
                };

                p.Open();

                // Validate by reading one line
                string line = p.ReadLine().Trim();
                if (float.TryParse(line, out _))
                {
                    _port = p;
                    return true;
                }

                p.Close();
            }
            catch
            {
                // ignore
            }

            return false;
        }

        public void Close()
        {
            try { _port?.Close(); } catch { }
        }

        public float Temperature()
        {
            // If port died or was never opened, try reconnecting
            if (_port == null || !_port.IsOpen)
            {
                Open(_configuredPort);
                if (_port == null)
                    return float.NaN;
            }

            try
            {
                string line = _port.ReadLine().Trim();
                if (float.TryParse(line, out float temp))
                    return temp;
            }
            catch
            {
                // If read fails, reconnect next update
                try { _port?.Close(); } catch { }
                _port = null;
            }

            return float.NaN;
        }
    }
}
