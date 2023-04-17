using UnityEngine;
using Cinemachine;

public class CameraDTO
{
    public int state { get; set; }
    public CinemachineVirtualCamera virtualCamera { get; set; }
    public Animator cameraMove { get; set; }
}
