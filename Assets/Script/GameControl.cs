using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    internal bool screenLock = false;
    internal bool moving_cm = false;
    internal Dictionary<(int, int), (int, string)> data = new Dictionary<(int, int), (int, string)>
    {
        {(0, 0), (0, "Ju")}, {(1, 0), (0, "Ma")}, {(2, 0), (0, "Xiang")}, {(3, 0), (0, "Shi")}, {(4, 0), (0, "Jiang")},
        {(5, 0), (0, "Shi")}, {(6, 0), (0, "Xiang")}, {(7, 0), (0, "Ma")}, {(8, 0), (0, "Ju")}, {(1, 2), (0, "Pao")},
        {(7, 2), (0, "Pao")}, {(0, 3), (0, "Zu")}, {(2, 3), (0, "Zu")}, {(4, 3), (0, "Zu")}, {(6, 3), (0, "Zu")},
        {(8, 3), (0, "Zu")},
        {(0, 9), (1, "Ju")}, {(1, 9), (1, "Ma")}, {(2, 9), (1, "Xiang")}, {(3, 9), (1, "Shi")}, {(4, 9), (1, "Jiang")},
        {(5, 9), (1, "Shi")}, {(6, 9), (1, "Xiang")}, {(7, 9), (1, "Ma")}, {(8, 9), (1, "Ju")}, {(1, 7), (1, "Pao")},
        {(7, 7), (1, "Pao")}, {(0, 6), (1, "Zu")}, {(2, 6), (1, "Zu")}, {(4, 6), (1, "Zu")}, {(6, 6), (1, "Zu")},
        {(8, 6), (1, "Zu")}
    };
    internal static bool Turn = true;

    internal Dictionary<(int, int), (int, string)> GetData()
    {
        return data;
    }

    internal bool GetTurn()
    {
        return Turn;
    }

    internal void ChangeTurn()
    {
        Turn = !Turn;
    }

    internal bool GetScreenLock()
    {
        return screenLock;
    }
    
    internal void ChangeScreenLock()
    {
        screenLock = !screenLock;
    }

    internal void StarMoving()
    {
        moving_cm = true;
    }
    
    internal void EndMoving()
    {
        moving_cm = false;
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
