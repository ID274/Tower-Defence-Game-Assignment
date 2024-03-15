using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public TextMeshProUGUI dialogueText;
    [SerializeField] private string[] tutorialDialogueContents;
    [SerializeField] private int currentTextID;
    [SerializeField] private string currentText;
    [SerializeField] private float delay = 0.03f;

    private bool onTutorial = true;
    private bool coroutineStarted = false;

    // Start is called before the first frame update
    
    
    
    void OnEnable()
    {
        if (!SettingsScript.main.tutorialsEnabled)
        {
            onTutorial = false;
            SkipDialogue();
            gameObject.SetActive(false);
        }
        if (onTutorial)
        {
            Debug.Log("Tutorial dialogue length:" + tutorialDialogueContents.Length);
            currentTextID = 0;
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        coroutineStarted = true;
        string fullText = tutorialDialogueContents[currentTextID];
        for (int i = 0; i < fullText.Length; i++)
        {
            if (coroutineStarted)
            {
                currentText += fullText[i]; // Append one letter at a time
                dialogueText.text = currentText;
                yield return new WaitForSecondsRealtime(delay);
                Debug.Log("Tutorial for loop " + i);
            }
        }
        coroutineStarted = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentTextID + 1 < tutorialDialogueContents.Length && !coroutineStarted)
        {
            Debug.Log("Pointer down");
            currentTextID++;
            dialogueText.text = "";
            currentText = "";
            StartCoroutine(ShowText());
        }
        else if (currentTextID + 1 >= tutorialDialogueContents.Length)
        {
            onTutorial = false;
            gameObject.SetActive(false);
        }
    }

    public void SkipDialogue()
    {
        if (onTutorial)
        {
            StopCoroutine(ShowText());
            coroutineStarted = false;
            currentTextID = 10;
            currentTextID = tutorialDialogueContents.Length;
            currentText = tutorialDialogueContents[currentTextID - 1];
            dialogueText.text = currentText;
        }
    }
}
