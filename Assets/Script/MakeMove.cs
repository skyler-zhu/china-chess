using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMove : MonoBehaviour
{
    internal move moveObj;
    internal logic logicObj;
    // Start is called before the first frame update
    void Start()
    {
        moveObj = gameObject.GetComponentInParent<move>();
        logicObj = GameObject.Find("Logic").GetComponent<logic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        CleanAllIndicator();
        var transform1 = transform.position;
        moveObj.MakeMove((int) transform1.x, (int) transform1.z);
    }

    private void CleanAllIndicator()
    {
        GameObject[] indicators = GameObject.FindGameObjectsWithTag("Indicator");
        foreach (GameObject i in indicators)
        {
            Destroy(i);
        }
    }
}
