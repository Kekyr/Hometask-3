using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private float _step;

        public bool TryMove(Vector3 direction)
        {
            var forwardRay = new Ray(transform.position, direction);

            if (Physics.Raycast(forwardRay, out RaycastHit hit, _step, _obstacleMask))
                return false;
            

            transform.forward = direction;
            transform.Translate(direction * _step, Space.World);
            return true;
        }
    }
}