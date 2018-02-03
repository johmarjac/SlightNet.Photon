using Ether.Network.Photon.Common.Protocol;
using SlightNet.Common.Interface;
using SlightNet.Photon.Server;

namespace SlightNet.Photon.Client
{
    public sealed class PhotonPacketProcessor : IPacketProcessor
    {
        public int HeaderSize => 9;

        public bool SplitPacket => false;

        public int ProcessHeader(byte[] header)
        {
            if (header[0] == 240)
                return HeaderSize;

            var offs = 1;
            var len = 0;
            Protocol.Deserialize(out len, header, ref offs);
            return len;
        }

        public IPacketStream ProcessPacket(byte[] packet)
        {
            return new PhotonServerPacket(packet); ;
        }
    }
}