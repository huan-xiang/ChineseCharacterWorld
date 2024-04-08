using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��ɫ��������0-��������1-��������2-buff��
/// </summary>
public class CharacterStates : MonoBehaviour
{
    //public RectMask2D rectMask2D;    
    /// <summary>
    /// ��ťԤ����
    /// </summary>
    public GameObject saveButton;
    /// <summary>
    /// �Ƿ���ʾ
    /// </summary>
    public bool isDisplay;
    /// <summary>
    /// �Ƿ���Ҫ����UI
    /// </summary>
    public bool needUpdate;
    /// <summary>
    /// ��ť����
    /// </summary>
    public bool lockButton;
    /// <summary>
    /// �������ָ���
    /// </summary>
    public int saveCharacterNumber;
    /// <summary>
    /// ״̬����
    /// </summary>
    public List<GameObject> statesItems;
    /// <summary>
    /// ���ֱ�
    /// </summary>
    public List<ChineseCharacter> chineseCharacters;
    /// <summary>
    /// �������
    /// </summary>
    public int createInterval;
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    public enum CharacterStatesType
    {
        ������,������,�з�������
    }
    public CharacterStatesType characterStateType;
    /// <summary>
    /// Ĭ��״̬����ʱ�䣨��������٣�
    /// </summary>
    public int defaultStateDestoryTime;
    /// <summary>
    /// �����İ�ť���ڵ�
    /// </summary>
    public GameObject buttonRoot;
    /// <summary>
    /// ������������
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
        /*������������*/
        if(chineseCharacters.Count != saveCharacterNumber)
        {
            saveCharacterNumber = chineseCharacters.Count;
            needUpdate = true;
        }
        /*��������*/
        if (needUpdate)
        {
            if (characterStateType == CharacterStatesType.������) ActiveUpdateString();
            else if (characterStateType == CharacterStatesType.������
                || characterStateType == CharacterStatesType.�з�������) PassiveUpdateString();
            needUpdate = false;
        }
        AutoInvalidChineseCharacter();
    }
    /// <summary>
    /// ������ʾ������
    /// </summary>
    public void ActiveUpdateString()
    {
        /*�����µİ�ť*/
        for (int i = statesItems.Count; i < chineseCharacters.Count; i++)
        {
            GameObject newButton = Instantiate(saveButton) as GameObject;
            newButton.transform.SetParent(buttonRoot.transform);
            /*����*/
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
            /*�޸��ı�*/
            statesItems[i].transform.GetComponentInChildren<Text>().text = characterName;
            Button button = statesItems[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            /*��Ӱ�ť�¼�,�����һ�����������ƥ�䵽���ٰ�����ɾ��*/
            button.onClick.AddListener(delegate {
                gameManagement.skillManagement.choiceSkillName = SkillManagement.SkillName.����;
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
                    /*��Ӱ�ť�¼�*/
                    button.onClick.AddListener(delegate {
                        gameManagement.skillManagement.choiceSkillName = (SkillManagement.SkillName)indexj;
                        gameManagement.skillListObj.transform.localPosition = new Vector3(100, -50, 0);
                        gameManagement.skillManagement.otherName = "";
                    });
                }
            }
            int index = new int();
            index = i;
            /*��Ӱ�ť�¼�*/
            button.onClick.AddListener(delegate {
                SplitCharacter(index);
            });
        }
    }
    /// <summary>
    /// �Ƿ���ں���
    /// </summary>
    /// <param name="characterName">��������</param>
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
    /// ������ʾ������
    /// </summary>
    public void PassiveUpdateString()
    {
        /*�����µİ�ť*/
        for (int i = statesItems.Count; i < chineseCharacters.Count; i++)
        {
            GameObject newButton = Instantiate(saveButton) as GameObject;
            newButton.transform.SetParent(transform);
            newButton.transform.localPosition = new Vector3((i + 1) * createInterval, 0, 0);
            newButton.transform.localScale = new Vector3(2, 2, 1);
            newButton.GetComponent<MyButton>().isDisplaying = true;
            statesItems.Add(newButton);
        }
        /*ɾ�����ఴť*/
        for (int i = statesItems.Count; i > chineseCharacters.Count; i--)
        {
            GameObject delObj = statesItems[i - 1];
            statesItems.Remove(delObj);
            delObj.GetComponent<MyButton>().isDestroying = true;
            delObj.transform.GetComponentInChildren<Text>().text = "";
            delObj.transform.GetComponentInChildren<Button>().enabled = false;
            delObj.GetComponent<MyButton>().isHiding = true;
        }
        /*�޸İ�ť��Ϣ*/
        for (int i = 0; i < chineseCharacters.Count; i++)
        {
            /*�޸��ı�*/
            statesItems[i].transform.GetComponentInChildren<Text>().text = chineseCharacters[i].characterName;
            //Button button = statesItems[i].GetComponent<Button>();
            //button.onClick.RemoveAllListeners();
            //int index = new int();
            //index = i;
            /*��Ӱ�ť�¼�*/
            //button.onClick.AddListener(delegate {
            //    SplitCharacter(index);
            //});
        }
    }
    /// <summary>
    ///  ��⺺��
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
    /// �ı���������ڵ���Χ
    /// </summary>
    /// <param name="number">����</param>
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
    /// �Զ�ʧЧ����
    /// </summary>
    public void AutoInvalidChineseCharacter()
    {
        foreach(ChineseCharacter chineseCharacter in chineseCharacters)
        {
            /*�ų��������ٵ�*/
            if(chineseCharacter.invalidTime != -1)
            {
                /*����ʱ�䵽*/
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
