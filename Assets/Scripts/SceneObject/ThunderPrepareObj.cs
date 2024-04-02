using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPrepareObj : MonoBehaviour
{
    public GameManagement gameManagement;
    public GameObject user;
    public int delayTime;
    private int time;
    private AudioSource nowAudioSource;
    void Update()
    {
        if(time < delayTime)
        {
            time++;
        }
        else
        {
            Thunder();
        }
    }
    public void Thunder()
    {
        GameObject thunder = Instantiate(gameManagement.skillManagement.skillObjList[(int)SkillManagement.ProjectileName.À×»÷]);
        nowAudioSource = thunder.GetComponent<AudioSource>();
        nowAudioSource.clip = gameManagement.audioManager.thunderAudio;
        nowAudioSource.Play();
        thunder.transform.position = transform.position;
        thunder.transform.localPosition -= new Vector3(0,1.3f,0);
        thunder.transform.SetParent(gameManagement.skillManagement.createRoot.transform);
        ProjectileObj thunderObj = thunder.GetComponent<ProjectileObj>();
        thunderObj.user = user;
        thunderObj.gameManagement = gameManagement;
        thunderObj.damage = 5;
        thunderObj.lifeTime = 300;
        Destroy(gameObject);
    }
}
