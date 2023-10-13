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
    public GameObject stateIcon;
    public GameObject burnEffect;

    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    private Vector2 input;
    private Base theBase;
    private float originalMoveSpeed;
    private float originalHeath;    
    private EnemyState enemyState = EnemyState.normal;

     enum EnemyState
    {
        normal,
        burn,
        slow,
        stunned

    }
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
        originalHeath = enemyHeath;
        originalMoveSpeed= moveSpeed;
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
    public void takeDamage(float damage)
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


    public void ApplyBurnEffect(float duration, float damagePercent) {
        float totalDamage = originalHeath * damagePercent;
        StartCoroutine(BurnEnemy(duration, totalDamage));
    }

    public void ApplySlowEffect(float duration, float slowPercent) {
        StartCoroutine(SlowEnemy(duration, slowPercent));
    }

    public void ApplyStunnedEffect(float duration){
        StartCoroutine(StunEnemy(duration));
    }

    private IEnumerator SlowEnemy(float duration, float slowPercent)
    {
        float elapsedTime = 0;
        moveSpeed = moveSpeed * (1 - slowPercent);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (enemyState == EnemyState.stunned)
            {
                yield return null;
            }
            else
            {
                UpdateState(EnemyState.slow);
                animator.speed = 1 - slowPercent;                
                yield return null;
            }
            
        }
        animator.speed = 1;
        moveSpeed = originalMoveSpeed;
        UpdateState(EnemyState.normal);
    }

    private IEnumerator StunEnemy(float duration)
    {
        float elapsedTime = 0;

        UpdateState(EnemyState.stunned);
        while (elapsedTime < duration)
        {
            moveSpeed = 0;
            animator.speed = 0;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.speed = 1;
        moveSpeed = originalMoveSpeed;
        UpdateState(EnemyState.normal);
    }

    private IEnumerator BurnEnemy(float duration, float totalDamage)
    {
        float elapsedTime = 0;
        float damageByTime;
    
        if (burnEffect != null) burnEffect.SetActive(true);                 
        UpdateState(EnemyState.burn);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime <= duration)
            {
                damageByTime = totalDamage * (Time.deltaTime / duration);
            }
            else
            {
                damageByTime = totalDamage * ((Time.deltaTime - (elapsedTime - duration)) / duration);
            }
            takeDamage(damageByTime);
            yield return null;
        }
        if (burnEffect != null)
            burnEffect.SetActive(false);
        UpdateState(EnemyState.normal);
    }

    private void UpdateState(EnemyState state)
    {
        enemyState = state;
        SpriteRenderer spriteRenderer = stateIcon.GetComponent<SpriteRenderer>();
        Object[]  sprites = Resources.LoadAll("Spirites/Enemy/States");
        switch (state)
        {
            case EnemyState.normal:
                spriteRenderer.sprite = null;
                break;
            case EnemyState.burn:
                spriteRenderer.sprite = (Sprite) sprites[25];
                break;
            case EnemyState.slow:
                spriteRenderer.sprite = (Sprite)sprites[72];
                break;
            case EnemyState.stunned:
                spriteRenderer.sprite = (Sprite)sprites[56];
                break;
        }
    }

}
