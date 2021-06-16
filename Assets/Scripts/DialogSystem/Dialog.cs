using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public Message[] messages;    
}
[System.Serializable]
public class Message
{
    public string name;     //конструктор

    [TextArea(3, 10)]
    public string sentences;
}

