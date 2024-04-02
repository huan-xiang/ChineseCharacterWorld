using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationUISystem : MonoBehaviour
{
    /// <summary>
    /// Ŀ�꺺��
    /// </summary>
    public ChineseCharacter aimCharacter;
    /// <summary>
    /// ��Ϻ��ֵ��ı�
    /// </summary>
    public Text characterNameText;
    /// <summary>
    /// ��Ϻ������������б�
    /// </summary>
    public List<Text> needNamesText;
    /// <summary>
    /// �Ƿ���б�
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
                /*����Ҫ�ռ�*/
                hasList[i] = true;
                needNamesText[i].gameObject.SetActive(false);
            }
        }
    }
}
