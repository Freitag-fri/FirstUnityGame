using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2.0f, 40, 100, 20), Money.MoneyPlayer.ToString());
    }
}
