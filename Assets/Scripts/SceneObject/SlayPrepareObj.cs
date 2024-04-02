using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayPrepareObj : MonoBehaviour
{
    public GameManagement gameManagement;
    public GameObject user;
    public int delayTime;
    public int time;
    public AudioSource nowSource;
    private int clipIndex = 0;
    /// <summary>
    /// »Ó»÷´ÎÊý
    /// </summary>
    public int times;
    /// <summary>
    /// ·½Ïò-×ó+ÓÒ
    /// </summary>
    public int direction;
    private void Start()
    {
        nowSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        time++;
        if (time% delayTime == 0)
        {
            if(time / delayTime <= times)
            {
                nowSource.clip = gameManagement.audioManager.slayAudioList[clipIndex];
                nowSource.playOnAwake = false;
                nowSource.Play();
                clipIndex++;
                if (clipIndex==4)
                {
                    clipIndex = 0;
                }
                Slay();
                transform.localRotation = Quaternion.Euler(0, 0, time / delayTime * 90);
                transform.localPosition += new Vector3(direction, 0, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// Â¾»÷
    /// </summary>
    public void Slay()
    {
        GameObject thunder = Instantiate(gameManagement.skillManagement.skillObjList[(int)SkillManagement.ProjectileName.Â¾»÷]);
        thunder.transform.position = transform.position;
        thunder.transform.localPosition -= new Vector3(0, 1.3f, 0);
        thunder.transform.rotation= transform.rotation;
        thunder.transform.SetParent(gameManagement.skillManagement.createRoot.transform);
        ProjectileObj thunderObj = thunder.GetComponent<ProjectileObj>();
        thunderObj.user = user;
        thunderObj.gameManagement = gameManagement;
        thunderObj.damage = 10;
        thunderObj.lifeTime = 20;
    }
}
