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
        主城,
        桃花林,
        竹林,
        树林,
        Boss战
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
        if(bgm==bgmKind.主城)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.spawnBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if(bgm == bgmKind.桃花林)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.punchBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if (bgm == bgmKind.竹林)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.bambooBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if (bgm == bgmKind.树林)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.forestBGM;
            mainCamera.GetComponent<AudioSource>().Play();
        }
        else if (bgm == bgmKind.Boss战)
        {
            mainCamera.GetComponent<AudioSource>().clip = gameManagement.audioManager.bossFightAudio;
            mainCamera.GetComponent<AudioSource>().Play();
        }
    }

}
