using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("Open");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Close");
        }

    }
    public void OnDogApproach()
    {

        anim.SetTrigger("Open");
    }
}
