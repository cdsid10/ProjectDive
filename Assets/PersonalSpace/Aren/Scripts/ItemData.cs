using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class ItemData : MonoBehaviour
{
    [SerializeField] public static GameObject[] itemObjects;

    [SerializeField]
    public static Dictionary<string, Dictionary<string, string>> itemData = new()
    {
        {"Test_Item", new Dictionary<string, string>()
            {
                {"Index", "0"},
                {"Info", "Item used for testing!" },
                {"Type", "Item"}
            }
        },
        {"Key", new Dictionary<string, string>()
            {
                {"Index", "1"},
                {"Info", "A key, perhaps required to open a door." },
                {"Type", "Item"}
            }
        }
    };


    public static string GetItemInfo(string itemName, string infoRequest)
    {
        return itemData[itemName][infoRequest];
    }


    public static GameObject GetObjectFromItemName(string itemName)
    {
        return itemObjects[int.Parse(GetItemInfo(itemName, "Index"))];
    }


    [SerializeField] GameObject object_1;
    private void Awake()
    {
        Array.Resize(ref itemObjects, itemData.Count());
        itemObjects[1] = object_1;
    }
}
