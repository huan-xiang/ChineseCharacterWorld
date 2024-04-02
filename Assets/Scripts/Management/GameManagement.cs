using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static int relifePosIndex = 0;
    public PixelCharacterController playerController;
    public AbsorbObj absorbObj;
    public SplitUISystem splitUISystem;
    /// <summary>
    /// ��Һ�������0-��������1-������,2-���˱�����
    /// </summary>
    public List<CharacterStates> characterStates;
    /// <summary>
    /// ѡ�����̹�����
    /// </summary>
    public ChoiceRouletteManagement choiceRouletteManagement;
    /// <summary>
    /// ���ܹ�����
    /// </summary>
    public SkillManagement skillManagement;
    /// <summary>
    /// �������ڵ�
    /// </summary>
    public GameObject sceneObjectRoot;
    /// <summary>
    /// ���Ԥ����
    /// </summary>
    public GameObject goldPrefab;
    /// <summary>
    /// ���ֹ�����
    /// </summary>
    public ChineseCharacterManagement chineseCharacterManagement;
    /// <summary>
    /// buff������
    /// </summary>
    public BuffManagement buffManagement;
    /// <summary>
    /// ���������
    /// </summary>
    public MonsterManagement monsterManagement;
    /// <summary>
    /// �����¼�
    /// </summary>
    public KeyBoardEvent keyBoardEvent;
    /// <summary>
    /// ��Ƶ������
    /// </summary>
    public AudioManager audioManager;
    public static bool _stop = false;
    public BossManager bossManager;
    public List<GameObject> relifePosList;
    /// <summary>
    /// ���ھ�һ��npc����ûдmanager
    /// </summary>
    public GameObject NPC;
    /// <summary>
    /// ���ؼ����б�
    /// </summary>
    public GameObject skillListObj;
    /// <summary>
    /// ��������
    /// </summary>
    public IdiomListObj idiomListObj;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
