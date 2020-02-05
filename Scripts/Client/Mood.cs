using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mood
{
    public bool status;
    private int moodOriginal;
    private float time;
    public ColorMood colorMood;
    public int MoodProportion
    {
        get { return moodOriginal / 12;  }
       // set { moodOriginal = value; }
    }

    public int MoodOriginal
    {
        get { return moodOriginal; }
        // set { moodOriginal = value; }
    }

    public void AddMood(int value)
    {
        moodOriginal += value * 12;
        if(moodOriginal > 120)
        {
            moodOriginal = 120;
        }
    }

    public void LowerMood()
    {
        time += Time.deltaTime;
        if (status && time > 1)
        {
            moodOriginal -= 2;
            time = 0;
        }
    }

    public Mood()
    {
        colorMood = new ColorMood( Color.green,  Color.red, 120);
        moodOriginal = 120;
        status = true;
    }
}


public class ColorMood
{
     Color colorStart;
     Color colorFinish;
    float maxValue = 0;

    public ColorMood( Color colorFinish,  Color colorStart, float maxValue)
    {
        this.colorStart = colorStart;
        this.colorFinish = colorFinish;
        this.maxValue = maxValue;
    }

    public  Color ReturnColor(float time)
    {
        float r = Interpolation(0, maxValue, time, colorStart.r, colorFinish.r);
        float g = Interpolation(0, maxValue, time, colorStart.g, colorFinish.g);
        float b = Interpolation(0, maxValue, time, colorStart.b, colorFinish.b);
        return new Color(r, g, b);
        
    }


    private float Interpolation(float x1, float x2, float x, float y1, float y2)
    {
        return y1 + ((y2 - y1) / (x2 - x1)) * (x - x1); ;
    }
}