using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Movement))]
    public class BulletMovement : MonoBehaviour
    {
        public Vector3 Direction { get; set; }

        private Movement _movement;

        private bool canMove=true;

        private void Start()
        {
            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            if(!canMove) 
                Destroy(gameObject);
            
           canMove=_movement.TryMove(Direction);
        }
    }
}
