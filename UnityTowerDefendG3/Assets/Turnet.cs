using UnityEngine;
using System.Collections.Generic;

public class Turnet : MonoBehaviour
{
    public float range = 3f;
    public Collider2D[] colliderInRange;
    public List<EnemyController> enemiesInRange = new List<EnemyController>();
    public float checkTime = .2f;
    private float checkCounter;

    void Start()
    {
        checkCounter = checkTime;
    }

    void Update()
    {
        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = checkTime;
            colliderInRange = Physics2D.OverlapCircleAll(transform.position, range);
            enemiesInRange.Clear();
            foreach (Collider2D item in colliderInRange)
            {
                EnemyController enemy = item.GetComponent<EnemyController>();
                if (enemy != null) // Kiểm tra nếu đó thực sự là một Enemy
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }
}
