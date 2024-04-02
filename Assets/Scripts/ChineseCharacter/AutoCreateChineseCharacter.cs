using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 管理员修改预制体数据，打包不要加
/// </summary>
public class AutoCreateChineseCharacter : MonoBehaviour
{
    /// <summary>
    /// 默认汉字预制体
    /// </summary>
    public GameObject defaultChineseCharacter;
    /// <summary>
    /// 生成的新汉字的根节点
    /// </summary>
    public GameObject root;
    [Header("自动生成缺失汉字按键")]
    public KeyCode autoCreateKey;
    [Header("自动生成汉字Canbe列表按键")]
    public KeyCode autoCreateCanBe;
    private void Update()
    {
        if (Input.GetKey(autoCreateKey))
        {
            AutoCreate();
        }
        if (Input.GetKey(autoCreateCanBe))
        {
            AutoCreateCanBe();
        }
    }
    /// <summary>
    /// 自动生成缺失汉字
    /// </summary>
    public void AutoCreate()
    {
        GameObject[] allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/");
        List<ChineseCharacter> chineseCharacters= new List<ChineseCharacter>();
        /*新生成的汉字名称列表*/
        List<string> newChineseCharacterList = new List<string>();
        for (int i = 0; i < allCharacterObj.Length; i++)
        {
            chineseCharacters.Add(allCharacterObj[i].GetComponent<ChineseCharacter>());
        }
        /*检查是否有不存在的字*/
        foreach(ChineseCharacter chineseCharacter in chineseCharacters)
        {
            /*检查拆解字库*/
            for(int i = 0; i < chineseCharacter.split_aim.Count; i++)
            {
                string newName = chineseCharacter.split_aim[i].uniWord;
                /*有不存在的字*/
                if (Resources.Load("ChineseCharacter/" + newName) == null
                    && !newChineseCharacterList.Contains(newName))
                {
                    newChineseCharacterList.Add(newName);
                    GameObject newChineseCharacter = Instantiate(defaultChineseCharacter) as GameObject;
                    newChineseCharacter.transform.SetParent(root.transform);
                    newChineseCharacter.name = newName;
                    newChineseCharacter.GetComponent<ChineseCharacter>().characterName = newName;
                }
            }
            /*检查需求字库*/
            for (int i = 0; i < chineseCharacter.need.Count; i++)
            {
                string newName = chineseCharacter.need[i].uniWord;
                /*有不存在的字*/
                if (Resources.Load("ChineseCharacter/" + newName) == null
                    && !newChineseCharacterList.Contains(newName))
                {
                    newChineseCharacterList.Add(newName);
                    GameObject newChineseCharacter = Instantiate(defaultChineseCharacter) as GameObject;
                    newChineseCharacter.transform.SetParent(root.transform);
                    newChineseCharacter.name = newName;
                    newChineseCharacter.GetComponent<ChineseCharacter>().characterName = newName;
                }
            }
        }
    }
    /// <summary>
    /// 自动生成汉字Canbe列表
    /// </summary>
    public void AutoCreateCanBe()
    {
        bool finnish = true;
        GameObject[] allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/");
        List<ChineseCharacter> chineseCharacters = new List<ChineseCharacter>();
        for (int i = 0; i < allCharacterObj.Length; i++)
        {
            chineseCharacters.Add(allCharacterObj[i].GetComponent<ChineseCharacter>());
        }
        /*修改预制体*/
        foreach (ChineseCharacter chineseCharacter in chineseCharacters)
        {
            /*只要检查需求库*/
            /*检查需求字库*/
            for (int i = 0; i < chineseCharacter.need.Count; i++)
            {
                string needName = chineseCharacter.need[i].uniWord;
                GameObject needObj = Resources.Load<GameObject>("ChineseCharacter/" + needName);
                /*存在这个字*/
                if (needObj != null)
                {
                    List<string> canbe = needObj.GetComponent<ChineseCharacter>().canBe;
                    /*没有则加入*/
                    if (!canbe.Contains(chineseCharacter.characterName))
                    {
                        canbe.Add(chineseCharacter.characterName);
                        /*只要改过一个就说明存在需要修改的预制体，需要再次运行*/
                        finnish = false;
                    }
                }
            }
        }
        /*保存预制体*/
        foreach(GameObject gameObject in allCharacterObj)
        {
            var path = AssetDatabase.GetAssetPath(gameObject);
            var instance = PrefabUtility.LoadPrefabContents(path);
            PrefabUtility.SaveAsPrefabAsset(instance, path);
            PrefabUtility.UnloadPrefabContents(instance);
            EditorUtility.SetDirty(gameObject);
        }
        AssetDatabase.SaveAssets();
        Debug.Log(finnish?"全部修改完毕":"已修改，请重新运行来检查");
    }
}
