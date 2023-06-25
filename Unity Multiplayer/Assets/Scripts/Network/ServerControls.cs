using UnityEngine;
using Unity.Netcode;

public class ServerControls : MonoBehaviour
{
    public void StartClient() {
        NetworkManager.Singleton.StartClient();
    }

    public void StartHost() {
        NetworkManager.Singleton.StartHost();
    }
}
