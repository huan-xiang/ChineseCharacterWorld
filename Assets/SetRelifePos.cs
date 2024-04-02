using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRelifePos : MonoBehaviour
{
    public GameManagement gameManagement;
    public dir myDir;
    public enum dir
    {
        下一个,
        上一个
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }

    public GameObject SetPlayerRelifePos()
    {
        if(myDir==dir.下一个)
        {
            GameManagement.relifePosIndex++;
            GameObject tPos = gameManagement.relifePosList[GameManagement.relifePosIndex];
            return tPos;
        }
        else
        {
            GameManagement.relifePosIndex--;
            GameObject tPos = gameManagement.relifePosList[GameManagement.relifePosIndex];
            return tPos;
        }
    }
}
