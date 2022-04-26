using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_physics : MonoBehaviour
{

    [SerializeField] AudioSource itemDrop;
    [SerializeField] AudioSource itemUse;

    public void UseItem()
    {
        itemUse.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            itemDrop.Play();
        }
    }
}
