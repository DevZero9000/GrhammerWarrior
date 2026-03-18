using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    public Transform playerTransform;
    public float smoothTime = 0.15f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Zoom")]
    public bool orthographic = true;
    public float targetOrthoSize = 5f;   // for 2D orthographic zoom-in
    public float orthoZoomSpeed = 5f;

    public float targetFOV = 40f;        // for 3D perspective zoom-in
    public float fovZoomSpeed = 5f;

    Vector3 velocity = Vector3.zero;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>() ?? Camera.main;
    }

    void LateUpdate()
    {
        if (playerTransform == null) return;

        // Smooth follow (preserves z from offset)
        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // Smooth zoom
        if (cam != null)
        {
            if (orthographic && cam.orthographic)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, Time.deltaTime * orthoZoomSpeed);
            }
            else if (!orthographic && !cam.orthographic)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovZoomSpeed);
            }
        }
    }
}
