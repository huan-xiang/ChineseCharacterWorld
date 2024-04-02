using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 拥有这个脚本的物体会把汉字赋予碰撞单位
/// </summary>
public class PassiveAddObj : MonoBehaviour
{
    public ChineseCharacter[] chineseCharacters;
    public GameManagement gameManagement;
    /// <summary>
    /// 持续时间
    /// </summary>
    public int continueTime;
    private void Awake()
    {
        chineseCharacters = gameObject.GetComponentsInChildren<ChineseCharacter>();
    }

    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterStates playerStates = gameManagement.characterStates[1];
            foreach (ChineseCharacter chineseCharacter in chineseCharacters)
            {
                if (!playerStates.chineseCharacters.Contains(chineseCharacter))
                {
                    chineseCharacter.invalidTime = continueTime;
                    playerStates.chineseCharacters.Add(chineseCharacter);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterStates playerStates = gameManagement.characterStates[1];
            /*将汉字子物体的汉字全部附加到角色*/
            foreach (ChineseCharacter chineseCharacter in chineseCharacters)
            {
                if (!playerStates.chineseCharacters.Contains(chineseCharacter))
                {
                    chineseCharacter.invalidTime = continueTime;
                    playerStates.chineseCharacters.Add(chineseCharacter);
                }
            }
        }
    }
}
