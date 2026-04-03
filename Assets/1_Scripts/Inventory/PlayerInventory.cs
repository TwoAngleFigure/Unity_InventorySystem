using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public CanvasGroup UIView;

    public Dictionary<string, ItemData> ItemInventory = new();

    public Action<ItemData[]> InventoryChangedAction;
    public Action<bool> OnInventoryToggled;
    public bool inventoryToggleState = false;

    [Header("Input Action")]
    PlayerFieldControl _InventoryAction;
    InputAction _InventoryToggleAction;

    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _InventoryAction = new();
        _InventoryToggleAction = _InventoryAction.Player.Inventory;
        SetInventoryUIState(inventoryToggleState);
    }

    public void AddItem(ItemData newItem)
    {
        if (ItemInventory.TryGetValue(newItem.id, out ItemData oldItem))
        {
            oldItem.stack += newItem.stack;
        }
        else
        {
            ItemInventory.Add(newItem.id, newItem);
        }
        InventoryChangedAction?.Invoke(ItemInventory.Values.ToArray());
    }

    public bool RemoveItem(string id)
    {
        if (ItemInventory.TryGetValue(id, out ItemData oldItem))
        {
            oldItem.stack -= 1;
            if (oldItem.stack <= 0)
            {
                ItemInventory.Remove(id);
            }
            oldItem.Use(BasePlayer.Instance.gameObject);
            InventoryChangedAction?.Invoke(ItemInventory.Values.ToArray());
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        _InventoryAction.Enable();
        _InventoryToggleAction.performed += OnInventoryToggle;
    }

    private void OnDisable()
    {
        _InventoryToggleAction.performed -= OnInventoryToggle;
        _InventoryAction.Disable();
    }

    public void OnInventoryToggle(InputAction.CallbackContext callbackContext)
    {
        SetInventoryUIState(!inventoryToggleState);

        InventoryChangedAction?.Invoke(ItemInventory.Values.ToArray());
    }

    public void SetInventoryUIState(bool state)
    {
        if (state)
        {
            inventoryToggleState = true;
            UIView.alpha = 1;
            UIView.interactable = true;
        }
        else
        {
            inventoryToggleState = false;
            UIView.alpha = 0;
            UIView.interactable = false;
        }

        OnInventoryToggled?.Invoke(inventoryToggleState);
    }

    public ItemData GetItemData(string id)
    {
        if (ItemInventory.TryGetValue(id, out ItemData itemData))
        {
            return itemData;
        }
        return null;
    }
}