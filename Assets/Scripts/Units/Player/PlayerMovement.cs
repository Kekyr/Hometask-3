using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Movement))]
    public class PlayerMovement : MonoBehaviour
    {
        private Movement _movement;

        private void Start()
        {
            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                _movement.TryMove(Vector3.forward);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                _movement.TryMove(Vector3.back);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                _movement.TryMove(Vector3.right);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                _movement.TryMove(Vector3.left);
        }

    }
}
