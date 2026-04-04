using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Item Slot")]
    public GameObject slotPrefab;
    private GridLayoutGroup slotParent;
    private List<ItemSlotUI> itemSlotUIs = new();

    [Header("Sorting")]
    public SortingType currentSortingType = SortingType.TimeAscending;
    public TMP_Dropdown sortDropdown;

    [Header("Item Select Page")]
    private ItemSelectPageUI itemSelectPage;

    public void Start()
    {
        PlayerInventory.Instance.InventoryChangedAction += UpdateUI;

        if (slotParent == null) slotParent = GetComponentInChildren<GridLayoutGroup>();

        if (itemSelectPage == null) itemSelectPage = GetComponentInChildren<ItemSelectPageUI>();
        itemSelectPage.SetButtonEvent(() => PlayerInventory.Instance.RemoveItem(itemSelectPage.GetCurrentItemData().id));

        if (sortDropdown == null) sortDropdown = GetComponentInChildren<TMP_Dropdown>();
        sortDropdown.value = (int)currentSortingType;
        sortDropdown.onValueChanged.AddListener(OnSortDropdownValueChanged);

    }

    public void UpdateUI(ItemData[] itemDatas)
    {
        itemDatas = SortItemDatas(itemDatas);

        //itemDatas 개수 만큼 SlotUI 활성화
        for (int i = 0; i < itemDatas.Length; i++)
        {
            if (i >= itemSlotUIs.Count)
            {
                itemSlotUIs.Add(CreateNewItemSlot());
            }

            ItemData currentItem = itemDatas[i]; //람다 클로저 변수 캡처 -> 없으면 IndexOutOfRangeException
            itemSlotUIs[i].ActiveSlot(currentItem, () => itemSelectPage.SetItemData(currentItem));
        }

        //남은 SlotUI 비활성화
        for (int i = itemDatas.Length; i < itemSlotUIs.Count; i++)
        {
            itemSlotUIs[i].DesableSlot();
        }

        itemSelectPage.IsSelectItem();
    }

    public ItemSlotUI CreateNewItemSlot()
    {
        ItemSlotUI newSlot = Instantiate(slotPrefab).GetComponent<ItemSlotUI>();
        newSlot.transform.SetParent(slotParent.gameObject.transform, false);
        return newSlot;
    }

    public void OnSortDropdownValueChanged(int index)
    {
        currentSortingType = (SortingType)index;
        if (PlayerInventory.Instance != null && PlayerInventory.Instance.ItemInventory != null)
        {
            UpdateUI(PlayerInventory.Instance.ItemInventory.Values.ToArray());
        }
    }

    private ItemData[] SortItemDatas(ItemData[] itemDatas)
    {
        switch (currentSortingType)
        {
            case SortingType.TimeAscending:
                return itemDatas;
            case SortingType.TimeDescending:
                return itemDatas.Reverse().ToArray();
            case SortingType.NameAscending:
                return itemDatas.OrderBy(x => x.itemName).ToArray();
            case SortingType.NameDescending:
                return itemDatas.OrderByDescending(x => x.itemName).ToArray();
            case SortingType.StackAscending:
                return itemDatas.OrderBy(x => x.stack).ToArray();
            case SortingType.StackDescending:
                return itemDatas.OrderByDescending(x => x.stack).ToArray();
            default:
                return itemDatas;
        }
    }
}

public enum SortingType
{
    TimeAscending,
    TimeDescending,
    NameAscending,
    NameDescending,
    StackAscending,
    StackDescending,
}