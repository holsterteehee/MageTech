using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NPC : MonoBehaviour
{
    public GameObject DialoguePanel;

    public TMP_Text DialogueText;

    public string[] dialogue;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (DialoguePanel.activeInHierarchy)
            {
                zeroText();
            }

            else
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (DialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

     public void zeroText()
    {
        if (DialogueText != null)
        {
            DialogueText.text = "";
            index = 0;
        }

        if (DialoguePanel != null)
        {
            DialoguePanel.SetActive(false);
        }

        if (contButton != null)
        {
            contButton.SetActive(false);
        }
    }

     IEnumerator Typing()
    {
        if (DialogueText != null && dialogue != null && index < dialogue.Length)
        {
            foreach (char letter in dialogue[index].ToCharArray())
            {
                if (DialogueText != null)
                {
                    DialogueText.text += letter;
                    yield return new WaitForSeconds(wordSpeed);
                }
            }
        }
    }

 public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            DialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
