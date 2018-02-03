using Ether.Network.Photon.Common;

namespace SlightNet.Photon.Common
{
    public interface IPhotonConnection
    {
        ProtocolBase SerializationProtocol { get; }
    }
}