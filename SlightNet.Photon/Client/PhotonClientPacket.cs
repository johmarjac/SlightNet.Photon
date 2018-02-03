using System;
using System.IO;
using SlightNet.Photon.Common;

namespace SlightNet.Photon.Client
{
    public sealed class PhotonClientPacket : PhotonPacket
    {
        private PhotonClientPacket(bool isPingPacket = false, byte channelId = 0, byte reliable = 1) : base(isPingPacket, channelId, reliable)
        {
            if (IsPingPacket)
                Write(0); // Client TickCount Placeholder
        }

        public PhotonClientPacket(byte[] buffer) : base(buffer)
        {
        }

        protected override byte[] BuildBuffer()
        {
            var oldPos = Position;
            Seek(1, SeekOrigin.Begin);

            if (IsPingPacket)
            {
                if (Size != 5)
                    throw new InvalidOperationException("A Photon Ping Packet from Client requires a total size of 5 bytes.");

                SerializeBigEndianInt32(Environment.TickCount);
            }
            else
                SerializeBigEndianInt32(Size);

            return ToArray();
        }

        public static PhotonClientPacket CreatePacket(PhotonCode code = PhotonCode.OperationRequest)
        {
            var packet = new PhotonClientPacket();
            packet.Write((byte)code);
            return packet;
        }

        public static PhotonClientPacket CreatePingPacket()
        {
            return new PhotonClientPacket(isPingPacket: true);
        }
    }
}