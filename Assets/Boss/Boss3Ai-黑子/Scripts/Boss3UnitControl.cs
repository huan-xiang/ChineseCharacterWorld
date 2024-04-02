using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3UnitControl : MonoBehaviour
{
    public float flashAttackInterval = 3f;
    public bool flashBack = false;
    public bool flashAttack = false;
    public Vector3 resetPos;
    public GameObject myView;
    public bool FlashBack
    {
        get { return flashBack; }
        set
        {
            flashBack = value;
        }
    }

    public bool FlashAttack
    {
        get { return flashAttack; }
        set
        {
            flashAttack = value;
        }
    }

    public void bossOver()
    {
        flashBack = false;
        flashAttack = false;
    }
    public void bossStart()
    {
        myView.SetActive(false);
        flashBack = true;
    }
}
