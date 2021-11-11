using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    InputActionProperty menuToggle;
    public int MenuToggle { get; set; }

    GameObject menuObject = null;

    [SerializeField]
    bool isActive = true;

    [SerializeField]
    MenuBaseComponent currentPanel;

    private void Start()
    {
        currentPanel.ToggleActive(true);
        menuObject = this.gameObject;
        menuToggle.action.started += ToggleShowMenu;
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
