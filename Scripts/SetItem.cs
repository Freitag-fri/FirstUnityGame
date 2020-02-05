using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    /*
    public GameObject placeObj;
    public GameObject objOnTable;
    public bool canTake;
    */

    // Start is called before the first frame update
    void Start()
    {
       // canTake = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObj(GameObject item, GameObject parent, GameObject placeObj)
    {
        parent = item;

        parent.transform.SetParent(placeObj.transform);
        parent.transform.position = placeObj.transform.position;
    }

    public void DonotCanTakeObj()
    {
       // canTake = false;
    }

    
}
