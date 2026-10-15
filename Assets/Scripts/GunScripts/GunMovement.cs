using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private SpriteRenderer rend;

    private bool isPlayerGun;

    // gun movement
    [SerializeField] private float speed;
    private Vector2 direction;
    private float angle;
    private Quaternion rotation;

    private bool isFlipped = false;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        //Checks if Gun is Player or Enemy
        if (transform.parent.tag == "Player")
        {
            isPlayerGun = true;
        }
        else
        {
            isPlayerGun = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalcGunRotation();
        FlipGun();
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed);
    }

    //calculates the rotation needed for the gun
    private void CalcGunRotation()
    {
        //Determines target to change direction (mouse if player : player if enemy)
        if (isPlayerGun)
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;   // Mouse position
        }
        else
        {
            if (GameObject.Find("Player") != null)
            {
                direction = GameObject.Find("Player").transform.position - transform.position;   // Player position
            }
        }
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //angle needed to rotate
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Flips if mouse is LEFT of player
        if (Math.Abs(angle) > 90f)
        {
            rend.flipY = true;
        }
        //Flips if mouse is RIGHT to player
        else
        {
            rend.flipY = false;
        }
    }

    private void FlipGun()
    {
        if (DetermineFlip())
        {
            rend.flipY = !rend.flipY;
        }
    }

    private bool DetermineFlip()
    {
        if (Mathf.Abs(angle) > 90f)
        {
            isFlipped = true;
        }
        //Flips if mouse is RIGHT to player
        else
        {
            isFlipped = false;
        }

        return (isFlipped != rend.flipY);
    }

}
