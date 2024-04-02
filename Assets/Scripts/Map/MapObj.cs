using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObj : MonoBehaviour
{
    /// <summary>
    /// 地图名称
    /// </summary>
    public string mapName;
    /// <summary>
    /// 背景
    /// </summary>
    public GameObject background;
    /// <summary>
    /// 地图场景物体根节点
    /// </summary>
    public GameObject sceneObj;
    /// <summary>
    /// 离开地图
    /// </summary>
    public void Exit()
    {
        background.SetActive(false);
    }
    /// <summary>
    /// 进入地图
    /// </summary>
    public void Enter()
    {
        background.SetActive(true);
    }
}
