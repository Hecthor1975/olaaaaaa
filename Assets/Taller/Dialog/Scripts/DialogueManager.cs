using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Links")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Dialog Settings")]
    public GameObject dialogUI;
    public float timePerLetter = 0.04f;
    public Dialogue myDialog;

    [Header("Use Target")]
    public Transform targetPosition;

    public static DialogueManager singleton;
    private Queue<string> sentences;
    Coroutine textDisplay = null;
    bool startDisplayingText = false;
    string lastDisplay = "";
    #region Singleton
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion

    void Start()
    {
        sentences = new Queue<string>();
        sentences.Clear();
    }

    public void Update()
    {
        if (targetPosition != null)
        {
            RePositionUI();
        }
    }

    public void RePositionUI()
    {
        dialogUI.transform.position = targetPosition.position;
    }

    public void UseSetDialog()
    {
        StartDialogue(myDialog);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogUI.SetActive(true);

        nameText.text = dialogue.name;
        dialogueText.text = "";
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (startDisplayingText == false)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();

            if (textDisplay != null) StopCoroutine(textDisplay);
            textDisplay = StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            if (textDisplay != null) StopCoroutine(textDisplay);
            dialogueText.text = lastDisplay;
            startDisplayingText = false;
        }
    }
    IEnumerator TypeSentence (string sentence)
    {
        lastDisplay = sentence;
        startDisplayingText = true;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timePerLetter);
        }
        startDisplayingText = false;
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
        sentences.Clear();
    }

    public void OnDestroy()
    {
        singleton = null;
    }
}
