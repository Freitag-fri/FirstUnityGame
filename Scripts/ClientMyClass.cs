using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusClient {createClient, setClient, createOrder, readyOrder, eat, waitPay, leaveClient };

public class ClientMyClass : MonoBehaviour
{
    //const int ArrStatusClient = 6;
    public delegate void moveClient(int i);
    static public event moveClient moveClientEv;

    static public void MoveClient(int i)
    {
        moveClientEv(i);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
