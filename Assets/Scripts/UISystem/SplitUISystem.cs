using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���뺺��UIϵͳ
/// </summary>
public class SplitUISystem : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// ѡ��ĺ���
    /// </summary>
    public ChineseCharacter mainCharacter;
    /// <summary>
    /// ѡ��ĺ�������
    /// </summary>
    public Text characterName;
    /// <summary>
    /// ѡ��ĺ���˵��
    /// </summary>
    public Text characterInfo;
    /// <summary>
    /// �������ĺ��������б�
    /// </summary>
    public List<GameObject> splitList;
    /// <summary>
    /// �Ƿ���Ҫ����UI
    /// </summary>
    public bool needUpdate;
    /// <summary>
    /// �Ƿ����ڹر�UI
    /// </summary>
    public bool isClosing;
    /// <summary>
    /// �Ƿ񱻲��
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
        /*��ť�仯���ر�*/
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
            /*�����ʾ�ĺ���*/
            characterName.text = "";
            characterInfo.text = "";
            /*�ر�UI*/
            this.gameObject.SetActive(false);
            /*����������*/
            CharacterStates active = gameManagement.characterStates[0];
            active.lockButton = false;
        }
    }
    /// <summary>
    /// ����UI
    /// </summary>
    public void UpdateUI()
    {
        /*����ѡ��ĺ���*/
        characterName.text = mainCharacter.characterName;
        characterInfo.text = mainCharacter.info;
        /*�ı�ָ�������ֵ���ʾ*/
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
    /// �ر�UI
    /// </summary>
    public void Cancel()
    {
        /*��������*/
        //if (isSplited)
        //{
        //    CharacterStates active = gameManagement.characterStates[0];
        //    active.chineseCharacters.Remove(mainCharacter);
        //    isSplited = false;
        //}
        /*�ر����а�ť*/
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
    /// �����
    /// </summary>
    public void SplitCharacter(int i)
    {
        /*�Ѿ�����*/
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
    /// ȫ��ʰȡ
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
    /// ����ѡ����
    /// </summary>
    public void Discard()
    {
        isSplited = true;
        Cancel();
    }
}
