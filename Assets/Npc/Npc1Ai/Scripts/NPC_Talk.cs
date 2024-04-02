using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{
    //公共参数
    [Header("NPC姓名")]
    public string npcName;
    [Header("是否是可对话NPC")]
    public bool allowTalk;
    [Header("是否循环对话")]
    public bool isLoop;
    [Header("对话文本")]
    public TextAsset[] talkTxt;
    [Header("对话提示")]
    public GameObject talkSign;

    //内部参数
    [HideInInspector] public bool canTalk;
    private int txtOrder; //文本指针
    //private GameObject player;
    private GameObject text;
    private int textRow;
    private bool isTalking;

    void Start()
    {
        canTalk = true;
        textRow = 0;

        //player = GameObject.Find("Player");
    }


    void Update()
    {
        //ShowSign();
        showText();
        CleanData();
    }

    //private void ShowSign() //生成头顶标识
    //{
    //    if (canTalk)
    //    {
    //        this.talkSign.SetActive(true);
    //    }
    //    else
    //    {
    //        this.talkSign.SetActive(false);
    //    }
    //}

    public void StartTalk() //点击按钮显示对话UI 并重置Txt文本读取位置
    {
        if (canTalk)
        {
            isTalking = true;
            GameManagement._stop = true;
            GameObject canvas = GameObject.Find("Canvas");
            Transform panel = canvas.transform.Find("NPCTalk_Panel");
            panel.gameObject.SetActive(true);
            textRow = 0;
        }
    }

    private void showText() //链接txt文本与UI界面Text 并且逐行读取显示 读取完毕隐藏UI
    {
        GameObject canvas = GameObject.Find("Canvas");
        Transform panel = canvas.transform.Find("NPCTalk_Panel");
        Text text = canvas.transform.Find("NPCTalk_Panel/NPCWord").gameObject.GetComponent<Text>();

        string[] str = talkTxt[txtOrder].text.Split('\n');

        if (Input.GetMouseButtonDown(0) && isTalking)
        {
            string[] subStr = str[textRow].Split('：');
            if(!(this.GetComponent<Monologue>()||this.GetComponent<InteractableMonologue>()))
            {
                if (subStr[0] == "我")
                {
                    this.GetComponent<BossObject>().player_enemy.GetComponent<PlayerObject>().conversationFrame.SetActive(true);
                    this.GetComponent<BossObject>().conversationFrame.SetActive(false);
                }
                else
                {
                    this.GetComponent<BossObject>().player_enemy.GetComponent<PlayerObject>().conversationFrame.SetActive(false);
                    this.GetComponent<BossObject>().conversationFrame.SetActive(true);
                }
            }
            //canvas.transform.Find("NPCTalk_Panel/NPCName/Text").gameObject.GetComponent<Text>().text = npcName;
            //canvas.transform.Find("NPCTalk_Panel/Sprite").gameObject.GetComponent<Image>().sprite = this.GetComponent<SpriteRenderer>().sprite;
            text.text = str[textRow];
            textRow = textRow + 1;
        }

        if (textRow == str.Length)
        {
            panel.gameObject.SetActive(false);
            if(!(this.GetComponent<Monologue>() || this.GetComponent<InteractableMonologue>()))
            {
                this.GetComponent<BossObject>().player_enemy.GetComponent<PlayerObject>().conversationFrame.SetActive(false);
                this.GetComponent<BossObject>().conversationFrame.SetActive(false);
                //战斗开始
                if (this.GetComponent<BossUnitControl>())
                {
                    this.GetComponent<BossUnitControl>().bossStart();
                }
                else if (this.GetComponent<Boss2UnitControl>())
                {
                    this.GetComponent<Boss2UnitControl>().bossStart();
                }
                else if (this.GetComponent<Boss3UnitControl>())
                {
                    this.GetComponent<Boss3UnitControl>().bossStart();
                }
                else if (this.GetComponent<Boss4UnitControl>())
                {
                    this.GetComponent<Boss4UnitControl>().BossStart();
                }
                if (this.GetComponent<bgmChange>())
                {
                    this.GetComponent<bgmChange>().ChangeBGM(this.GetComponent<bgmChange>().bgm1);
                }
                this.GetComponent<BossObject>().myAirWall.SetActive(true);
            }


            GameManagement._stop = false;
            textRow = 0;

            txtOrder = txtOrder + 1; //第一个文本播完后 加载第二个文本
            if (txtOrder == talkTxt.Length)
            {
                txtOrder = 0; //全部文本播完后 重置文本指针
                if (!isLoop) //如果为不循环播放 则变为不可Talk的NPC
                {
                    allowTalk = false;
                    canTalk = false;
                }
            }
            isTalking = false;
        }
    }

    private void CleanData()    //走出对话区域重置当前文本
    {
        if (!canTalk && isTalking)
        {
            GameObject canvas = GameObject.Find("Canvas");
            Transform panel = canvas.transform.Find("NPCTalk_Panel");

            textRow = 0;
            isTalking = false;
            panel.gameObject.SetActive(false);
        }
    }
}

