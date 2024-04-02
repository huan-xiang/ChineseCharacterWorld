using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimatorObj : MonoBehaviour
{
    public PixelCharacter fx;
    public PlayerObject playerObject;
    public UnityEvent attackStart;
    public UnityEvent attackOver;
    public UnityEvent spellStart;
    void Awake()
    {
        playerObject = fx.GetComponent<PlayerObject>();
    }
    /// <summary>
    /// ����֡�¼���������ʼ
    /// </summary>
    public void AttackStart()
    {
        fx.IsAttacking = true;
        playerObject.weaponObj.GetComponent<Collider2D>().enabled = true;
        attackStart?.Invoke();
    }
    /// <summary>
    /// ����֡�¼�����������
    /// </summary>
    public void AttackOver()
    {
        fx.IsAttacking = false;
        playerObject.weaponObj.GetComponent<Collider2D>().enabled = false;
        attackOver?.Invoke();
    }
    /// <summary>
    /// ʩ����ʼ
    /// </summary>
    public void SpellStart()
    {
        spellStart?.Invoke();
    }
}
