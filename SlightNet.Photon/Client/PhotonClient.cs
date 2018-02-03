using Ether.Network.Photon.Common;
using Ether.Network.Photon.Common.Protocol;
using SlightNet.Client;
using SlightNet.Common.Interface;

namespace SlightNet.Photon.Client
{
    public abstract class PhotonClient : SlightClient, IPhotonClient
    {
        public void SendOperationRequest(OperationRequest request)
        {
            using (var packet = PhotonClientPacket.CreatePacket())
            using (var buffer = new StreamBuffer(0))
            {
                SerializationProtocol.SerializeOperationRequest(buffer, request.OperationCode, request.Parameters, false);
                packet.Write(buffer.ToArray(), 0, (int)buffer.Length);
                Send(packet);
            }
        }

        protected override IPacketProcessor PacketProcessor => new PhotonPacketProcessor();

        public ProtocolBase SerializationProtocol => new Protocol16();
    }
}