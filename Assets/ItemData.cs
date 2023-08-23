using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class ItemData : MonoBehaviour
{
    [SerializeField] public static List<Sprite> itemSprites = new();
    [SerializeField] public static Dictionary<string, Dictionary<string, string>> itemData = new()
    {
        {"Test_Item", new Dictionary<string, string>()
            {
                {"SpriteNumber", "0"},
                {"Info", "Item used for testing!" }
            } 
        }
    };


    public static string GetItemInfo(string itemName, string infoRequest)
    {
        return itemData[itemName][infoRequest];
    }


    [SerializeField] Sprite sprite_0;
    private void Awake()
    {
        // manual insert
        itemSprites.Add(sprite_0); 
    }
}
