using UnityEngine;
using UnityEngine.EventSystems;

public class EyeFollow : MonoBehaviour
{
    private RectTransform pupilRectTransform;
    private RectTransform parentRect;
    private Vector2 initialPosition;
    public float maxDistance = 30f; // Batas radius dalam pixel
    public Canvas canvas; // Drag Canvas-mu ke sini di inspector

    void Start()
    {
        pupilRectTransform = GetComponent<RectTransform>();
        parentRect = pupilRectTransform.parent as RectTransform;
        initialPosition = pupilRectTransform.anchoredPosition;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 localPoint;
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        // Konversi posisi mouse ke local space Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            mousePos,
            cam,
            out localPoint
        );

        // Hitung arah dari posisi awal ke kursor
        Vector2 direction = localPoint - initialPosition;

        // Clamp jarak maksimal
        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        // Set posisi pupil relatif ke awal
        pupilRectTransform.anchoredPosition = Vector2.Lerp(
        pupilRectTransform.anchoredPosition,
        initialPosition + direction,
        Time.deltaTime * 10f // Bisa disesuaikan speed-nya
        );

    }
}