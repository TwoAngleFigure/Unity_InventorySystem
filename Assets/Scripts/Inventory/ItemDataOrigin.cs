using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "SC/Item Data")]
public class ItemDataOrigin : ScriptableObject
{
    public string id;
    public string itemName;
    public string description;

    public GameObject prefab;
    public Sprite icon;

    public EffectData[] effects;
}