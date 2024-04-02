using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    /// <summary>
    /// ��ǰ��ͼ
    /// </summary>
    public MapObj nowMap;
    /// <summary>
    /// Ŀ���ͼ
    /// </summary>
    public MapObj aimMap;
    /// <summary>
    /// Ŀ�괫����
    /// </summary>
    public Portal aimPortal;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                nowMap.Exit();
                aimMap.Enter();
                collision.transform.position = aimPortal.transform.position;
                if (this.GetComponent<bgmChange>())
                {
                    this.GetComponent<bgmChange>().ChangeBGM(this.GetComponent<bgmChange>().bgm1);
                }
                if(this.GetComponent<SetRelifePos>())
                {
                    collision.GetComponent<PlayerObject>().nowRelifePos = this.GetComponent<SetRelifePos>().SetPlayerRelifePos();
                }
            }
        }
    }
}
