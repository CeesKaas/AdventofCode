using System;
using System.Collections.Generic;
using System.Text;

namespace day13.implementation
{
    class InfiniteLayer : ILayer
    {
        private Queue<Packet> _queue = new Queue<Packet>();

        public void AddPacket(Packet packet)
        {
            if (packet == null) return;
            _queue.Enqueue(packet);
        }

        public Packet RemoveTopPacket()
        {
            try
            {
                return _queue.Dequeue();
            }
            catch (InvalidOperationException)
            {

            }
            return null;
        }
    }
}
