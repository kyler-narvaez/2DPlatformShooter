using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    //Player Movement
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private int MaxjumpCount;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private BoxCollider2D jumpHitBox;

    private int jumpCount;
    private int jumpsUsed;
    private float distanceTravelled;
    private float direction;

    public float sloMoEffect;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsUsed = 0;
        distanceTravelled = 0;
        sloMoEffect = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        {
            distanceTravelled += 0.1f;
        }
        
        rb.velocity = new Vector2(direction * speed * sloMoEffect, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpsUsed < MaxjumpCount)
        {
            Jump();
        }

    }

    private void Jump()
    {

        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        jumpsUsed++;
        jumpCount++;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (jumpHitBox.IsTouching(collision.collider))
        {
            //fxPlayer.PlayLandingEffect(); FIXME does it a million times, need to do it once
            jumpsUsed = 0;
        }
    }

    //Skill tree Functions
    public void IncreaseJumpAbility(int amount)
    {
        MaxjumpCount += amount;
    }

    public void IncreaseSpeedAbility(int amount)
    {
        speed += amount;
    }

    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    public float GetJumpsUsed()
    {
        return jumpCount;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetJumpCount()
    {
        return MaxjumpCount;
    }
}
