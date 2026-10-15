using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private ParticleSystem deathParticles;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 firePointPosition;
    private Vector2 direction;

    private float bulletSpeed;
    private int bulletDamage;
    private Color bulletColor;

    // Start is called before the first frame update
    void Start()
    {
        firePointPosition = transform.position;

        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //Effects of bullet and trail
        SetEffects();

        //DetermineTarget();
        CalcShootDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Destroys bullet when gets too far away
        if (Vector2.Distance(firePointPosition, gameObject.transform.position) > 30f)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != transform.tag)
        {
            deathParticles = Instantiate(deathParticles, transform.position, transform.rotation);
            ParticleSystem.MainModule main = deathParticles.main;
            main.startColor = sr.color;
            main.startSize = 0.1f;  // Effect size
            main.startLifetime = 0.5f;  // Effect life
            Destroy(gameObject);
        }
    }

    /*
     * Calculates the direction the bullet goes
     * target   : Mouse position
     * myPos    : Player position
     * direction: difference between target and myPos
     * rotation : rotation of bullet
     */
    private void CalcShootDirection()
    {
        direction = transform.right;
    }

    //Setters

    private void SetEffects()
    {
        //Bullet Effects
        sr.color = bulletColor;
        //Trail effects
        tr.startWidth = transform.lossyScale.y;
        tr.endWidth = tr.startWidth / 2.5f;

        tr.startColor = sr.color;
        tr.endColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.2f);  // last is the opaque
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }
    /*
     * Pushes Rigidbody2D towards mouse
     * direction    : Found through CalcShootDirection()
     * bulletSpeed  : Set during Start
     */

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    public void SetBulletColor(Color color)
    {
        bulletColor = color;
    }

    //Getters

    public int GetBulletDamage()
    {
        return bulletDamage;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
}
