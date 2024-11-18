using System;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Projectile
{
    public class BulletProjectile : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private int _damage;
        [SerializeField] private float _selfDestroyTime = 3f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(_damage);
                Destroy(gameObject);
            } 
            Destroy(gameObject, _selfDestroyTime);
        }
    }
}
