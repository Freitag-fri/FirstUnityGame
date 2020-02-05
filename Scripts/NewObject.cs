using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObject : MonoBehaviour
{
    //public GameObject prefabCube;
    GameObject test;

    IdTable orderId;


    public void CreateObj(GameObject obj)
    {
        if(GetComponent<SavedItem>().objOnTable == null)
        {
            test = Instantiate(obj, GetComponent<SavedItem>().placeObj.transform.position, transform.rotation);
            test.transform.SetParent(GetComponent<SavedItem>().placeObj.transform);
            GetComponent<SavedItem>().objOnTable = test;
            GetComponent<SavedItem>().canTake = true;
        }
    }

    public void CreateObj(GameObject obj, int id, OrderProcessing linkOnFood)
    {
        if (GetComponent<SavedItem>().objOnTable == null)
        {
            test = Instantiate(obj, GetComponent<SavedItem>().placeObj.transform.position, transform.rotation);
            test.transform.SetParent(GetComponent<SavedItem>().placeObj.transform);
            GetComponent<SavedItem>().objOnTable = test;
            GetComponent<SavedItem>().canTake = true;

            orderId = GetComponent<SavedItem>().objOnTable.AddComponent<IdTable>();
            orderId.id = id;
            orderId.link = linkOnFood;

            if (GetComponent<OrderProcessing>() != null)
            {
                transform.GetComponent<OrderProcessing>().readyFood = test;         // сохраняем сылку на заказ
            }
                
            else
                linkOnFood.readyFood = test;
        }
    }
}
