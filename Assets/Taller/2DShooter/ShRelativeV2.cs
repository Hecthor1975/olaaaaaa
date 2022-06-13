using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Arcos.Taller
{
    public class ShRelativeV2 : MonoBehaviour
    {
        public SpriteRenderer aim;
        public MvRelative moveRelative;

        [Header("Spread")]
        public float spreadFactor = 1; // Presition at 10 unity unit
        public float maxAddedSpreadFactor = 10f;
        public float timeToIncreaseSpread = 2f;
        public float factorToReduceSpread = 1f;
        private float actualspreadFactor = 1;
        private float timeToSpread = 0; // From 0 to 1
        public float timeSpreadPerShoot = 0;

        [Header("Shoot Speed")]
        public float bulletSpeed = 10;
        public float shootSpeed = 1; // 1 per second
        float actualTimeWaited = 0;

        [Header("Bullet Settings")]
        public GameObject prefabBulletToSpawn;
        public int actualBullet = 4;
        public int maxBulletsPerCharge = 4;
        public int totalBullets = 4;
        public float timeToRecharge = 1f;
        float actualTimeWaitedToReacharge = 0;
        bool reloading = false;

        public TextMeshProUGUI bulletDisplay;

        [Header("Debug")]
        public bool debugActive;

        bool thereIsLastShoot = false;
        Vector3 lastShootPosition;

        [Header("On Actions")]
        public UnityEvent onShoot;
        public UnityEvent onReload;

        [Header("Data")]
        [SerializeField] private DataChangerString dataChanger;
        // Start is called before the first frame update
        void Start()
        {
            actualspreadFactor = spreadFactor;
        }

        // Update is called once per frame
        void Update()
        {

            aim.gameObject.SetActive(true);
            Aim();
        }

        public void Aim()
        {
            CheckMovement();
            Reload();
            Shoot();
            UpdateUI();
        }

        public void CheckMovement()
        {
            if (timeToIncreaseSpread == 0 && moveRelative != null) return;

            if (moveRelative.IsMoving())
            {
                timeToSpread += Time.deltaTime;
                if (timeToSpread > timeToIncreaseSpread) timeToSpread = timeToIncreaseSpread;

            }
            else
            {
                timeToSpread -= Time.deltaTime * factorToReduceSpread;
                if (timeToSpread < 0) timeToSpread = 0;
            }

            actualspreadFactor = shootSpeed + maxAddedSpreadFactor * (timeToSpread / timeToIncreaseSpread);
        }

        public void Shoot()
        {
            Vector3 cameraMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aim.transform.position = new Vector3(cameraMousePosition.x, cameraMousePosition.y, aim.transform.position.z);

            if (Input.GetMouseButton(0) && actualTimeWaited <= 0 && actualBullet > 0 && reloading == false)
            {
                timeToSpread += timeSpreadPerShoot;
                timeToSpread = timeToSpread >= timeToIncreaseSpread ? timeToIncreaseSpread : timeToSpread;
                actualBullet -= 1;

                // Next bullet wait
                actualTimeWaited = 1 / shootSpeed;
                if (onShoot != null) onShoot.Invoke();

                //Shoot
                float distance = Vector2.Distance(transform.position, cameraMousePosition);
                Vector2 maxDistance = Vector2.ClampMagnitude(cameraMousePosition, 10);
                Vector3 otherPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

                // Spread Options
                float alpha = Mathf.Atan2(otherPos.x, otherPos.y) * Mathf.Rad2Deg;
                float alphaSpread = alpha + actualspreadFactor;
                float alphaSpread2 = alpha - actualspreadFactor;

                //Select Random Between the angles !
                float finalAngle = Random.Range(alphaSpread, alphaSpread2);
                thereIsLastShoot = true;

                // Pass the final data
                Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * finalAngle), Mathf.Cos(Mathf.Deg2Rad * finalAngle));
                lastShootPosition = transform.position + (Vector3)(bulletDirection * 10);
                SpawnBullet(finalAngle, bulletDirection);
            }

            if (actualTimeWaited >= 0) actualTimeWaited -= Time.deltaTime;
        }

        public void SpawnBullet(float angle, Vector2 bulletDirection)
        {
            GameObject newObject = Instantiate<GameObject>(prefabBulletToSpawn);
            newObject.transform.position = this.transform.position;
            
            float rotz = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            newObject.transform.rotation = Quaternion.Euler(0f, 0f, rotz - 90);

            newObject.tag = "Bullet";

            // push the created objects, but only if they have a Rigidbody2D
            Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                Vector3 realDirection = lastShootPosition - this.transform.position;
                realDirection = realDirection.normalized;
                rigidbody2D.AddForce(realDirection * bulletSpeed, ForceMode2D.Impulse);
                //rigidbody2D.AddForce(bulletDirection * bulletSpeed, ForceMode2D.Impulse);
            }
        }
        public void Reload()
        {
            if (Input.GetMouseButton(0) && actualBullet <= 0 && actualTimeWaited <= 0 && totalBullets > 0)
            {
                if (onReload != null) onReload.Invoke();
                //Reload
                actualTimeWaitedToReacharge = timeToRecharge;
                reloading = true;
            }

            if (reloading)
            {
                actualTimeWaitedToReacharge -= Time.deltaTime;
                if (actualTimeWaitedToReacharge <= 0)
                {
                    reloading = false;
                    int rechargeBullets = 0;
                    if (totalBullets >= maxBulletsPerCharge) rechargeBullets = maxBulletsPerCharge;
                    if (totalBullets < maxBulletsPerCharge) rechargeBullets = totalBullets;

                    totalBullets -= rechargeBullets;
                    actualBullet += rechargeBullets;
                }
            }
        }

        public void AddAmunition(float addAmmo)
        {
            totalBullets += Mathf.FloorToInt(addAmmo);
            if (totalBullets < 0) totalBullets = 0;
        }

        public void UpdateUI()
        {
            if (bulletDisplay == null) return;
            bulletDisplay.text = totalBullets + " | " + actualBullet;
        }

        public void DrawPresition()
        {
            if (debugActive == false || Application.isPlaying == false) return;

            Vector3 cameraMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.color = Color.yellow;

            Vector3 otherPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // Posible Positions
            float alpha = Mathf.Atan2(otherPos.x, otherPos.y) * Mathf.Rad2Deg;
            float alphaSpread = alpha + actualspreadFactor;
            float alphaSpread2 = alpha - actualspreadFactor;

            Vector2 directionAlpha = new Vector2(Mathf.Sin(Mathf.Deg2Rad * alpha), Mathf.Cos(Mathf.Deg2Rad * alpha));
            Vector2 directionAlphaSpread = new Vector2(Mathf.Sin(Mathf.Deg2Rad * alphaSpread), Mathf.Cos(Mathf.Deg2Rad * alphaSpread));
            Vector2 directionAlphaSpread2 = new Vector2(Mathf.Sin(Mathf.Deg2Rad * alphaSpread2), Mathf.Cos(Mathf.Deg2Rad * alphaSpread2));

            Gizmos.DrawLine(this.transform.position, transform.position + (Vector3)(directionAlpha * 10));
            Gizmos.DrawLine(this.transform.position, transform.position + (Vector3)(directionAlphaSpread * 10));
            Gizmos.DrawLine(this.transform.position, transform.position + (Vector3)(directionAlphaSpread2 * 10));

            // Last Shoot
            if (thereIsLastShoot == false) return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, lastShootPosition);

        }


        public void UpdateData()
        {
            string shooterData = "";

            shooterData += spreadFactor + "|"; // Presition at 10 unity unit
            shooterData += maxAddedSpreadFactor + "|";
            shooterData += timeToIncreaseSpread + "|";
            shooterData += factorToReduceSpread + "|";
            shooterData += timeSpreadPerShoot + "|";
            shooterData += bulletSpeed + "|";
            shooterData += shootSpeed + "|";
            shooterData += actualBullet + "|";
            shooterData += maxBulletsPerCharge + "|";
            shooterData += totalBullets + "|";
            shooterData += timeToRecharge + "|";

            dataChanger.ChangeValue(shooterData);
        }

        // 0111010 -> string, donde 1 segnigica que estara apagado y 1 prendido
        public void ChangeItemsFromData(string newshooterData)
        {
            string[] shooterData = newshooterData.Split('|');
            if (shooterData.Length < 9) return;

            spreadFactor            = float.Parse(shooterData[0]);
            maxAddedSpreadFactor    = float.Parse(shooterData[1]);
            timeToIncreaseSpread    = float.Parse(shooterData[2]);
            factorToReduceSpread    = float.Parse(shooterData[3]);
            timeSpreadPerShoot      = float.Parse(shooterData[4]);
            bulletSpeed             = float.Parse(shooterData[5]);
            shootSpeed              = float.Parse(shooterData[6]);
            actualBullet            = int.Parse(shooterData[7]);
            maxBulletsPerCharge     = int.Parse(shooterData[8]);
            totalBullets            = int.Parse(shooterData[9]);
            timeToRecharge          = float.Parse(shooterData[10]);
        }

        private void OnDrawGizmos()
        {
            DrawPresition();
        }
    }
}