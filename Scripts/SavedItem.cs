using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedItem : MonoBehaviour
{
    public GameObject placeObj;
    public GameObject objOnTable;
    public bool canTake;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject TakeIteme()
    {
        if (objOnTable != null && canTake)
        {
            GameObject bufObjOnTable = objOnTable;
            objOnTable = null;
            return bufObjOnTable;
        }
        return null;
    }
}
