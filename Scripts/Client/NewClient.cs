using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewClient : MonoBehaviour
{
    public int numberOfPeople;
    public GameObject parentGame;

    public GameObject[] arrClient;
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private GameObject order;

    private int zoom = 17;              //растояние от камеры до места появления клиентов
    private int top = 6;
    private int left = 40;
    private int distancX = 35;          //между человечками <->
    public int distancY = 60;

    private float time = 0f;

    public int placeInLine;         //место в очериди 

    public StatusClient status;

    public Mood mood;
    public int MOODrES;

    public Vector2 coordinateBox;          //координаты настроения

    void Start()
    {
        money = Resources.Load("Money") as GameObject;
        order = Resources.Load("Order") as GameObject;
        mood = new Mood();
        MOODrES = mood.MoodProportion;
        status = StatusClient.createClient;
    }

    void Update()
    {
        mood.LowerMood();
        if(mood.MoodProportion <= 0)        //настроение испортилось, удаление клиента
        {
            DeleteClient();
            return;
        }

        if (status == StatusClient.setClient)
        {
            Money.SetClient();
            ClientMyClass.MoveClient(placeInLine);      //вызов события на опускание очереди    
            status = StatusClient.createOrder;
            time = 0;
            mood.status = false;
        }

        else if(status == StatusClient.createOrder)
        {
            time += Time.deltaTime;
            if(time > 1)
            {
                status = StatusClient.readyOrder;
                CreateOrder();
                time = 0;
                mood.status = true;
            }
        }

        else if(status == StatusClient.eat)
        {
            mood.status = false;
            time += Time.deltaTime;
            if(time > 2)
            {
                CreateMonye();
            }
        }

        else if(status == StatusClient.leaveClient)
        {
            Destroy(gameObject);
        }
    }

    void CreateMonye()
    {
        //Destroy(transform.parent.GetComponent<SavedItem>().objOnTable);
        transform.parent.GetComponent<OrderProcessing>().CreateEmptyPlate();
        transform.parent.GetComponent<SavedItem>().objOnTable = null;
        transform.parent.GetComponent<NewObject>().CreateObj(money);
        transform.parent.GetComponent<OrderProcessing>().eatFood = false;
        transform.parent.GetComponent<OrderProcessing>().CreateEmptyPlate();

        mood.status = true;
        status = StatusClient.waitPay;
    }

    void CreateOrder()
    {
        int id = transform.parent.GetComponent<OrderProcessing>().id;
        transform.parent.GetComponent<NewObject>().CreateObj(order, id, transform.parent.GetComponent<OrderProcessing>());
        //status = StatusClient.readyOrder;
    }

    public void CreateClient(int numberOfPeople, GameObject parentGame, GameObject prefab, int c)
    {
        this.numberOfPeople = numberOfPeople;

        arrClient = new GameObject[numberOfPeople];
        parentGame.name = "client";

        parentGame.tag = "New client";
        parentGame.AddComponent<Rigidbody>();
        parentGame.AddComponent<BoxCollider>();
        parentGame.GetComponent<BoxCollider>().isTrigger = true;
        parentGame.GetComponent<Rigidbody>().useGravity = false;

        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3 coor = Camera.main.ScreenToWorldPoint(new Vector3(left + distancX * i, top + distancY * c, zoom));
            arrClient[i] = Instantiate(prefab, coor, prefab.transform.rotation);
            arrClient[i].transform.SetParent(parentGame.transform);
        }
    }

    private void DeleteClient()     //когда закончилось натроение
    {
        if (status == StatusClient.createClient)
        {
            ClientMyClass.MoveClient(placeInLine);      //вызов события на опускание очереди
        }
        else
        {
            transform.GetComponentInParent<OrderProcessing>().CliearTable();        //очистка стола
        }
        Destroy(gameObject);
    }

    private void OnGUI()
    {
        if(status == StatusClient.createClient)
        {
            coordinateBox = new Vector2(left - 15, Screen.height - (top + (distancY * placeInLine) + 10));
        }
           
        GUI.Box(new Rect(coordinateBox.x, coordinateBox.y, 100, 15), "");

        int newMood = mood.MoodProportion;
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, mood.colorMood.ReturnColor(mood.MoodOriginal));
        texture.Apply();
        GUIStyle myBoxStyle2 = new GUIStyle(GUI.skin.box);
        myBoxStyle2.normal.background = texture;

        for(int i = 0; i < newMood; i++)
        {
            GUI.Box(new Rect(coordinateBox.x + (i * 7 + 3 * i), coordinateBox.y, 7, 13), "", myBoxStyle2);
        } 
    }
}
