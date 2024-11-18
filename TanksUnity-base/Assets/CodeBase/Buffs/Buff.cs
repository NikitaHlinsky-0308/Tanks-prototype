using System;
using UnityEngine;

namespace CodeBase.Buffs
{
    public class Buff : MonoBehaviour
    {
        
        [Header("Buff move settings")]
        [Range(min:0, max: 1)]
        [SerializeField] private float _amplitude;
        [Range(min:0, max: 1)]
        [SerializeField] private float _frequency;
        [SerializeField] private float _rotationSpeed; 
       
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        void Update()
        {
            ApplyBuffMovement();
        }

        private void ApplyBuffMovement()
        {
            float posY = Mathf.Sin(Time.time * _frequency) * _amplitude;
            
            transform.Rotate(new Vector3(0, _rotationSpeed, 0) * Time.deltaTime);
            transform.position = _startPosition + new Vector3(0, posY, 0);
        }


        public void DestroyBuff()
        {
            Destroy(this.gameObject);
        }
    }
}
