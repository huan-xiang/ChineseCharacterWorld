using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteChoice : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ������
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// ��ǰ����ı��
    /// </summary>
    public int number;
    /// <summary>
    /// ����󶨺���
    /// </summary>
    public ChineseCharacter aimCharacter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MouseObj")
        {
            gameManagement.choiceRouletteManagement.choiceNumber = number;
            gameManagement.choiceRouletteManagement.choiceCharacter = aimCharacter;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "MouseObj")
        {
            gameManagement.choiceRouletteManagement.choiceNumber = number;
            gameManagement.choiceRouletteManagement.choiceCharacter = aimCharacter;
        }
    }
}
