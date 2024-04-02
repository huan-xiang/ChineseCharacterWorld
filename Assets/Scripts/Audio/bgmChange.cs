using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmChange : MonoBehaviour
{
    public GameObject mainCamera;
    public GameManagement gameManagement;
    public bgmKind bgm1;
    public enum bgmKind
    {
        ����,
        �һ���,
        ����,
        ����,
        Bossս
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBGM(bgmKind bgm)
    {
        if(bgm==bgmKind.����)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.spawnBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if(bgm == bgmKind.�һ���)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.punchBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if (bgm == bgmKind.����)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.bambooBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if (bgm == bgmKind.����)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.forestBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if (bgm == bgmKind.Bossս)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.bossFightAudio;
            mainCamera.GetComponent<AudioSource>().Play();
        }
    }

}
