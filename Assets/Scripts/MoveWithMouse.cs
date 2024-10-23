using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MoveWithMouse : MonoBehaviour
{
    public float speed =200;

    public bool controlX = false;
    public bool controlY = false;
    private void Update()
    {
        if (controlX) moveinX();
        if (controlY) moveinY();
    }

    private void moveinX()
    {
        float xmove = Input.GetAxis("Mouse X");
        if (xmove !=0)
        {   
            transform.Rotate(0,-xmove*speed,0);
        }
    }  
    private void moveinY()
    {
        float xmove = Input.GetAxis("Mouse Y");
        if (xmove !=0)
        {   
            transform.Rotate(xmove*speed,0,0);
        }
    }
}
