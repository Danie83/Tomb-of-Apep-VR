using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    InputActionProperty menuToggle;
    public int MenuToggle { get; set; }

    [SerializeField]
    bool isActive = true;

    [SerializeField]
    MenuBaseComponent currentPanel;

    GameObject menuObject = null;

    private void Awake()
    {
        menuObject = this.gameObject;
        menuObject.SetActive(isActive);
        HideAllMenus();
        currentPanel.ToggleActive(true);
        menuToggle.action.started += ToggleShowMenu;
    }

    private void HideAllMenus()
    {
        MenuBaseComponent[] components = menuObject.GetComponentsInChildren<MenuBaseComponent>();
        foreach (MenuBaseComponent component in components)
        {
            component.ToggleActive(false);
        }
    }

    private void ToggleShowMenu(InputAction.CallbackContext context)
    {
        isActive = !isActive;
        menuObject.SetActive(isActive);
    }


    public void SetCurrent(MenuBaseComponent newCurrent)
    {
        currentPanel.ToggleActive(false);
        currentPanel = newCurrent;
        currentPanel.ToggleActive(true);
    }
}
