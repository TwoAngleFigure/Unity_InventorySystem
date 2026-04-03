using UnityEngine;

public class BaseFieldItem : MonoBehaviour
{
    public ItemDataOrigin ItemDataTemplate;

    public ItemData ItemData;
    public LayerMask targetMask;

    public void Awake()
    {
        ItemData = new(
            ItemDataTemplate.id,
            ItemDataTemplate.itemName,
            ItemDataTemplate.description,
            1,
            ItemDataTemplate.icon,
            ItemDataTemplate.effects
            );
    }

    public void OnTriggerEnter(Collider other)
    {
        if (IsInTargetLayer(other.gameObject))
        {
            PlayerInventory.Instance.AddItem(ItemData);
            Destroy(gameObject);
        }
    }

    protected bool IsInTargetLayer(GameObject obj)
    {
        return ((1 << obj.layer) & targetMask) != 0;
    }
}