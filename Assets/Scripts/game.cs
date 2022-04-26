using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class game : MonoBehaviour
{
    #region Tutorial Variables
    public static bool startTutorial;
    public static bool startLevel;
    public static bool inTutorial;

    bool endMessage;

    [SerializeField] AudioSource tutComplete;
    [SerializeField] AudioSource tutFailed;

    [SerializeField] List<GameObject> tutFires;
    [SerializeField] List<GameObject> tutDoors;
    [SerializeField] List<GameObject> tutInteractDoors;
    [SerializeField] GameObject TutorialObjects;
    int tutFiresAlive;
    #endregion

    #region Level Variables
    [SerializeField] List<GameObject> levelFires;
    [SerializeField] List<GameObject> levelDoors;
    [SerializeField] List<GameObject> levelInteractDoors;
    [SerializeField] GameObject LevelObjects;
    int levelFiresAlive;
    public static bool resetLevel;
    public static bool levelActive;
    #endregion

    #region Timer Variables
    [SerializeField] int timer = 100;
    [SerializeField] GameObject timerBar;
    [SerializeField] GameObject levelCounter;
    [SerializeField] GameObject initialCountdown;
    int level;

    float timerPercent;

    #endregion

    bool levelOngoing = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || resetLevel)
        {
            resetLevel = false;
            LevelReset();
            
        }

        if (startTutorial)
        {
            startTutorial = false;
            StartCoroutine("TutorialStart");
        }

        if (startLevel)
        {
            startLevel = false;
            LevelReset();
        }
    }



    void LevelReset()
    {

        //Increase Level
        level++;
        levelCounter.GetComponent<TextMeshProUGUI>().text = level.ToString();

        //Reset Timer
        timerBar.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 10f);
        levelOngoing = false;


        StartCoroutine("ResetLevel"); //Reset Doors and Fires

        if (inTutorial)
        {
            int chosenDoor = Random.Range(0, tutDoors.Count);
            tutDoors[chosenDoor].SetActive(true);

            chosenDoor = Random.Range(0, tutDoors.Count);
            tutDoors[chosenDoor].SetActive(true);

            chosenDoor = Random.Range(0, tutDoors.Count);
            tutDoors[chosenDoor].SetActive(true);


            int chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, tutFires.Count);
            tutFires[chosenFire].SetActive(true);

        } //Randomize Tutorial Doors and Fires
        else
        {
            int chosenDoor = Random.Range(0, levelDoors.Count);
            levelDoors[chosenDoor].SetActive(true);

            chosenDoor = Random.Range(0, levelDoors.Count);
            levelDoors[chosenDoor].SetActive(true);

            chosenDoor = Random.Range(0, levelDoors.Count);
            levelDoors[chosenDoor].SetActive(true);



            int chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);

            chosenFire = Random.Range(0, levelFires.Count);
            levelFires[chosenFire].SetActive(true);
        } //Randomize Level Doors and Fires

        StartCoroutine("LevelCountdown"); //Start Visual Countdown

    }

    IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(3);
        GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(85f);
    }

    IEnumerator LevelCountdown()
    {
        endMessage = false;
        timer = Random.Range(180, 200);
        timerPercent = 300f/timer;

        yield return new WaitForSeconds(1f);
        initialCountdown.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        initialCountdown.GetComponent<TextMeshProUGUI>().text = "3";
        yield return new WaitForSeconds(1f);
        initialCountdown.GetComponent<TextMeshProUGUI>().text = "2";
        yield return new WaitForSeconds(1f);
        initialCountdown.GetComponent<TextMeshProUGUI>().text = "1";
        yield return new WaitForSeconds(1f);
        initialCountdown.GetComponent<TextMeshProUGUI>().text = "GO!";

        StartCoroutine("CountdownFade");
        StartCoroutine("TimerCountdown");
    }

    IEnumerator ResetLevel()
    {
        if (inTutorial)
        {
            if (GameObject.Find("TutorialObjects") != null)
            {
                Destroy(GameObject.Find("TutorialObjects"));
            }
            else
            {
                Destroy(GameObject.Find("TutorialObjects(Clone)"));
            }

            Instantiate(TutorialObjects, GameObject.Find("Tutorial").transform);

            foreach (var item in tutDoors)
            {
                item.SetActive(false);
            }

            foreach (var item in tutFires)
            {
                item.SetActive(false);
                item.transform.localScale = new Vector3(2, 1, 2);
            }
            foreach (var item in tutInteractDoors)
            {
                item.GetComponent<PlayableDirector>().time = 0;
            }
            yield return null;
        }
        else
        {
            if (GameObject.Find("LevelObjects") != null)
            {
                Destroy(GameObject.Find("LevelObjects"));
            }
            else
            {
                Destroy(GameObject.Find("LevelObjects(Clone)"));
            }

            Instantiate(LevelObjects, GameObject.Find("Level").transform);

            foreach (var item in levelDoors)
            {
                item.SetActive(false);
            }

            foreach (var item in levelFires)
            {
                item.SetActive(false);
                item.transform.localScale = new Vector3(2, 1, 2);
            }
            foreach (var item in levelInteractDoors)
            {
                item.GetComponent<PlayableDirector>().time = 0;
            }
            yield return null;
        }
    }

    IEnumerator CountdownFade()
    {
        for (int i = 0; i < 60; i++)
        {
            initialCountdown.GetComponent<TextMeshProUGUI>().color -= new Color(0f, 0f, 0f, 1f/60f);
            yield return null;
        }
       
    }

    IEnumerator TimerCountdown()
    {
        while (timer > 0)
        {
            if (!player_move.inPause)
            {
                timer--;
                timerBar.GetComponent<RectTransform>().sizeDelta -= new Vector2(timerPercent, 0);
                yield return new WaitForSeconds(1f);
                StartCoroutine("CheckFires");
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator CheckFires()
    {
        if (inTutorial)
        {
            tutFiresAlive = 0;
            foreach (var fire in tutFires)
            {
                if (fire.activeInHierarchy)
                {
                    tutFiresAlive++;
                }
            }
            if (tutFiresAlive > 0 && timer == 0)
            {
                tutFailed.Play();
                level = 0;
                levelCounter.GetComponent<TextMeshProUGUI>().text = level.ToString();
                levelActive = false;
                //Restart The Try
            }
            else if (tutFiresAlive == 0 && !endMessage)
            {
                endMessage = true;
                yield return new WaitForSeconds(1f);
                tutComplete.Play();
                level = 0;
                levelCounter.GetComponent<TextMeshProUGUI>().text = level.ToString();
                yield return new WaitForSeconds(7f);
                player_move.inMenu = true;
                player_move.inPause = false;
                levelActive = false;
                menu_ui.menuSector = "main";
            }
        }
        else
        {
            levelFiresAlive = 0;
            foreach (var fire in levelFires)
            {
                if (fire.activeInHierarchy)
                {
                    levelFiresAlive++;
                }
            }

            if (levelFiresAlive > 0 && timer == 0)
            {
                Debug.Log("Round Lost");
                //Play Ending Sound
                level = 0;
                levelCounter.GetComponent<TextMeshProUGUI>().text = level.ToString();
                player_move.inMenu = true;
                player_move.inPause = false;
                levelActive = false;
                menu_ui.menuSector = "main";
            }
            else if (levelFiresAlive == 0)
            {
                Debug.Log("Round Won");
                //Congratulate
                LevelReset();

            }

        }
    }
}