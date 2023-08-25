using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [NonSerialized] public List<string> inventory = new List<string>();
    [SerializeField] private RectTransform _item;
    [NonSerialized] public int totalGb;
    private Transform _backpack;
    private Transform _hotbar;
    private int _equippedGb;
    [SerializeField] private StarterAssetsInputs _playerInputScript;
    [SerializeField] private Transform _grip;
    [SerializeField] private KeyCode _invKey;


    private void Awake()
    {
        _backpack = transform.Find("Backpack");
        _hotbar = transform.Find("Hotbar");

        for (int _ = 0; _ < transform.childCount; _++)
        {
            inventory.Add("Empty");
        }

        AddItem("Key", 3);
        AddItem("Test_Item", 19);
    }

    private void FixedUpdate()
    {
        // update inventory info
        int _ = 0;
        foreach (Transform gridbox in transform.GetComponentsInChildren<Transform>()) 
        {
            if (gridbox.childCount > 0 && gridbox.name.Contains("GridBox"))
            {
                inventory[_] = gridbox.GetComponentInChildren<ItemInvScript>().itemName;
            }
            else
            {
                inventory[_] = "Empty";
            }
        }
    }


    public void AddItem(string itemName, int gb)
    {
        // search hotbar
        string _gbName = "GridBox_Inv (" + gb.ToString() + ")";
        Transform toGb = _hotbar.Find(_gbName);
        if (toGb == null)
        {
            // else search backpack
            toGb = _backpack.Find(_gbName);
            if (toGb == null)
            {
                return;
            }
        }

        // add
        Debug.Log(toGb.name);
        RectTransform _newItem = Instantiate(_item, toGb.position, Quaternion.identity, toGb);
        _newItem.anchoredPosition = Vector2.zero;
        _newItem.GetComponent<ItemInvScript>().itemName = itemName;
    }


    private void EquipTool()
    {
        Transform _gb = _hotbar.Find("GridBox_Inv (" + _equippedGb.ToString() + ")");
        string _toolName = "";

        if (_gb.childCount > 0)
        {
            _toolName = _gb.GetChild(0).GetComponent<ItemInvScript>().itemName;
        }
        else
        {
            _toolName = "Empty";
        }

        if (_grip.childCount > 0)
        {
            foreach (Transform _childTool in _grip.GetComponentsInChildren<Transform>())
            {
                if (_childTool.name == _toolName)
                {
                    _childTool.gameObject.SetActive(true);
                    return;
                }
                else if (_childTool != _grip)
                {
                    _childTool.gameObject.SetActive(false);
                }
            }
        }

        if (_toolName != "Empty")
        {
            GameObject _toolObj = ItemData.GetObjectFromItemName(_toolName);
            if (_toolObj != null)
            {
                Transform _tool = Instantiate(_toolObj.transform, _grip);
                _tool.name = _tool.name.Replace("(Clone)", "");
                _tool.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
        }
    }


    private void Update()
    {
        // check for hotbar input
        string inputString = Input.inputString;
        if (int.TryParse(inputString, out _equippedGb)) { EquipTool(); }

        // check for inv key input
        bool _invKeyDown = Input.GetKey(_invKey);
        _backpack.gameObject.SetActive(_invKeyDown);
        _playerInputScript.cursorLocked = !_invKeyDown;
    }
}
