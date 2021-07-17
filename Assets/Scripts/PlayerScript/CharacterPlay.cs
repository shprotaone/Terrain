using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlay : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> items;
    public List<DisplayItem> displayCells;

    public GameObject itemInHand;
    public GameObject inventoryContent; 
    public GameObject taskDesk;

    private int weapon;
    private GameObject currentItem;

    private void Update()
    {      
        ChangeWeapon();
        DisplayItem();
        CallDesk();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            currentItem = other.transform.gameObject;
            if(currentItem.transform.parent != itemInHand.gameObject.transform)
            {
                TakeItem(currentItem.GetComponent<Item>());
                currentItem.transform.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Берем предмет
    /// </summary>
    /// <param name="item"></param>
    private void TakeItem(Item item)
    {
        if (item)
        {
            items.Add(item.gameObject);
            item.transform.SetParent(itemInHand.transform);
            item.transform.localPosition = item.pickPosition;
            item.transform.localEulerAngles = item.pickRotation;
        }
    }
    /// <summary>
    /// Меняем оружие
    /// </summary>
    private void ChangeWeapon()
    {

            float currentWeapon = Input.GetAxis("Mouse ScrollWheel");

            foreach (GameObject activeItem in items)
            {
                if (activeItem.activeInHierarchy)
                {
                    weapon = items.IndexOf(activeItem);
                }
            }

        if(items.Count != 0)
        {
            items[weapon].SetActive(false);

            if (currentWeapon > 0f)
            {
                if (weapon >= items.Count - 1)
                    weapon = 0;
                else weapon++;
            }

            if (currentWeapon < 0f)
            {
                if (weapon <= 0)
                    weapon = items.Count - 1;
                else weapon--;
            }

            items[weapon].SetActive(true);


        }
            
        
        
    }
    /// <summary>
    /// Отображение инвентаря
    /// </summary>
    private void DisplayItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            displayCells[i].item = items[i];
            displayCells[i].image.sprite = Resources.Load<Sprite>(items[i].GetComponent<Item>().pathSprite);
            displayCells[i].itemName.text = items[i].GetComponent<Item>().itemName;

            if (i == weapon)
            {
                displayCells[i].image.color = Color.yellow;
            }
            else displayCells[i].image.color = Color.white;
        }      
    }
    /// <summary>
    /// Вызов доски заданий
    /// </summary>
    private void CallDesk()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            taskDesk.SetActive(true);
        }
        else taskDesk.SetActive(false);
    }

}
