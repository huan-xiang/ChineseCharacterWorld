using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monologue : MonoBehaviour
{
    public GameManagement gameManagement;
    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            this.GetComponent<NPC_Talk>().StartTalk();
            for (int i = 0; i < gameManagement.bossManager.bossLIist.Count; i++)
            {
                gameManagement.bossManager.bossLIist[i].SetActive(true);
            }
            gameManagement.NPC.SetActive(true);

        }
    }
}
