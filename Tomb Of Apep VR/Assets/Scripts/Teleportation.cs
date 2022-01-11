using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
