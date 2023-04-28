using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    Vector3 shootDirection;



    void FixedUpdate()
    {
        this.transform.Translate(shootDirection * speed, Space.World);
    }

    // 2
    public void FireProjectile(Ray shootRay)
    {
        this.shootDirection = shootRay.direction;
        this.transform.position = shootRay.origin;
    }

    // 3
    void OnCollisionEnter(Collision col)
    {
        Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

}
