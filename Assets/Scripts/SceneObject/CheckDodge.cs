using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 闪避检测
/// </summary>
public class CheckDodge : MonoBehaviour
{
    public GameManagement gameManagement;
    public int delayTime;
    private int time;
    /// <summary>
    /// 使用者
    /// </summary>
    public GameObject user;
    private void FixedUpdate()
    {
        if (time < delayTime)
        {
            time++;
            return;
        }
        /*创建目标地影子*/
        GameObject shadowObj = Instantiate(gameManagement.skillManagement.skillObjList[(int)SkillManagement.ProjectileName.影子]);
        shadowObj.transform.position = transform.position;
        shadowObj.transform.position += new Vector3(0,1,-1);
        shadowObj.transform.SetParent(transform.parent);
        ProjectileObj shadow = shadowObj.GetComponent<ProjectileObj>();
        shadow.user = user;
        shadow.gameManagement = gameManagement;
        shadow.lifeTime = 20;
        shadow.damage = 1;
        /*创建原来地影子*/
        GameObject user_shadowObj = Instantiate(gameManagement.skillManagement.skillObjList[(int)SkillManagement.ProjectileName.影子]);
        user_shadowObj.transform.position = user.transform.position;
        user_shadowObj.transform.position += new Vector3(0, 1, -1);
        user_shadowObj.transform.SetParent(transform.parent);
        ProjectileObj user_shadow = user_shadowObj.GetComponent<ProjectileObj>();
        user_shadow.user = user;
        user_shadow.gameManagement = gameManagement;
        user_shadow.lifeTime = 20;
        user_shadow.damage = 1;
        user.transform.position = transform.position;
        Destroy(gameObject);
    }
}
