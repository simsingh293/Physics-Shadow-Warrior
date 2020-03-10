using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;
    [SerializeField] Transform cameraPivot = null;

    private void Awake()
    {
        cameraPivot = this.transform;
        mainCamera.transform.position = cameraPivot.position;
    }

    private void Update()
    {
        mainCamera.transform.position = cameraPivot.position;
        mainCamera.transform.rotation = cameraPivot.rotation;
    }

    private void FixedUpdate()
    {
        
    }
}
