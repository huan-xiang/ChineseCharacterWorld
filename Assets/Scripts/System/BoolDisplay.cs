using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 用来在UI中显示bool值
/// </summary>
public class BoolDisplay : MonoBehaviour
{
    public bool value;
    /// <summary>
    /// 用来显示真的图片
    /// </summary>
    public Sprite trueSprite;
    /// <summary>
    /// 用来显示假的图片
    /// </summary>
    public Sprite falseSprite;
    /// <summary>
    /// 图片
    /// </summary>
    public Image image;
    void Update()
    {
        image.sprite = value ? trueSprite : falseSprite;
    }
}
