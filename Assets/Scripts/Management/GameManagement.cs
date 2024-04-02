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
    /// 玩家汉字栏，0-主动栏，1-被动栏,2-敌人被动栏
    /// </summary>
    public List<CharacterStates> characterStates;
    /// <summary>
    /// 选择轮盘管理器
    /// </summary>
    public ChoiceRouletteManagement choiceRouletteManagement;
    /// <summary>
    /// 技能管理器
    /// </summary>
    public SkillManagement skillManagement;
    /// <summary>
    /// 场景根节点
    /// </summary>
    public GameObject sceneObjectRoot;
    /// <summary>
    /// 金币预制体
    /// </summary>
    public GameObject goldPrefab;
    /// <summary>
    /// 汉字管理器
    /// </summary>
    public ChineseCharacterManagement chineseCharacterManagement;
    /// <summary>
    /// buff管理器
    /// </summary>
    public BuffManagement buffManagement;
    /// <summary>
    /// 怪物管理器
    /// </summary>
    public MonsterManagement monsterManagement;
    /// <summary>
    /// 键盘事件
    /// </summary>
    public KeyBoardEvent keyBoardEvent;
    /// <summary>
    /// 音频管理器
    /// </summary>
    public AudioManager audioManager;
    public static bool _stop = false;
    public BossManager bossManager;
    public List<GameObject> relifePosList;
    /// <summary>
    /// 现在就一个npc所以没写manager
    /// </summary>
    public GameObject NPC;
    /// <summary>
    /// 加载技能列表
    /// </summary>
    public GameObject skillListObj;
    /// <summary>
    /// 成语物体
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
