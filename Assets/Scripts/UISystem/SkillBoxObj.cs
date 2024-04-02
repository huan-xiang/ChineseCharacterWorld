using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBoxObj : MonoBehaviour
{
    public GameManagement gameManagement;
    public SkillManagement.SkillName skillName;
    public Text skillNameText;
    public Text skillNumberText;
    /// <summary>
    /// 非技能名称
    /// </summary>
    public string otherName;
    /// <summary>
    /// 冷却
    /// </summary>
    public int cd;
    /// <summary>
    /// 冷却的图片
    /// </summary>
    public Image cdImage;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(cd > 0)
        {
            cd--;
        }
        cdImage.fillAmount = (float)cd / 300.0f;
        if (otherName.Length == 0)
        {
            skillNameText.text = Enum.GetName(typeof(SkillManagement.SkillName), skillName);
        }
        else
        {
            skillNameText.text = otherName;
        }
    }
    public void ChangeSkill()
    {
        otherName = gameManagement.skillManagement.otherName;
        if (gameManagement.skillManagement.choiceSkillName == SkillManagement.SkillName.无)
        {
            return;
        }
        skillName = gameManagement.skillManagement.choiceSkillName;
        gameManagement.skillManagement.choiceSkillName = SkillManagement.SkillName.无;
        gameManagement.skillListObj.transform.localPosition = new Vector3(-350, 625, 0);
    }
}
