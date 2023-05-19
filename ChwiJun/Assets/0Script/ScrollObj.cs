﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObj : MonoBehaviour
{
    public float speed = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {  //&& !GameManager.instance.isGameClear
        if (!GameManager.instance.isGameOver )
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}