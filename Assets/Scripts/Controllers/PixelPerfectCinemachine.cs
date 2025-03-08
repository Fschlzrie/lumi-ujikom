using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.U2D;

public class PixelPerfectCinemachine : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private PixelPerfectCamera pixelPerfect;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        pixelPerfect = Camera.main.GetComponent<PixelPerfectCamera>();
    }

    void LateUpdate()
    {
        if (cam != null && pixelPerfect != null)
        {
            Vector3 camPos = cam.transform.position;
            float unitsPerPixel = 1f / pixelPerfect.assetsPPU;

            camPos.x = Mathf.Round(camPos.x / unitsPerPixel) * unitsPerPixel;
            camPos.y = Mathf.Round(camPos.y / unitsPerPixel) * unitsPerPixel;

            cam.transform.position = camPos;
        }
    }
}
