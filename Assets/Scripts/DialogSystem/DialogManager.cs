using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;

    Queue<string> sentences,names;

    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();      
    }
    /// <summary>
    /// Записывает имя и предложения в очередь
    /// </summary>
    /// <param name="dialog"></param>
    public void StartDialog(Dialog dialog)
    {       
        sentences.Clear();
        names.Clear();

        for(int i = 0; i < dialog.messages.Length; i++)
        {
            names.Enqueue(dialog.messages[i].name);
            sentences.Enqueue(dialog.messages[i].sentences);
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
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();

        nameText.text = name;
        dialogText.text = sentence;
    }
    void EndDialog()
    {
        Debug.Log("End Conversation");
    }
}
