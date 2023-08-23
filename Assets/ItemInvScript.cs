using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInvScript : MonoBehaviour
{
    public int currentGb;
    [NonSerialized] public string itemName = "Empty";
    private bool mouseDown = false;

    private void FixedUpdate()
    {
        mouseDown = Input.GetMouseButtonDown(0);

        // update sprite
        Sprite itemSprite = ItemData.itemSprites[int.Parse(ItemData.GetItemInfo(itemName, "SpriteNumber"))];

        if (GetComponent<Image>().sprite != itemSprite)
        {
            GetComponent<Image>().sprite = itemSprite;
        }
    }


    public IEnumerator OnSelect()
    {
        Debug.Log("selected");
        int fromGb = currentGb;
        int toGb;

        while (mouseDown)
        {
            transform.position = Input.mousePosition;
            yield return null;
        }

        //place item ui on closest grid on release
        GameObject closestGb = null;
        float closestDistance = 10000.0f;

        InventoryScript invScript = transform.GetComponentInParent<InventoryScript>();
        for (int gb = 0; gb < invScript.totalGb; gb++)
        {
            Transform gridT = invScript.GetGbFromNumber(gb);
            float dist = Vector3.Distance(transform.position, gridT.position);
            if (dist < closestDistance)
            {
                closestGb = gridT.gameObject;
                closestDistance = dist;
            }
        }
        if (closestGb != null)
        {
            transform.position = closestGb.transform.position;
            toGb = int.Parse(closestGb.name);

            invScript.inventory[fromGb] = invScript.inventory[toGb];
            invScript.inventory[toGb] = invScript.inventory[fromGb];
        }
    }
}
