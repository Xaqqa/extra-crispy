using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class player_interact : MonoBehaviour
{
    [SerializeField] GameObject CO2_particle;
    [SerializeField] GameObject water_particle;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject itemTagUI;
    [SerializeField] Transform uiItemPosition;
    [SerializeField] Transform handItemPosition;

    [SerializeField] List<GameObject> itemSprites;

    [SerializeField] bool firing;
    Vector3 currentHeldPosition;
    GameObject currentHeld;
    Vector3 currentHeldSize;

    void Awake()
    {
        currentHeldPosition = new Vector3(0f, 0f, 0f);
    }


    void Update()
    {
        Vector3 fwd = playerCam.transform.TransformDirection(Vector3.forward);
        RaycastHit Hit;

        if (Physics.Raycast(playerCam.transform.position, fwd, out Hit, 2.0f))
        {
            itemTagUI.GetComponent<TextMeshProUGUI>().text = Hit.transform.tag;

            if (Input.GetButtonDown("Interact")) //interacting with level
            {
                if (Hit.transform.CompareTag("Untagged") || Hit.transform.CompareTag("Fire"))
                {

                }
                else if (Hit.transform.CompareTag("Level_Start"))
                {
                    //LevelCalculation();
                }
                else if (Hit.transform.CompareTag("Tutorial_Start"))
                {
                    if (!game.levelActive)
                    {
                        GameObject.Find("Game Event Manager").GetComponent<PlayableDirector>().Stop();
                        game.levelActive = true;
                        game.inTutorial = true;
                        game.resetLevel = true;
                    }

                }
                else if (Hit.transform.CompareTag("Door"))
                {

                    Hit.transform.gameObject.GetComponent<PlayableDirector>().Play();
                    Hit.transform.gameObject.GetComponent<AudioSource>().Play();
                }


                else
                {
                    if (currentHeld != null)
                    {

                        GameObject droppedItem = Instantiate(currentHeld, handItemPosition.position, transform.rotation);
                        Destroy(currentHeld);
                        if (!droppedItem.GetComponent<Rigidbody>().useGravity)
                        {
                            droppedItem.GetComponent<Rigidbody>().useGravity = true;
                        }
                    }

                    currentHeld = Instantiate(Hit.transform.gameObject, currentHeldPosition, transform.rotation);

                    if (currentHeld.GetComponent<Rigidbody>().useGravity)
                    {
                        currentHeld.GetComponent<Rigidbody>().useGravity = false;
                    }

                    StartCoroutine("SpriteReset");

                    Destroy(Hit.transform.gameObject);

                }
            }
        }





        if (Physics.Raycast(playerCam.transform.position, fwd, out Hit, 5.0f)) //AIMING FOR PROJECTILES
        {

            if (Input.GetAxisRaw("RightTrigger") > 0.5f && !firing && Hit.transform.CompareTag("Fire") && currentHeld != null)
            {
                firing = true;
                currentHeld.GetComponent<item_physics>().UseItem();
                if (currentHeld.CompareTag("Fire_Extinguisher"))
                {
                    CO2_particle.SetActive(true);
                    Hit.transform.localScale -= new Vector3(0f, 1f, 0f);
                    StartCoroutine("DestroyHeld");
                }
                else if (currentHeld.CompareTag("Water_Bottle"))
                {
                    water_particle.SetActive(true);
                    Hit.transform.localScale -= new Vector3(0f, .25f, 0f);
                    StartCoroutine("DestroyHeld");
                }
            }


            if (Input.GetButtonDown("Fire1") && Hit.transform.CompareTag("Fire") && currentHeld != null)
            {
                currentHeld.GetComponent<item_physics>().UseItem();
                if (currentHeld.CompareTag("Fire_Extinguisher"))
                {
                    CO2_particle.SetActive(true);
                Hit.transform.localScale -= new Vector3(0f, 1f, 0f);
                    StartCoroutine("DestroyHeld");
                }
                else if (currentHeld.CompareTag("Water_Bottle"))
                {
                    water_particle.SetActive(true);
                    Hit.transform.localScale -= new Vector3(0f, .25f, 0f);
                    StartCoroutine("DestroyHeld");
                }
            }

            if (Hit.transform.localScale.y <= 0)
            {
                Hit.transform.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DestroyHeld()
    { 
        if (currentHeld.CompareTag("Fire_Extinguisher"))
        {
            yield return new WaitForSeconds(2);
            Destroy(currentHeld);
        }
        else if (currentHeld.CompareTag("Water_Bottle"))
        {
            yield return new WaitForSeconds(1);
            Destroy(currentHeld);
        }
        else
        {
            Destroy(currentHeld);
        }
        currentHeld = null;
        StartCoroutine("SpriteReset");
        firing = false;

    }
    IEnumerator SpriteReset()
    {
        if (currentHeld == null)
        {

            foreach (var i in itemSprites)
            {
                i.SetActive(false);
            }
        }
        else
        {


            foreach (var i in itemSprites)
            {
                if (i.name == currentHeld.tag)
                {
                    i.SetActive(true);
                }
                else
                {
                    i.SetActive(false);
                }
            }
        }
        yield return null;
    }

}