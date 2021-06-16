using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour
{
    public Text textName;
    public Toggle toggle;

    public Task task;

    private void Awake()
    {
        if (task != null) ConstructTask(task);
    }
    void Update()
    {
        if (task != null) ConstructTask(task);
    }
    public void ConstructTask(Task task)
    {
        this.task = task;
        if (textName != null)
            textName.text = task.displayName;
        if (toggle != null)
            toggle.isOn = task.isActive;
    }
}
