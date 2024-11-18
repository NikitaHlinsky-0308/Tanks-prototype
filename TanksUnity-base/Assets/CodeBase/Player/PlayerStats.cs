using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Player
{
    public class PlayerStats : MonoBehaviour, IStates
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        [SerializeField] private int _damage;
        [SerializeField] private float _damageBuffDuration;
        
        private int _initialDamage;

        private void Start()
        {
            _health = _maxHealth;
            _initialDamage = _damage;
        }
        
        public void TakeDamage(int amount)
        {
            if (amount < _health) _health -= amount;
            else if (amount > _health)
            {
                _health = 0;
                Died();
            }
            Debug.Log(_health);
        }

        public void Heal(int amount)
        {
            if (amount < _health) _health += amount;
            else if (amount > _health)
            {
                _health = _maxHealth;
            }
        }


        public void Died()
        {
            Debug.Log("You are dead!");
        }

        public void DamegeBuff()
        {
            StartCoroutine(DamegeBuffCoroutine(_damageBuffDuration));
        }

        private IEnumerator DamegeBuffCoroutine(float time)
        {
            _damage = _initialDamage * 3;
            yield return new WaitForSeconds(time);
            _damage = _initialDamage;
        }
        
        public void HealthBuff()
        {
            Heal(_maxHealth);
        }

    }
}