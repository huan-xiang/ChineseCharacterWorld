using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2UnitControl : MonoBehaviour
{
    private bool thunder = false;
    private bool rest = false;
    public Vector3 resetPos;
    public GameObject myView;
    public bool Thunder
    {
        get { return thunder; }
        set
        {
            thunder = value;
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
        thunder = false;
        rest = false;
}
    public void bossStart()
    {
        myView.SetActive(false);
        thunder = true;
    }
}
