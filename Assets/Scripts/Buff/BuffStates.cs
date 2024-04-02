using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff��
/// </summary>
public class BuffStates : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// buff�б�
    /// </summary>
    public List<BuffObj> buffList;
    /// <summary>
    /// �������ֱ������������̳б�����
    /// </summary>
    public List<ChineseCharacter> passiveChineseCharacterList;
    /// <summary>
    /// buff���Ľ�ɫ
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
    /// �ж��Ƿ����buff
    /// </summary>
    /// <param name="buffName">buff����</param>
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
