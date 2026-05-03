using UnityEngine;
using Unity.Netcode;

namespace NGO.Core.Player
{
    public class PlayerAiming : NetworkBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Transform turret;

        private void LateUpdate() {
            // Still happens every frame but after the normal Update so the movement happens first and then the aiming so there's no jitter
            if (!IsOwner) return;

            Vector2 aimPos = Camera.main.ScreenToWorldPoint(inputReader.AimPosition);
            turret.up = aimPos - (Vector2)turret.up;
        }
    }
}
