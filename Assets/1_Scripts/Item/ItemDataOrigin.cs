using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "SO/Item Data")]
public class ItemDataOrigin : ScriptableObject
{
    public string id;
    public string itemName;
    public string description;
    public ItemType itemType;

    public GameObject prefab;
    public Sprite icon;

    public EffectData[] effects;
}

public enum ItemType
{
    Default,
    UseAble
}