using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// ����Ա�޸�Ԥ�������ݣ������Ҫ��
/// </summary>
public class AutoCreateChineseCharacter : MonoBehaviour
{
    /// <summary>
    /// Ĭ�Ϻ���Ԥ����
    /// </summary>
    public GameObject defaultChineseCharacter;
    /// <summary>
    /// ���ɵ��º��ֵĸ��ڵ�
    /// </summary>
    public GameObject root;
    [Header("�Զ�����ȱʧ���ְ���")]
    public KeyCode autoCreateKey;
    [Header("�Զ����ɺ���Canbe�б���")]
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
    /// �Զ�����ȱʧ����
    /// </summary>
    public void AutoCreate()
    {
        GameObject[] allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/");
        List<ChineseCharacter> chineseCharacters= new List<ChineseCharacter>();
        /*�����ɵĺ��������б�*/
        List<string> newChineseCharacterList = new List<string>();
        for (int i = 0; i < allCharacterObj.Length; i++)
        {
            chineseCharacters.Add(allCharacterObj[i].GetComponent<ChineseCharacter>());
        }
        /*����Ƿ��в����ڵ���*/
        foreach(ChineseCharacter chineseCharacter in chineseCharacters)
        {
            /*������ֿ�*/
            for(int i = 0; i < chineseCharacter.split_aim.Count; i++)
            {
                string newName = chineseCharacter.split_aim[i].uniWord;
                /*�в����ڵ���*/
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
            /*��������ֿ�*/
            for (int i = 0; i < chineseCharacter.need.Count; i++)
            {
                string newName = chineseCharacter.need[i].uniWord;
                /*�в����ڵ���*/
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
    /// �Զ����ɺ���Canbe�б�
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
        /*�޸�Ԥ����*/
        foreach (ChineseCharacter chineseCharacter in chineseCharacters)
        {
            /*ֻҪ��������*/
            /*��������ֿ�*/
            for (int i = 0; i < chineseCharacter.need.Count; i++)
            {
                string needName = chineseCharacter.need[i].uniWord;
                GameObject needObj = Resources.Load<GameObject>("ChineseCharacter/" + needName);
                /*���������*/
                if (needObj != null)
                {
                    List<string> canbe = needObj.GetComponent<ChineseCharacter>().canBe;
                    /*û�������*/
                    if (!canbe.Contains(chineseCharacter.characterName))
                    {
                        canbe.Add(chineseCharacter.characterName);
                        /*ֻҪ�Ĺ�һ����˵��������Ҫ�޸ĵ�Ԥ���壬��Ҫ�ٴ�����*/
                        finnish = false;
                    }
                }
            }
        }
        /*����Ԥ����*/
        foreach(GameObject gameObject in allCharacterObj)
        {
            var path = AssetDatabase.GetAssetPath(gameObject);
            var instance = PrefabUtility.LoadPrefabContents(path);
            PrefabUtility.SaveAsPrefabAsset(instance, path);
            PrefabUtility.UnloadPrefabContents(instance);
            EditorUtility.SetDirty(gameObject);
        }
        AssetDatabase.SaveAssets();
        Debug.Log(finnish?"ȫ���޸����":"���޸ģ����������������");
    }
}
