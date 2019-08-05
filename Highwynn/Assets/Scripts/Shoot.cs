    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shoot : MonoBehaviour {
    public Rigidbody2D projectile;
    public Transform projectileSpawnPoint;
    public float projectileVelocity;
    public float timeBetweenShots;
    private float timeBetweenShotsCounter;
    private bool canShoot;
    // Use this for initialization
    void Start () {
        canShoot = false;
        timeBetweenShotsCounter = timeBetweenShots;
    }
  
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl) && canShoot)
        {
            Rigidbody2D bulletInstance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, transform.localEulerAngles.z))) as Rigidbody2D;
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(projectileSpawnPoint.right * projectileVelocity);
            canShoot = false;
        }
        if (!canShoot)
        {
            timeBetweenShotsCounter -= Time.deltaTime;
            if(timeBetweenShotsCounter <= 0)
            {
                canShoot = true;
                timeBetweenShotsCounter = timeBetweenShots;
            }
        }
    }
}