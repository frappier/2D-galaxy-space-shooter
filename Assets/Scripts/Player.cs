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
    private float _speed = 3.5f;

   
    // Start is called before the first frame update
    void Start()
    {
        //Move player to starting position at start
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(new Vector3(1, 0, 0));
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //or
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        //or
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);   //putting the direction into a variable to use in the Translate
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
