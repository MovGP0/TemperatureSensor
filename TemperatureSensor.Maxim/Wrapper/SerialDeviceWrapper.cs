using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace TemperatureSensor.Maxim
{
    public sealed class SerialDeviceWrapper : ISerialDevice
    {
        private SerialDevice Device { get; }

        public SerialDeviceWrapper (SerialDevice device)
        {
            Debug.Assert(device != null);
            Device = device;
            ErrorReceived = Observable.FromEvent<TypedEventHandler<SerialDevice, ErrorReceivedEventArgs>, ErrorReceivedEventArgs>(eh => Device.ErrorReceived += eh, eh => Device.ErrorReceived -= eh);
            PinChanged = Observable.FromEvent<TypedEventHandler<SerialDevice, PinChangedEventArgs>, PinChangedEventArgs>(eh => Device.PinChanged += eh, eh => Device.PinChanged -= eh);
        }

        public void Dispose()
        {
            Device.Dispose();
        }

        public uint BaudRate
        {
            get { return Device.BaudRate; }
            set { Device.BaudRate = value; }
        }

        public bool BreakSignalState
        {
            get { return Device.BreakSignalState; }
            set { Device.BreakSignalState = value; }
        }

        public uint BytesReceived => Device.BytesReceived;
        public bool CarrierDetectState => Device.CarrierDetectState;
        public bool ClearToSendState => Device.ClearToSendState;
        public ushort DataBits
        {
            get { return Device.DataBits; }
            set { Device.DataBits = value; }
        }

        public bool DataSetReadyState => Device.DataSetReadyState;
        public SerialHandshake Handshake
        {
            get { return Device.Handshake; }
            set { Device.Handshake = value; }
        }

        public IInputStream InputStream => Device.InputStream;
        public bool IsDataTerminalReadyEnabled
        {
            get { return Device.IsDataTerminalReadyEnabled; }
            set { Device.IsDataTerminalReadyEnabled = value; }
        }

        public bool IsRequestToSendEnabled
        {
            get { return Device.IsRequestToSendEnabled; }
            set { Device.IsRequestToSendEnabled = value; }
        }

        public IOutputStream OutputStream => Device.OutputStream;
        public SerialParity Parity
        {
            get { return Device.Parity; }
            set { Device.Parity = value; }
        }

        public string PortName => Device.PortName;
        public TimeSpan ReadTimeout
        {
            get { return Device.ReadTimeout; }
            set { Device.ReadTimeout = value; }
        }

        public SerialStopBitCount StopBits
        {
            get { return Device.StopBits; }
            set { Device.StopBits = value; }
        }

        public ushort UsbProductId => Device.UsbProductId;
        public ushort UsbVendorId => Device.UsbVendorId;

        public TimeSpan WriteTimeout
        {
            get { return Device.WriteTimeout; }
            set { Device.WriteTimeout = value; }
        }

        public IObservable<ErrorReceivedEventArgs> ErrorReceived { get; }
        public IObservable<PinChangedEventArgs> PinChanged { get; }
    }
}