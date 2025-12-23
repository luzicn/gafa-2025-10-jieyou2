using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiaotai : MonoBehaviour
{
    PlatformEffector2D pe2d;

    void Start()
    {
        pe2d = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            pe2d.rotationalOffset = 180f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            pe2d.rotationalOffset = 0f;
        }
    }
}