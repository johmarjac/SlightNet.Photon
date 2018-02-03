using Ether.Network.Photon.Common.Protocol;
using SlightNet.Photon.Common;

namespace SlightNet.Photon.Server
{
    public interface IPhotonUser : IPhotonConnection
    {
        void SendOperationResponse(OperationResponse response);
    }
}