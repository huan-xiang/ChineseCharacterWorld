using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGround : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Boss")
        {
            collision.gameObject.GetComponent<BossObject>().isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossObject>().isGround = false;
        }
    }
}
