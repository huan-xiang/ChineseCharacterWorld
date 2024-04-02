using Cainos.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMonologue : MonoBehaviour
{
    public GameObject interactableObj;
    public GameObject interactableUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            this.GetComponent<NPC_Talk>().StartTalk();
            collision.GetComponent<PixelCharacterController>().canAbsorb = true;
            collision.GetComponent<PixelCharacterController>().canConbinate = true;
            Action();
        }
    }

    public void Action()
    {
        interactableObj.SetActive(false);
        interactableUI.SetActive(true);
    }
}
