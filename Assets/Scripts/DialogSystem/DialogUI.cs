using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI 
{
    public string name;
    public string sentence;

    public DialogUI(string actor,string sentence)
    {
        name = actor;
        this.sentence = sentence;
    }
}
