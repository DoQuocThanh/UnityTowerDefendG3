using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    [Header("References")]
   
    [Header("Attributes")]
    public float firerateBullet = 0.001f;
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
       
        float angle = Vector3.SignedAngle(transform.up, target.transform.position - transform.position, transform.forward);
        transform.Rotate(0f, 0f, angle);
        AudioManeger.Instance.PlaySFX("gun");

        transform.position = Vector2.MoveTowards(transform.position,target.transform.position,
           firerateBullet * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("cc");
            other.gameObject.GetComponent<EnemyController>().takeDamage(bulletDamage);
            AudioManeger.Instance.PlaySFX("tieng_sung");

        }
        Destroy(gameObject);
    }

  

}
