using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class player_look : MonoBehaviour
{
    [SerializeField] GameObject playerCam;

    float HorizontalSpeed = 3f;
    float VerticalSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        if (!player_move.inMenu && !player_move.inPause)
        {
            float h = HorizontalSpeed * Input.GetAxisRaw("Mouse X"); //Obtains Mouse X Position
            float v = VerticalSpeed * Input.GetAxisRaw("Mouse Y"); //Obtains Mouse Y Position

            if (menu_ui.invertLook)
            {
                playerCam.transform.Rotate(-v, 0f, -playerCam.transform.eulerAngles.z); //Camera Vertical Look
            }
            else
            {
                playerCam.transform.Rotate(v, 0f, -playerCam.transform.eulerAngles.z); //Camera Vertical Look
            }

            playerCam.transform.localRotation = new Quaternion(Mathf.Clamp(playerCam.transform.localRotation.x, -0.6f, 0.6f), 0f, 0f, playerCam.transform.localRotation.w); //Locks Vertical Look
            this.transform.Rotate(0f, h, 0f); //Player Horizontal Look
        }
    }
}
