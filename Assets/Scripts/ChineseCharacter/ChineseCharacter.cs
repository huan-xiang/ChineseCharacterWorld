using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChineseCharacter : MonoBehaviour
{
    public string characterName;
    /// <summary>
    /// 能够拆解出的字
    /// </summary>
    public List<UniWord> split_aim;
    /// <summary>
    /// 组合时需要的字串，全写入
    /// </summary>
    public List<UniWord> need;
    /// <summary>
    /// 汉字介绍
    /// </summary>
    public string info;
    /// <summary>
    /// 能够拼出的汉字
    /// </summary>
    public List<string> canBe;
    /// <summary>
    /// 失效时间，-1为不销毁
    /// </summary>
    public int invalidTime = -1;
    /// <summary>
    /// 需要销毁
    /// </summary>
    public bool wantDestory;
}
/// <summary>
/// 组合需要的单位字
/// </summary>
[System.Serializable]
public struct UniWord
{
    /// <summary>
    /// 是个什么字
    /// </summary>
    public string uniWord;
    /// <summary>
    /// 这个字拥有几号部位，1234四部分,12代表1部分和2部分
    /// </summary>
    public int number;
}
