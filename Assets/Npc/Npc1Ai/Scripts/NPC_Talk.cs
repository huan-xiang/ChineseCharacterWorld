using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{
    //��������
    [Header("NPC����")]
    public string npcName;
    [Header("�Ƿ��ǿɶԻ�NPC")]
    public bool allowTalk;
    [Header("�Ƿ�ѭ���Ի�")]
    public bool isLoop;
    [Header("�Ի��ı�")]
    public TextAsset[] talkTxt;
    [Header("�Ի���ʾ")]
    public GameObject talkSign;

    //�ڲ�����
    [HideInInspector] public bool canTalk;
    private int txtOrder; //�ı�ָ��
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

    //private void ShowSign() //����ͷ����ʶ
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

    public void StartTalk() //�����ť��ʾ�Ի�UI ������Txt�ı���ȡλ��
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

    private void showText() //����txt�ı���UI����Text �������ж�ȡ��ʾ ��ȡ�������UI
    {
        GameObject canvas = GameObject.Find("Canvas");
        Transform panel = canvas.transform.Find("NPCTalk_Panel");
        Text text = canvas.transform.Find("NPCTalk_Panel/NPCWord").gameObject.GetComponent<Text>();

        string[] str = talkTxt[txtOrder].text.Split('\n');

        if (Input.GetMouseButtonDown(0) && isTalking)
        {
            string[] subStr = str[textRow].Split('��');
            if(!(this.GetComponent<Monologue>()||this.GetComponent<InteractableMonologue>()))
            {
                if (subStr[0] == "��")
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
                //ս����ʼ
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

            txtOrder = txtOrder + 1; //��һ���ı������ ���صڶ����ı�
            if (txtOrder == talkTxt.Length)
            {
                txtOrder = 0; //ȫ���ı������ �����ı�ָ��
                if (!isLoop) //���Ϊ��ѭ������ ���Ϊ����Talk��NPC
                {
                    allowTalk = false;
                    canTalk = false;
                }
            }
            isTalking = false;
        }
    }

    private void CleanData()    //�߳��Ի��������õ�ǰ�ı�
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

