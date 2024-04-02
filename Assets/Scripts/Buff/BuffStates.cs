using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff栏
/// </summary>
public class BuffStates : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// buff列表
    /// </summary>
    public List<BuffObj> buffList;
    /// <summary>
    /// 被动汉字表，如果是主角则继承被动栏
    /// </summary>
    public List<ChineseCharacter> passiveChineseCharacterList;
    /// <summary>
    /// buff栏的角色
    /// </summary>
    public GameObject character;
    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        if (gameObject.tag == "Player")
        {
            passiveChineseCharacterList = gameManagement.characterStates[1].chineseCharacters;
        }
    }
    private void Update()
    {
        gameManagement.buffManagement.CheckPassiveList(this);
    }
    /// <summary>
    /// 判断是否存在buff
    /// </summary>
    /// <param name="buffName">buff名称</param>
    /// <returns></returns>
    public bool CheckBuffObjList(string buffName)
    {
        foreach(BuffObj buffObj in buffList)
        {
            if(Enum.GetName(typeof(BuffManagement.BuffName), buffObj.buffName) == buffName)
            {
                return true;
            }
        }
        return false;
    }
}
