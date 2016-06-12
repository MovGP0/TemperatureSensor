using System;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace TemperatureSensor.Maxim
{
    public interface ISerialDevice : IDisposable
    {
        uint BaudRate { get; set; }
        bool BreakSignalState { get; set; }
        uint BytesReceived { get; }
        bool CarrierDetectState { get; }
        bool ClearToSendState { get; }
        ushort DataBits { get; set; }
        bool DataSetReadyState { get; }
        SerialHandshake Handshake { get; set; }
        IInputStream InputStream { get; }
        bool IsDataTerminalReadyEnabled { get; set; }
        bool IsRequestToSendEnabled { get; set; }
        IOutputStream OutputStream { get; }
        SerialParity Parity { get; set; }
        string PortName { get; }
        TimeSpan ReadTimeout { get; set; }
        SerialStopBitCount StopBits { get; set; }
        ushort UsbProductId { get; }
        ushort UsbVendorId { get; }
        TimeSpan WriteTimeout { get; set; }
        IObservable<ErrorReceivedEventArgs> ErrorReceived { get; }
        IObservable<PinChangedEventArgs> PinChanged { get; }
    }
}