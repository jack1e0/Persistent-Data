using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Create new circles upon pressing button
 * Inherits methods from IDataSaver
 */

public class CircleSpawner : MonoBehaviour, IDataSaver
{
    public GameObject originalCircle;

    public List<GameObject> circles;

    private void Start()
    {
        circles = new List<GameObject>();
        circles.Add(originalCircle);
    }

    //on button press
    public void createCircle()
    {
        GameObject newObj = Instantiate(originalCircle, new Vector3(originalCircle.transform.position.x + 2, 
            originalCircle.transform.position.y + 2, 0), originalCircle.transform.rotation);

        circles.Add(newObj);

        originalCircle = newObj;
    }

    //on load game
    private void loadCircles(Pos p)
    {
        GameObject newObj = Instantiate(originalCircle, new Vector3(p.x,
            p.y, 0), originalCircle.transform.rotation);

        circles.Add(newObj);
    }
    
    public void SaveData(ref CircleData data)
    {
        data.positions.Clear();
        
        foreach (GameObject circle in circles)
        {
            Pos p = new Pos(circle.transform.localPosition.x, circle.transform.localPosition.y);
            data.positions.Add(p);
        }

    }

    public void LoadData(CircleData data)
    {
        foreach (Pos p in data.positions)
        {
            loadCircles(p);
        }

        circles.Remove(originalCircle);
    }
    
}
