using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;

    private Rigidbody rigidBody;
    private KeyCode[] inputKeys;
    private Vector3[] directionsForKeys;

    public int health = 3;

    public event Action<PlayerMovement> onPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        inputKeys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        directionsForKeys = new Vector3[] { Vector3.forward, Vector3.left, Vector3.back, Vector3.right };
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < inputKeys.Length; i++)
        {
            var key = inputKeys[i];

            // 2
            if (Input.GetKey(key))
            {
                // 3
                Vector3 movement = directionsForKeys[i] * acceleration * Time.deltaTime;
                movePlayer(movement);
            }
        }
    }
    void movePlayer(Vector3 movement)
    {
        if (rigidBody.velocity.magnitude * acceleration > maxSpeed)
        {
            rigidBody.AddForce(movement * -1);
        }
        else
        {
            rigidBody.AddForce(movement);
        }
    }
    void collidedWithEnemy(Enemy enemy)
    {
        enemy.Attack(this);
        if (health <= 0)
        {
            if (onPlayerDeath != null)
            {
                onPlayerDeath(this);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            collidedWithEnemy(enemy);
        }
    }
}
