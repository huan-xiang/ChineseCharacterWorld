using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ѡ�����̹�����������ѡ����Ϻ���
/// </summary>
public class ChoiceRouletteManagement : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// ����Ϻ����б�
    /// </summary>
    public List<ChineseCharacter> chineseCharacters;
    /// <summary>
    /// ѡ��ĺ���
    /// </summary>
    public ChineseCharacter choiceCharacter;
    /// <summary>
    /// ѡ�������б�
    /// </summary>
    public List<RouletteChoice> choiceObjList;
    /// <summary>
    /// �����ı��б�
    /// </summary>
    public List<Text> texts;
    /// <summary>
    /// Ŀǰѡ��ı��
    /// </summary>
    public int choiceNumber;
    /// <summary>
    /// ��ǰҳ��
    /// </summary>
    public int page;
    /// <summary>
    /// ��һҳ�ı��
    /// </summary>
    public int nextNumber;
    /// <summary>
    /// ������ͼƬ�б�
    /// </summary>
    public List<Sprite> backgroundSpriteList;
    /// <summary>
    /// ����ͼƬ
    /// </summary>
    public Image backgroundImage;
    /// <summary>
    /// ����ѡ��״̬
    /// </summary>
    public enum ChoiceState
    {
        δѡ��,
        ѡ��ֱ,
        ѡ����б,
        ���´�ֱ,
        ������б
    }
    /// <summary>
    /// ��ǰ����ѡ��״̬
    /// </summary>
    public ChoiceState choiceState;
    /// <summary>
    /// ����ѡ����
    /// </summary>
    public bool isChoosing;
    /// <summary>
    /// ��Ϻ��ֵ�UI
    /// </summary>
    public GameObject aimCharacterUI;
    /// <summary>
    /// ���������������
    /// </summary>
    public GameObject MouseObj;
    /// <summary>
    /// ��ɫ��ʾ
    /// </summary>
    public GameObject playerTips;
    private void Awake()
    {
        /*�����ã������к����õ�*/
        //GameObject[] allCharacterObj = Resources.LoadAll<GameObject>("ChineseCharacter/");
        //for (int i = 0; i < allCharacterObj.Length; i++)
        //{
        //    chineseCharacters.Add(allCharacterObj[i].GetComponent<ChineseCharacter>());
        //}
        //GetNewCharacterList();
        /*��0��ʼ*/
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
    /// ���±���ͼƬ
    /// </summary>
    public void UpdateBackground()
    {
        /*����ѡ�����ָı�����״̬*/
        choiceState = choiceNumber % 2 == 0 ? ChoiceState.ѡ��ֱ : ChoiceState.ѡ����б;
        choiceState = choiceNumber == -1 ? ChoiceState.δѡ�� : choiceState;
        /*����״̬�ı�ͼƬ*/
        backgroundImage.sprite = backgroundSpriteList[(int)choiceState];
        /*���ݵ�ǰѡ��������ת*/
        int times = Mathf.CeilToInt(choiceNumber / 2f)%4;
        backgroundImage.gameObject.transform.localEulerAngles = new Vector3(0, 0, -90 * times);
    }
    /// <summary>
    /// ���º���
    /// </summary>
    public void UpdateCharacter()
    {
        /*���ֱ�ţ���һҳ��7���֣�����ҳֻ��5����*/
        int characterNumber = page == 0?0: page * 5 + 2;
        /*�����ı�*/
        for(int i = 0; i < 8; i++)
        {
            /*��һҳ��ť�ı����������һҳ*/
            if(i == page%8 && page < (chineseCharacters.Count -3)/5)
            {
                texts[i].text = "��";
                /*���º���*/
                choiceObjList[i].aimCharacter = null;
            }
            /*���ı�����Ҫ������һҳ*/
            else if (i == (page + 7) % 8 && page > 0)
            {
                texts[i].text = "";
                /*���º���*/
                choiceObjList[i].aimCharacter = null;
            }
            /*��һҳ�ı�����Ҫ������һҳ*/
            else if (i == (page + 6) % 8 && page > 0)
            {
                texts[i].text = "��";
                /*���º���*/
                choiceObjList[i].aimCharacter = null;
            }
            /*�����ı�*/
            else if(characterNumber < chineseCharacters.Count)
            {
                texts[i].text = chineseCharacters[characterNumber].characterName;
                /*���º���*/
                choiceObjList[i].aimCharacter = chineseCharacters[characterNumber];
                characterNumber++;
            }
            /*��*/
            else
            {
                texts[i].text = "";
                /*���º���*/
                choiceObjList[i].aimCharacter = null;
            }
        }
    }
    /// <summary>
    /// ���ѡ���¼�
    /// </summary>
    public void CheckMouse()
    {
        bool inputMouse = Input.GetKey(KeyCode.Mouse0);
        if (Vector2.Distance(Vector2.zero, MouseObj.transform.localPosition) > 100)
        {
            inputMouse = false;
        }
        /*���û�а���*/
        if (!inputMouse)
        {
            /*�ƶ�����һҳ�İ�ť*/
            if(choiceNumber == page % 8 && page < (chineseCharacters.Count - 3) / 5)
            {
                /*�Զ���ҳ*/
                page++;
                return;
            }
            /*�ƶ�����һҳ��ť*/
            else if (choiceNumber == (page + 6) % 8 && page > 0)
            {
                /*�Զ���ҳ*/
                page--;
                return;
            }
        }
        /*��갴�£� ��һ֡û����һ֡����*/
        else if(!beforeInput&& inputMouse)
        {
            beforeInput = inputMouse;
            /*�Ƿ�ѡ����*/
            if (choiceCharacter != null && !isChoosing)
            {
                /*��ʼѡ������*/
                isChoosing = true;
                /*��ʼ��Ϻ���*/
                aimCharacterUI.SetActive(true);
                aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter = choiceCharacter;
                /*����Ϊ�������к���*/
                chineseCharacters.Clear();
                chineseCharacters.AddRange(gameManagement.characterStates[0].chineseCharacters);
            }
            else if(choiceCharacter != null)
            {
                CombinationUISystem combinationUI = aimCharacterUI.GetComponent<CombinationUISystem>();
                ChineseCharacter aim = combinationUI.aimCharacter;
                /*�ṩ��*/
                int provideInt = 0;
                /*��ѯѡ�����ܷ�ϳ�Ŀ����*/
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
                    Debug.Log("��������ƴд�����");
                    playerTips.gameObject.SetActive(true);
                    playerTips.GetComponentInChildren<TextMesh>().text = "��������ƴд�����";
                    playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
                    return;
                }
                /*���б�bool����*/
                List<bool> saveHasList = new List<bool>();
                saveHasList.AddRange(combinationUI.hasList);
                /*�õ�Ŀ�꺺��ʣ����������*/
                List<int> needIntList = new List<int>(); 
                for(int i = 0; i < aim.split_aim.Count; i++)
                {
                    /*�Ѿ�����Ϊ��ʱ*/
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
                    /*�ṩ�ĳ���������Ҫ��*/
                    if (!needIntList.Contains(provideInt % 10) && !needIntList.Contains(provideInt))
                    {
                        Debug.Log("���ܷ��������");
                        playerTips.gameObject.SetActive(true);
                        playerTips.GetComponentInChildren<TextMesh>().text = "�Ѿ����������";
                        playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
                        return;
                    }
                    else
                    {
                        /*��¼�ṩ��*/
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
                /*��¼���һ���ṩ��*/
                for (int i = 0; i < needIntList.Count; i++)
                {
                    if (provideInt == needIntList[i])
                    {
                        needIntList[i] = -1;
                    }
                }
                /*���³��б�*/
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
    /// ȡ��
    /// </summary>
    public void Cancel()
    {
        /*��������*/
        chineseCharacters.Clear();
        GetNewCharacterList();
        //chineseCharacters.AddRange(gameManagement.characterStates[0].chineseCharacters);
        /*���ó��б�*/
        CombinationUISystem combinationUI = aimCharacterUI.GetComponent<CombinationUISystem>();
        for(int i = 0; i < combinationUI.hasList.Count; i++)
        {
            combinationUI.hasList[i] = false;
        }
    }
    /// <summary>
    /// ȷ��
    /// </summary>
    public void Confirm()
    {
        /*���û���ռ���*/
        if (aimCharacterUI.GetComponent<CombinationUISystem>().hasList.Contains(false))
        {
            Debug.Log("�뼯�����в���������");
            playerTips.gameObject.SetActive(true);
            playerTips.GetComponentInChildren<TextMesh>().text = "�뼯�����в���������";
            playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
            return;
        }
        /*���ĺ���*/
        //gameManagement.characterStates[0].chineseCharacters.Clear();
        //gameManagement.characterStates[0].chineseCharacters.AddRange(chineseCharacters);
        /*��Ϻ���*/
        if (!gameManagement.characterStates[0].Contains(aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter.characterName))
        {
            Debug.Log("�ɹ�");
            playerTips.gameObject.SetActive(true);
            playerTips.GetComponentInChildren<TextMesh>().text = "�ɹ�ƴ��" + aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter.characterName;
            playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
            gameManagement.characterStates[0].chineseCharacters.Add(aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter);
        }
        else
        {
            Debug.Log("�Ѿ�����");
            playerTips.gameObject.SetActive(true);
            playerTips.GetComponentInChildren<TextMesh>().text = "�Ѿ�����" + aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter.characterName;
            playerTips.GetComponentInChildren<AutoDestroy>().time = 0;
        }
        choiceCharacter = null;
        isChoosing = false;
        List<bool> hasList = aimCharacterUI.GetComponent<CombinationUISystem>().hasList;
        for(int i = 0; i < hasList.Count; i++)
        {
            hasList[i] = false;
        }
        /*��ճ��б�*/
        /*�ر���Ϻ���*/
        aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter = null;
        aimCharacterUI.SetActive(false);
        /*�ر�����*/
        gameObject.SetActive(false);
    }
    public void Close()
    {
        Cancel();
        /*�ر���Ϻ���*/
        aimCharacterUI.GetComponent<CombinationUISystem>().aimCharacter = null;
        aimCharacterUI.SetActive(false);
        /*�ر�����*/
        gameObject.SetActive(false);
    }
    /// <summary>
    /// �õ��µĺ����б�
    /// </summary>
    public void GetNewCharacterList()
    {
        /*���ú����б�*/
        //chineseCharacters.Clear();
        gameManagement.chineseCharacterManagement.createChineseCharacterList.Clear();
        CharacterStates active = gameManagement.characterStates[0];
        /*�ٴα���������*/
        for (int i = 0; i < active.chineseCharacters.Count; i++)
        {
            /*�õ������*/
            ChineseCharacter chineseCharacter = active.chineseCharacters[i];
            /*����canbe�б�*/
            foreach (string newName in chineseCharacter.canBe)
            {
                /*�õ�����ƴ�ɵ���*/
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
    /// �Ƿ���ں���
    /// </summary>
    /// <param name="characterName">��������</param>
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
