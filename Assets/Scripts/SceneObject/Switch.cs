using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    public bool value;
    /// <summary>
    /// ��������
    /// </summary>
    public List<Switch> other;
    public Animator animator;
    public int time;
    public int delayTime;
    /// <summary>
    /// ���ƻ���
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
    /// �ı���������
    /// </summary>
    /// <param name="value">ֵ</param>
    public void ChangeAllOtherSwitch(bool value)
    {
        foreach (Switch switchObj in other)
        {
            switchObj.ChangeValue(value);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*������ȴ*/
        if (time < delayTime) return;
        if (collision.gameObject.tag == "Weapon")
        {
            WeaponObj weapon = collision.gameObject.GetComponent<WeaponObj>();
            /*��ɫ����*/
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
