using Ether.Network.Photon.Common;
using Ether.Network.Photon.Common.Protocol;
using SlightNet.Common;

namespace SlightNet.Photon.Server
{
    public abstract class PhotonUser : SlightUser, IPhotonUser
    {
        public void SendOperationResponse(OperationResponse response)
        {
            using(var packet = PhotonServerPacket.CreatePacket())
            using (var buffer = new StreamBuffer(0))
            {
                SerializationProtocol.SerializeOperationResponse(buffer, response, false);
                packet.Write(buffer.ToArray(), 0, (int)buffer.Length);
                Send(packet);
            }
        }

        public ProtocolBase SerializationProtocol => new Protocol16();
    }
}