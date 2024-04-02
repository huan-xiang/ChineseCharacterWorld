using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������������
/// </summary>
public class AbsorbObj : MonoBehaviour
{
    /// <summary>
    /// ���յķ�Χ����
    /// </summary>
    public GameObject range;
    /// <summary>
    /// ����ĺ��ֱ�
    /// </summary>
    public List<ChineseCharacter> saveChineseCharacters;
    /// <summary>
    /// �����б�
    /// </summary>
    public CharacterStates activeList;
    void Update()
    {
        

    }

    public void AbsorbStart()
    {
        /*�����ײ�б�*/
        saveChineseCharacters.Clear();
    }
    /// <summary>
    /// ���ս���
    /// ��ȡ����
    /// </summary>
    public void AbsorbOver()
    {
        foreach (ChineseCharacter chineseCharacter in saveChineseCharacters)
        {
            if(!activeList.Contains(chineseCharacter.characterName))
                activeList.chineseCharacters.Add(chineseCharacter);
        }
        /*�����ײ�б�*/
        saveChineseCharacters.Clear();
        /*����*/
        transform.gameObject.SetActive(false);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "SceneObject")
        {
            /*�������Ļ������ּ��봢�溺�ֱ�*/
            ChineseCharacter[] sceneObjects = collision.gameObject.GetComponentsInChildren<ChineseCharacter>();
            foreach(ChineseCharacter sceneObject in sceneObjects)
            {
                if (!saveChineseCharacters.Contains(sceneObject))
                {
                    saveChineseCharacters.Add(sceneObject);
                }
            }
        }
    }
}
