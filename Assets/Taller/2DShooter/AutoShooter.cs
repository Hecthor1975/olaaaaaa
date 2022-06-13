using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcos.Taller
{
    public class AutoShooter : MonoBehaviour
    {
        public GameObject prefabBulletToSpawn;
        public float bulletSpeed = 4;
        public float timePerShoot = 1;
        float actualTimeToWait = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Shoot();
        }

        public void Shoot()
        {
            if (actualTimeToWait <= 0)
            {
                actualTimeToWait = timePerShoot;
                float angle = this.transform.rotation.eulerAngles.z;
                Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -angle), Mathf.Cos(Mathf.Deg2Rad * -angle));
                SpawnBullet(angle, bulletDirection);
            }
            else
            {
                actualTimeToWait -= Time.deltaTime;
            }
        }

        public void SpawnBullet(float angle, Vector2 bulletDirection)
        {
            if (prefabBulletToSpawn == null) return;
            GameObject newObject = Instantiate<GameObject>(prefabBulletToSpawn);
            newObject.transform.position = this.transform.position;

            float rotz = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            newObject.transform.rotation = Quaternion.Euler(0f, 0f, rotz - 90);

            newObject.tag = "Bullet";

            // push the created objects, but only if they have a Rigidbody2D
            Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.AddForce(bulletDirection * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }
}