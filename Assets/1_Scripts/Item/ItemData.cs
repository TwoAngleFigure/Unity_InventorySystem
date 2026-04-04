using UnityEngine;

public class ItemData
{
    public string id;
    public string itemName;
    public string description;
    public int stack;
    public ItemType type;

    public Sprite icon;
    public EffectData[] effects;

    public ItemData(string id, string itemName, string description, int stack, ItemType type, Sprite icon, EffectData[] effects)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.stack = stack;
        this.type = type;
        this.icon = icon;
        this.effects = effects;
    }

    public void Use(GameObject target)
    {
        if (effects == null) return;
        foreach (var data in effects)
        {
            var effect = EffectMapper.GetEffect(data);
            if (effect != null)
            {
                effect.Effect(target);
            }
        }
    }
}