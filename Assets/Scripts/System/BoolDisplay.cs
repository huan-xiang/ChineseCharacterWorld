using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������UI����ʾboolֵ
/// </summary>
public class BoolDisplay : MonoBehaviour
{
    public bool value;
    /// <summary>
    /// ������ʾ���ͼƬ
    /// </summary>
    public Sprite trueSprite;
    /// <summary>
    /// ������ʾ�ٵ�ͼƬ
    /// </summary>
    public Sprite falseSprite;
    /// <summary>
    /// ͼƬ
    /// </summary>
    public Image image;
    void Update()
    {
        image.sprite = value ? trueSprite : falseSprite;
    }
}
