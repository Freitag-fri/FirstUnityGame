using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderProcessing : MonoBehaviour
{
    public int id;

    [SerializeField]
    private GameObject[] client;
    [SerializeField]
    private GameObject[] foodOnTable;
    public int nomberOfPlace;
    public int clientOnTable;
    [SerializeField]
    private GameObject[] placeForClient;
    [SerializeField]
    private GameObject[] placeForFood;

    [SerializeField]
    private GameObject order;
    [SerializeField]
    private GameObject food;
    [SerializeField]
    private GameObject foodForClient;
    /*
    [SerializeField]
    private GameObject money;
    */
    [SerializeField]
    private GameObject emptyPlate;

    public bool eatFood;
    ///ссылка для удаления заказа
    public GameObject readyFood;         


    void Start()
    {
        placeForClient = new GameObject[nomberOfPlace];
        placeForFood = new GameObject[nomberOfPlace];
        client = new GameObject[nomberOfPlace];
        foodOnTable = new GameObject[nomberOfPlace];

        for (int i = 0; i < nomberOfPlace; i++)
        {  
            placeForClient[i] = transform.Find("Place for client/Place" + i.ToString()).gameObject;
        }

        for (int i = 0; i < nomberOfPlace; i++)
        {
            placeForFood[i] = transform.Find("Place for food/Place" + i.ToString()).gameObject;
        }
    }

    public void CreateFood()
    {
        //GetComponent<SetItem>().DonotCanTakeObj();
        eatFood = true;
        for (int i = 0; i < clientOnTable; i++)
        {
            foodOnTable[i] = Instantiate(foodForClient, placeForFood[i].transform.position, transform.rotation);
            foodOnTable[i].transform.SetParent(placeForFood[i].transform);
            foodOnTable[i].transform.position = placeForFood[i].transform.position;
        }

        GameObject test = transform.Find("client").gameObject;
        test.GetComponent<NewClient>().status = StatusClient.eat;
        test.GetComponent<NewClient>().mood.AddMood(10);
        Money.SetFood();
    }

    public void SetClient(GameObject client)
    {
        NewClient newClient = client.GetComponent<NewClient>();
        newClient.transform.SetParent(transform);
        clientOnTable = newClient.numberOfPeople;

        GameObject pos = transform.Find("PositionMood").gameObject;
        newClient.coordinateBoxMood = Camera.main.WorldToScreenPoint(new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z));
        newClient.coordinateBoxMood = new Vector2(newClient.coordinateBoxMood.x, Screen.height - newClient.coordinateBoxMood.y);

        newClient.tag = "Table";            //временно
        for (int i = 0; i < clientOnTable; i++)
        {
            newClient.arrClient[i].tag = "Table";            //временно
            //client.GetComponent<BoxCollider>().isTrigger = false;
            this.client[i] = newClient.arrClient[i];

            newClient.arrClient[i].transform.position = placeForClient[i].transform.position;
            newClient.arrClient[i].transform.rotation = placeForClient[i].transform.rotation;
        }
    }
    
    private void OnGUI()
    {
        if(client[0] != null)
        {
            StatusClient status = client[0].transform.GetComponentInParent<NewClient>().status;
            if (status == StatusClient.setClient || status == StatusClient.createOrder || status == StatusClient.readyOrder)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 20;
                style.normal.textColor = Color.green;
                Vector3 bufCamera = Camera.main.WorldToScreenPoint(this.transform.position);
                GUI.Label(new Rect(bufCamera.x-15, Screen.height - bufCamera.y-20, 100, 20), id.ToString(), style);
            }
        }
    }

    public void CreateEmptyPlate()
    {
        for (int i = 0; i < clientOnTable; i++)
        {
            Destroy(foodOnTable[i]);
            foodOnTable[i] = Instantiate(emptyPlate, placeForFood[i].transform.position, transform.rotation);
            foodOnTable[i].transform.SetParent(placeForFood[i].transform);
            foodOnTable[i].transform.position = placeForFood[i].transform.position;
        }
    }

    public void CreateEmptyPlateForTake()
    {
        transform.GetComponent<NewObject>().CreateObj(emptyPlate);
    }

    public void ClearEmptyPlate()
    {
        for (int i = 0; i < clientOnTable; i++)
        {
            Destroy(foodOnTable[i]);
        }
    }

    public void CliearTable()   //вызывается при уделении клиента(когда испортиось настроение)
    {
        //ClearEmptyPlate();
        //GameObject buf = transform.Find("client").gameObject;
        //buf.GetComponent<NewClient>().status = StatusClient.readyOrder;
        Destroy(readyFood);
        clientOnTable = 0;
        Destroy(transform.GetComponent<SavedItem>().objOnTable);
    }
}

