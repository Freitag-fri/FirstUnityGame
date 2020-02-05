using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandClient : MonoBehaviour
{
    public bool timeOn;
    public float time;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeOn)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                transform.GetComponent<CreatePlaceForClient>().create = true;
                transform.GetComponent<CreatePlaceForClient>().numberOfPeople = 3;
                time = 0;
            }
        } 
    }
}
