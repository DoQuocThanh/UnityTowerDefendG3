using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    [Header("References")]
   
    [Header("Attributes")]
    public float firerateBullet = 1f;
    public float bulletDamage;
    private EnemyController target;
    public void SetTarget(EnemyController _target)
    {
        target = _target;
    }
    //FixedUpdate is call once per 0.02s(xu li vat li) 
    private void FixedUpdate()
    {
        if (target == null) return;
        transform.position = Vector2.MoveTowards(transform.position,target.transform.position,
           firerateBullet * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("cc");
            other.gameObject.GetComponent<EnemyController>().takedamage(bulletDamage);
        }
        Destroy(gameObject);
    }

   /* private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }*/

}
