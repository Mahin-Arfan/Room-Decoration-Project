using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeScript : MonoBehaviour
{
    public int selectedObject = 0;
    public int childCount;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = 0;
        SelectObject();
        childCount = transform.childCount;
        childCount = childCount - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectObject()
    {
        int i = 0;
        foreach (Transform obj in transform)
        {
            if (i == selectedObject)
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void ChangedObject()
    {
        if(selectedObject+1 <= childCount)
        {
            selectedObject++;
            SelectObject();
        }
        else
        {
            selectedObject = 0;
            SelectObject();
        }
    }
}
