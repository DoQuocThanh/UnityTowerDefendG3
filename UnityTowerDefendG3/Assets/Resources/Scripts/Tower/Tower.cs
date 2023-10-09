using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public enum Target
{
    WeakestEnememies,
    StrongestEnememies,
    First,
    Last
}
public class Tower : MonoBehaviour
{
    [Header("Target Selection")]
    public Target target;

    [Header("Attribute")]
    public float firerate = .2f;
    private float checkCounter;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firingPoint;
    private List<EnemyController> eList = new List<EnemyController>();
    public EnemyController enemyController { get; set; }
    private Base theBase;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();

    }
    void Start()
    {
        theBase = FindObjectOfType<Base>();
        checkCounter = firerate;
    }
    private void OnMouseDown()
    {
        float radius = circleCollider.radius;
        range.transform.localScale = new Vector3(radius, radius, radius);

    }
    private void Update()
    {
        if (theBase.currentHeath > 0)
        {
            SelectionTarget();
            RotateTowardsTarget();
            if (enemyController != null)
            {
                Shoot();
            }
        }
    }
    private void SelectionTarget()
    {
        switch (target)
        {
            case Target.First:
                if (eList.Count > 0)
                {
                    enemyController = eList[0];
                }
                break;
            case Target.Last:
                if (eList.Count > 0)
                {
                    enemyController = eList[eList.Count - 1];
                }
                break;
            case Target.StrongestEnememies:
                var desc = eList.OrderByDescending(o => o.GetComponent<EnemyController>().enemyHeath);
                enemyController = desc.FirstOrDefault();
                break;
            case Target.WeakestEnememies:
                var asc = eList.OrderBy(o => o.GetComponent<EnemyController>().enemyHeath);
                enemyController = asc.FirstOrDefault();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D objec)
    {
       
            if (objec.tag == "Enemy")
            {
                EnemyController enemy = objec.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    eList.Add(enemy);
                }
            }      

    }

    private void OnTriggerExit2D(Collider2D objec)
    {
        if (objec.tag == "Enemy")
        {
            EnemyController enemy = objec.gameObject.GetComponent<EnemyController>();
            if (eList.Contains(enemy))
            {
                eList.Remove(enemy);
            }
        }
    }

    private void RotateTowardsTarget()
    {
        if (isHuman)
        {
            if (enemyController == null) return;
        

        }
        else
        {
            if (enemyController == null) return;
            float angle = Vector3.SignedAngle(transform.up, enemyController.transform.position - transform.position, transform.forward);
            transform.Rotate(0f, 0f, angle);
        }
       
       
        //Xac dinh bool right or left
    }
    private void Shoot()
    {

        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = firerate;
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetTarget(enemyController);
        }

    }

    

    /* private void OnDrawGizmos()
     {
         if (!_gameStarted)
             Gizmos.DrawWireSphere(transform.position, range);
     }*/


}
