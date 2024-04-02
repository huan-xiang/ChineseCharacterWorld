using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4UnitControl : MonoBehaviour
{
    public bool downForward = false;
    public bool slay = false;
    public bool jumpBack = false;
    public bool fireBall = false;
    public bool thunder = false;
    public Vector3 resetPos;
    public GameObject myView;
    public bool DownForward
    {
        get {return downForward; }
        set
        {
            downForward = value;
        }
    }

    public bool Slay
    {
        get { return slay; }
        set
        {
            slay = value;
        }
    }

    public bool JumpBack 
    {
        get { return jumpBack; }
        set
        {
            jumpBack = value;
        }
    }
    public bool FireBall
    {
        get { return fireBall; }
        set
        {
            fireBall = value;
        }
    }
    public bool Thunder
    {
        get { return thunder; }
        set
        {
            thunder = value;
        }
    }
    
    public void BossOver()
    {
        downForward = false;
        slay = false;
        jumpBack = false;
        fireBall = false;
        thunder = false;
    }
    public void BossStart()
    {
        myView.SetActive(false);
        downForward = true;
    }
}
