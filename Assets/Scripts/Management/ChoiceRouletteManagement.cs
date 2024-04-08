using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 选择轮盘管理器，用来选择组合汉字
/// </summary>
public class ChoiceRouletteManagement : MonoBehaviour
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// 可组合汉字列表
    /// </summary>
    public List<ChineseCharacter> chineseCharacters;
    /// <summary>
    /// 选择的汉字
    /// </summary>
    public ChineseCharacter choiceCharacter;
    /// <summary>
    /// 选择物体列表
    /// </summary>
    public List<RouletteChoice> choiceObjList;
    /// <summary>
    /// 汉字文本列表
    /// </summary>
    public List<Text> texts;
    /// <summary>
    /// 目前选择的编号
    /// </summary>
    public int choiceNumber;
    /// <summary>
    /// 当前页码
    /// </summary>
    public int page;
    /// <summary>
    /// 下一页的编号
    /// </summary>
    public int nextNumber;
    /// <summary>
    /// 背景的图片列表
    /// </summary>
    public List<Sprite> backgroundSpriteList;
    /// <summary>
    /// 背景图片
    /// </summary>
    public Image backgroundImage;
    /// <summary>
    /// 轮盘选择状态
    /// </summary>
    public enum ChoiceState
    {
        未选中,
        选择垂直,
        选择倾斜,
        按下垂直,
        按下倾斜
    }
    /// <summary>
    /// 当前轮盘选择状态
    /// </summary>
    public ChoiceState choiceState;
    /// <summary>
    /// 正在选择汉字
    /// </summary>
    public bool isChoosing;
    /// <summary>
    /// 组合汉字的UI
    /// </summary>
    public GameObject aimCharacterUI;
    /// <summary>
    /// 用来替代鼠标的物体
    /// </summary>
    public GameObject MouseObj;
    /// <summary>
    /// 角色提示
    /// </summary>
    public GameObject playerTips;
    private void Awake()
    {
        /*测试用，把所有汉字拿到*/
        //GameObject[] allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/");
        //for (int i = 0; i < allCharacterObj.Length; i++)
        //{
        //    chineseCharacters.Add(allCharacterObj[i].GetComponent<ChineseCharacter>());
        //}
        //GetNewCharacterList();
        /*从0开始*/
        page = 0;
        choiceNumber = -1;
        isChoosing = false;
    }
    private int saveCharacterStatesCount;
    private void FixedUpdate()
    {
        if (saveCharacterStatesCount != gameManagement.characterStates[0].chineseCharacters.Count)
        {
            saveCharacterStatesCount = gameManagement.characterStates[0].chineseCharacters.Count;
            GetNewCharacterList();
        }
        CheckMouse();
        UpdateBackground();
        UpdateCharacter();
    }
    /// <summary>
    /// 更新背景图片
    /// </summary>
    public void UpdateBackground()
    {
        /*根据选择数字改变轮盘状态*/
        choiceState = choiceNumber % 2 == 0 ? ChoiceState.选择垂直 : ChoiceState.选择倾斜;
        choiceState = choiceNumber == -1 ? ChoiceState.未选中 : choiceState;
        /*根据状态改变图片*/
        backgroundImage.sprite = backgroundSpriteList[(int)choiceState];
        /*根据当前选择数字旋转*/
        int times = Mathf.CeilToInt(choiceNumber / 2f)%4;
        backgroundImage.gameObject.transform.localEulerAngles = new Vector3(0, 0, -90 * times);
    }
    /// <summary>
    /// 更新汉字
    /// </summary>
    public void UpdateCharacter()
    {
        /*汉字编号，第一页有7个字，其他页只有5个字*/
        int characterNumber = page == 0?0: page * 5 + 2;
        /*更新文本*/
        for(int i = 0; i < 8; i++)
        {
            /*下一页按钮文本，不是最后一页*/
            if(i == page%8 && page < (chineseCharacters.Count -3)/5)
            {
                texts[i].text = "下";
                /*绑定新汉字*/
                choiceObjList[i].aimCharacter = null;
            }
            /*无文本，需要存在上一页*/
            else if (i == (page + 7) % 8 && page > 0)
            {
                texts[i].text = "";
                /*绑定新汉字*/
                choiceObjList[i].aimCharacter = null;
            }
            /*上一页文本，需要存在上一页*/
            else if (i == (page + 6) % 8 && page > 0)
            {
                texts[i].text = "上";
                /*绑定新汉字*/
                choiceObjList[i].aimCharacter = null;
            }
            /*汉字文本*/
            else if(characterNumber < chineseCharacters.Count)
            {
                texts[i].text = chineseCharacters[characterNumber].characterName;
                /*绑定新汉字*/
                choiceObjList[i].aimCharacter = chineseCharacters[characterNumber];
                characterNumber++;
            }
            /*无*/
            else
            {
                texts[i].text = "";
                /*绑定新汉字*/
                choiceObjList[i].aimCharacter = null;
            }
        }
    }
    /// <summary>
    /// 检测选择事件
    /// </summary>
    public void CheckMouse()
    {
        bool inputMouse = Input.GetKey(KeyCode.Mouse0);
        if (Vector2.Distance(Vector2.zero, MouseObj.transform.localPosition) > 100)
        {
            inputMouse = false;
        }
        /*鼠标没有按下*/
        if (!inputMouse)
        {
            /*移动到下一页的按钮*/
            if(choiceNumber == page % 8 && page < (chineseCharacters.Count - 3) / 5)
            {
                /*自动翻页*/
                page++;
                return;
            }
            /*移动到上一页按钮*/
            else if (choiceNumber == (page + 6) % 8 && page > 0)
            {
                /*自动翻页*/
                page--;
                return;
            }
        }
        /*鼠标按下， 上一帧没按这一帧按了*/
        else if(!beforeInput&& inputMouse)
        {
            beforeInput = inputMouse;
            /*是否选择汉字*/
            if (choiceCharacter != null && !isChoosing)
            {
                /*开始选择消耗*/
                isChoosing = true;
                /*开始组合汉字*/
                aimCharacterUI.SetActive(true);
                aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter = choiceCharacter;
                /*更新为主动持有汉字*/
                chineseCharacters.Clear();
                chineseCharacters.AddRange(gameManagement.characterStates[0].chineseCharacters);
            }
            else if(choiceCharacter != null)
            {
                CombinationUISystem combinationUI = aimCharacterUI.GetComponent<CombinationUISystem>();
                ChineseCharacter aim = combinationUI.aimCharacter;
                /*提供数*/
                int provideInt = 0;
                /*查询选择字能否合成目标字*/
                for(int i = 0; i < aim.need.Count; i++)
                {
                    if(aim.need[i].uniWord == choiceCharacter.characterName)
                    {
                        provideInt = aim.need[i].number;
                        break;
                    }
                }
                if (provideInt == 0)
                {
                    Debug.Log("不能用来拼写这个字");
                    playerTips.gameObject.SetActive(true);
                    playerTips.GetComponentInChildren<TextMesh>().text = "不能用来拼写这个字";
                    playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
                    return;
                }
                /*持有表bool副本*/
                List<bool> saveHasList = new List<bool>();
                saveHasList.AddRange(combinationUI.hasList);
                /*得到目标汉字剩余需求数组*/
                List<int> needIntList = new List<int>(); 
                for(int i = 0; i < aim.split_aim.Count; i++)
                {
                    /*已经持有为假时*/
                    if (!combinationUI.hasList[i])
                    {
                        needIntList.Add(aim.split_aim[i].number);
                    }
                    else
                    {
                        needIntList.Add(-1);
                    }
                }
                do
                {
                    /*提供的超出了我需要的*/
                    if (!needIntList.Contains(provideInt % 10) && !needIntList.Contains(provideInt))
                    {
                        Debug.Log("不能放入这个字");
                        playerTips.gameObject.SetActive(true);
                        playerTips.GetComponentInChildren<TextMesh>().text = "已经含有相关字";
                        playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
                        return;
                    }
                    else
                    {
                        /*记录提供数*/
                        for(int i = 0;i<needIntList.Count; i++)
                        {
                            if(provideInt % 10 == needIntList[i])
                            {
                                needIntList[i] = -1;
                            }
                        }
                    }
                    provideInt /= 10;
                } while (provideInt > 10);
                /*记录最后一个提供数*/
                for (int i = 0; i < needIntList.Count; i++)
                {
                    if (provideInt == needIntList[i])
                    {
                        needIntList[i] = -1;
                    }
                }
                /*更新持有表*/
                for (int i = 0; i < needIntList.Count; i++)
                {
                    if (needIntList[i] == -1)
                    {
                        combinationUI.hasList[i] = true;
                    }
                }
                //chineseCharacters.Remove(choiceCharacter);
            }
        }
        beforeInput = inputMouse;
    }
    private bool beforeInput;
    /// <summary>
    /// 取消
    /// </summary>
    public void Cancel()
    {
        /*重置轮盘*/
        chineseCharacters.Clear();
        GetNewCharacterList();
        //chineseCharacters.AddRange(gameManagement.characterStates[0].chineseCharacters);
        /*重置持有表*/
        CombinationUISystem combinationUI = aimCharacterUI.GetComponent<CombinationUISystem>();
        for(int i = 0; i < combinationUI.hasList.Count; i++)
        {
            combinationUI.hasList[i] = false;
        }
    }
    /// <summary>
    /// 确定
    /// </summary>
    public void Confirm()
    {
        /*如果没有收集齐*/
        if (aimCharacterUI.GetComponent<CombinationUISystem>().hasList.Contains(false))
        {
            Debug.Log("请集齐所有部件再重试");
            playerTips.gameObject.SetActive(true);
            playerTips.GetComponentInChildren<TextMesh>().text = "请集齐所有部件再重试";
            playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
            return;
        }
        /*消耗汉字*/
        //gameManagement.characterStates[0].chineseCharacters.Clear();
        //gameManagement.characterStates[0].chineseCharacters.AddRange(chineseCharacters);
        /*组合汉字*/
        if (!gameManagement.characterStates[0].Contains(aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter.characterName))
        {
            Debug.Log("成功");
            playerTips.gameObject.SetActive(true);
            playerTips.GetComponentInChildren<TextMesh>().text = "成功拼出" + aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter.characterName;
            playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
            gameManagement.characterStates[0].chineseCharacters.Add(aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter);
        }
        else
        {
            Debug.Log("已经含有");
            playerTips.gameObject.SetActive(true);
            playerTips.GetComponentInChildren<TextMesh>().text = "已经持有" + aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter.characterName;
            playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
        }
        choiceCharacter = null;
        isChoosing = false;
        List<bool> hasList = aimCharacterUI.GetComponent<CombinationUISystem>().hasList;
        for(int i = 0; i < hasList.Count; i++)
        {
            hasList[i] = false;
        }
        /*清空持有表*/
        /*关闭组合汉字*/
        aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter = null;
        aimCharacterUI.SetActive(false);
        /*关闭轮盘*/
        gameObject.SetActive(false);
    }
    public void Close()
    {
        Cancel();
        /*关闭组合汉字*/
        aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter = null;
        aimCharacterUI.SetActive(false);
        /*关闭轮盘*/
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 得到新的汉字列表
    /// </summary>
    public void GetNewCharacterList()
    {
        /*重置汉字列表*/
        //chineseCharacters.Clear();
        gameManagement.chineseCharacterManagement.createChineseCharacterList.Clear();
        CharacterStates active = gameManagement.characterStates[0];
        /*再次遍历主动字*/
        for (int i = 0; i < active.chineseCharacters.Count; i++)
        {
            /*拿到这个字*/
            ChineseCharacter chineseCharacter = active.chineseCharacters[i];
            /*遍历canbe列表*/
            foreach (string newName in chineseCharacter.canBe)
            {
                /*得到可以拼成的字*/
                if (!Contains(newName) && !gameManagement.chineseCharacterManagement.CheckContains(newName))
                {
                    ChineseCharacter newCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter(newName);
                    chineseCharacters.Add(newCharacter);
                    Debug.Log(chineseCharacters.Count);
                }
            }
        }
        Debug.Log(chineseCharacters.Count);
    }
    /// <summary>
    /// 是否存在汉字
    /// </summary>
    /// <param name="characterName">汉字名称</param>
    /// <returns></returns>
    public bool Contains(string characterName)
    {
        foreach (ChineseCharacter chineseCharacter in chineseCharacters)
        {
            if (chineseCharacter.characterName == characterName)
            {
                return true;
            }
        }
        return false;
    }
}
