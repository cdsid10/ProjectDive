using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemInvScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Image _image;
    [NonSerialized] public int currentGb;
    [NonSerialized] public string itemName = "Empty";
    [NonSerialized] public Transform parentAfterDrag;
    [NonSerialized] public Transform parentBeforeDrag;
    private float _imageWidth;
    [NonSerialized] public GameObject itemObj;
    [NonSerialized] public TextMeshProUGUI text;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _imageWidth = _image.GetComponent<RectTransform>().localScale.x;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        parentBeforeDrag = transform.parent;
        transform.SetParent(GetComponentInParent<Canvas>().transform);
        _image.raycastTarget = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag.childCount > 0)
        {
            parentAfterDrag.GetChild(0).SetParent(parentBeforeDrag);
        }
        transform.SetParent(parentAfterDrag);
        _image.raycastTarget = true;
    }


    public void OnDrop(PointerEventData eventData)
    {
        ItemInvScript pointerItemScript = eventData.pointerDrag.GetComponent<ItemInvScript>();

        // set replacer's parent to replacee's parent
        pointerItemScript.parentAfterDrag = transform.parent;

        // set replacee to replacer's parent before drag
        transform.SetParent(pointerItemScript.parentBeforeDrag);
    }
}
