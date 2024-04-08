using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    /// <summary>
    /// 当前地图
    /// </summary>
    public MapObj nowMap;
    /// <summary>
    /// 目标地图
    /// </summary>
    public MapObj aimMap;
    /// <summary>
    /// 目标传送门
    /// </summary>
    public Portal aimPortal;
    public PixelCharacterController characterController;
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (characterController != null)
            {
                nowMap.Exit();
                aimMap.Enter();
                characterController.transform.position = aimPortal.transform.position;
                if (this.GetComponent<bgmChange>())
                {
                    this.GetComponent<bgmChange>().ChangeBGM(this.GetComponent<bgmChange>().bgm1);
                }
                if (this.GetComponent<SetRelifePos>())
                {
                    characterController.GetComponent<PlayerObject>().nowRelifePos = this.GetComponent<SetRelifePos>().SetPlayerRelifePos();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    nowMap.Exit();
            //    aimMap.Enter();
            //    collision.transform.position = aimPortal.transform.position;
            //    if (this.GetComponent<bgmChange>())
            //    {
            //        this.GetComponent<bgmChange>().ChangeBGM(this.GetComponent<bgmChange>().bgm1);
            //    }
            //    if (this.GetComponent<SetRelifePos>())
            //    {
            //        collision.GetComponent<PlayerObject>().nowRelifePos = this.GetComponent<SetRelifePos>().SetPlayerRelifePos();
            //    }
            //}
            characterController = collision.GetComponent<PixelCharacterController>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            characterController = null;
        }
    }
}
