using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationUISystem : MonoBehaviour
{
    /// <summary>
    /// 目标汉字
    /// </summary>
    public ChineseCharacter aimCharacter;
    /// <summary>
    /// 组合汉字的文本
    /// </summary>
    public Text characterNameText;
    /// <summary>
    /// 组合汉字需求名称列表
    /// </summary>
    public List<Text> needNamesText;
    /// <summary>
    /// 是否持有表
    /// </summary>
    public List<bool> hasList;
    void Update()
    {
        if (aimCharacter == null) return;
        characterNameText.text = aimCharacter.characterName;
        for (int i = 0; i < 6; i++)
        {
            if(i < aimCharacter.split_aim.Count)
            {
                needNamesText[i].gameObject.SetActive(true);
                needNamesText[i].text = aimCharacter.split_aim[i].uniWord;
                needNamesText[i].gameObject.GetComponentInChildren<BoolDisplay>().value = hasList[i];
            }
            else
            {
                needNamesText[i].text = "";
                /*不需要收集*/
                hasList[i] = true;
                needNamesText[i].gameObject.SetActive(false);
            }
        }
    }
}
