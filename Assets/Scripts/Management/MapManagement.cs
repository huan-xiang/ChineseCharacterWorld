using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagement : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// ��ͼ�б�
    /// </summary>
    public List<MapObj> mapList;
    private void Start()
    {
        gameManagement = (GameManagement)FindObjectOfType(typeof(GameManagement));
    }
}
