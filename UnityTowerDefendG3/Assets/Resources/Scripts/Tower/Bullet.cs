using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    [Header("Attributes")]
    public float firerateBullet = 5f;
    public float bulletDamage;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    //FixedUpdate is call once per 0.02s(xu li vat li) 
    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * firerateBullet;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().takedamage(bulletDamage);
        }
        Destroy(gameObject);
    }
   
}
