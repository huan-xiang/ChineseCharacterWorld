using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="MovableGround")
        {
            collision.GetComponent<Office>().open = !collision.GetComponent<Office>().open;
        }
    }
}
