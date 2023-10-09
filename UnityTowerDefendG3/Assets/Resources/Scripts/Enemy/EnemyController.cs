using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [Header("Attribute")]
    public float moveSpeed;
    public float enemyHeath;
    public int moneyOnDeath = 50;

    [Header("References")]
    public Animator animator;
    public Slider enemyHeathSlider;


    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    private Vector2 input;
    private Base theBase;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        thePath = FindObjectOfType<Path>();
        theBase = FindObjectOfType<Base>();
        enemyHeathSlider.maxValue = enemyHeath;
        enemyHeathSlider.value = enemyHeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (theBase.currentHeath > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, thePath.points[currentPoint].position,
            moveSpeed * Time.deltaTime);
            //animation of move
            input = (thePath.points[currentPoint].position - transform.position).normalized;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        
            if (Vector2.Distance(transform.position, thePath.points[currentPoint].position) < .2f)
            {
                currentPoint = currentPoint + 1;
                if (currentPoint >= thePath.points.Length)
                {
                    theBase.takeDamage(enemyHeath);
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void takedamage(float damage)
    {
        enemyHeath = Mathf.Clamp(enemyHeath - damage, 0, enemyHeathSlider.maxValue);
        if (enemyHeath > 0)
        {
            enemyHeathSlider.value = enemyHeath;
        }
        else
        {
            Money.instance.GiveMoney(moneyOnDeath);
            Destroy(gameObject);
        }
        
    }
}
