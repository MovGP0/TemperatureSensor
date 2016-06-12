using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace TemperatureSensor.Maxim
{
    // TODO: this needs 100% unit test coverage before beeing used! 
    /// <summary>
    /// Maxim Max323 1-Wire Protocol.
    /// </summary>
    public sealed class OneWire
    {
        private enum ResponseCode
        {
            UartNotConnected = 0xFF,
            NonOneWireDevice = 0xF0
        }

        private const int StartByte = 0xF0;
        
        public OneWire(ISerialDeviceFactory serialDeviceFactory, Func<IOutputStream, IDataWriter> dataWriterFactory, Func<IInputStream, IDataReader> dataReaderFactory)
        {
            if(serialDeviceFactory == null) throw new ArgumentNullException(nameof(serialDeviceFactory));
            if(dataWriterFactory == null) throw new ArgumentNullException(nameof(dataWriterFactory));
            if(dataReaderFactory == null) throw new ArgumentNullException(nameof(dataReaderFactory));

            SerialDeviceFactory = serialDeviceFactory;
            DataWriterFactory = dataWriterFactory;
            DataReaderFactory = dataReaderFactory;
        }

        private ISerialDevice SerialPort { get; set; }
        private IDataWriter DataWriteObject { get; set; }
        private IDataReader DataReaderObject { get; set; }
        private ISerialDeviceFactory SerialDeviceFactory { get; }
        private Func<IOutputStream, IDataWriter> DataWriterFactory { get; }
        private Func<IInputStream, IDataReader> DataReaderFactory { get; }
        
        public void Shutdown()
        {
            if (SerialPort == null) return;
            SerialPort.Dispose();
            SerialPort = null;
        }

        // TODO: put into dedicated factory 
        internal async Task<ISerialDevice> GetNewSerialPortAsync(string deviceId, BaudRate baudRate)
        {
            var serialDevice = await SerialDeviceFactory.FromIdAsync(deviceId);
            serialDevice.WriteTimeout = TimeSpan.FromMilliseconds(1000);
            serialDevice.ReadTimeout = TimeSpan.FromMilliseconds(1000);
            serialDevice.BaudRate = (uint)baudRate;
            serialDevice.Parity = SerialParity.None;
            serialDevice.StopBits = SerialStopBitCount.One;
            serialDevice.DataBits = 8;
            serialDevice.Handshake = SerialHandshake.None;
            return serialDevice;
        }

        private async Task<bool> TryOneWireResetAsync(string deviceId)
        {
            try
            {
                SerialPort?.Dispose();
                SerialPort = await GetNewSerialPortAsync(deviceId, BaudRate.Low);

                DataWriteObject = DataWriterFactory(SerialPort.OutputStream);
                DataWriteObject.WriteByte(StartByte);
                await DataWriteObject.StoreAsync();

                DataReaderObject = new DataReader(SerialPort.InputStream);
                await DataReaderObject.LoadAsync(1);
                var resp = DataReaderObject.ReadByte();

                if (resp == (int)ResponseCode.UartNotConnected)
                {
                    Debug.WriteLine("Nothing connected to UART");
                    return false;
                }

                if (resp == (int)ResponseCode.NonOneWireDevice)
                {
                    Debug.WriteLine("No 1-wire devices are present");
                    return false;
                }
                
                Debug.WriteLine($"Response: {resp}");
                SerialPort.Dispose();
                SerialPort = await GetNewSerialPortAsync(deviceId, BaudRate.High);
                DataWriteObject = DataWriterFactory(SerialPort.OutputStream);
                DataReaderObject = DataReaderFactory(SerialPort.InputStream);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
        
        public async Task OneWireWriteByteAsync(byte b)
        {
            for (byte i = 0; i < 8; i++, b = (byte)(b >> 1))
            {
                // Run through the bits in the byte, extracting the
                // LSB (bit 0) and sending it to the bus
                await OneWireBitAsync((byte)(b & 0x01));
            }
        }

        private async Task<byte> OneWireBitAsync(byte b)
        {
            var bit = b > 0 ? 0xFF : 0x00;
            DataWriteObject.WriteByte((byte)bit);
            await DataWriteObject.StoreAsync();
            await DataReaderObject.LoadAsync(1);
            var data = DataReaderObject.ReadByte();
            return (byte)(data & 0xFF);
        }

        private async Task<byte> OneWireReadByteAsync()
        {
            byte b = 0;
            for (byte i = 0; i < 8; i++)
            {
                // Build up byte bit by bit, LSB first
                b = (byte)((b >> 1) + 0x80 * await OneWireBitAsync(1));
            }

            Debug.WriteLine($"OneWireReadByteAsync result: {b}");
            return b;
        }

        public async Task<double> GetTemperatureAsync(string deviceId)
        {
            var tempCelsius = -273.15;

            if (!await TryOneWireResetAsync(deviceId))
                return tempCelsius;

            await OneWireWriteByteAsync(0xCC); //1-Wire SKIP ROM command (ignore device Id)
            await OneWireWriteByteAsync(0x44); //DS18B20 convert T command 
            
            // (initiate single temperature conversion)
            // thermal data is stored in 2-byte temperature 
            // register in scratchpad memory

            // Wait for at least 750ms for data to be collated
            await Task.Delay(750);

            // Get the data
            await TryOneWireResetAsync(deviceId);
            await OneWireWriteByteAsync(0xCC); //1-Wire Skip ROM command (ignore device Id)
            await OneWireWriteByteAsync(0xBE); //DS18B20 read scratchpad command
            
            // DS18B20 will transmit 9 bytes to master (us)
            // starting with the LSB

            var tempLeastSignificantByte = await OneWireReadByteAsync(); //read lsb
            var tempMostSignificantByte = await OneWireReadByteAsync(); //read msb

            // Reset bus to stop sensor sending unwanted data
            await TryOneWireResetAsync(deviceId);

            // TODO: the unit conversion should not be here 
            // Log the Celsius temperature
            tempCelsius = ((tempMostSignificantByte * 256) + tempLeastSignificantByte) / 16.0;
            var temp2 = ((tempMostSignificantByte << 8) + tempLeastSignificantByte) * 0.0625; //just another way of calculating it

            Debug.WriteLine("Temperature: " + tempCelsius + " degrees C " + temp2);
            return tempCelsius;
        }
    }
}