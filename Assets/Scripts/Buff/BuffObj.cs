using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj : MonoBehaviour
{
    public BuffManagement buffManagement;
    /// <summary>
    /// buff栏
    /// </summary>
    public BuffStates buffStates;
    /// <summary>
    /// buff名称
    /// </summary>
    public BuffManagement.BuffName buffName;
    /// <summary>
    /// 当前时间
    /// </summary>
    public int time;
    /// <summary>
    /// 触发效果间隔
    /// </summary>
    public int triggerTime;
    /// <summary>
    /// 持续时间
    /// </summary>
    public int finalTime;
    /// <summary>
    /// buff的动画状态机
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
    /// buff结束
    /// </summary>
    public void BuffEnd()
    {
        buffStates.buffList.Remove(this);
        Destroy(gameObject);
    }
}
