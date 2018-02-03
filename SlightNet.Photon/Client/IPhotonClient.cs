using Ether.Network.Photon.Common.Protocol;
using SlightNet.Photon.Common;

namespace SlightNet.Photon.Client
{
    public interface IPhotonClient : IPhotonConnection
    {
        void SendOperationRequest(OperationRequest request);
    }
}