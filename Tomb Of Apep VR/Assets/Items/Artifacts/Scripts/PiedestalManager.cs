using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PiedestalManager : MonoBehaviour
{
    public PiedestalActivatorArea[] piedestals;

    private bool isActivatedAll = false;

    public UnityEvent onAllActivated;

    public UnityEvent onDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        piedestals = GetComponentsInChildren<PiedestalActivatorArea>();
    }

    private bool IsActivatedAll()
    {
        foreach (PiedestalActivatorArea p in piedestals)
        {
            if (!p.isActive)
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
        }
        else if (!tmp_isActivatedAll)
        {
            isActivatedAll = false;
            onDeactivate.Invoke();
        }

    }
}
