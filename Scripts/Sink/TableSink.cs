using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSink : MonoBehaviour
{
    public void PutItem(GameObject newItem)
    {
        if (newItem.tag == "Plate" || newItem.tag == "Coffee")
        {
            transform.GetComponent<Wash>().WashPlate(newItem);
        }
    }
}
