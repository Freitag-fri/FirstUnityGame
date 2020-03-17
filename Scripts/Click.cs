using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Click : MonoBehaviour //, IPointerClickHandler
{
    [SerializeField]
    private GameObject click;
    Vector3 positionClick;

    public GameObject takeItem;
    public GameObject buffTakeItem;

    public GameObject targetClient;
    private GameObject positionPlayer;
    private Queue<GameObject> wayPlayer = null;

    void Start()
    {
        positionPlayer = GetComponent<MovePlayer>().posStartPlayer;
        transform.position = positionPlayer.transform.position;
        positionClick = transform.position;
    }


    void Update()
    {
        ClickMouse();       //обработка нажатия
        Move();             //перемещение
    }

    void ClickMouse()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                click = hit.transform.gameObject;
                if(click.tag == "New client" )
                {
                    SetTargetClient();
                }
                else if(targetClient != null && click.tag == "Table")
                {
                    SeatDownOnTable(); 
                }
                else
                {
                    MovePlayer();
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            targetClient = null;
        }
    }

    void SetTargetClient()
    {
        targetClient = click;
    }

    void SeatDownOnTable()
    {
        if (targetClient.transform.parent.GetComponent<NewClient>().numberOfPeople <= click.GetComponent<OrderProcessing>().nomberOfPlace && click.GetComponent<OrderProcessing>().clientOnTable == 0)
        {
            click.GetComponent<OrderProcessing>().SetClient(targetClient.transform.parent.gameObject);
            targetClient.transform.parent.GetComponent<NewClient>().status = StatusClient.setClient;

            targetClient = null;
            click = null;
        }
    }

    void MovePlayer()  
    {
        
        if (click.GetComponent<TableControlPos>() as TableControlPos)
        {
            positionClick = click.GetComponent<TableControlPos>().posGame.transform.position;
            GameObject bufPos = click.GetComponent<TableControlPos>().posGame;
            wayPlayer = GetComponent<MovePlayer>().GetRoute(positionPlayer, bufPos);
            //positionClick = click.GetComponent<TableControlPos>().position;
            //transform.LookAt(positionClick);
        } 

        else if(click.GetComponentInParent<TableControlPos>() as TableControlPos)
        {
            positionClick = click.GetComponentInParent<TableControlPos>().posGame.transform.position;
            GameObject bufPos = click.GetComponentInParent<TableControlPos>().posGame;
            wayPlayer = GetComponent<MovePlayer>().GetRoute(positionPlayer, bufPos);

            //positionClick = click.GetComponentInParent<TableControlPos>().position;
            //transform.LookAt(positionClick);
        }
        
        /*
        if (click.GetComponent<TableControlPos>() as TableControlPos || click.GetComponentInParent<TableControlPos>() as TableControlPos)
        {
            positionClick = click.GetComponentInParent<TableControlPos>().posGame.transform.position;
            GameObject bufPos = click.GetComponent<TableControlPos>().posGame;
            wayPlayer = GetComponent<MovePlayer>().GetRoute(positionPlayer, bufPos);
        }
        */
    }

    void Move()
    {
        if (wayPlayer != null && transform.position != wayPlayer.Peek().transform.position) //продолжаем движение
        {
            if(!GetComponent<Animation>().IsPlaying("RunPlayer"))
            {
                GetComponent<Animation>().Play("RunPlayer");
            }
            transform.LookAt(wayPlayer.Peek().transform.position);
            transform.position = Vector3.MoveTowards(transform.position, wayPlayer.Peek().transform.position, Time.deltaTime * 20);
        }

        else if(transform.position != positionClick)
        {
            positionPlayer = wayPlayer.Dequeue();
        }

        else if(click != null)      //дошли до нужного места
        {
            positionPlayer = click.GetComponent<TableControlPos>().posGame;
            GetComponent<Animation>().Stop("RunPlayer");
            GetComponent<Animation>().Play("StopRunPlayer");
            LastClick();
            click = null;
        }
    }

    void LastClick()
    {
        if (buffTakeItem == null)              //взять (в руках ничего нет)
        {
            if (click.GetComponent<SavedItem> () as SavedItem)
            {
                if(click.tag == "Table")
                {
                    buffTakeItem = click.GetComponent<TableForClient>().TakeItem();
                }
                else
                {
                    buffTakeItem = click.GetComponent<SavedItem>().TakeIteme();   //взять предмет со стола
                }
                
                if (buffTakeItem == null) return;
                buffTakeItem.transform.SetParent(takeItem.transform);
                buffTakeItem.transform.position = takeItem.transform.position;
            }
        }

        else          //положить
        {
            if (click.tag == "Table")
            {
                click.GetComponent<TableForClient>().PutItem(buffTakeItem);                
            }
            else if (click.tag == "Order")
            {
                click.GetComponent<TableOrder>().PutItem(buffTakeItem);
            }
            else if (click.tag == "Sink")
            {
                click.GetComponent<TableSink>().PutItem(buffTakeItem);
            }
        }
    }
}


