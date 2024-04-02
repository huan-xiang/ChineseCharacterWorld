using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChineseCharacterManagement : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// 凭空创造的汉字列表
    /// </summary>
    public List<ChineseCharacter> createChineseCharacterList;
    private void Update()
    {
        foreach(ChineseCharacter chineseCharacter in createChineseCharacterList)
        {
            if(!gameManagement.choiceRouletteManagement.chineseCharacters.Contains(chineseCharacter)
                && !gameManagement.characterStates[0].chineseCharacters.Contains(chineseCharacter))
            {
                createChineseCharacterList.Remove(chineseCharacter);
                return;
            }
        }
    }
    /// <summary>
    /// 创建新汉字
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ChineseCharacter CreateChineseCharacter(string name)
    {
        GameObject newCharacterObj = Resources.Load<GameObject>("ChineseCharacter/" + name);
        if(newCharacterObj == null)
        {
            Debug.Log(name);
            return Resources.Load<GameObject>("ChineseCharacter/无").GetComponent<ChineseCharacter>();
        }
        ChineseCharacter newCharacter = newCharacterObj.GetComponent<ChineseCharacter>();
        createChineseCharacterList.Add(newCharacter);
        return newCharacter;
    }
}
