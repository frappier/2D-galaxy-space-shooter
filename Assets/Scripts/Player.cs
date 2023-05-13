﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public or Private
    //Have a data type: int, float, bool, string
    //have to have a name
    //Optional assigned value

    [SerializeField]
    private float speed = 3.5f;

   
    // Start is called before the first frame update
    void Start()
    {
        //Move player to starting position at start
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(1, 0, 0));
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
