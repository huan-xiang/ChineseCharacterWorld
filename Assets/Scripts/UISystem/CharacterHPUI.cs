using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHPUI : MonoBehaviour
{
    public GameObject character;
    public Slider hpSlider;
    void Update()
    {
        if (character.GetComponent<MonsterObj>())
        {
            hpSlider.value = 1 - character.GetComponent<MonsterObj>().hp / character.GetComponent<MonsterObj>().maxHp;
            if (character.GetComponent<MonsterObj>().pixelMonster.IsDead)
            {
                Destroy(gameObject);
            }
        }
        else if(character.GetComponent<BossObject>())
        {
            hpSlider.value = 1 - character.GetComponent<BossObject>().hp / character.GetComponent<BossObject>().maxHp;
        }
    }
}
