using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverManager : MonoBehaviour
{
    public LeverControll[] levers;

    private bool isActivatedAll = false;

    public UnityEvent onAllActivated;

    public UnityEvent onDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        levers = GetComponentsInChildren<LeverControll>();
    }

    private bool IsActivatedAll()
    {
        foreach (LeverControll lever in levers)
        {
            if(!lever.IsActivated)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        bool tmp_isActivatedAll = IsActivatedAll();
        if (tmp_isActivatedAll && !isActivatedAll)
        {
            isActivatedAll = true;
            onAllActivated.Invoke();
        } else if (!tmp_isActivatedAll)
        {
            isActivatedAll = false;
            onDeactivate.Invoke();
        }

    }
}
