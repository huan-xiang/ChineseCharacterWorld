using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机追踪跟随，非锁定
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public GameManagement gameManagement;
    public GameObject target;
    /// <summary>
    /// 摄像机移动速度
    /// </summary>
    public float lerpSpeed;
    /// <summary>
    /// 上下左右观看的范围
    /// </summary>
    public float expandRange;
    /// <summary>
    /// 坐标差
    /// </summary>
    public Vector3 offset;
    /// <summary>
    /// 目标坐标
    /// </summary>
    private Vector3 targetPos;
    
    private void Start()
    {
        if (target == null) return;
        lerpSpeed = 1.0f;
        expandRange = 3f;
        //offset = transform.position - target.transform.position;
    }

    private void Update()
    {
        /*无目标返回*/
        if (target == null) return;

        targetPos = target.transform.position + offset;
        //ChangeTargetPos();
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
    private void ChangeTargetPos()
    {
        /*向上看*/
        if (Input.GetKey(gameManagement.playerController.lookUpKey))
        {
            Vector3 cameraMove = new Vector3(0, expandRange, 0);
            targetPos = target.transform.position + offset + cameraMove;
        }
        /*向下看*/
        if (Input.GetKey(gameManagement.playerController.crouchKey))
        {
            Vector3 cameraMove = new Vector3(0, -expandRange, 0);
            targetPos = target.transform.position + offset + cameraMove;
        }

    }
}
