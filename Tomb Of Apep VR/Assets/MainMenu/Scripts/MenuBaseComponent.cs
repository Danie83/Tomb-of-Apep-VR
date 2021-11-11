using UnityEngine;

public abstract class MenuBaseComponent : MonoBehaviour
{
    public bool IsActive { get; set; } = false;

    public void ToggleActive(bool? forceActive = null)
    {
        IsActive = (forceActive != null)? (bool)forceActive : !IsActive;
        this.gameObject.SetActive(IsActive);
    }
}
