using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        private bool isShooting, readyToShoot;
        
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireRate;
        [SerializeField] private float firePower;
        
        // refs
        [SerializeField] private Transform FirePoint;
        //[SerializeField] private UnityEngine.Camera cam;

        private void Awake()
        {
            readyToShoot = true;
        }

        void Start()
        {
        
        }

        void Update()
        {
            FireInput();
        }

        private void FireInput()
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            if (isShooting && readyToShoot) Shoot(); 
        }

        private void Shoot()
        {
            readyToShoot = false;
            
            Ray ray = UnityEngine.Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if(Physics.Raycast(ray, out hit)) targetPoint = hit.point;
            else targetPoint = ray.GetPoint(100);

            Vector3 direction = targetPoint - FirePoint.position;
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
            bullet.transform.forward = direction.normalized;
            
            bullet.GetComponent<Rigidbody>().AddForce(FirePoint.forward * firePower, ForceMode.Impulse);
            readyToShoot = true;
        }
    }
}
