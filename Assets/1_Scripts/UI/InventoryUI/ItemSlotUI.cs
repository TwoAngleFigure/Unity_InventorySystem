using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text stackText;
    public Button button;
    public CanvasGroup canvasGroup;

    public void Awake()
    {
        if (icon == null) icon = GetComponentInChildren<Image>();
        if (stackText == null) stackText = GetComponentInChildren<TextMeshPro>();
        if (button == null) button = GetComponentInChildren<Button>();
        if (canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void ActiveSlot(ItemData itemData, UnityAction call)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        icon.sprite = itemData.icon;
        stackText.text = itemData.stack.ToString();
        button.onClick.AddListener(call);
    }

    public void DesableSlot()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        button.onClick.RemoveAllListeners();
    }
}
