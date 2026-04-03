using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public GameObject slotPrefab;
    public List<ItemSlotUI> itemSlotUIs;

    public ItemSelectPage selectPage;

    public void Start()
    {
        PlayerInventory.Instance.InventoryChangedAction += UpdateUI;
        if (selectPage == null) selectPage = GetComponentInParent<ItemSelectPage>();
        if (gridLayout == null) gridLayout = GetComponentInChildren<GridLayoutGroup>();
    }

    public void UpdateUI(ItemData[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            string itemId = list[i].id;
            if (i >= itemSlotUIs.Count)
            {
                itemSlotUIs.Add(CreateNewItemSlot(itemId, list[i].icon, list[i].stack.ToString()));
            }

            ItemSlotUI slot = itemSlotUIs[i];
            slot.gameObject.SetActive(true);
            slot.SetData(itemId, list[i].icon, list[i].stack.ToString());
            slot.button.onClick.RemoveAllListeners();
            slot.button.onClick.AddListener(() => selectPage.SetSelectPage(itemId));
        }

        for (int i = list.Length; i < itemSlotUIs.Count; i++)
        {
            itemSlotUIs[i].gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(selectPage.currentItemId))
        {
            selectPage.SetSelectPage(selectPage.currentItemId);
        }
    }

    public ItemSlotUI CreateNewItemSlot(string itemId, Sprite newIcon, string newStack)
    {
        ItemSlotUI newSlot = Instantiate(slotPrefab).GetComponent<ItemSlotUI>();
        newSlot.transform.SetParent(gridLayout.gameObject.transform, false);
        newSlot.SetData(itemId, newIcon, newStack);
        return newSlot;
    }
}