using Cainos.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffManagement : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// buff预制体
    /// </summary>
    public GameObject saveBuffObj;
    public enum BuffName
    {
        火,愈,雷,炎
    }
    /// <summary>
    /// 动画列表
    /// </summary>
    public List<RuntimeAnimatorController> animationList;
    /// <summary>
    /// buff影响
    /// </summary>
    /// <param name="buffName">buff名称</param>
    /// <param name="character">影响单位</param>
    public void BuffInfluent(BuffName buffName,GameObject character)
    {
        switch (buffName)
        {
            case BuffName.火:
                Burn(character);
                break;
            case BuffName.愈:
                Heal(character);
                break;
            case BuffName.雷:
                Paralysis(character);
                break;
            case BuffName.炎:
                BurnLevel2(character);
                break;
        }
    }
    /// <summary>
    /// 检查被动汉字表
    /// </summary>
    /// <param name="passiveChineseCharacterList">被动汉字表</param>
    public void CheckPassiveList(BuffStates buffStates)
    {
        foreach(ChineseCharacter chineseCharacter in buffStates.passiveChineseCharacterList)
        {
            /*遍历枚举列表*/
            foreach(string buffName in Enum.GetNames(typeof(BuffName)))
            {
                /*被动含有buff字*/
                if(chineseCharacter.characterName == buffName)
                {
                    /*不会重复添加buff*/
                    if (!buffStates.CheckBuffObjList(buffName))
                    {
                        /*字符串转换成枚举*/
                        GetBuff(buffStates, (BuffName)Enum.Parse(typeof(BuffName), buffName));
                        buffStates.passiveChineseCharacterList.Remove(chineseCharacter);
                        gameManagement.chineseCharacterManagement.createChineseCharacterList.Remove(chineseCharacter);
                        return;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 单位获得buff
    /// </summary>
    /// <param name="buffStates"></param>
    public void GetBuff(BuffStates buffStates, BuffName buffName)
    {
        GameObject newBuffObj = Instantiate(saveBuffObj);
        BuffObj newBuff = newBuffObj.GetComponent<BuffObj>();
        switch (buffName)
        {
            case BuffName.火:
                newBuffObj.gameObject.name = "火buff";
                newBuffObj.transform.SetParent(buffStates.transform);
                newBuffObj.transform.localPosition = new Vector3(0, 0.5f, -1);
                newBuff.buffManagement = this;
                newBuff.triggerTime = 60;
                newBuff.finalTime = 600;
                newBuff.buffStates = buffStates;
                newBuff.buffName = buffName;
                newBuff.animator.SetInteger("BuffNumber", (int)BuffName.火);
                buffStates.buffList.Add(newBuff);
                break;
            case BuffName.愈:
                newBuffObj.gameObject.name = "愈buff";
                newBuffObj.transform.SetParent(buffStates.transform);
                newBuffObj.transform.localPosition = new Vector3(0, 1, -1);
                newBuff.buffManagement = this;
                newBuff.triggerTime = 60;
                newBuff.finalTime = 600;
                newBuff.buffStates = buffStates;
                newBuff.buffName = buffName;
                newBuff.animator.SetInteger("BuffNumber", (int)BuffName.愈);
                buffStates.buffList.Add(newBuff);
                break;
            case BuffName.雷:
                newBuffObj.gameObject.name = "雷buff";
                newBuffObj.transform.SetParent(buffStates.transform);
                newBuffObj.transform.localPosition = new Vector3(0, 0.25f, -1);
                newBuff.buffManagement = this;
                newBuff.triggerTime = 20;
                newBuff.finalTime = 120;
                newBuff.buffStates = buffStates;
                newBuff.buffName = buffName;
                newBuff.animator.SetInteger("BuffNumber", (int)BuffName.雷);
                buffStates.buffList.Add(newBuff);
                break;
            default:
                Destroy(newBuffObj);
                break;
        }
    }
    /// <summary>
    /// 烧伤
    /// </summary>
    public void Burn(GameObject character)
    {
        if (character.GetComponent<PlayerObject>())
        {
            character.GetComponent<PlayerObject>().GetDamage(1);
        }
        else if(character.GetComponent<MonsterObj>())
        {
            character.GetComponent<MonsterObj>().GetDamage(1);
        }
    }
    /// <summary>
    /// 二级烧伤
    /// </summary>
    /// <param name="character"></param>
    public void BurnLevel2(GameObject character)
    {
        if (character.GetComponent<PlayerObject>())
        {
            character.GetComponent<PlayerObject>().GetDamage(3);
        }
        else if (character.GetComponent<MonsterObj>())
        {
            character.GetComponent<MonsterObj>().GetDamage(3);
        }
    }
    /// <summary>
    /// 治愈
    /// </summary>
    public void Heal(GameObject character)
    {
        if (character.GetComponent<PlayerObject>())
        {
            character.GetComponent<PlayerObject>().GetDamage(-3);
        }
        else if (character.GetComponent<MonsterObj>())
        {
            character.GetComponent<MonsterObj>().GetDamage(-3);
        }
    }
    /// <summary>
    /// 麻痹
    /// </summary>
    /// <param name="character"></param>
    public void Paralysis(GameObject character)
    {
        if (character.GetComponent<PlayerObject>())
        {
            character.GetComponent<PixelCharacterController>().cantMoveTime = 10;
        }
    }
}
