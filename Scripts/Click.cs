using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Click : MonoBehaviour //, IPointerClickHandler
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject click;
    Vector3 positionClick;

    public GameObject takeItem;
    public GameObject buffTakeItem;

    public GameObject targetClient;

    void Start()
    {
        positionClick = transform.position;
    }


    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, transform.position.z));
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
        if(click.GetComponent<TableControlPos>() as TableControlPos)
        {
            positionClick = click.GetComponent<TableControlPos>().position;
            transform.LookAt(positionClick);
        } 

        else if(click.GetComponentInParent<TableControlPos>() as TableControlPos)
        {
            positionClick = click.GetComponentInParent<TableControlPos>().position;
            transform.LookAt(positionClick);
        }
    }

    void Move()
    {
        if (transform.position != positionClick) //продолжаем движение
        {
            transform.position = Vector3.MoveTowards(transform.position, positionClick, Time.deltaTime * 20);
        }

        else if(click != null)      //дошли до нужного места
        {
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


