using SlightNet.Common.Interface;
using SlightNet.Server;

namespace SlightNet.Photon.Server
{
    public abstract class PhotonServer<T> : SlightServer<T> where T : PhotonUser, new()
    {
        protected override IPacketProcessor PacketProcessor => new PhotonPacketProcessor();
    }
}