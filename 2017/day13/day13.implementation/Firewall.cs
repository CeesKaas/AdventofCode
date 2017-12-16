using System;
using System.Collections.Generic;
using System.Linq;

namespace day13.implementation
{
    public class Firewall
    {
        private List<ILayer> _layers = new List<ILayer>();
        private List<Scanner> _scanners = new List<Scanner>();
        private Queue<Packet> _packetsToAdd = new Queue<Packet>();

        public Firewall(string configuration)
        {
            var lines = configuration.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var scannedLayers = lines.ToDictionary(_ => int.Parse(_.Substring(0, _.IndexOf(":"))), _ => int.Parse(_.Substring(_.IndexOf(":") + 1).Trim()));

            for (int i = 0; i <= scannedLayers.Keys.Max(); i++)
            {
                if (scannedLayers.TryGetValue(i, out int depth))
                {
                    var layer = new Layer(depth);
                    _layers.Add(layer);
                    _scanners.Add(new Scanner(i, layer));
                }
                else
                {
                    _layers.Add(new InfiniteLayer());
                }
            }

        }

        public event EventHandler<Packet> FinishedPacket;

        public void InjectPacket(Packet packet)
        {
            _packetsToAdd.Enqueue(packet);
        }

        public void Tick()
        {
            //Console.WriteLine("Tick start");
            Packet[] packetsToMove = new Packet[_layers.Count];

            for (int i = 0; i < _layers.Count; i++)
            {
                packetsToMove[i] = _layers[i].RemoveTopPacket();
            }
            try
            {
                _layers[0].AddPacket(_packetsToAdd.Dequeue());
            }
            catch
            {
                _layers[0].AddPacket(null);
            }
            for (int i = 0; i < _layers.Count-1; i++)
            {
                _layers[i + 1].AddPacket(packetsToMove[i]);
            }

            FinishedPacket?.Invoke(this, packetsToMove.Last());
            foreach (var s in _scanners)
            {
                s.Scan();
                s.Advance();
            }
            //Console.WriteLine("Tick end");
        }
    }
}