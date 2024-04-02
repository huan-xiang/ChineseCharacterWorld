using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 分离汉字UI系统
/// </summary>
public class SplitUISystem : MonoBehaviour
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// 选择的汉字
    /// </summary>
    public ChineseCharacter mainCharacter;
    /// <summary>
    /// 选择的汉字名称
    /// </summary>
    public Text characterName;
    /// <summary>
    /// 选择的汉字说明
    /// </summary>
    public Text characterInfo;
    /// <summary>
    /// 拆解出来的汉字物体列表
    /// </summary>
    public List<GameObject> splitList;
    /// <summary>
    /// 是否需要更新UI
    /// </summary>
    public bool needUpdate;
    /// <summary>
    /// 是否正在关闭UI
    /// </summary>
    public bool isClosing;
    /// <summary>
    /// 是否被拆解
    /// </summary>
    public bool isSplited;
    void Start()
    {
        
    }
    void Update()
    {
        if (needUpdate)
        {
            UpdateUI();
            needUpdate = false;
        }
        /*按钮变化完后关闭*/
        if (isClosing)
        {
            for (int i = 0; i < splitList.Count; i++)
            {
                if(splitList[i].GetComponent<MyButton>().isHiding
                    || splitList[i].GetComponent<MyButton>().isDisplaying)
                {
                    return;
                }
            }
            isClosing = false;
            /*清空显示的汉字*/
            characterName.text = "";
            characterInfo.text = "";
            /*关闭UI*/
            this.gameObject.SetActive(false);
            /*解锁主动栏*/
            CharacterStates active = gameManagement.characterStates[0];
            active.lockButton = false;
        }
    }
    /// <summary>
    /// 更新UI
    /// </summary>
    public void UpdateUI()
    {
        /*更新选择的汉字*/
        characterName.text = mainCharacter.characterName;
        characterInfo.text = mainCharacter.info;
        /*改变分割出来的字的显示*/
        List<UniWord> split_aim = mainCharacter.split_aim;
        int i;
        for (i = 0; i < split_aim.Count; i++)
        {
            splitList[i].GetComponentInChildren<Text>().text = split_aim[i].uniWord;
            splitList[i].GetComponent<MyButton>().ButtonDisplay();
        }
        while (i < splitList.Count)
        {
            splitList[i].GetComponentInChildren<Text>().text = "";
            splitList[i].GetComponent<MyButton>().ButtonHide();
            i++;
        }
    }
    /// <summary>
    /// 关闭UI
    /// </summary>
    public void Cancel()
    {
        /*不消耗字*/
        //if (isSplited)
        //{
        //    CharacterStates active = gameManagement.characterStates[0];
        //    active.chineseCharacters.Remove(mainCharacter);
        //    isSplited = false;
        //}
        /*关闭所有按钮*/
        for(int i = 0; i < splitList.Count; i++)
        {
            splitList[i].GetComponentInChildren<Text>().text = "";
            splitList[i].GetComponent<MyButton>().ButtonHide();
        }
        isClosing = true;
        //Button button = splitList[0].GetComponent<Button>();
        //button.onClick.AddListener(() =>
        //{
        //    Image image = splitList[0].GetComponent<Image>();
        //    image.fillAmount = 0;
        //});
        gameManagement.skillListObj.transform.localPosition = new Vector3(-350, 625, 0);

    }
    /// <summary>
    /// 拆解字
    /// </summary>
    public void SplitCharacter(int i)
    {
        /*已经拿了*/
        if (splitList[i].GetComponentInChildren<Text>().text == "")
            return;
        CharacterStates active = gameManagement.characterStates[0];
        string characterName = splitList[i].GetComponentInChildren<Text>().text;
        if (!gameManagement.characterStates[0].Contains(characterName))
        {
            ChineseCharacter newCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter(characterName);
            active.chineseCharacters.Add(newCharacter);
        }

        splitList[i].GetComponentInChildren<Text>().text = "";
        splitList[i].GetComponent<MyButton>().ButtonHide();

        isSplited = true;
    }
    /// <summary>
    /// 全部拾取
    /// </summary>
    public void All()
    {
        for(int i = 0; i < mainCharacter.split_aim.Capacity; i++)
        {
            SplitCharacter(i);
        }
        Cancel();
    }
    /// <summary>
    /// 丢弃选择汉字
    /// </summary>
    public void Discard()
    {
        isSplited = true;
        Cancel();
    }
}
