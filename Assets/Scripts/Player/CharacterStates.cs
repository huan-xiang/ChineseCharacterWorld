using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 角色汉字栏，0-主动栏，1-被动栏，2-buff栏
/// </summary>
public class CharacterStates : MonoBehaviour
{
    //public RectMask2D rectMask2D;    
    /// <summary>
    /// 按钮预制体
    /// </summary>
    public GameObject saveButton;
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool isDisplay;
    /// <summary>
    /// 是否需要更新UI
    /// </summary>
    public bool needUpdate;
    /// <summary>
    /// 按钮上锁
    /// </summary>
    public bool lockButton;
    /// <summary>
    /// 监听汉字个数
    /// </summary>
    public int saveCharacterNumber;
    /// <summary>
    /// 状态物体
    /// </summary>
    public List<GameObject> statesItems;
    /// <summary>
    /// 汉字表
    /// </summary>
    public List<ChineseCharacter> chineseCharacters;
    /// <summary>
    /// 创建间隔
    /// </summary>
    public int createInterval;
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    public enum CharacterStatesType
    {
        主动栏,被动栏,敌方被动栏
    }
    public CharacterStatesType characterStateType;
    /// <summary>
    /// 默认状态销毁时间（如果会销毁）
    /// </summary>
    public int defaultStateDestoryTime;
    /// <summary>
    /// 创建的按钮根节点
    /// </summary>
    public GameObject buttonRoot;
    /// <summary>
    /// 主动汉字数量
    /// </summary>
    public int activeCharacterNumber;
    private void Start()
    {
        isDisplay = false;
        activeCharacterNumber = 0; 
    }
    private void Update()
    {
        //aimNumber = chineseCharacters.Count;
        /*监听汉字数量*/
        if(chineseCharacters.Count != saveCharacterNumber)
        {
            saveCharacterNumber = chineseCharacters.Count;
            needUpdate = true;
        }
        /*更新文字*/
        if (needUpdate)
        {
            if (characterStateType == CharacterStatesType.主动栏) ActiveUpdateString();
            else if (characterStateType == CharacterStatesType.被动栏
                || characterStateType == CharacterStatesType.敌方被动栏) PassiveUpdateString();
            needUpdate = false;
        }
        AutoInvalidChineseCharacter();
    }
    /// <summary>
    /// 更新显示的文字
    /// </summary>
    public void ActiveUpdateString()
    {
        /*创建新的按钮*/
        for (int i = statesItems.Count; i < chineseCharacters.Count; i++)
        {
            GameObject newButton = Instantiate(saveButton) as GameObject;
            newButton.transform.SetParent(buttonRoot.transform);
            /*左面*/
            if (activeCharacterNumber < 24)
            {
                newButton.transform.localPosition = new Vector3((activeCharacterNumber % 4 - 4) * createInterval,
                    (-activeCharacterNumber / 4 + 2) * createInterval, 0);
            }
            else if(activeCharacterNumber < 48)
            {
                newButton.transform.localPosition = new Vector3((activeCharacterNumber % 4 + 1) * createInterval,
                    (-activeCharacterNumber / 4 + 8) * createInterval, 0);
            }
            activeCharacterNumber++;
            newButton.transform.localScale = new Vector3(1, 1, 1);
            statesItems.Add(newButton);
            string characterName = chineseCharacters[i].characterName;
            /*修改文本*/
            statesItems[i].transform.GetComponentInChildren<Text>().text = characterName;
            Button button = statesItems[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            /*添加按钮事件,先添加一个其他，如果匹配到了再把其他删掉*/
            button.onClick.AddListener(delegate {
                gameManagement.skillManagement.choiceSkillName = SkillManagement.SkillName.其他;
                gameManagement.skillListObj.transform.localPosition = new Vector3(100, -50, 0);
                gameManagement.skillManagement.otherName = characterName;
            });
            for (int j = 0; j < Enum.GetNames(typeof(SkillManagement.SkillName)).Length; j++)
            {
                string skillName = Enum.GetName(typeof(SkillManagement.SkillName), j);
                if (characterName == skillName)
                {
                    button.onClick.RemoveAllListeners();
                    statesItems[i].transform.GetComponentInChildren<Text>().color = new Color(1, 0, 0);
                    int indexj = new int();
                    indexj = j;
                    /*添加按钮事件*/
                    button.onClick.AddListener(delegate {
                        gameManagement.skillManagement.choiceSkillName = (SkillManagement.SkillName)indexj;
                        gameManagement.skillListObj.transform.localPosition = new Vector3(100, -50, 0);
                        gameManagement.skillManagement.otherName = "";
                    });
                }
            }
            int index = new int();
            index = i;
            /*添加按钮事件*/
            button.onClick.AddListener(delegate {
                SplitCharacter(index);
            });
        }
    }
    /// <summary>
    /// 是否存在汉字
    /// </summary>
    /// <param name="characterName">汉字名称</param>
    /// <returns></returns>
    public bool Contains(string characterName)
    {
        foreach(ChineseCharacter chineseCharacter in chineseCharacters)
        {
            if(chineseCharacter.characterName == characterName)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 更新显示的文字
    /// </summary>
    public void PassiveUpdateString()
    {
        /*创建新的按钮*/
        for (int i = statesItems.Count; i < chineseCharacters.Count; i++)
        {
            GameObject newButton = Instantiate(saveButton) as GameObject;
            newButton.transform.SetParent(transform);
            newButton.transform.localPosition = new Vector3((i + 1) * createInterval, 0, 0);
            newButton.transform.localScale = new Vector3(2, 2, 1);
            newButton.GetComponent<MyButton>().isDisplaying = true;
            statesItems.Add(newButton);
        }
        /*删除多余按钮*/
        for (int i = statesItems.Count; i > chineseCharacters.Count; i--)
        {
            GameObject delObj = statesItems[i - 1];
            statesItems.Remove(delObj);
            delObj.GetComponent<MyButton>().isDestroying = true;
            delObj.transform.GetComponentInChildren<Text>().text = "";
            delObj.transform.GetComponentInChildren<Button>().enabled = false;
            delObj.GetComponent<MyButton>().isHiding = true;
        }
        /*修改按钮信息*/
        for (int i = 0; i < chineseCharacters.Count; i++)
        {
            /*修改文本*/
            statesItems[i].transform.GetComponentInChildren<Text>().text = chineseCharacters[i].characterName;
            //Button button = statesItems[i].GetComponent<Button>();
            //button.onClick.RemoveAllListeners();
            //int index = new int();
            //index = i;
            /*添加按钮事件*/
            //button.onClick.AddListener(delegate {
            //    SplitCharacter(index);
            //});
        }
    }
    /// <summary>
    ///  拆解汉字
    /// </summary>
    public void SplitCharacter(int i)
    {
        if (lockButton) return;
        ChineseCharacter origin = chineseCharacters[i];
        SplitUISystem splitUISystem = gameManagement.splitUISystem;
        splitUISystem.mainCharacter = origin;
        splitUISystem.needUpdate = true;
        splitUISystem.gameObject.SetActive(true);
        lockButton = true;
        //List<string> split_aim = origin.split_aim;
    }
    /// <summary>
    /// 改变各种栏的遮挡范围
    /// </summary>
    /// <param name="number">个数</param>
    public void ChangePaddingRight(int number)
    {
        //if (isDisplay)
        //{
        //    aimNumber = 0;
        //    isDisplay = false;
        //}
        //else
        //{
        //    aimNumber = number;
        //    isDisplay = true;
        //}
    }
    /// <summary>
    /// 自动失效汉字
    /// </summary>
    public void AutoInvalidChineseCharacter()
    {
        foreach(ChineseCharacter chineseCharacter in chineseCharacters)
        {
            /*排除不会销毁的*/
            if(chineseCharacter.invalidTime != -1)
            {
                /*销毁时间到*/
                if (chineseCharacter.invalidTime == 0)
                {
                    chineseCharacters.Remove(chineseCharacter);
                    return;
                }
                else chineseCharacter.invalidTime--;
            }
        }
    }
}
