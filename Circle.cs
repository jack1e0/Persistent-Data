using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basic drag-and-drop script.
 * Can be instantiated by CircleSpawner
 */

public class Circle : MonoBehaviour
{
    private float distX;
    private float distY;
    private bool isHeld = false;

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            drag();
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        distX = mousePos.x - this.transform.localPosition.x;
        distY = mousePos.y - this.transform.localPosition.y;

        isHeld = true;
    }

    private void OnMouseUp()
    {
        isHeld = false;
    }

    private void drag()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        this.gameObject.transform.localPosition = new Vector3(mousePos.x - distX, mousePos.y - distY, 0);
    }
    /* OLD VERSION
    public void SaveData(ref CircleData data)
    {

        data.position[0] = this.transform.localPosition.x;
        data.position[1] = this.transform.localPosition.y;

    }

    public void LoadData(CircleData data)
    {
        
        this.transform.localPosition = new Vector3(data.position[0], data.position[1], 0);
    }*/
}
