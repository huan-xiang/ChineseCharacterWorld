using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public GameObject tipImageObj;
    /// <summary>
    /// �Ƿ���ʾ
    /// </summary>
    public bool isDisplay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isDisplay)
            {
                tipImageObj.SetActive(true);
                isDisplay = true;
            }
            else
            {
                tipImageObj.SetActive(false);
                isDisplay = false;
            }
        }
    }
}
