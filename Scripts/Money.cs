using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static int MoneyPlayer { get; private set; }

    public static void SetClient()
    {
        MoneyPlayer += 10;
    }

    public static void TakeOrder()
    {
        MoneyPlayer += 5;
    }

    public static void SetFood()
    {
        MoneyPlayer += 15;
    }

    public static void TakeManey()
    {
        MoneyPlayer += 30;
    }

    public static void ClearPlate()
    {
        MoneyPlayer += 5;
    }
}