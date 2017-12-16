namespace day13.implementation
{
    internal interface ILayer
    {
        void AddPacket(Packet packet);
        Packet RemoveTopPacket();
    }
}