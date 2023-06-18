using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRMechanicsTookit.SuperHot
{
    public class ShootingHelper : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab = null;

        [SerializeField]
        private Transform bulletSpawnPoint = null;

        [SerializeField]
        private float bulletSpeed = 20f;

        [SerializeField]
        private float fireRate = 0.1f;

        [SerializeField]
        private ActionBasedController controller;

        private float nextFire = 0f;

        private void Update()
        {
            if (IsTriggerPressed(controller) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }
        }

        private bool IsTriggerPressed(ActionBasedController controller)
        {
            // Assuming the trigger uses the 'activate' action
            return controller.activateAction.action.ReadValue<float>() > 0.5f;
        }

        private void Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * bulletSpawnPoint.up;
            Destroy(bullet, 3f);
        }
    }
}

