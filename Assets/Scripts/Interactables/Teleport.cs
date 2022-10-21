using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform _anotherTeleport;
        [SerializeField] private float _teleportDelay;

        private bool _isTeleport;
        private Vector3 _destination;
        private float _timer = 0;
        private Player _player;


        private void Start()
        {
            _timer = _teleportDelay;
            _destination = _anotherTeleport.position;
        }

        private void Update()
        {
            if (_isTeleport)
                TeleportTimerTick();
            else
                _timer = _teleportDelay;

        }
        
        private void TeleportTimerTick()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                Teleporting(_player);
                _timer = _teleportDelay;
                EndTeleporting();
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _isTeleport = true;
                _player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                EndTeleporting();
            }
        }

        private void Teleporting(Player player)
        {
            player.gameObject.SetActive(false);
            player.transform.position = new Vector3(_destination.x, player.transform.position.y, _destination.z);
            player.gameObject.SetActive(true);
        }

        private void EndTeleporting()
        {
            _isTeleport = false;
            _player = null;
        }
        
    }
}