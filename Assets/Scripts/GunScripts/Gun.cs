using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float fireRate;
    [SerializeField] private float shootTime;
    [SerializeField] private float reloadTime;

    [SerializeField] private ParticleSystem reloadParticles;

    private Color bulletColor;
    private bool isPlayerGun;
    private Bullet bullet;

    private void Start()
    {
        // Check if gun is for the player or enemy
        if (transform.parent.CompareTag("Player"))
        {
            isPlayerGun = true;
        }
        else
        {
            isPlayerGun = false;
            StartCoroutine(FireRythm());
        }
        //Gives all bullets color of parent
        bulletColor = transform.parent.GetComponent<SpriteRenderer>().color;
    }


    private void Update()
    {
        if (isPlayerGun && Input.GetKeyDown(KeyCode.Mouse0) && Time.deltaTime != 0)
        {
            CreateBullet();
        }
        
    }

    private void CreateBullet()
    {
        
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        bullet = newBullet.GetComponent<Bullet>();
        bullet.SetBulletDamage(bulletDamage);
        bullet.SetBulletSpeed(bulletSpeed);
        bullet.SetBulletColor(bulletColor);
        
        bullet.transform.tag = transform.parent.tag;

        FindObjectOfType<AudioManager>().Play("BulletSound");
        newBullet.SetActive(true);
    }

    /// <summary>
    /// Fire rate = bullets per second
    /// Shoot time = time before reload
    /// Reload time = time before allowed to shoot
    /// </summary>
    /// <returns></returns>
    IEnumerator FireRythm()
    {

        InvokeRepeating("CreateBullet", 0.1f, fireRate);  //1s delay, repeat every 1s FIXME make time dependant

        yield return new WaitForSeconds(shootTime);
        ParticleSystem temp = Instantiate(reloadParticles, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletSpawnPoint);
        CancelInvoke();
        yield return new WaitForSeconds(reloadTime);
        Destroy(temp);
        StartCoroutine(FireRythm());
    }

    //Getters
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }
}
