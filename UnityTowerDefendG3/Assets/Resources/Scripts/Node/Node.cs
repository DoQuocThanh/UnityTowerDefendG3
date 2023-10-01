using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public GameObject Tower;
    private bool hasTower = false;

    private void OnMouseDown()
    {
        if(hasTower)
        {
            Instantiate(Tower, transform.position, Quaternion.identity);
            hasTower = true;
        }
    }
}
