using UnityEngine;

public class LevelSelectorAttribute : PropertyAttribute
{
    public bool allowUntagged;
    public LevelSelectorAttribute(bool allowUntagged)
    {
        this.allowUntagged = allowUntagged;
    }
}