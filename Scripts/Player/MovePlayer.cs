using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    public GameObject posStartPlayer;
    [SerializeField]
    private GameObject posTableForClient0;
    [SerializeField]
    private GameObject posTableForClient1;
    [SerializeField]
    private GameObject posTableForClient2;
    [SerializeField]
    private GameObject posTableForClient3;
    [SerializeField]
    private GameObject posTableForClient4;
    [SerializeField]
    private GameObject posTableForClient5;
    [SerializeField]
    private GameObject posTableForClient6;
    [SerializeField]
    private GameObject posReadyFood0;
    [SerializeField]
    private GameObject posReadyFood1;
    [SerializeField]
    private GameObject posReadyFood2;
    [SerializeField]
    private GameObject posReadyFood3;
    [SerializeField]
    private GameObject posTableOrder;
    [SerializeField]
    private GameObject posCoffee;
    [SerializeField]
    private GameObject posSink;

    PossiblePositionDirections tableForClient0;
    PossiblePositionDirections tableForClient1;
    PossiblePositionDirections tableForClient2;
    PossiblePositionDirections tableForClient3;
    PossiblePositionDirections tableForClient4;
    PossiblePositionDirections tableForClient5;
    PossiblePositionDirections tableForClient6;
    PossiblePositionDirections tableOrder;
    PossiblePositionDirections tableSink;
    PossiblePositionDirections tableCoffee;
    PossiblePositionDirections readyFood0;
    PossiblePositionDirections readyFood1;
    PossiblePositionDirections readyFood2;
    PossiblePositionDirections readyFood3;
    PossiblePositionDirections startPlayer;
    List <PossiblePositionDirections> listPos = new List<PossiblePositionDirections>(10);      //все контрольные точки 
    Queue<GameObject> way;                   //путь

    void Start()
    {
        way = new Queue<GameObject>();
        listPos = new List<PossiblePositionDirections>();
        tableForClient0 = new PossiblePositionDirections(posTableForClient0, posTableForClient1, posTableForClient6, posTableForClient3, posSink, posStartPlayer);
        listPos.Add(tableForClient0);
        tableForClient1 = new PossiblePositionDirections(posTableForClient1, posTableForClient0, posTableForClient4, posTableForClient2);
        listPos.Add(tableForClient1);
        tableForClient2 = new PossiblePositionDirections(posTableForClient2, posTableForClient1, posTableForClient5, posTableForClient3);
        listPos.Add(tableForClient2);
        tableForClient3 = new PossiblePositionDirections(posTableForClient3, posTableForClient2, posTableForClient5, posTableForClient0, posStartPlayer, posCoffee);
        listPos.Add(tableForClient3);
        tableForClient4 = new PossiblePositionDirections(posTableForClient4, posTableForClient1, posTableForClient5);
        listPos.Add(tableForClient4);
        tableForClient5 = new PossiblePositionDirections(posTableForClient5, posTableForClient2, posTableForClient3, posTableForClient4, posTableForClient6);
        listPos.Add(tableForClient5);
        tableForClient6 = new PossiblePositionDirections(posTableForClient6, posTableForClient5, posTableForClient0, posStartPlayer);
        listPos.Add(tableForClient6);
        tableSink = new PossiblePositionDirections(posSink, posTableForClient0, posStartPlayer);
        listPos.Add(tableSink);
        tableOrder = new PossiblePositionDirections(posTableOrder, posStartPlayer);
        listPos.Add(tableOrder);
        tableCoffee = new PossiblePositionDirections(posCoffee, posTableForClient3, posStartPlayer);
        listPos.Add(tableCoffee);
        readyFood0 = new PossiblePositionDirections(posReadyFood0, posStartPlayer);
        listPos.Add(readyFood0);
        readyFood1 = new PossiblePositionDirections(posReadyFood1, posStartPlayer);
        listPos.Add(readyFood1);
        readyFood2 = new PossiblePositionDirections(posReadyFood2, posStartPlayer);
        listPos.Add(readyFood2);
        readyFood3 = new PossiblePositionDirections(posReadyFood3, posStartPlayer);
        listPos.Add(readyFood3);
        readyFood3 = new PossiblePositionDirections(posReadyFood3, posStartPlayer);
        listPos.Add(readyFood3);
        startPlayer = new PossiblePositionDirections(posStartPlayer, posSink, posTableOrder, posReadyFood0, posReadyFood1, posReadyFood2, posReadyFood3, posCoffee, posTableForClient0, posTableForClient3, posTableForClient6);
        listPos.Add(startPlayer);
        // Queue<GameObject> test = GetRoute(posTableForClient1, posTableForClient3);
    }


    public Queue<GameObject> GetRoute(GameObject startPosition , GameObject endPosition)
    {
        if (startPosition == endPosition) return null;
        PossiblePositionDirections findPos = FindPosition(startPosition);

        if (findPos.position != null)
        {
            while(findPos.position != endPosition)
            {
                if (findPos.position == null) break;
                float distanceBuf = Vector3.Distance(findPos.nextPosition[0].transform.position, endPosition.transform.position);
                GameObject gameObjectBuf = null;
                foreach (GameObject i in findPos.nextPosition)
                {
                    float distNew = Vector3.Distance(i.transform.position, endPosition.transform.position);
                    if (distNew <= distanceBuf)
                    {
                        distanceBuf = distNew;
                        gameObjectBuf = i;
                    }
                }
                way.Enqueue(gameObjectBuf);
                findPos = FindPosition(gameObjectBuf);

                if (way.Count > 30) { Debug.LogError("Cлишком большой маршрут "); return null; }       
            }
            return way;
        }


        else
        {
            return null;
        }
            

    }

    private PossiblePositionDirections FindPosition(GameObject pos)
    {
        foreach (PossiblePositionDirections i in listPos)
        {
            if (i.position == pos)
            {
                return i;
 
            }
        }
        return null;
    }
}
