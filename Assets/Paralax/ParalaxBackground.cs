using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 paralaxEffectMultiplier; 
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        // transform.position += new Vector3 (deltaMovement.x * paralaxEffectMultiplier.x);
        lastCameraPosition = cameraTransform.position;
    }
}
