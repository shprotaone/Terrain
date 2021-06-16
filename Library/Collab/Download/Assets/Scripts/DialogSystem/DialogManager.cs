using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;

    Queue<string> sentences,sentences2;

    void Start()
    {
        sentences = new Queue<string>();
        sentences2 = new Queue<string>();
    }
    /// <summary>
    /// Записывает предложения в очередь
    /// </summary>
    /// <param name="dialog1"></param>
    public void StartDialogue(Dialog dialog1)
    {

        nameText.text = dialog1.name1;

        sentences.Clear();

        foreach(string sentence in dialog1.sentences1)
        {
            sentences.Enqueue(sentence);
        }



        DisplayNextSentence();
    }
    /// <summary>
    /// Отображает следующее предложение.
    /// </summary>
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
        Debug.Log(sentence);
    }

    void EndDialogue()
    {
        Debug.Log("End Conversation");
    }
}
