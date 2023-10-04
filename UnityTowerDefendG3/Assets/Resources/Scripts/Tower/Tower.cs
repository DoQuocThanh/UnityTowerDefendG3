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

    [Header("References")]
    public LayerMask enemyMask;
    private bool _gameStarted;
    public GameObject bulletPrefab;
    public Transform firingPoint;

    private Transform target;

    void Start()
    {
        _gameStarted = true;

        checkCounter = firerate;

    }
    void Update()
    {
        if (target == null)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range,
                (Vector2)transform.position, 0f, enemyMask);
            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }
            return;
        }
        RotateTowardsTarget();


        if (Vector2.Distance(target.position, transform.position) > range)
        {
            target = null;
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = firerate;
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void RotateTowardsTarget()
    {
        float angle = Vector3.SignedAngle(transform.up, target.transform.position - transform.position, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
            Gizmos.DrawWireSphere(transform.position, range);
    }

   
}
