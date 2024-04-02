using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObj : MonoBehaviour
{
    /// <summary>
    /// ��ͼ����
    /// </summary>
    public string mapName;
    /// <summary>
    /// ����
    /// </summary>
    public GameObject background;
    /// <summary>
    /// ��ͼ����������ڵ�
    /// </summary>
    public GameObject sceneObj;
    /// <summary>
    /// �뿪��ͼ
    /// </summary>
    public void Exit()
    {
        background.SetActive(false);
    }
    /// <summary>
    /// �����ͼ
    /// </summary>
    public void Enter()
    {
        background.SetActive(true);
    }
}
