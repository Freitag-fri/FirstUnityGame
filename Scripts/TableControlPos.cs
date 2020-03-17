using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableControlPos : MonoBehaviour
{

    public GameObject posGame;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.Find("TableZone").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
