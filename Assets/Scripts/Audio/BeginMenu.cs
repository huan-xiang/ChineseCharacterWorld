using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class BeginMenu : MonoBehaviour
{
    public VideoPlayer movTexture;
    public AudioSource movAudio;
    public Text startText;
    private void Start()
    {
        movTexture.isLooping = false;
        movTexture.Play();
        movAudio.loop = false;
        Invoke("PlayMovAudio", 0.3f);
        Invoke("PlayMovAudio", 2f);
        Invoke("StartText", 10);
    }
    private bool isDisplaying;
    private bool ready;
    private void FixedUpdate()
    {
        if (!ready) return;
        if(startText.color.a < 1 && isDisplaying)
        {
            startText.color += new Color(0, 0, 0, 0.05f);
        }
        else if(startText.color.a > 0 && !isDisplaying)
        {
            startText.color -= new Color(0, 0, 0, 0.05f);
        }
        else
        {
            isDisplaying = !isDisplaying;
        }
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameMain");
        }
    }
    void PlayMovAudio()
    {
        movAudio.Play();
    }
    void StartText()
    {
        startText.gameObject.SetActive(true);
        ready = true;
    }
}
