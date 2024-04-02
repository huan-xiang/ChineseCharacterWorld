using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChineseCharacter : MonoBehaviour
{
    public string characterName;
    /// <summary>
    /// �ܹ���������
    /// </summary>
    public List<UniWord> split_aim;
    /// <summary>
    /// ���ʱ��Ҫ���ִ���ȫд��
    /// </summary>
    public List<UniWord> need;
    /// <summary>
    /// ���ֽ���
    /// </summary>
    public string info;
    /// <summary>
    /// �ܹ�ƴ���ĺ���
    /// </summary>
    public List<string> canBe;
    /// <summary>
    /// ʧЧʱ�䣬-1Ϊ������
    /// </summary>
    public int invalidTime = -1;
    /// <summary>
    /// ��Ҫ����
    /// </summary>
    public bool wantDestory;
}
/// <summary>
/// �����Ҫ�ĵ�λ��
/// </summary>
[System.Serializable]
public struct UniWord
{
    /// <summary>
    /// �Ǹ�ʲô��
    /// </summary>
    public string uniWord;
    /// <summary>
    /// �����ӵ�м��Ų�λ��1234�Ĳ���,12����1���ֺ�2����
    /// </summary>
    public int number;
}
