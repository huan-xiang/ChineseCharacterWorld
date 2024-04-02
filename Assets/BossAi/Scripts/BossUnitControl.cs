using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnitControl : MonoBehaviour
{
    public bool jumpBack = false;
    public bool fireBall = false;
    public bool jumpForward = false;
    public bool downForward = false;
    public bool rest = false;
    public Vector3 resetPos;
    public GameObject myView;
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

    public bool JumpForward
    {
        get { return jumpForward; }
        set
        {
            jumpForward = value;
        }
    }
    public bool DownForward
    {
        get { return downForward; }
        set
        {
            downForward = value;
        }
    }

    public bool Rest
    {
        get { return rest; }
        set
        {
            rest = value;
        }
    }

    public void bossOver()
    {
        jumpBack = false;
        fireBall = false;
        jumpForward = false;
        downForward = false;
        rest = false;
    }
    public void bossStart()
    {
        myView.SetActive(false);
        jumpBack = true;
    }
}
