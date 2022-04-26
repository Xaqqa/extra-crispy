using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texture_show : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RotateSphere");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator RotateSphere ()
    {
        for (int i = 0; i < 5; i++)
        {
            this.gameObject.transform.eulerAngles += new Vector3(0f, .1f, .1f);
        }
        yield return null;

        StartCoroutine("RotateSphere");
    }
}
