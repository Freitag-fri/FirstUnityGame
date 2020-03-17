using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coffe : MonoBehaviour
{
    public GameObject coffe;
    public float time;
    public float maxTime;
    ColorMood color;
    [SerializeField]
    private Canvas canvas;
    private GameObject slider;

    void Start()
    {
        maxTime = 6;
        color = new ColorMood(Color.green, Color.yellow, Color.red, maxTime);

        canvas = Instantiate(canvas);
        canvas.transform.SetParent(transform);
        slider = canvas.transform.Find("Slider").gameObject;
        slider.GetComponent<Slider>().maxValue = maxTime;

        Vector2 coordinateBox;
        GameObject pos = transform.Find("PositionProcess").gameObject;
        coordinateBox = Camera.main.WorldToScreenPoint(new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z));
        slider.transform.position = new Vector2(coordinateBox.x, coordinateBox.y);
    }

    // Update is called once per frame
    void Update()
    {
        Slider();
        if (transform.GetComponent<SavedItem>().objOnTable == null)
        {
            time += Time.deltaTime;
            if (time > maxTime)
            {
                transform.GetComponent<NewObject>().CreateObj(coffe);
                time = 0;
            }
        } 
    }

    private void Slider()
    {
        slider.GetComponent<Slider>().value = time;
        slider.GetComponentInChildren<Image>().color = color.ReturnColor(time);
    }

    /*

    private void OnGUI()
    {
        if(transform.GetComponent<SavedItem>().objOnTable == null)
        {
            Vector2 coordinateBox;
            GameObject pos = transform.Find("PositionProcess").gameObject;
            coordinateBox = Camera.main.WorldToScreenPoint(new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z));
            coordinateBox = new Vector2(coordinateBox.x, Screen.height - coordinateBox.y);

            //Vector2 coordinateBox = new Vector2(left - 15, Screen.height - (top + (distancY * placeInLine) + 10));

            GUI.Box(new Rect(coordinateBox.x, coordinateBox.y, 100, 15), "");



            float newMood = time * 10 / maxTime;        //10 - макс кол делений
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color.ReturnColor(time));
            texture.Apply();
            GUIStyle myBoxStyle2 = new GUIStyle(GUI.skin.box);
            myBoxStyle2.normal.background = texture;

            for (int i = 0; i < newMood; i++)
            {
                GUI.Box(new Rect(coordinateBox.x + (i * 7 + 3 * i), coordinateBox.y, 7, 13), "", myBoxStyle2);
            }
        }
        
    }
    */
}
