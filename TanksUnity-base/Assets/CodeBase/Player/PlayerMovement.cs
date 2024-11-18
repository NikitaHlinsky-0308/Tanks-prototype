using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Buffs;
using CodeBase.Camera;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private Vector3 lookDirection;
        private float timer = 10f;
        private Ray ray;

        
        [Header("Camera")]
        [SerializeField] private UnityEngine.Camera mainCamera;
        [Header("Movement")]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _speed;
        [Header("Aiming")]
        [SerializeField] private Transform _aimTransform;
        [SerializeField] private GameObject _tower;
        [SerializeField] private float _towerSpeed;
        [SerializeField] private LayerMask _aimLayerMask;
        [Header("Speed buff")]
        [Range(min:1f, max:3f)]
        [SerializeField] private float _speedBuffMultiplier;
        [Range(min:1f, max:10f)]
        [SerializeField] private float _speedBuffDuration;
        
        [Space][Header("Events")]
        [SerializeField] private UnityEvent _playerOnSpeedBuff;
        [SerializeField] private UnityEvent _playerOnHealthBuff;
        [SerializeField] private UnityEvent _playerOnDamageBuff;
        [SerializeField] private UnityEvent _playerOnDied;

        private float _initialSpeed;

        private void Start()
        {
            mainCamera.GetComponent<CameraFollow>().Follow(this.gameObject.transform);
            _controller = GetComponent<CharacterController>();
            _initialSpeed = _speed;
        }


        private void Update()
        {
            ApplyMovement();
            ApplyTowerRotation();
        }
        
        
        private void ApplyTowerRotation()
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimLayerMask))
            {
                lookDirection = hit.point - transform.position;
                lookDirection.y = 0;
                lookDirection.Normalize();
                
                _tower.transform.forward = Vector3.Lerp(_tower.transform.forward, lookDirection, Time.deltaTime * _towerSpeed);
                _aimTransform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }

        private void ApplyMovement()
        {
            Vector3 movementVector = Vector3.zero;

            float moveForwardBackward = Input.GetAxisRaw("Vertical"); 
            float rotateLeftRight = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(moveForwardBackward) > 0)
            {
                movementVector = transform.forward * moveForwardBackward;
            }

            if (Mathf.Abs(rotateLeftRight) > 0)
            {
                float rotationY = rotateLeftRight * _rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotationY, 0);
            }

            _controller.Move(movementVector * _speed * Time.deltaTime);
        }

        public void SpeedBuff()
        {
            StartCoroutine(SpeedBuffCoroutine(_speedBuffDuration));
        }

        private IEnumerator SpeedBuffCoroutine(float time)
        {
            _speed = _initialSpeed * _speedBuffMultiplier;
            yield return new WaitForSeconds(time);
            _speed = _initialSpeed;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("SpeedCollectable"))
            {
                _playerOnSpeedBuff.Invoke();
                other.gameObject.GetComponent<Buff>().DestroyBuff();
            }
            if(other.CompareTag("HealthCollectable"))
            {
                _playerOnHealthBuff.Invoke();
                other.gameObject.GetComponent<Buff>().DestroyBuff();
            }
            if(other.CompareTag("DamageCollectable"))
            {
                _playerOnDamageBuff.Invoke();
                other.gameObject.GetComponent<Buff>().DestroyBuff();
            }
        }
    }
}
