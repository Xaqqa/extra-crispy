using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursor_collider : MonoBehaviour
{
    GameObject player;


    bool cursorMinimized;
    bool buttonIncreased;

    GameObject button;

    [SerializeField] Texture2D tickBoxActive;
    [SerializeField] Texture2D tickBoxInactive;


    private void Awake()
    {
        player = GameObject.Find("Player");
    }


    private void OnTriggerStay2D(Collider2D trigger)
    {
        button = trigger.gameObject;
        StartCoroutine(IncreaseButton(button));
        StartCoroutine("MinimizeCursor");

        if (menu_ui.menuNavigationAssist)
        {
            menu_ui.cursorSpeed = 10f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            menu_ui.cursorSpeed = 15f;
            if (trigger.gameObject.CompareTag("Play_Button"))
            {
                player_move.inMenu = false;
                player_move.inPause = false;
                game.inTutorial = false;
                game.startLevel = true;
                player.transform.position = new Vector3(37, 7, 25);
                Debug.Log("Play");
            }
            else if (trigger.gameObject.CompareTag("Tutorial_Button"))
            {
                player_move.inMenu = false;
                player_move.inPause = false;
                game.inTutorial = true;
                player.transform.position = new Vector3(-85, -1, -24);
                game.startTutorial = true;
                Debug.Log("Tutorial");
            }
            else if (trigger.gameObject.CompareTag("Settings_Button"))
            {
                menu_ui.menuSector = "settings";
                Debug.Log("Settings");
            }
            else if (trigger.gameObject.CompareTag("Credits_Button"))
            {
                menu_ui.menuSector = "credits";
                Debug.Log("Credits");
            }
            else if (trigger.gameObject.CompareTag("Graphics_Button"))
            {
                menu_ui.menuSector = "graphics";
                Debug.Log("Graphics");
            }
            else if (trigger.gameObject.CompareTag("Controls_Button"))
            {
                menu_ui.menuSector = "controls";
                Debug.Log("Controls");
            }
            else if (trigger.gameObject.CompareTag("Sounds_Button"))
            {
                menu_ui.menuSector = "sounds";
                Debug.Log("Sounds");
            }
            else if (trigger.gameObject.CompareTag("ReturnToGame_Button"))
            {
                player_move.inPause = false;
                menu_ui.menuSector = "main";
            }

            #region Control Settings
            else if (trigger.gameObject.CompareTag("MenuNavigation_Button"))
            {
                if (menu_ui.menuNavigationAssist)
                {
                    trigger.gameObject.GetComponent<RawImage>().texture = tickBoxInactive;
                    menu_ui.menuNavigationAssist = false;
                    menu_ui.cursorSpeed = 15f;
                }
                else
                {
                    trigger.gameObject.GetComponent<RawImage>().texture = tickBoxActive;
                    menu_ui.menuNavigationAssist = true;
                }
            }
            else if (trigger.gameObject.CompareTag("InvertLook_Button"))
            {
                if (menu_ui.invertLook)
                {
                    trigger.gameObject.GetComponent<RawImage>().texture = tickBoxInactive;
                    menu_ui.invertLook = false;
                }
                else
                {
                    trigger.gameObject.GetComponent<RawImage>().texture = tickBoxActive;
                    menu_ui.invertLook = true;
                }
            }
            #endregion

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine("NormalizeCursor");
        StartCoroutine(DecreaseButton(button));

        if (menu_ui.cursorSpeed <= 15f)
        {
            menu_ui.cursorSpeed = 15f;
        }
    }


    IEnumerator MinimizeCursor()
    {
        if (!cursorMinimized)
        {
            cursorMinimized = true;
            for (int i = 0; i < 30; i++)
            {
                this.gameObject.GetComponent<RectTransform>().localScale -= new Vector3(0.1f / 30, 0.1f / 30);
            }
            yield return null;
        }
    }

    IEnumerator NormalizeCursor()
    {
        if (cursorMinimized)
        {
            cursorMinimized = false;
            for (int i = 0; i < 30; i++)
            {
                this.gameObject.GetComponent<RectTransform>().localScale += new Vector3(0.1f / 30, 0.1f / 30);
            }
            yield return null;
        }
    }


    IEnumerator IncreaseButton(GameObject button)
    {
        if (!buttonIncreased && button.name != "Tick_Box")
        {
            buttonIncreased = true;
            for (int i = 0; i < 30; i++)
            {
                button.transform.localScale += new Vector3(0.05f / 30, 0.05f / 30);
            }
            yield return null;
            button.transform.localScale = new Vector3(1.05f, 1.05f);
        }

    }

    IEnumerator DecreaseButton(GameObject button)
    {
        if (buttonIncreased && button.name != "Tick_Box")
        {
            buttonIncreased = false;
            for (int i = 0; i < 30; i++)
            {
                button.transform.localScale -= new Vector3(0.05f / 30, 0.05f / 30);
            }
            button.transform.localScale = new Vector3(1f, 1f);
            yield return null;
        }
    }
}
