using StarterAssets;
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
    [SerializeField] private RectTransform item;
    [NonSerialized] public int totalGb;
    private Transform backpack;
    private Transform hotbar;
    private int equippedGb;
    [SerializeField] private StarterAssetsInputs playerInputScript;


    private void Awake()
    {
        backpack = transform.Find("Backpack");
        hotbar = transform.Find("Hotbar");

        for (int i = 0; i < transform.childCount; i++)
        {
            inventory.Add("Empty");
        }

        AddItem("Test_Item", 3);
        AddItem("Test_Item", 19);
    }

    private void FixedUpdate()
    {
        // update inventory info
        int i = 0;
        foreach (Transform gridbox in transform.GetComponentsInChildren<Transform>()) 
        {
            if (gridbox.childCount > 0 && gridbox.name.Contains("GridBox"))
            {
                inventory[i] = gridbox.GetComponentInChildren<ItemInvScript>().itemName;
            }
            else
            {
                inventory[i] = "Empty";
            }
        }
    }


    public void AddItem(string itemName, int gb)
    {
        // search hotbar
        string gbName = "GridBox_Inv (" + gb.ToString() + ")";
        Transform toGb = hotbar.Find(gbName);
        if (toGb != null)
        {
            // else search backpack
            backpack.Find(gbName);
            if (toGb != null)
            {
                // add
                RectTransform newItem = Instantiate(item, toGb.position, Quaternion.identity, toGb);
                newItem.anchoredPosition = Vector2.zero;
                newItem.GetComponent<ItemInvScript>().itemName = itemName;
            }
        }
    }


    private void EquipTool()
    {
        string toolName = hotbar.Find("GridBox_Inv (" + equippedGb.ToString() + ")").name;
        GameObject toolObj = ItemData.GetObjectFromItemName(toolName);
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            string inputString = Input.inputString;

            if (int.TryParse(inputString, out equippedGb)) { EquipTool(); }
            if (inputString == "`")
            {
                bool bpActive = backpack.gameObject.activeSelf;
                backpack.gameObject.SetActive(!bpActive);
                playerInputScript.cursorLocked = bpActive;
            }
        }
    }
}
