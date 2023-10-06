using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanage : MonoBehaviour
{
    // Start is called before the first frame update

    public static Levelmanage main;

    public Transform startPoint;
    public Transform[] path;
    private void Awake()
    {
        main = this;    
    }

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
