using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectPage : MonoBehaviour
{
    public GameObject itemIconObject;
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text itemStack;
    public TMP_Text itemDescription;
    public Button useButton;
    public CanvasGroup canvasGroup;
    public string currentItemId;

    public void Awake()
    {
        if (itemIcon == null) itemIcon = itemIconObject.GetComponent<Image>();
        if (useButton == null) useButton = GetComponentInChildren<Button>();
        useButton.onClick.AddListener(UseItem);
    }

    public void SetSelectPage(string itemId)
    {
        currentItemId = itemId;
        ItemData data = PlayerInventory.Instance.GetItemData(currentItemId);
        if (data == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        itemIcon.sprite = data.icon;
        itemName.text = data.itemName;
        itemStack.text = data.stack.ToString();
        itemDescription.text = data.description;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void UseItem()
    {
        PlayerInventory.Instance.RemoveItem(currentItemId);
    }
}
