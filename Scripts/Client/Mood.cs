using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mood
{
    public bool status;
    private int moodOriginal;
    private float time;
    public ColorMood colorMood;
    public int maxValue;
    /*
    public int MoodProportion
    {
        get { return moodOriginal / 12;  }
       // set { moodOriginal = value; }
    }
    */

    public int MoodOriginal
    {
        get { return moodOriginal; }
        // set { moodOriginal = value; }
    }

    public void AddMood(int value)
    {
        moodOriginal += value;
        if(moodOriginal > maxValue)
        {
            moodOriginal = maxValue;
        }
    }

    public void LowerMood()
    {
        time += Time.deltaTime;
        if (status && time > 1)
        {
            moodOriginal -= 1;
            time = 0;
        }
    }

    public Mood()
    {
        maxValue = 100;
        colorMood = new ColorMood(Color.green, Color.yellow, Color.red, maxValue);
        moodOriginal = maxValue;
        status = true;
    }
}


public class ColorMood
{
    Color colorStart;
    Color colorFinish;
    Color colorMedium;
    float maxValue = 0;

    public ColorMood( Color colorFinish, Color colorMedium,  Color colorStart, float maxValue)
    {
        this.colorStart = colorStart;
        this.colorFinish = colorFinish;
        this.colorMedium = colorMedium;
        this.maxValue = maxValue/2;
    }

    public  Color ReturnColor(float time)
    {
        float r;
        float g;
        float b;
        if (time > maxValue)
        {
            time -= maxValue;
            r = Interpolation(0, maxValue, time, colorMedium.r, colorFinish.r);
            g = Interpolation(0, maxValue, time, colorMedium.g, colorFinish.g);
            b = Interpolation(0, maxValue, time, colorMedium.b, colorFinish.b);
        }
        else
        {
            r = Interpolation(0, maxValue, time, colorStart.r, colorMedium.r);
            g = Interpolation(0, maxValue, time, colorStart.g, colorMedium.g);
            b = Interpolation(0, maxValue, time, colorStart.b, colorMedium.b);            
        }
        return new Color(r, g, b);
        
    }


    private float Interpolation(float x1, float x2, float x, float y1, float y2)
    {
        return y1 + ((y2 - y1) / (x2 - x1)) * (x - x1); ;
    }
}