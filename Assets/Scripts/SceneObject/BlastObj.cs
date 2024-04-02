using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastObj : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*��ʯͷ*/
        if (collision.GetComponent<StoneObj>())
        {
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
