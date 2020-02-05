using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    public void PutItem(GameObject newItem)
    {
        if (newItem.tag == "Order")
        {
            transform.GetComponent<CookOrder>().ReceivOrder(newItem);
            newItem = null;
        }
    }
}
