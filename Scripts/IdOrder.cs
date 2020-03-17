using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdOrder : MonoBehaviour
{
    public int id = 0;
    int y = -25;
    void Start()
    {
        id = transform.GetComponent<IdTable>().id;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.green;
        //Vector3 bufCamera = Camera.main.WorldToScreenPoint(transform.Find("NumberTable").position);
        Vector3 bufCamera = Camera.main.WorldToScreenPoint(this.transform.position);
        GUI.Label(new Rect(bufCamera.x, Screen.height - bufCamera.y + y, 100, 20), id.ToString(), style);
    }
}
