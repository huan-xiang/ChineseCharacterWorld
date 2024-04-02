using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : PlayerObject
{
    public Vector3 leftBorder;
    public Vector3 rightBorder;
    public GameObject enemy;
    public float maxAttackDistance;
    public GameObject sawWall;
    public bool isGround = true;
    public GameObject myAirWall;
    private void Start()
    {
        pixelCharacter = GetComponent<PixelCharacter>();
        gameManagement = (GameManagement)FindObjectOfType(typeof(GameManagement));
        myAudioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");
        InjuredTwinkle();
        if(enemy!=null)
        {
            player_enemy = enemy;
        }
    }
}
