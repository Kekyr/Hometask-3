using UnityEngine;

namespace Game
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _shootDelay;

        private BulletMovement _bulletMovement;
        private Vector3 _spawnPosition;
        private float _timer = 0;

        private void Awake()
        {
            _timer = _shootDelay;
            _spawnPosition = new Vector3(transform.position.x, _bulletPrefab.transform.position.y, transform.position.z);
            Shoot();
        }

        private void Update()
        {
            ShootTimerTick();
        }

        private void ShootTimerTick()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                Shoot();
                _timer = _shootDelay;
            }
        }

        private void Shoot()
        {
            _bulletMovement = Instantiate(_bulletPrefab, _spawnPosition, _bulletPrefab.transform.rotation)
                .GetComponent<BulletMovement>();
            
            _bulletMovement.Direction = transform.forward;
        }

    }
}
