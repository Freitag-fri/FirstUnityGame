using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdTable : MonoBehaviour
{
    public int id;
    public OrderProcessing link;        //ссылка на заказ 

    public IdTable(int id, OrderProcessing link)
    {
        this.id = id;
        this.link = link;
    }
}
