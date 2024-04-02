using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardEvent : MonoBehaviour
{
    public UnityEvent leftKey;
    public UnityEvent rightKey;
    public UnityEvent lookUpKey;
    public UnityEvent crouchKey;
    public UnityEvent combinationKey;
    public UnityEvent absorbKey;
    public UnityEvent absorbOverKey;
    public UnityEvent jumpKey;
    public UnityEvent moveModifierKey;
    public UnityEvent attackKey;
    public UnityEvent spellKey;
    public UnityEvent spellOverKey;
    public UnityEvent changeSkill;

    public void leftKeyEvent()
    {
        leftKey?.Invoke();
    }
    public void rightKeyEvent()
    {
        rightKey?.Invoke();
    }
    public void lookUpKeyEvent() 
    {
        lookUpKey?.Invoke();
    }
    public void crouchKeyEvent() 
    {
        crouchKey?.Invoke();
    }
    public void combinationKeyEvent() 
    {
        combinationKey?.Invoke();
    }
    public void absorbKeyEvent() 
    {
        absorbKey?.Invoke();
    }
    public void absorbOverKeyEvent()
    {
        absorbOverKey?.Invoke();
    }
    public void jumpKeyEvent()
    {
        jumpKey?.Invoke();
    }
    public void moveModifierKeyEvent()
    {
        moveModifierKey?.Invoke();
    }
    public void attackKeyEvent()
    {
        attackKey?.Invoke();
    }
    public void spellKeyEvent()
    {
        spellKey?.Invoke();
    }
    public void spellOverKeyEvent()
    {
        spellOverKey?.Invoke();
    }
    public void changeSkillEvent() 
    {
        changeSkill?.Invoke();
    }


}
