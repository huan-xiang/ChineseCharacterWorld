using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 跟随角色中心
/// </summary>
public class FollowCharacterCenter : MonoBehaviour
{
    /// <summary>
    /// 跟随角色，可以是怪物或者玩家
    /// </summary>
    public GameObject character;
    /// <summary>
    /// x方向偏移
    /// </summary>
    public float numX;
    /// <summary>
    /// y方向偏移
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
