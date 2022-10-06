
using System;
using UnityEngine;

namespace Game
{
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            player.Kill();
    }
}
}

