using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed;
    public bool isMoving;
    private Vector2 input;

    // Start is called before the first frame update

    private void Awake()
    {

        animator = GetComponent<Animator>();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");   
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

    }
}
