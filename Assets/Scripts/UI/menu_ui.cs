using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class menu_ui : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource menuMusic;
    bool controllerConnected;
    public static float cursorSpeed = 15f;

    public static bool menuNavigationAssist;
    public static bool invertLook;
    
    [SerializeField] GameObject consoleCursor;

    [SerializeField] GameObject menuBackground;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject main;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject credits;

    [SerializeField] GameObject graphics;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject sounds;

    [SerializeField] GameObject game;

    public static string menuSector;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        consoleCursor.SetActive(true);
        player_move.inMenu = true;
        menuSector = "main";
    }

    // Update is called once per frame
    void Update()
    {
    
        if (player_move.inMenu)
        {
            if (!menuMusic.isPlaying)
            {
                menuMusic.Play();
            }
            menuBackground.SetActive(true);
            audioMixer.SetFloat("SFXVol", -80f);
            game.SetActive(false);
            menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (player_move.inPause)
        {
            menuBackground.SetActive(false);
            audioMixer.SetFloat("SFXVol", -80f);
            game.SetActive(false);
            menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (menuMusic.isPlaying)
            {
                menuMusic.Stop();
            }
            game.SetActive(true);
            audioMixer.SetFloat("SFXVol", 0f);
            menu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        #region Menu Navigation
        if (player_move.inMenu || player_move.inPause)
        {
            if (menuSector == "main")
            {
                main.SetActive(true);
                pause.SetActive(false);
                credits.SetActive(false);
                settings.SetActive(false);
                graphics.SetActive(false);
                controls.SetActive(false);
                sounds.SetActive(false);
            }

            else if (menuSector == "pause")
            {
                main.SetActive(false);
                pause.SetActive(true);
                settings.SetActive(false);
                graphics.SetActive(false);
                controls.SetActive(false);
                sounds.SetActive(false);
            }

            else if (menuSector == "settings")
            {
                main.SetActive(false);
                pause.SetActive(false);
                credits.SetActive(false);
                settings.SetActive(true);
                graphics.SetActive(false);
                controls.SetActive(false);
                sounds.SetActive(false);
            }

            else if (menuSector == "credits")
            {
                main.SetActive(false);
                credits.SetActive(true);
                settings.SetActive(false);
                graphics.SetActive(false);
                controls.SetActive(false);
                sounds.SetActive(false);
            }

            else if (menuSector == "graphics")
            {
                main.SetActive(false);
                credits.SetActive(false);
                settings.SetActive(false);
                graphics.SetActive(true);
                controls.SetActive(false);
                sounds.SetActive(false);
            }

            else if (menuSector == "controls")
            {
                main.SetActive(false);
                credits.SetActive(false);
                settings.SetActive(false);
                graphics.SetActive(false);
                controls.SetActive(true);
                sounds.SetActive(false);
            }

            else if (menuSector == "sounds")
            {
                main.SetActive(false);
                credits.SetActive(false);
                settings.SetActive(false);
                graphics.SetActive(false);
                controls.SetActive(false);
                sounds.SetActive(true);
            }


            if (Input.GetButtonDown("Back"))
            {
                if (player_move.inMenu) {
                    if (menuSector == "graphics" || menuSector == "controls" || menuSector == "sounds")
                    {
                        menuSector = "settings";
                    }
                    else
                    {
                        menuSector = "main";
                    }
                }

                else if (player_move.inPause)
                {
                    if (menuSector == "graphics" || menuSector == "controls" || menuSector == "sounds")
                    {
                        menuSector = "settings";
                    }
                    /*/
                    else if (menuSector == "pause")
                    {
                        player_move.inPause = false;
                        menuSector = "main";
                    
                    /*/
                    else
                    {
                        menuSector = "pause";
                    }
                }
            }
        }

        #endregion

        #region Controller Cursor Movement
        if (player_move.inMenu || player_move.inPause)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                consoleCursor.transform.Translate(Vector3.right * cursorSpeed * Input.GetAxisRaw("Horizontal"));
                menu.transform.Translate(Vector3.left * cursorSpeed * Input.GetAxisRaw("Horizontal") / 100);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                consoleCursor.transform.Translate(Vector3.left * cursorSpeed * -Input.GetAxisRaw("Horizontal"));
                menu.transform.Translate(Vector3.right * cursorSpeed * -Input.GetAxisRaw("Horizontal") / 100);
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                consoleCursor.transform.Translate(Vector3.up * cursorSpeed * Input.GetAxisRaw("Vertical"));
                menu.transform.Translate(Vector3.down * cursorSpeed * Input.GetAxisRaw("Vertical") / 100);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                consoleCursor.transform.Translate(Vector3.down * cursorSpeed * -Input.GetAxisRaw("Vertical"));
                menu.transform.Translate(Vector3.up * cursorSpeed * -Input.GetAxisRaw("Vertical") /100 );
            }
        }
        #endregion
    }
}