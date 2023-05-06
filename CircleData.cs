using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CircleData 
{
    public List<Pos> positions;

    public CircleData()
    {
        positions = new List<Pos>();
    }
}

[System.Serializable]
public class Pos
{
    public float x;
    public float y;

    public Pos(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
