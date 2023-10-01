using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attribute")]
    public float range = 3f;
    public float firerate = .2f;
    private float checkCounter;
    public float bps = 1f;

    [Header("References")]
    public Transform turretRotationPoint;
    public LayerMask enemyMask;
    private bool _gameStarted;
    public GameObject bulletPrefab;
    public Transform firingPoint;


    private Transform target;
    private float timeUntilFire;
    void Start()
    {
        _gameStarted = true;
       
        checkCounter = firerate;

    }
    void Update()
    {
       if (target == null) {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range,
                transform.position, 0f,enemyMask);   
            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }
            return;
        }
        RotateTowardsTarget();
        if (Vector2.Distance(target.position,transform.position) > range)
        {
            target = null;
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0) {
                checkCounter = firerate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x -
        transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = targetRotation;
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.forward,range);
        }
    }

  /*  public float fireRate = 2f;
    public float damage = 100f;
    private Collider2D[] colliderInRange;
    public List<EnemyController> enemiesInRange;
  */

    /* private void GetCurrentEnemyTarget()
     {


         if (enemiesInRange.Count<=0)
         {
             CurrentEnemyTarget = null;
         }
         else
         {
             CurrentEnemyTarget = enemiesInRange[0];

         }

     }

     private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.CompareTag("Enemy"))
         {
             checkCounter -= Time.deltaTime;
             if (checkCounter <= 0)
             {
                 checkCounter = checkTime;
                 colliderInRange = Physics2D.OverlapCircleAll(transform.position, range);
                 enemiesInRange.Clear();
                 foreach (Collider2D item in colliderInRange)
                 {
                     enemiesInRange.Add(item.GetComponent<EnemyController>());
                 }
             }
         }
     }
     private void RotateTowardsTarget()
     {
         if (CurrentEnemyTarget != null)
         {
             Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
             float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
             transform.Rotate(0f, 0f, angle);
             bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, CurrentEnemyTarget.transform.position,
                fireRate * Time.deltaTime);
             CurrentEnemyTarget.enemyHeath = damage - CurrentEnemyTarget.enemyHeath;
             if (CurrentEnemyTarget.enemyHeath <=0)
             {
                 Destroy(CurrentEnemyTarget);
             }
         }
         else return;
     }*/
}
