using UnityEngine;

namespace Assets.MainMenu.Scripts
{
    class ExitMenu: MenuBaseComponent
    {
        public void OnExit()
        {
            Debug.Log("Application Exit");
            Application.Quit();
        }
    }
}
