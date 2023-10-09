using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyMovementDoThanh : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [Header("References")]
    private Transform target;
    public Animator animator;
    private int pathIndex = 0;
    private Vector2 input;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        target = Levelmanage.main.path[0];   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Levelmanage.main.path[pathIndex].position,
           moveSpeed * Time.deltaTime);
        input = (Levelmanage.main.path[pathIndex].position - transform.position).normalized;
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);
        if (Vector2.Distance(target.position, transform.position) <= 0.2f) {
               pathIndex = pathIndex + 1;                   
        
            if (pathIndex >= Levelmanage.main.path.Length)
            {
                SpanwerEnemy.enemiesAlies--;
                Destroy(gameObject);
                return;
            }
            else {
                target = Levelmanage.main.path[pathIndex];
            }
        }
        
    }
 
}
