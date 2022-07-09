using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{

    [SerializeField] private AudioSource talk;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if player left click audio will play
        if (Input.GetMouseButtonDown(0))
        {
            talk.Play();

        }


    }
}