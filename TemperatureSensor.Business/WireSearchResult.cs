using System;
using System.Diagnostics;

namespace TemperatureSensor.Business
{
    [Obsolete("not in use", true)]
    public struct WireSearchResult
    {
        public WireSearchResult (byte[] id, int lastForkPoint = 0)
        {
            Debug.Assert(id.Length == 8);
            Id = id;
            LastForkPoint = lastForkPoint;
        }

        public byte[] Id { get; }
        public int LastForkPoint { get; }
    }
}