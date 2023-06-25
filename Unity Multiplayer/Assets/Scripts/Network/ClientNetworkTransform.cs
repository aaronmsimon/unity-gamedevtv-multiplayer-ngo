using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;
    }

    protected override void Update()
    {
        // this code was taken from a Unity public project - seems like excessive checks, but is better to be safe
        CanCommitToTransform = IsOwner; // this is probably to make sure still retains ownership after each update
        base.Update();
        if (NetworkManager != null) { // networktransform seems to cache the variable so .singleton is not needed
            if (NetworkManager.IsConnectedClient || NetworkManager.IsListening) {
                if (CanCommitToTransform) {
                    TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time); // to interpolate with different framerates
                }
            }
        }
    }
}
