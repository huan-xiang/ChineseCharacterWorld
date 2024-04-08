using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cainos.Character
{
    public class PixelCharacterController : MonoBehaviour
    {
        const float GROUND_CHECK_RADIUS = 0.1f;                 // radius of the overlap circle to determine if the character is on ground

        public MovementType defaultMovement = MovementType.Walk;
        public KeyCode leftKey = KeyCode.A;
        public KeyCode rightKey = KeyCode.D;
        public KeyCode lookUpKey = KeyCode.W;
        public KeyCode crouchKey = KeyCode.S;
        public KeyCode combinationKey = KeyCode.E;
        public KeyCode absorbKey = KeyCode.F;
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode moveModifierKey = KeyCode.LeftShift;

        public KeyCode attackKey = KeyCode.Mouse0;
        public KeyCode spellKey = KeyCode.Mouse1;
        public KeyCode changeSkill = KeyCode.Tab;

        public float walkSpeedMax = 2.5f;                       // max walk speed, ideally should be half of runSpeedMax
        public float walkAcc = 10.0f;                           // walking acceleration

        public float runSpeedMax = 5.0f;                        // max run speed
        public float runAcc = 10.0f;                            // running acceleration

        public float crouchSpeedMax = 1.0f;                     // max move speed while crouching
        public float crouchAcc = 8.0f;                          // crouching acceleration

        public float airSpeedMax = 2.0f;                        // max move speed while in air
        public float airAcc = 8.0f;                             // air acceleration

        public float groundBrakeAcc = 6.0f;                     // braking acceleration (from movement to still) while on ground
        public float airBrakeAcc = 1.0f;                        // braking acceleration (from movement to still) while in air

        public float jumpSpeed = 5.0f;                          // speed applied to character when jump
        public float jumpCooldown = 0.55f;                      // time to be able to jump again after landing
        public float jumpGravityMutiplier = 0.6f;               // gravity multiplier when character is jumping, should be within [0.0,1.0], set it to lower value so that the longer you press the jump button, the higher the character can jump    
        public float fallGravityMutiplier = 1.3f;               // gravity multiplier when character is falling, should be equal or greater than 1.0

        public float groundCheckRadius = 0.17f;                 // radius of the circle at the character's bottom to determine whether the character is on ground

        [ExposeProperty]                                        // is the character dead? if dead, plays dead animation and disable control
        public bool IsDead
        {
            get { return isDead; }
            set
            {
                isDead = value;
                fx.IsDead = isDead;
                fx.DropWeapon();
            }
        }
        private bool isDead;

        /*角色脚本*/
        private PixelCharacter fx;                              // the FXCharacter script attached the character
        private CapsuleCollider2D collider2d;                   // Collider compoent on the character
        /// <summary>
        /// 游戏管理器
        /// </summary>
        private GameManagement gameManagement;
        /// <summary>
        /// 角色组件的Rigidbody2D
        /// </summary>
        private Rigidbody2D rb2d;                               // Rigidbody2D component on the character
        public PlayerObject playerObject;
        /// <summary>
        /// 是否着陆
        /// </summary>
        private bool isGrounded;                                // is the character on ground
        /// <summary>
        /// 是否开启奔跑
        /// </summary>
        public  bool isRun;
        private Vector2 curVel;                                 // 流速
        private float jumpTimer;                                // timer for jump cooldown
        private Vector2 posBot;                                 // local position of the character's middle bottom
        private Vector2 posTop;                                 // local position of the character's middle top
        /// <summary>
        /// 是否可以二段跳
        /// </summary>
        private int canJumpTimes;
        private int saveCanJumpTimes;

        public bool inputCrounch = false;
        public bool inputMoveModifier = false;
        public bool inputJump = false;
        public bool inputJumpDown = false;
        public float inputH = 0.0f;
        public bool inputAttack = false;
        public bool inputAttackContinuous = false;
        public bool inputSpell = false;
        public bool combination;
        public bool absorb = false;
        public bool inputChangeSkill = false;

        public AudioSource myAudioSource;
        public bool canAbsorb = false;
        public bool canConbinate = false;

        private void Awake()
        {
            fx = GetComponent<PixelCharacter>();
            collider2d = GetComponent<CapsuleCollider2D>();
            rb2d = GetComponent<Rigidbody2D>();
            playerObject = GetComponent<PlayerObject>();
            gameManagement = (GameManagement)FindObjectOfType(typeof(GameManagement));
            myAudioSource = GetComponent<AudioSource>();
            canJumpTimes = 1;
            /*默认跑步*/
            isRun = true;
            /*默认boss不连击*/
            if(gameObject.tag == "Boss")
            {
                keepAttackAction = true;

            }
        }

        private void Start()
        {
            posBot = collider2d.offset - new Vector2 ( 0.0f , collider2d.size.y * 0.5f );
            posTop = collider2d.offset + new Vector2( 0.0f, collider2d.size.y * 0.5f );
        }

        private void Update()
        {
            if (GameManagement._stop == true)
            {
                isRun = false;
                inputH = 0;
                Move(inputH, inputCrounch, inputJumpDown, inputJump);
                Attack(inputAttack, inputAttackContinuous);
                Combination(combination);
                Absorb(absorb);
                FireSpell(inputSpell);
                //ChangeSkill(inputChangeSkill);
                isGrounded = false;
                Vector2 worldPos1 = transform.position;
                Collider2D[] colliders1 = Physics2D.OverlapCircleAll(worldPos1 + posBot, groundCheckRadius);
                for (int i = 0; i < colliders1.Length; i++)
                {
                    if (colliders1[i].isTrigger) continue;
                    if (colliders1[i].gameObject != gameObject) isGrounded = true;
                }
                return;
            }
            if (playerObject.pixelCharacter.IsDead) return;
            if(gameObject.tag=="Player")
            {
                if (jumpTimer < jumpCooldown) jumpTimer += Time.deltaTime;

                /*按键是否在UI外面，不用UI先注释*/
                bool pointerOverUI = EventSystem.current && EventSystem.current.IsPointerOverGameObject();
                if (!pointerOverUI)
                {
                    /*更新所有按键是否按下*/
                    /*按下攻击*/
                    inputAttack = Input.GetKeyDown(attackKey);
                    /*持续攻击无间隔*/
                    inputAttackContinuous = Input.GetKey(attackKey);
                    /*按下施法*/
                    inputSpell = Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4);
                }
                /*以下不受UI影响*/
                /*改变技能键*/
                //inputChangeSkill = Input.GetKey(changeSkill);
                /*组合汉字键*/
                combination = Input.GetKey(combinationKey);
                /*吸收键*/
                absorb = Input.GetKey(absorbKey);
                /*左键向左，右键向右，否则不动*/
                if (Input.GetKey(leftKey)) inputH = -1.0f;
                else
                if (Input.GetKey(rightKey)) inputH = 1.0f;
                else inputH = 0.0f;
                /*加速键,ps:使用KeyDown会可能没反应*/
                //inputMoveModifier = Input.GetKey(moveModifierKey);
                inputMoveModifier = Input.GetKeyDown(moveModifierKey);
                /*持续跳跃键*/
                inputJump = Input.GetKey(jumpKey);
                inputJumpDown = Input.GetKeyDown(jumpKey);
                /*下蹲键*/
                inputCrounch = Input.GetKey(crouchKey);
                /*修改是否奔跑*/
                //bool inputRun = false;
                //bool inputRun = isRun? true : fx.MovingBlend > 0.6;
                //if (defaultMovement == MovementType.Walk && inputMoveModifier) inputRun = true;
                //if (defaultMovement == MovementType.Run && !inputMoveModifier) inputRun = true;
                /*按下加速键时切换一次奔跑状态*/
                if (inputMoveModifier)
                {
                    isRun = !isRun;
                }
            }
            /*把变量传入行为*/
            Move(inputH, inputCrounch, inputJumpDown, inputJump);
            Attack(inputAttack, inputAttackContinuous);
            Combination(combination);
            Absorb(absorb);
            FireSpell(inputSpell);
            //ChangeSkill(inputChangeSkill);

            //CHECK IF THE CHARACTER IS ON GROUND
            isGrounded = false;
            Vector2 worldPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos + posBot, groundCheckRadius);
            for (int i = 0; i < colliders.Length; i++ )
            {
                if ( colliders[i].isTrigger ) continue;
                if ( colliders[i].gameObject != gameObject ) isGrounded = true;
            }
        }

        /// <summary>
        /// 角色攻击
        /// </summary>
        /// <param name="inputAttack">是否攻击</param>
        /// <param name="inputAttackContinuous">是否持续攻击</param>
        public void Attack( bool inputAttack , bool inputAttackContinuous)
        {
            if (gameObject.tag == "Player")
            {
                /*攻击间隔*/
                if (attackDelay > 30)
                {
                    attackDelay--;
                    return;
                }
                /*连击*/
                else if (attackDelay > 0)
                {
                    playerObject.deepAttack = false;
                    attackDelay--;
                    if (inputAttackContinuous && !attackBefore)
                    {
                        if (fx.AttackAction == PixelCharacter.AttackActionType.Swipe)
                        {
                            fx.AttackAction = PixelCharacter.AttackActionType.Stab;
                        }
                        else
                        {
                            fx.AttackAction = PixelCharacter.AttackActionType.Swipe;
                        }
                    }
                }
                else
                {
                    /*按下*/
                    if(inputAttackContinuous && !attackBefore)
                    {
                        attackBefore = inputAttackContinuous;
                        playerObject.spellTimeSlider.gameObject.SetActive(true);
                        playerObject.spellTimeSlider.value = 0;
                        return;
                    }
                    /*按着*/
                    else if(inputAttackContinuous && attackBefore)
                    {
                        attackBefore = inputAttackContinuous;
                        playerObject.spellTimeSlider.value += 2.5f;
                        return;
                    }
                    /*抬起*/
                    else if(!inputAttackContinuous && attackBefore)
                    {
                        attackBefore = inputAttackContinuous;
                        fx.AttackAction = PixelCharacter.AttackActionType.Swipe;
                        /*重击*/
                        if (playerObject.spellTimeSlider.value >= 100)
                        {
                            attackDelay = 90;
                            playerObject.deepAttack = true;
                        }
                        /*普通攻击*/
                        else
                        {
                            attackDelay = 70;
                        }
                        fx.Attack();
                        playerObject.spellTimeSlider.value = 0;
                        playerObject.spellTimeSlider.gameObject.SetActive(false);
                        playerObject.weaponObj.GetComponent<Collider2D>().enabled = false;
                        return;
                    }
                }
            }
            /*调用角色的攻击动作*/
            if (inputAttack)
            {
                fx.Attack();
                attackDelay = 70;
                playerObject.weaponObj.GetComponent<Collider2D>().enabled = false;
            }
        }
        private int attackDelay;
        private bool attackBefore;
        /// <summary>
        /// 攻击状态锁定
        /// </summary>
        public bool keepAttackAction;
        /// <summary>
        /// 角色施法
        /// </summary>
        /// <param name="inputSpell">是否施法</param>
        public void FireSpell(bool inputSpell)
        {
            /*按下*/
            if (!spellBefore && inputSpell)
            {
                //gameManagement.keyBoardEvent.spellKeyEvent();
                //attackDelay = 60;
                //fx.IsSpelling = true;
                //playerObject.spellTimeSlider.gameObject.SetActive(true);
                //playerObject.spellTimeSlider.value = 0;
                //gameManagement.keyBoardEvent.spellKeyEvent();
                fx.AttackAction = PixelCharacter.AttackActionType.Point;
                fx.Attack();
            }
            /*按着*/
            //else if(spellBefore && inputSpell)
            //{
            //    attackDelay++;
            //    playerObject.spellTimeSlider.value += 1.2f;
            //}
            /*松开*/
            //else if(spellBefore && !inputSpell)
            //{
            //    gameManagement.keyBoardEvent.spellOverKeyEvent();
            //    fx.IsSpelling = false;
            //    playerObject.spellTimeSlider.gameObject.SetActive(false);
            //    if (playerObject.spellTimeSlider.value >= 100)
            //    {
            //        fx.AttackAction = PixelCharacter.AttackActionType.Point;
            //        fx.Attack();
            //    }
            //    playerObject.spellTimeSlider.value = 0;
            //}
            spellBefore = inputSpell;
            //if (attackDelay > 0)
            //{
            //    attackDelay--;
            //    return;
            //}
            //if (inputSpell)
            //{
            //    attackDelay = 120;
            //    fx.AttackAction = PixelCharacter.AttackActionType.Point;
            //    fx.Attack();
            //}
        }
        private bool spellBefore;
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="inputH">-左右+</param>
        /// <param name="inputCrouch">是否下蹲</param>
        /// <param name="inputJumpDown">是否按下跳跃</param>
        /// <param name="inputJump">是否持续跳跃</param>
        public void Move(float inputH, bool inputCrouch, bool inputJumpDown, bool inputJump)
        {
            if (isDead) return;
            if(cantMoveTime > 0)
            {
                cantMoveTime--;
                return;
            }
            //GET CURRENT SPEED FROM RIGIDBODY
            curVel = rb2d.velocity;

            //根据条件设置加速度和最大速度
            /*加速度*/
            float acc = 0.0f;
            /*最大速度*/
            float max = 0.0f;
            float brakeAcc = 0.0f;

            if (isGrounded)/*地面上*/
            {
                acc = isRun ? runAcc : walkAcc;
                max = isRun ? runSpeedMax : walkSpeedMax;
                brakeAcc = groundBrakeAcc;

                if (inputCrouch)/*蹲下*/
                {
                    acc = crouchAcc;
                    max = crouchSpeedMax;
                }
            }
            else/*空中*/
            {
                /*加速跳跃时2倍速度*/
                acc = Mathf.Abs(fx.MovingBlend) > 0.6 ? airAcc * 1.5f : airAcc;
                max = Mathf.Abs(fx.MovingBlend) > 0.6 ? airSpeedMax * 1.5f : airSpeedMax;
                brakeAcc = airBrakeAcc;
            }


            //HANDLE HORIZONTAL MOVEMENT
            /*按下了移动键*/
            if (Mathf.Abs(inputH) > 0.01f)
            {
                //if current horizontal speed is out of allowed range, let it fall to the allowed range
                bool shouldMove = true;
                if (inputH > 0 && curVel.x >= max)
                {
                    curVel.x = Mathf.MoveTowards(curVel.x, max, brakeAcc * Time.deltaTime);
                    shouldMove = false;
                }
                if (inputH < 0 && curVel.x <= -max)
                {
                    curVel.x = Mathf.MoveTowards(curVel.x, -max, brakeAcc * Time.deltaTime);
                    shouldMove = false;
                }

                //otherwise, add movement acceleration to cureent velocity
                if (shouldMove) curVel.x += acc * Time.deltaTime * inputH;
            }
            /*停止到0*/
            else
            {
                curVel.x = Mathf.MoveTowards(curVel.x, 0.0f, brakeAcc * Time.deltaTime);
            }

            /*跳跃，地面或者空中*/
            if ((isGrounded && inputJumpDown && jumpTimer >= jumpCooldown)|| (!isGrounded && saveCanJumpTimes > 0 && inputJumpDown))
            {
                isGrounded = false;
                jumpTimer = 0.0f;
                curVel.y = jumpSpeed;
                saveCanJumpTimes--;
            }
            if ( inputJump && curVel.y > 0)
            {
                curVel.y += Physics.gravity.y * (jumpGravityMutiplier -1.0f) * Time.deltaTime;
            }
            else if (curVel.y > 0)
            {
                curVel.y += Physics.gravity.y * ( fallGravityMutiplier - 1.0f) * Time.deltaTime;
            }

            /*落地*/
            if (isGrounded)
                saveCanJumpTimes = canJumpTimes;
            rb2d.velocity = curVel;
            float movingBlend = Mathf.Abs(curVel.x) / runSpeedMax;
            /*修改角色移动状态*/
            /*跳跃冷却以及在地面时,会休息一下*/
            fx.MovingBlend = Mathf.Abs(curVel.x) / runSpeedMax;

            if (isGrounded) fx.IsCrouching = inputCrouch;

            fx.SpeedVertical = curVel.y;
            fx.Facing = Mathf.RoundToInt(inputH);
            fx.IsGrounded = isGrounded;
        }
        public enum MovementType
        {
            Walk,
            Run
        }
        /// <summary>
        /// 是否静止移动
        /// </summary>
        public int cantMoveTime;
        /// <summary>
        /// 吸收环境字
        /// </summary>
        /// <param name="nowFrame">这一帧是否按着</param>
        public void Absorb(bool nowFrame)
        {
            if (canAbsorb == false) return;
            AbsorbObj absorbObj = gameManagement.absorbObj;
            /*按下,上一帧没按这一帧按了*/
            if (!preAbsorbFrame&&nowFrame)
            {
                gameManagement.keyBoardEvent.absorbKeyEvent();
                /*播放施法动画*/
                fx.IsAbsorbing = true;
                myAudioSource.clip = gameManagement.audioManager.absorbAudio;
                myAudioSource.Play();
                /*调用吸收开始方法*/
                absorbObj.AbsorbStart();
                fx.animator.SetTrigger("Absorb");
                attackDelay = 120;
            }
            /*保持按键，上一帧和这一帧都按了*/
            else if (preAbsorbFrame && nowFrame)
            {
                
            }
            /*抬起按键，上一帧按了，这一帧没按*/
            else if (preAbsorbFrame && !nowFrame)
            {
                /*关闭施法动画*/
                fx.IsAbsorbing = false;
                /*调用吸收结束方法*/
                //absorbObj.AbsorbOver();
            }
            preAbsorbFrame = nowFrame;
        }
        private bool preAbsorbFrame;
        /// <summary>
        /// 组合字
        /// </summary>
        /// <param name="nowFrame">这一帧是否按着</param>
        public void Combination(bool nowFrame)
        {
            if (canConbinate == false) return;
            ChoiceRouletteManagement choiceRoulette = gameManagement.choiceRouletteManagement;
            /*按下,上一帧没按这一帧按了*/
            if (!preCombinationFrame && nowFrame)
            {
                choiceRoulette.gameObject.transform.position = Input.mousePosition;
                choiceRoulette.gameObject.SetActive(true);
                /*按下打开/关闭*/
                //if (choiceRoulette.gameObject.activeInHierarchy == true)
                //{
                //    choiceRoulette.isChoosing = false;
                //    choiceRoulette.Close();
                //}
                //else
                //{
                //    choiceRoulette.gameObject.transform.position = Input.mousePosition;
                //    choiceRoulette.gameObject.SetActive(true);
                //}
            }
            /*保持按键，上一帧和这一帧都按了*/
            else if (preCombinationFrame && nowFrame)
            {
            }
            /*抬起按键，上一帧按了，这一帧没按*/
            else if (preCombinationFrame && !nowFrame)
            {
                choiceRoulette.isChoosing = false;
                choiceRoulette.Close();
            }
            preCombinationFrame = nowFrame;
        }
        private bool preCombinationFrame;

        /// <summary>
        /// 改变技能
        /// </summary>
        //public void ChangeSkill(bool inputChangeSkill)
        //{
        //    /*获取可用技能列表*/
        //    List<SkillManagement.SkillName> skillNames = new List<SkillManagement.SkillName>();
        //    skillNames.Add(SkillManagement.SkillName.无);
        //    for(int i = 0; i < Enum.GetNames(typeof(SkillManagement.SkillName)).Length; i++)
        //    {
        //        foreach(ChineseCharacter chineseCharacter in gameManagement.characterStates[0].chineseCharacters)
        //        {
        //            string skillName = Enum.GetName(typeof(SkillManagement.SkillName), i);
        //            if (chineseCharacter.characterName == skillName)
        //            {
        //                skillNames.Add((SkillManagement.SkillName)i);
        //            }
        //        }
        //    }
        //    /*切换技能*/
        //    for(int i = 0; i < skillNames.Count; i++)
        //    {
        //        if(skillNames[i] == playerObject.loadSkillName)
        //        {
        //            /*按下才切换*/
        //            if (inputChangeSkill && !inputChangeSkillBefore)
        //            {
        //                if (i == skillNames.Count - 1)
        //                {
        //                    playerObject.loadSkillName = SkillManagement.SkillName.无;
        //                }
        //                else
        //                {
        //                    playerObject.loadSkillName = skillNames[i + 1];
        //                }
        //            }
        //            break;
        //        }
        //        if(i == skillNames.Count - 1 && skillNames[i] != playerObject.loadSkillName)
        //        {
        //            playerObject.loadSkillName = SkillManagement.SkillName.无;
        //        }
        //    }
        //    inputChangeSkillBefore = inputChangeSkill;
        //}
        //private bool inputChangeSkillBefore;
        private void OnDrawGizmosSelected()
        {
            //Draw the ground detection circle
            Gizmos.color = Color.white;
            Vector2 worldPos = transform.position;
            Gizmos.DrawWireSphere(worldPos + posBot, groundCheckRadius);
        }

    }
}



