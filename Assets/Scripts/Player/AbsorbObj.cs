using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 吸收区域物体
/// </summary>
public class AbsorbObj : MonoBehaviour
{
    /// <summary>
    /// 吸收的范围物体
    /// </summary>
    public GameObject range;
    /// <summary>
    /// 保存的汉字表
    /// </summary>
    public List<ChineseCharacter> saveChineseCharacters;
    /// <summary>
    /// 主动列表
    /// </summary>
    public CharacterStates activeList;
    void Update()
    {
        

    }

    public void AbsorbStart()
    {
        /*清空碰撞列表*/
        saveChineseCharacters.Clear();
    }
    /// <summary>
    /// 吸收结束
    /// 获取汉字
    /// </summary>
    public void AbsorbOver()
    {
        foreach (ChineseCharacter chineseCharacter in saveChineseCharacters)
        {
            if(!activeList.Contains(chineseCharacter.characterName))
                activeList.chineseCharacters.Add(chineseCharacter);
        }
        /*清空碰撞列表*/
        saveChineseCharacters.Clear();
        /*隐藏*/
        transform.gameObject.SetActive(false);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "SceneObject")
        {
            /*将碰到的环境汉字加入储存汉字表*/
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
