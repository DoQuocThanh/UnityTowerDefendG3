using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    public Animator animator;
    private Vector2 input;
    public float enemyHeath;
    private Base theBase;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        thePath = FindObjectOfType<Path>();
        theBase = FindObjectOfType<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theBase.currentHeath > 0)
        {


            if (reachedEnd == false)
            {

                transform.position = Vector2.MoveTowards(transform.position, thePath.points[currentPoint].position,
                moveSpeed * Time.deltaTime);
                //animation of move
                input = (thePath.points[currentPoint].position - transform.position).normalized;
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                if (Vector2.Distance(transform.position, thePath.points[currentPoint].position) < .2f)

                {
                    transform.position = Vector2.MoveTowards(transform.position, thePath.points[currentPoint].position,
                        moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, thePath.points[currentPoint].position) < .2f)
                    {
                        currentPoint = currentPoint + 1;
                        if (currentPoint >= thePath.points.Length)
                        {
                            reachedEnd = true;
                        }
                    }
                }

            }
            else
            {
                theBase.takeDamage(enemyHeath);
                gameObject.SetActive(false);
            }




        }
    }
}
