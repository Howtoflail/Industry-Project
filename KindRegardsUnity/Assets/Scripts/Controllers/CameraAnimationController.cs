using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAnimationController : MonoBehaviour
{
    private CameraDTO cameraDTO;
    CameraController cameraController;
    [SerializeField] Canvas canvas;

    private void Start()
    {
        cameraDTO = new CameraDTO();
        cameraController = GetComponent<CameraController>();
        cameraDTO.state = 0;
        cameraDTO.virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraDTO.cameraMove = cameraDTO.virtualCamera.GetComponent<Animator>();
    }

    public IEnumerator ChangePosition(int newState)
    {
        if (cameraDTO.state != newState)
        {
            if (cameraDTO.state != 0)
            {
                //All the camera animations back to the animal
                switch (cameraDTO.state)
                {
                    case 2:
                        //Starts the animation
                        cameraDTO.cameraMove.SetBool("SettingsIn", true);
                        Hide();
                        //waits for the animation to finish
                        yield return new WaitForSeconds(1);
                        //Make the sidebar visible again
                        Show();
                        cameraDTO.cameraMove.SetBool("SettingsIn", false);
                        break;
                    case 4:
                        //Starts the animation
                        cameraDTO.cameraMove.SetBool("MessageIn", true);
                        //Make the sidebar invisible so the button back can't be pressed
                        Hide();
                        //waits for the animation to finish
                        yield return new WaitForSeconds(3);
                        //Make the sidebar visible again
                        Show();
                        cameraDTO.cameraMove.SetBool("MessageIn", false);
                        break;
                    case 8:
                        //Starts the animation
                        cameraDTO.cameraMove.SetBool("StickersIn", true);
                        //Make the sidebar invisible so the button back can't be pressed
                        Hide();
                        //waits for the animation to finish
                        yield return new WaitForSeconds(3);
                        //Make the sidebar visible again
                        Show();
                        cameraDTO.cameraMove.SetBool("StickersIn", false);
                        break;
                    case 9:
                        //Starts the animation
                        cameraDTO.cameraMove.SetBool("DiaryIn", true);
                        //Make the sidebar invisible so the button back can't be pressed
                        Hide();
                        //waits for the animation to finish
                        yield return new WaitForSeconds(4);
                        //Make the sidebar visible again
                        Show();
                        cameraDTO.cameraMove.SetBool("DiaryIn", false);
                        break;
                    case 12:
                        //Starts the animation
                        cameraDTO.cameraMove.SetBool("MinigamesIn", true);
                        //Make the sidebar invisible so the button back can't be pressed
                        Hide();
                        //waits for the animation to finish
                        yield return new WaitForSeconds(4);
                        //Make the sidebar visible again
                        Show();
                        cameraDTO.cameraMove.SetBool("MinigamesIn", false);
                        break;
                }
            }

            //All the camera animations to the right section
            switch (newState)
            {
                case 2:
                    //selects the track to the right location
                    cameraDTO.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = GameObject.Find("CinemachineTrackSettings").GetComponent<CinemachineSmoothPath>();
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("SettingsOut", true);
                    //Make the sidebar invisible so the button back can't be pressed
                    Hide();
                    yield return new WaitForSeconds(1);
                    cameraDTO.cameraMove.SetBool("SettingsOut", false);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(2);
                    //Make the sidebar visible again
                    Show();
                    break;
                case 4:
                    //selects the track to the right location
                    cameraDTO.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = GameObject.Find("CinemachineTrackMessages").GetComponent<CinemachineSmoothPath>();
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("MessageOut", true);
                    //Make the sidebar invisible so the button back can't be pressed
                    Hide();
                    yield return new WaitForSeconds(1);
                    cameraDTO.cameraMove.SetBool("MessageOut", false);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(2);
                    //Make the sidebar visible again
                    Show();
                    break;
                case 8:
                    //selects the track to the right location
                    cameraDTO.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = GameObject.Find("CinemachineTrackStickers").GetComponent<CinemachineSmoothPath>();
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("StickersOut", true);
                    //Make the sidebar invisible so the button back can't be pressed
                    Hide();
                    yield return new WaitForSeconds(1);
                    cameraDTO.cameraMove.SetBool("StickersOut", false);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(2);
                    //Make the sidebar visible again
                    Show();
                    break;
                case 9:
                    //selects the track to the right location
                    cameraDTO.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = GameObject.Find("CinemachineTrackDiary").GetComponent<CinemachineSmoothPath>();
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("DiaryOut", true);
                    //Make the sidebar invisible so the button back can't be pressed
                    Hide();
                    yield return new WaitForSeconds(1);
                    cameraDTO.cameraMove.SetBool("DiaryOut", false);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(3);
                    //Make the sidebar visible again
                    Show();
                    break;
                case 12:
                    //selects the track to the right location
                    cameraDTO.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = GameObject.Find("CinemachineTrackMinigames").GetComponent<CinemachineSmoothPath>();
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("MinigamesOut", true);
                    //Make the sidebar invisible so the button back can't be pressed
                    Hide();
                    yield return new WaitForSeconds(1);
                    cameraDTO.cameraMove.SetBool("MinigamesOut", false);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(3);
                    //Make the sidebar visible again
                    Show();
                    break;
                case 17:
                    //selects the track to the right location
                    cameraDTO.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = GameObject.Find("CinemachineTrackPaperPlane").GetComponent<CinemachineSmoothPath>();
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("PaperPlaneOut", true);
                    //Make the sidebar invisible so the button back can't be pressed
                    Hide();
                    yield return new WaitForSeconds(1);
                    cameraDTO.cameraMove.SetBool("PaperPlaneOut", false);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(2);
                    //automatic return
                    yield return new WaitForSeconds(3);
                    //Starts the animation
                    cameraDTO.cameraMove.SetBool("PaperPlaneIn", true);
                    //waits for the animation to finish
                    yield return new WaitForSeconds(3);
                    //Make the sidebar visible again
                    Show();
                    cameraDTO.cameraMove.SetBool("PaperPlaneIn", false);
                    break;
            }
            cameraDTO.state = newState;
        }
    }

    public CameraDTO GetState()
    {
        return cameraDTO;
    }

    void Hide()
    {
        canvas.enabled = false;
    }

    void Show()
    {
        canvas.enabled = true;
    }
}

