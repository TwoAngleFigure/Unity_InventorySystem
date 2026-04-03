using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public string itemId;
    public Image icon;
    public TMP_Text stackText;
    public Button button;

    public void Awake()
    {
        if (icon == null) icon = GetComponentInChildren<Image>();
        if (stackText == null) stackText = GetComponentInChildren<TextMeshPro>();
        if (button == null) button = GetComponentInChildren<Button>();
    }

    public void SetData(string newItemId, Sprite newIcon, string newStack)
    {
        itemId = newItemId;
        icon.sprite = newIcon;
        stackText.text = newStack;
    }
}
