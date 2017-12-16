using System;

namespace day13.implementation
{
    internal class Layer : ILayer
    {
        private Packet[] _packets;
        private int _packetsInLayer;

        public Packet[] Packets => _packets;

        public int Depth { get; }

        public Layer(int depth)
        {
            _packets = new Packet[depth];
            Depth = depth;
        }
        public void AddPacket(Packet packet)
        {
            if (packet == null) return;
            if (_packetsInLayer == _packets.Length) throw new Exception("layer full");
            _packets[_packetsInLayer] = packet;
            _packetsInLayer++;
        }
        public Packet RemoveTopPacket()
        {
            if (_packetsInLayer > 0)
            {
                var packetToReturn = _packets[0];
                for (int i = 0; i <= _packetsInLayer - 1; i++)
                {
                    _packets[i] = _packets[i + 1];
                }
                _packetsInLayer--;
                return packetToReturn;
            }
            return null;
        }
    }
}