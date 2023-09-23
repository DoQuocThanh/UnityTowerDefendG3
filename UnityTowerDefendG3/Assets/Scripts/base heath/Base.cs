using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float totalHeath = 100f;
    private float currentHeath = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHeath = totalHeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float input)
    {
        currentHeath -= input;
        if (currentHeath <= 0)
        {
            currentHeath = 0;
            gameObject.SetActive(false);
        }
    }
}
