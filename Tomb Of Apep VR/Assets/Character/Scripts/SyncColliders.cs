using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncColliders : MonoBehaviour
{
    public CapsuleCollider _collider;
    public CharacterController controller;
    public Camera _camera;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        _collider.height = controller.height;
        _collider.center = controller.center;
    }
}
