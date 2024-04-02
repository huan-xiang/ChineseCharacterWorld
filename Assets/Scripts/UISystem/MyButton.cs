using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    /// <summary>
    /// 按钮图片
    /// </summary>
    public Image buttonImage;
    public bool isDisplaying;
    public bool isHiding;
    /// <summary>
    /// 正在删除
    /// </summary>
    public bool isDestroying;
    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum MyButtonType
    {
        显现式,
        书签式
    }
    /// <summary>
    /// 按钮初始位置
    /// </summary>
    public Vector3 originPos;
    public MyButtonType type;
    /// <summary>
    /// 显示/隐藏速度
    /// </summary>
    public int speed;
    private void Awake()
    {
        isDisplaying = false;
        isHiding = false;
        isDestroying = false;
    }
    private void Update()
    {
        /*逐渐显示*/
        if (isDisplaying)
        {
            display();
        }
        /*逐渐隐藏*/
        if (isHiding)
        {
            hide();
            isDisplaying = false;
        }
    }
    /// <summary>
    /// 显示
    /// </summary>
    private void display()
    {
        switch (type)
        {
            case MyButtonType.显现式:
                /*逐渐显示按钮*/
                if (buttonImage.fillAmount < 1)
                {
                    buttonImage.fillAmount += 0.03f;
                }
                else
                {
                    buttonImage.fillAmount = 1;
                    isDisplaying = false;
                }
                break;
            case MyButtonType.书签式:
                /*从背景后面钻出*/
                if (transform.localPosition.x < originPos.x)
                {
                    //transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
                    transform.localPosition = new Vector3(transform.localPosition.x + Time.deltaTime * speed, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    transform.localPosition = new Vector3(originPos.x, transform.localPosition.y, transform.localPosition.z);
                    isDisplaying = false;
                }
                break;
        }
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    private void hide()
    {
        switch (type)
        {
            case MyButtonType.显现式:
                /*逐渐隐藏按钮*/
                if (buttonImage.fillAmount > 0)
                {
                    buttonImage.fillAmount -= 0.03f;
                }
                else
                {
                    buttonImage.fillAmount = 0;
                    isHiding = false;
                    if (isDestroying)
                    {
                        Destroy(gameObject);
                    }
                }
                break;
            case MyButtonType.书签式:
                /*移动到背景后*/
                float aimX = originPos.x - transform.GetComponent<RectTransform>().rect.width * 5 / 8;
                if (transform.localPosition.x > aimX)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - Time.deltaTime * speed, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    transform.localPosition = new Vector3(aimX, transform.localPosition.y, transform.localPosition.z);
                    isHiding = false;
                    if (isDestroying)
                    {
                        Destroy(gameObject);
                    }
                }
                break;

        }
    }
    /// <summary>
    /// 按钮显示
    /// </summary>
    public void ButtonDisplay()
    {
        isDisplaying = true;
    }
    /// <summary>
    /// 隐藏按钮
    /// </summary>
    public void ButtonHide()
    {
        isHiding = true;
    }
}
