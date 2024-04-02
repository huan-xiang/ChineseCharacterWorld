using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ӵ������ű��������Ѻ��ָ�����ײ��λ
/// </summary>
public class PassiveAddObj : MonoBehaviour
{
    public ChineseCharacter[] chineseCharacters;
    public GameManagement gameManagement;
    /// <summary>
    /// ����ʱ��
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
            /*������������ĺ���ȫ�����ӵ���ɫ*/
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
