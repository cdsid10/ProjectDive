using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [NonSerialized] public List<string> inventory = new List<string>();
    [SerializeField] private RectTransform gb;
    [SerializeField] private RectTransform item;
    private RectTransform rt;
    private float gbWidth;
    [SerializeField] private float spacing;
    public int totalGb;


    private void Awake()
    {
        rt = GetComponent<RectTransform>();

        //get gridbox measurements
        RectTransform gbRt = Instantiate(gb, transform);
        gbWidth = gbRt.rect.width * gbRt.lossyScale.x;
        Destroy(gbRt.gameObject);
    }


    private void Start()
    {
        LoadGrid();
        inventory[3] = "Test_Item";
        inventory[30] = "Test_Item";
    }


    private void FixedUpdate()
    {
        UpdateInventory();
    }


    private void LoadGrid()
    {
        int rows = Mathf.FloorToInt(rt.rect.height / (gbWidth + spacing));
        int cols = Mathf.FloorToInt(rt.rect.width / (gbWidth + spacing));
        totalGb = rows * cols;

        for (int row = 0; row < rows; row++)
        {
            float y = (gbWidth + spacing) * -row;
            for (int col = 0; col < cols; col++)
            {
                // create gb
                float x = (gbWidth + spacing) * col;
                RectTransform newGb = Instantiate(gb, transform);
                newGb.anchoredPosition = new Vector2(x, y);

                // set gb name
                int number = (row * cols) + col + 1;
                newGb.name = number.ToString();

                // add gb info to list
                inventory.Add("Empty");
            }
        }
    }


    public Transform GetGbFromNumber(int number)
    {
        int newNumber = number + 1;
        return transform.Find(newNumber.ToString());
    }

    public void ClearGbItems(Transform gb)
    {
        Destroy(gb.GetChild(0).gameObject);
    }


    private void AddItemUI(string itemName, int gbNumber)
    {
        Transform toGb = transform.Find(gbNumber.ToString());
        RectTransform newItemUI = Instantiate(item, Vector2.zero, Quaternion.identity, toGb);
        newItemUI.anchoredPosition = Vector2.zero; 

        // set values
        newItemUI.GetComponent<ItemInvScript>().itemName = itemName;
    }


    private void UpdateInventory()
    {
        for (int gbNumber = 0; gbNumber < totalGb; gbNumber++)
        {
            Transform gb = GetGbFromNumber(gbNumber);
            if (inventory[gbNumber] != "Empty")
            {
                AddItemUI(inventory[gbNumber], gbNumber);
            }
            else if (gb.childCount > 0)
            {
                ClearGbItems(gb);
            }
        }
    }
}
