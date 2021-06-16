using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    public CharacterPlay inventory;
    public GameObject item;
    public Image image;
    public Text itemName;

    /// <summary>
    /// Обнуление информации о ячейке, !пока не использую!
    /// </summary>
    public void RemoveCell()
    {
        item = null;
        image = null;
        itemName.text = "";
    }
}
