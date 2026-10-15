using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    

    private Vector2 direction;

    private void Start()
    {
        //Fixme remove when endGame is applied
        CheckPlayerAlive();
        if (GameObject.Find(target.name) != null)
        {
            target = GameObject.Find(target.name).transform;    //Finds player within the scene
        }
        else
        {
            target = transform;
        }
    }


    private void FixedUpdate()
    {
        CheckPlayerAlive();
        direction = (target.position - transform.position);
        direction = new(direction.x, 0f);
        transform.Translate(direction * speed * Time.deltaTime * 0.1f, Space.World);
    }

    private void CheckPlayerAlive()
    {
        if (target == null)
        {
            target = transform;
        }
    }
}
