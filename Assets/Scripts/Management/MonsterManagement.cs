using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManagement : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// 所有敌人的列表
    /// </summary>
    public List<GameObject> allMonsterList;

    void Update()
    {
        UpdateMonsterPassive();
    }
    public void UpdateMonsterPassive()
    {
        float distance = 999;
        GameObject player = gameManagement.playerController.gameObject;
        GameObject aimMonster = null;
        /*得到最近敌人*/
        foreach (GameObject monster in allMonsterList)
        {
            float newDistance = Mathf.Abs(Vector2.Distance(player.transform.position, monster.transform.position));
            if (distance > newDistance)
            {
                distance = newDistance;
                aimMonster = monster;
            }
        }
        /*更新敌人被动栏*/
        if (aimMonster != null)
        {
            gameManagement.characterStates[2].chineseCharacters = aimMonster.GetComponent<BuffStates>().passiveChineseCharacterList;
            gameManagement.playerController.playerObject.player_enemy = aimMonster;
        }

    }
}
