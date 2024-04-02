using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagement : MonoBehaviour
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// 地图列表
    /// </summary>
    public List<MapObj> mapList;
    private void Start()
    {
        gameManagement = (GameManagement)FindObjectOfType(typeof(GameManagement));
    }
}
