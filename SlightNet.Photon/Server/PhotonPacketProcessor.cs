﻿using Ether.Network.Photon.Common.Protocol;
using SlightNet.Common.Interface;
using SlightNet.Photon.Client;

namespace SlightNet.Photon.Server
{
    public sealed class PhotonPacketProcessor : IPacketProcessor
    {
        public int HeaderSize => 5;

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
            return new PhotonClientPacket(packet);
        }
    }
}