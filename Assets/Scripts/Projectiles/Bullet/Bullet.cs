using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.Kill();
                Destroy(gameObject);
            }
        }
    }
}
