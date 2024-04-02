using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteChoice : MonoBehaviour
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public GameManagement gameManagement;
    /// <summary>
    /// 当前物体的编号
    /// </summary>
    public int number;
    /// <summary>
    /// 点击绑定汉字
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
