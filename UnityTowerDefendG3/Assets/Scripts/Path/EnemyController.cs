using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed ;

    private Path thePath;
    private int currentPoint;
    private bool reachedEnd ;

    public float timeBetweenAttacks ,damagePerAttack ;
    private float attackCounter;
    private Base theBase;
    // Start is called before the first frame update
    void Start()
    {
        thePath = FindObjectOfType<Path>(); 
        theBase = FindObjectOfType<Base>();
        attackCounter = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (reachedEnd == false)
        {
            //transform.LookAt(thePath.points[currentPoint].position);
            transform.position = Vector2.MoveTowards(transform.position, thePath.points[currentPoint].position,
                moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, thePath.points[currentPoint].position) < .01f)
            {
                currentPoint = currentPoint + 1;
                if (currentPoint >= thePath.points.Length)
                {
                        reachedEnd = true;
                }
            }
        }
        else
        {
            
              
                theBase.takeDamage(damagePerAttack);
                
           
        }
    }
}
