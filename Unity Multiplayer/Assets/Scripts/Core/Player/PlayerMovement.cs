using UnityEngine;
using Unity.Netcode;

namespace NGO.Core.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Transform tankTreads;
        [SerializeField] private Rigidbody2D rb;

        [Header("Settings")]
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float turningRate = 30f;

        private Vector2 previousMovementInput;

        public override void OnNetworkSpawn() {
            if (!IsOwner) return;

            inputReader.MoveEvent += HandleMove;
        }

        public override void OnNetworkDespawn() {
            if (!IsOwner) return;

            inputReader.MoveEvent -= HandleMove;
        }

        private void Update() {
            if (!IsOwner) return;

            float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
            tankTreads.Rotate(0f, 0f, zRotation);
        }

        private void FixedUpdate() {
            if (!IsOwner) return;

            rb.velocity = (Vector2)tankTreads.up * previousMovementInput.y * moveSpeed;
        }

        private void HandleMove(Vector2 movementInput) {
            previousMovementInput = movementInput;
        }
    }
}
