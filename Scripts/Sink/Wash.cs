using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wash : MonoBehaviour
{
    public GameObject plate;

    public void WashPlate(GameObject item)
    {
        if (item.tag == plate.tag)
        {
            Money.ClearPlate();
            Destroy(item);
        }

        else
        {
            Destroy(item);
        }
    }
}
