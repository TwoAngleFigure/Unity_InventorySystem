using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemSelectPageUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text itemStack;
    public TMP_Text itemDescription;
    public Button useButton;

    public ItemData currentItemData;

    public ItemData GetCurrentItemData() { return currentItemData; }

    public void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>();
        if (useButton == null) useButton = GetComponentInChildren<Button>();
    }

    public void ActiveSelectPage()
    {
        if (currentItemData == null) return;
        if (currentItemData.stack <= 0)
        {
            DisableSelectPage();
            return;
        }
        if(currentItemData.type == ItemType.UseAble) useButton.gameObject.SetActive(true);
        else useButton.gameObject.SetActive(false);

        itemIcon.sprite = currentItemData.icon;
        itemName.text = currentItemData.itemName;
        itemStack.text = currentItemData.stack.ToString();
        itemDescription.text = currentItemData.description;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void DisableSelectPage()
    {
        currentItemData = null;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void SetButtonEvent(UnityAction call)
    {
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(call);
    }

    public void SetItemData(ItemData itemData)
    {
        currentItemData = itemData;
        IsSelectItem();
    }

    public bool IsSelectItem()
    {
        if (currentItemData == null) { DisableSelectPage(); return false; }
        else { ActiveSelectPage(); return true; }
    }
}
