using UnityEngine;
using Cinemachine;

public class CameraOffsetA : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    private Vector3 defaultOffset;
    private CinemachineFramingTransposer framingTransposer;

    void Start()
    {
        if (virtualCam != null)
        {
            framingTransposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (framingTransposer != null)
            {
                defaultOffset = framingTransposer.m_TrackedObjectOffset;
            }
            else
            {
                Debug.LogWarning("Framing Transposer tidak ditemukan di virtualCam.");
            }
        }
        else
        {
            Debug.LogWarning("Cinemachine Virtual Camera belum di-assign.");
        }
    }

    public void LookUpOffset(float yOffset)
    {
        if (framingTransposer != null)
        {
            Vector3 newOffset = defaultOffset + new Vector3(0, yOffset, 0);
            framingTransposer.m_TrackedObjectOffset = newOffset;
        }
    }

    public void ResetOffset()
    {
        if (framingTransposer != null)
        {
            framingTransposer.m_TrackedObjectOffset = defaultOffset;
        }
    }
}
