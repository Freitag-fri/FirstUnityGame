using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlaceForClient : MonoBehaviour
{
    public int numberOfPeople;
    public bool create;
    GameObject parentGame;
    public GameObject prefab;
    public int i = 0;
    public List<GameObject> baseClient;
    [SerializeField]
    private Canvas canvas;


    void Start()
    {
        create = false;
        ClientMyClass.moveClientEv += Test;

        numberOfPeople = 5;
        CreateClient();

        numberOfPeople = 2;
        CreateClient();
        numberOfPeople = 2;
        CreateClient();
    }

    void Update()
    {
       if(create && numberOfPeople != 0 && i < 6)
       {
            CreateClient();
       } 
       else
       {
            create = false;
       }
    }

    void CreateClient()
    {
        create = false;
        parentGame = new GameObject();
        baseClient.Add(parentGame);
        parentGame.AddComponent<NewClient>();
        parentGame.GetComponent<NewClient>().CreateClient(numberOfPeople, parentGame, prefab, i);
        parentGame.GetComponent<NewClient>().placeInLine = i;
        parentGame.GetComponent<NewClient>().canvas = canvas;
        i++;
    }

    void Test(int group)
    {
        
        baseClient.RemoveAt(group);
        i--;

        for(; group < baseClient.Count; group++)
        {
            baseClient[group].GetComponent<NewClient>().placeInLine--;
            for (int persone = 0; persone < baseClient[group].GetComponent<NewClient>().numberOfPeople; persone++)
            {
                GameObject gamePersone = baseClient[group].GetComponent<NewClient>().arrClient[persone];
                Vector3 buf = Camera.main.WorldToScreenPoint(new Vector3(gamePersone.transform.position.x, gamePersone.transform.position.y, gamePersone.transform.position.z));

                gamePersone.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(buf.x, buf.y - baseClient[group].GetComponent<NewClient>().distancY, buf.z));
            }
        }     
    }    
}
