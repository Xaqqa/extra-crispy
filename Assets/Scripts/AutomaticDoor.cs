using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AutomaticDoor : MonoBehaviour
{

    [SerializeField] PlayableAsset OpenAnimation;
    [SerializeField] PlayableAsset CloseAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<PlayableDirector>().playableAsset = OpenAnimation;
            GetComponent<PlayableDirector>().Play();
            GetComponent<AudioSource>().Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<PlayableDirector>().playableAsset = CloseAnimation;
            GetComponent<PlayableDirector>().Play();
        }
    }
}
