using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableForClient : MonoBehaviour
{

    public void PutItem(GameObject newItem)
    {
        if(transform.GetComponentInChildren<NewClient>() != null)       //если сидит человек
        {
            if (newItem.tag == "Food" && newItem.GetComponent<IdTable>().id == transform.GetComponent<OrderProcessing>().id)     //соответствие id заказа и стола
            {
                transform.GetComponent<OrderProcessing>().CreateFood();
                Destroy(newItem);
                newItem = null;
            }

            else if (newItem.tag == "Coffee")
            {
                if (transform.GetComponentInChildren<NewClient>() != null)
                {
                    Destroy(newItem);
                    newItem = null;
                    transform.GetComponentInChildren<NewClient>().mood.AddMood(10);
                }
            }
        }
    }

    public GameObject TakeItem()
    {
        GameObject returnItem = transform.GetComponent<SavedItem>().TakeIteme();

        if(returnItem != null)
        {
            if (returnItem.tag == "Money") //забираем деньги
            {
                Money.TakeManey();          //Отдельный метод
                Destroy(returnItem);
                transform.Find("client").GetComponent<NewClient>().status = StatusClient.leaveClient;
                GetComponent<OrderProcessing>().CreateEmptyPlateForTake();   //поставить посуду
            }

            else if (returnItem.tag == "Plate")
            {
                transform.GetComponent<OrderProcessing>().ClearEmptyPlate();   //убрать посуду
            }

            else if (returnItem.tag == "Order")
            {
                Money.TakeOrder();  //Отдельный метод
            }
        }

        return returnItem;
    }
}
