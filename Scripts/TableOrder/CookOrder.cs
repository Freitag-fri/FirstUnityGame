using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookOrder : MonoBehaviour
{
    public int sizeListCooked;

    public GameObject food;
    public GameObject tableReadyOrder;
    public List <GameObject> sizePlace;

    public List<Cooked> listCooked = new List<Cooked>();
    public Queue<IdTable> readyOrder;                         //очередь готовых заказов

    // Start is called before the first frame update
    void Start()
    {
        readyOrder = new Queue<IdTable>();

        for (int i = 0; i < 4; i++)
        {
            sizePlace.Add(tableReadyOrder.transform.Find("Place item" + i.ToString()).gameObject);  
        }
    }

    void Update()
    {
        sizeListCooked = listCooked.Count;
        CallAddTime();

        if(readyOrder.Count > 0)
        {
            for(int i = 0; i < 4; i++)
            {
                if(sizePlace[i].GetComponent<SavedItem>().objOnTable == null)
                {
                    sizePlace[i].GetComponent<NewObject>().CreateObj(food, readyOrder.Peek().id, readyOrder.Dequeue().link);
                    return;
                }
            }
        }
    }

    public void ReceivOrder(GameObject item)
    {
        AddZakaz(item.GetComponent<IdTable>().id, item.GetComponent<IdTable>().link);
        Destroy(item);
    }

    public void AddZakaz(int id, OrderProcessing link)
    {
        
        listCooked.Add(new Cooked(id, link));
        listCooked[listCooked.Count - 1].IdReadyOrderEv += ReadyOrder;
    } 

    void ReadyOrder(int id, Cooked link, OrderProcessing linkOnFood)
    {
        readyOrder.Enqueue(new IdTable(id, linkOnFood));
        listCooked.Remove(link);

    }

    void CallAddTime()
    {
        for(int i = 0; i < listCooked.Count; i++)
        {
            listCooked[i].AddTime();
        }
    }
    
}


public class Cooked
{
    public int id = 0;
    public OrderProcessing link;
    float time = 0;
    float timeCooker = 3;
    public bool cook = false;

    public delegate void IdReadyOrder(int id, Cooked link, OrderProcessing linkOnFood);
    public event IdReadyOrder IdReadyOrderEv;

    public Cooked(int id, OrderProcessing link)
    {
        this.id = id;
        this.link = link;
    }

    public void AddTime()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            IdReadyOrderEv(id, this, link);
        }
    }
}
