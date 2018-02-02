using System;
using System.IO;
using SlightNet.Photon.Common;

namespace SlightNet.Photon.Server
{
    public sealed class PhotonServerPacket : PhotonPacket
    {
        private PhotonServerPacket(bool isPingPacket = false, byte channelId = 0, byte reliable = 1) : base(isPingPacket, channelId, reliable)
        {
            if (IsPingPacket)
                Write((long)0); // Server and Client TickCount Placeholder
        }

        protected override byte[] BuildBuffer()
        {
            var oldPos = Position;
            Seek(1, SeekOrigin.Begin);

            if (IsPingPacket)
            {
                if (Size != 9)
                    throw new InvalidOperationException("A Photon Ping Packet from Server requires a total size of 9 bytes.");

                SerializeBigEndianInt32(Environment.TickCount);
            }
            else
                SerializeBigEndianInt32(Size);

            Seek(oldPos, SeekOrigin.Begin);
            return ToArray();
        }

        public static PhotonServerPacket CreatePacket(PhotonCode code = PhotonCode.OperationResponse)
        {
            var packet = new PhotonServerPacket();
            packet.Write((byte)code);
            return packet;
        }

        public static PhotonServerPacket CreatePingPacket()
        {
            return new PhotonServerPacket(isPingPacket: true);
        }
    }
}