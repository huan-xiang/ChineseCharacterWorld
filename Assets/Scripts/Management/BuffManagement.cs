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
    /// buffԤ����
    /// </summary>
    public GameObject saveBuffObj;
    public enum BuffName
    {
        ��,��,��,��
    }
    /// <summary>
    /// �����б�
    /// </summary>
    public List<RuntimeAnimatorController> animationList;
    /// <summary>
    /// buffӰ��
    /// </summary>
    /// <param name="buffName">buff����</param>
    /// <param name="character">Ӱ�쵥λ</param>
    public void BuffInfluent(BuffName buffName,GameObject character)
    {
        switch (buffName)
        {
            case BuffName.��:
                Burn(character);
                break;
            case BuffName.��:
                Heal(character);
                break;
            case BuffName.��:
                Paralysis(character);
                break;
            case BuffName.��:
                BurnLevel2(character);
                break;
        }
    }
    /// <summary>
    /// ��鱻�����ֱ�
    /// </summary>
    /// <param name="passiveChineseCharacterList">�������ֱ�</param>
    public void CheckPassiveList(BuffStates buffStates)
    {
        foreach(ChineseCharacter chineseCharacter in buffStates.passiveChineseCharacterList)
        {
            /*����ö���б�*/
            foreach(string buffName in Enum.GetNames(typeof(BuffName)))
            {
                /*��������buff��*/
                if(chineseCharacter.characterName == buffName)
                {
                    /*�����ظ����buff*/
                    if (!buffStates.CheckBuffObjList(buffName))
                    {
                        /*�ַ���ת����ö��*/
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
    /// ��λ���buff
    /// </summary>
    /// <param name="buffStates"></param>
    public void GetBuff(BuffStates buffStates, BuffName buffName)
    {
        GameObject newBuffObj = Instantiate(saveBuffObj);
        BuffObj newBuff = newBuffObj.GetComponent<BuffObj>();
        switch (buffName)
        {
            case BuffName.��:
                newBuffObj.gameObject.name = "��buff";
                newBuffObj.transform.SetParent(buffStates.transform);
                newBuffObj.transform.localPosition = new Vector3(0, 0.5f, -1);
                newBuff.buffManagement = this;
                newBuff.triggerTime = 60;
                newBuff.finalTime = 600;
                newBuff.buffStates = buffStates;
                newBuff.buffName = buffName;
                newBuff.animator.SetInteger("BuffNumber", (int)BuffName.��);
                buffStates.buffList.Add(newBuff);
                break;
            case BuffName.��:
                newBuffObj.gameObject.name = "��buff";
                newBuffObj.transform.SetParent(buffStates.transform);
                newBuffObj.transform.localPosition = new Vector3(0, 1, -1);
                newBuff.buffManagement = this;
                newBuff.triggerTime = 60;
                newBuff.finalTime = 600;
                newBuff.buffStates = buffStates;
                newBuff.buffName = buffName;
                newBuff.animator.SetInteger("BuffNumber", (int)BuffName.��);
                buffStates.buffList.Add(newBuff);
                break;
            case BuffName.��:
                newBuffObj.gameObject.name = "��buff";
                newBuffObj.transform.SetParent(buffStates.transform);
                newBuffObj.transform.localPosition = new Vector3(0, 0.25f, -1);
                newBuff.buffManagement = this;
                newBuff.triggerTime = 20;
                newBuff.finalTime = 120;
                newBuff.buffStates = buffStates;
                newBuff.buffName = buffName;
                newBuff.animator.SetInteger("BuffNumber", (int)BuffName.��);
                buffStates.buffList.Add(newBuff);
                break;
            default:
                Destroy(newBuffObj);
                break;
        }
    }
    /// <summary>
    /// ����
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
    /// ��������
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
    /// ����
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
    /// ���
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
