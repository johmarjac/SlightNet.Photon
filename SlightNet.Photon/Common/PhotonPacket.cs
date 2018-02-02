using System;
using Ether.Network.Photon.Common.Protocol;
using SlightNet.Common;

namespace SlightNet.Photon.Common
{
    public abstract class PhotonPacket : SlightPacketStream
    {
        protected PhotonPacket(bool isPingPacket = false, byte channelId = 0, byte reliable = 1)
        {
            IsPingPacket = isPingPacket;
            ChannelId = channelId;
            Reliable = reliable;

            Write(IsPingPacket ? (byte)0xF0 : (byte)0xFB); // Ping Packet == 0xF0 else 0xFB
            if(!IsPingPacket)
            {
                Write(0); // Length Placeholder
                Write(ChannelId); // Channel ID
                Write(Reliable); // Reliable
                Write((byte)0xF3); // Internal OpCode
            }
        }

        protected void SerializeBigEndianInt32(int value)
        {
            var offs = 0;
            var buffer = new byte[4];
            Protocol.Serialize(value, buffer, ref offs);
            Write(BitConverter.ToInt32(buffer, 0));
        }

        protected abstract byte[] BuildBuffer();

        public override byte[] Buffer => BuildBuffer();
        protected bool IsPingPacket { get; }
        protected byte ChannelId { get; }
        protected byte Reliable { get; }

        public enum PhotonCode
        {
            ClientInit,
            ServerInit,
            OperationRequest,
            OperationResponse,
            ClientKey = 6,
            ServerKey,
        }
    }
}