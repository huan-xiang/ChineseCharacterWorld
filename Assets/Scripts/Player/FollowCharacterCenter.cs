using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����ɫ����
/// </summary>
public class FollowCharacterCenter : MonoBehaviour
{
    /// <summary>
    /// �����ɫ�������ǹ���������
    /// </summary>
    public GameObject character;
    /// <summary>
    /// x����ƫ��
    /// </summary>
    public float numX;
    /// <summary>
    /// y����ƫ��
    /// </summary>
    public float numY;
    // Update is called once per frame
    void Update()
    {
        if(character == null)
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        Vector3 characterPos = character.transform.position;
        transform.position = new Vector3(characterPos.x + numX, characterPos.y + numY, -4);
    }
}
