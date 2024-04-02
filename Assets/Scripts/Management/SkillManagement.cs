using Cainos.Character;
using Cainos.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{
    public GameManagement gameManagement;
    /// <summary>
    /// 技能物体物列表
    /// </summary>
    public List<GameObject> skillObjList;
    public AudioSource nowAudioSource;
    public enum ProjectileName
    {
        火球,雷击瞄准物体,雷击,闪避判定,戮击点,戮击,影子,爆破,火上浇油
    }
    /// <summary>
    /// 创建投射物的根节点
    /// </summary>
    public GameObject createRoot;
    /// <summary>
    /// 选择的技能
    /// </summary>
    public SkillName choiceSkillName;
    /// <summary>
    /// 除技能外的名字
    /// </summary>
    public string otherName;
    public enum SkillName
    {
        无,火,愈,雷,闪,戮,破,其他
    }
    /// <summary>
    /// 技能栏列表
    /// </summary>
    public List<SkillBoxObj> skillBoxObjs;
    public void UseSkill(GameObject user)
    {
        SkillName useSkill = SkillName.无;
        if (user.GetComponent<PlayerObject>())
        {
            if (Input.GetKey(KeyCode.Alpha1) && skillBoxObjs[0].cd == 0 && skillBoxObjs[0].skillName != SkillName.无)
            {
                useSkill = skillBoxObjs[0].skillName;
                skillBoxObjs[0].cd = 300;
                gameManagement.idiomListObj.AddIdiom(skillBoxObjs[0].skillNameText.text);
            }
            if (Input.GetKey(KeyCode.Alpha2) && skillBoxObjs[1].cd == 0 && skillBoxObjs[0].skillName != SkillName.无)
            {
                useSkill = skillBoxObjs[1].skillName;
                skillBoxObjs[1].cd = 300;
                gameManagement.idiomListObj.AddIdiom(skillBoxObjs[1].skillNameText.text);
            }
            if (Input.GetKey(KeyCode.Alpha3) && skillBoxObjs[2].cd == 0 && skillBoxObjs[0].skillName != SkillName.无)
            {
                useSkill = skillBoxObjs[2].skillName;
                skillBoxObjs[2].cd = 300;
                gameManagement.idiomListObj.AddIdiom(skillBoxObjs[2].skillNameText.text);
            }
            if (Input.GetKey(KeyCode.Alpha4) && skillBoxObjs[3].cd == 0 && skillBoxObjs[0].skillName != SkillName.无)
            {
                useSkill = skillBoxObjs[3].skillName;
                skillBoxObjs[3].cd = 300;
                gameManagement.idiomListObj.AddIdiom(skillBoxObjs[3].skillNameText.text);
            }
        }
        switch (useSkill)
        {
            case SkillName.火:
                FireBall(user);
                break;
            case SkillName.愈:
                Heal(user);
                break;
            case SkillName.雷:
                Thunder(user);
                break;
            case SkillName.闪:
                Dodge(user, new Vector2());
                break;
            case SkillName.戮:
                Slay(user);
                break;
            case SkillName.破:
                Blast(user);
                break;
        }
    }
    /// <summary>
    /// 火球术
    /// </summary>
    /// <param name="user">使用者</param>
    public void FireBall(GameObject user)
    {
        float direction = 0;
        Vector3 spellPos = new Vector3();
        if (user.GetComponent<PlayerObject>())
        {
            direction = user.GetComponent<PixelCharacter>().Facing;
            if(user.tag=="Player")
            {
                spellPos = user.GetComponent<PlayerObject>().weaponObj.spellPos.transform.position;
            }
            else
            {
                spellPos = user.GetComponent<BossObject>().weaponObj.spellPos.transform.position;
            }
        }
        else if (user.GetComponent<PixelMonster>())
        {
            direction = user.GetComponent<PixelMonster>().Facing;
            spellPos = user.GetComponent<MonsterObj>().transform.position;
    }
        GameObject fireball = Instantiate(skillObjList[(int)ProjectileName.火球]);
        fireball.transform.position = spellPos;
        fireball.transform.SetParent(createRoot.transform);
        ProjectileObj fireballObj =  fireball.GetComponent<ProjectileObj>();
        fireballObj.direction = new Vector2(direction,0);
        nowAudioSource = fireball.GetComponent<AudioSource>();
        nowAudioSource.clip = gameManagement.audioManager.fireBallAudio;
        nowAudioSource.Play();
        fireballObj.user = user;
        fireballObj.gameManagement = gameManagement;
        fireballObj.Push(fireballObj.direction, 3);
        fireballObj.canBeDestory = true;
        fireballObj.lifeTime = 600;
    }
    /// <summary>
    /// 火上浇油
    /// </summary>
    /// <param name="user">使用者</param>
    public void AddFuelToTheFlames(GameObject user)
    {
        GameObject flamesObj = Instantiate(skillObjList[(int)ProjectileName.火上浇油]);
        flamesObj.transform.position = user.transform.position + new Vector3(0,1,0);
        flamesObj.transform.SetParent(createRoot.transform);
        ProjectileObj flamesProjectileObj = flamesObj.GetComponent<ProjectileObj>();
        flamesProjectileObj.user = user;
        flamesProjectileObj.gameManagement = gameManagement;
        flamesProjectileObj.canBeDestory = false;
        flamesProjectileObj.lifeTime = 60;
    }
    /// <summary>
    /// 治愈术
    /// </summary>
    /// <param name="user">使用者</param>
    public void Heal(GameObject user)
    {
        if (user.GetComponent<PlayerObject>())
        {
            ChineseCharacter newChineseCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter("愈");
            gameManagement.characterStates[1].chineseCharacters.Add(newChineseCharacter);
            nowAudioSource = GetComponent<AudioSource>();
            nowAudioSource.clip = gameManagement.audioManager.healingAudio;
            nowAudioSource.Play();
        }
        else if(user.GetComponent<MonsterObj>())
        {
            ChineseCharacter newChineseCharacter = gameManagement.chineseCharacterManagement.CreateChineseCharacter("愈");
            user.GetComponent<BuffStates>().passiveChineseCharacterList.Add(newChineseCharacter);
        }
    }
    /// <summary>
    /// 雷击
    /// </summary>
    /// <param name="user">使用者</param>
    public void Thunder(GameObject user)
    {
        GameObject thunderAimObj = Instantiate(skillObjList[(int)ProjectileName.雷击瞄准物体]);
        if (user.GetComponent<PlayerObject>().player_enemy != null)
            thunderAimObj.transform.position = user.GetComponent<PlayerObject>().player_enemy.transform.position;
        else
            thunderAimObj.transform.position = user.transform.position;
        thunderAimObj.transform.position = new Vector3(thunderAimObj.transform.position.x, thunderAimObj.transform.position.y, -1);
        thunderAimObj.transform.SetParent(createRoot.transform);
        ThunderPrepareObj thunderPrepare = thunderAimObj.GetComponent<ThunderPrepareObj>();
        thunderPrepare.gameManagement = gameManagement;
        thunderPrepare.user = user;
        thunderPrepare.delayTime = 60;
    }
    /// <summary>
    /// 闪避
    /// </summary>
    /// <param name="user">使用者</param>
    public void Dodge(GameObject user,Vector2 aimPos)
    {
        if (user.gameObject.tag == "Player")
        {
            aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        GameObject dodgeObj = Instantiate(skillObjList[(int)ProjectileName.闪避判定]);
        dodgeObj.transform.position = aimPos;
        nowAudioSource = dodgeObj.GetComponent<AudioSource>();
        nowAudioSource.clip = gameManagement.audioManager.flashAudio;
        nowAudioSource.Play();
        dodgeObj.transform.SetParent(createRoot.transform);
        dodgeObj.GetComponent<CheckDodge>().delayTime = 10;
        dodgeObj.GetComponent<CheckDodge>().user = user;
        dodgeObj.GetComponent<CheckDodge>().gameManagement = gameManagement;
    }
    /// <summary>
    /// 戮
    /// </summary>
    /// <param name="user"></param>
    public void Slay(GameObject user)
    {
        GameObject slayObj = Instantiate(skillObjList[(int)ProjectileName.戮击点]);
        slayObj.transform.position = new Vector3(user.transform.position.x, user.transform.position.y + 4f, -1);
        slayObj.transform.SetParent(createRoot.transform);
        SlayPrepareObj slayPrepareObj = slayObj.GetComponent<SlayPrepareObj>();
        slayPrepareObj.gameManagement = gameManagement;
        slayPrepareObj.user = user;
        slayPrepareObj.delayTime = 40;
        if (user.GetComponent<PlayerObject>())
        {
            slayPrepareObj.direction = user.GetComponent<PlayerObject>().pixelCharacter.Facing * 5;
        }
        else if (user.GetComponent<MonsterObj>())
        {
            slayPrepareObj.direction = user.GetComponent<MonsterObj>().pixelMonster.Facing * 5;
        }
    }
    /// <summary>
    /// 爆破
    /// </summary>
    /// <param name="user"></param>
    public void Blast(GameObject user)
    {
        GameObject blastObj = Instantiate(skillObjList[(int)ProjectileName.爆破]);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        blastObj.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
        blastObj.transform.SetParent(createRoot.transform);
        ProjectileObj blastProjectileObj = blastObj.GetComponent<ProjectileObj>();
        blastProjectileObj.user = user;
        blastProjectileObj.gameManagement = gameManagement;
        blastProjectileObj.lifeTime = 120;

    }
}
