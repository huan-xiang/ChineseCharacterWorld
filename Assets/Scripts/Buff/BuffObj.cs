using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj : MonoBehaviour
{
    public BuffManagement buffManagement;
    /// <summary>
    /// buff��
    /// </summary>
    public BuffStates buffStates;
    /// <summary>
    /// buff����
    /// </summary>
    public BuffManagement.BuffName buffName;
    /// <summary>
    /// ��ǰʱ��
    /// </summary>
    public int time;
    /// <summary>
    /// ����Ч�����
    /// </summary>
    public int triggerTime;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public int finalTime;
    /// <summary>
    /// buff�Ķ���״̬��
    /// </summary>
    public Animator animator;
    void FixedUpdate()
    {
        if (time < finalTime)
        {
            time++;
        }
        else
        {
            BuffEnd();
        }
        if(time%triggerTime == 0)
        {
            buffManagement.BuffInfluent(buffName, buffStates.character);
        }
    }
    /// <summary>
    /// buff����
    /// </summary>
    public void BuffEnd()
    {
        buffStates.buffList.Remove(this);
        Destroy(gameObject);
    }
}
