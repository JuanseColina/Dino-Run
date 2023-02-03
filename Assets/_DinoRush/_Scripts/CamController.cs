using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController instance;
    [SerializeField] private CinemachineVirtualCamera[] cameras;


    private void Awake()
    {
        instance = this;
    }

    private void ResetCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = 0;
        }
    }

    public void ChangeCam(int n)
    {
        ResetCameras();
        cameras[n].Priority = 10;
    }
}
