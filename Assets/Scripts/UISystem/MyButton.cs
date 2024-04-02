using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    /// <summary>
    /// ��ťͼƬ
    /// </summary>
    public Image buttonImage;
    public bool isDisplaying;
    public bool isHiding;
    /// <summary>
    /// ����ɾ��
    /// </summary>
    public bool isDestroying;
    /// <summary>
    /// ��ť����
    /// </summary>
    public enum MyButtonType
    {
        ����ʽ,
        ��ǩʽ
    }
    /// <summary>
    /// ��ť��ʼλ��
    /// </summary>
    public Vector3 originPos;
    public MyButtonType type;
    /// <summary>
    /// ��ʾ/�����ٶ�
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
        /*����ʾ*/
        if (isDisplaying)
        {
            display();
        }
        /*������*/
        if (isHiding)
        {
            hide();
            isDisplaying = false;
        }
    }
    /// <summary>
    /// ��ʾ
    /// </summary>
    private void display()
    {
        switch (type)
        {
            case MyButtonType.����ʽ:
                /*����ʾ��ť*/
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
            case MyButtonType.��ǩʽ:
                /*�ӱ����������*/
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
    /// ����
    /// </summary>
    private void hide()
    {
        switch (type)
        {
            case MyButtonType.����ʽ:
                /*�����ذ�ť*/
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
            case MyButtonType.��ǩʽ:
                /*�ƶ���������*/
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
    /// ��ť��ʾ
    /// </summary>
    public void ButtonDisplay()
    {
        isDisplaying = true;
    }
    /// <summary>
    /// ���ذ�ť
    /// </summary>
    public void ButtonHide()
    {
        isHiding = true;
    }
}
