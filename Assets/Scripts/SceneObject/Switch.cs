using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    /// <summary>
    /// 开关值
    /// </summary>
    public bool value;
    /// <summary>
    /// 其他开关
    /// </summary>
    public List<Switch> other;
    public Animator animator;
    public int time;
    public int delayTime;
    /// <summary>
    /// 控制机关
    /// </summary>
    public Office office;
    void Update()
    {
        if (time < delayTime)
        {
            time++;
        }
        office.open = value;
    }
    public void ChangeValueOver()
    {
        value = !value;
    }
    public void ChangeValue(bool v)
    {
        animator.SetBool("open", v);
    }
    /// <summary>
    /// 改变其他开关
    /// </summary>
    /// <param name="value">值</param>
    public void ChangeAllOtherSwitch(bool value)
    {
        foreach (Switch switchObj in other)
        {
            switchObj.ChangeValue(value);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*激活冷却*/
        if (time < delayTime) return;
        if (collision.gameObject.tag == "Weapon")
        {
            WeaponObj weapon = collision.gameObject.GetComponent<WeaponObj>();
            /*角色攻击*/
            if (weapon.userObj.gameObject.GetComponent<PixelCharacter>().IsAttacking)
            {
                ChangeValue(!value);
                ChangeAllOtherSwitch(!value);
                this.GetComponent<AudioSource>().Play();
                office.GetComponent<AudioSource>().Play();
                time = 0;
            }
        }
    }
}
