using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    internal logic logicObj;
    internal GameControl gameControl;
    public string type;
    public bool side;
    internal bool turn;
    internal CreateBoard createBoard;
    logic.Chessman cs;
    internal float moveDu = 0.5f;
    internal float speed = 3f;
    internal Vector3 newPosi;
    internal Vector3 oldPosi;
    internal bool moving = false;
    internal float disFix = 0.15f;
    GameObject dead;
    internal float remainTime = 1.5f;

    internal void SetType(string t)
    {
        type = t;
    }
    
    internal void SetSide(bool s)
    {
        side = s;
    }

    // Start is called before the first frame update
    void Start()
    {
        logicObj = GameObject.Find("Logic").GetComponent<logic>();
        createBoard = GameObject.Find("Spawner").GetComponent<CreateBoard>();
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        turn = gameControl.GetTurn();
        switch (type)
        {
            case "Zu":
                Vector3 localPosition = transform.localPosition;
                cs = new logic.Zu(((int, int)) (localPosition.x, localPosition.z), side);
                break;
            case "Ju":
                Vector3 localPosition1 = transform.localPosition;
                cs = new logic.Ju(((int, int)) (localPosition1.x, localPosition1.z), side);
                break;
            case "Pao":
                Vector3 localPosition2 = transform.localPosition;
                cs = new logic.Pao(((int, int)) (localPosition2.x, localPosition2.z), side);
                break;
            case "Ma":
                Vector3 localPosition3 = transform.localPosition;
                cs = new logic.Ma(((int, int)) (localPosition3.x, localPosition3.z), side);
                break;
            case "Xiang":
                Vector3 localPosition4 = transform.localPosition;
                cs = new logic.Xiang(((int, int)) (localPosition4.x, localPosition4.z), side);
                break;
            case "Shi":
                Vector3 localPosition5 = transform.localPosition;
                cs = new logic.Shi(((int, int)) (localPosition5.x, localPosition5.z), side);
                break;
            case "Jiang":
                Vector3 localPosition6 = transform.localPosition;
                cs = new logic.Jiang(((int, int)) (localPosition6.x, localPosition6.z), side);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector3 v = (newPosi - oldPosi) / moveDu;
            //Vector3.Normalize(v);
            //v = v * speed;
            remainTime -= Time.deltaTime;
            if (remainTime >= moveDu / 2)
            {
                transform.localPosition += new Vector3(0, 0.3f, 0) * Time.deltaTime;
            }
            else
            {
                transform.localPosition -= new Vector3(0, 0.3f, 0) * Time.deltaTime;
            }

            // if (Math.Abs(v.x) <= 0.5f || Math.Abs(v.z) <= 0.5f)
            // {
            //     v = v * 3f;
            // }

            //Vector3 v = (newPosi - oldPosi) * speed;
            transform.localPosition += v * Time.deltaTime;
            Vector3 r = transform.localPosition - newPosi;
            if (Math.Abs(r.x) <= disFix && Math.Abs(r.z) <= disFix)
            {
                transform.localPosition = newPosi;
                moving = false;
                if (dead)
                {
                    MoveToDead(dead);
                    dead = null;
                }
                gameControl.ChangeTurn();
            }
        }
    }

    internal void MakeMove(int xP, int zP)
    {
        newPosi = new Vector3(xP, 2, zP);
        oldPosi = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
        (int x, int z) r = cs.MakeMove(xP, zP);
        if (r.x != -1 && r.z != -1)
        {
            string deadCm = r.x.ToString() + " " + r.z.ToString();
            dead = GameObject.Find(deadCm);
        }
        gameControl.StarMoving();
        moving = true;
        transform.name = xP.ToString() + " " + zP.ToString();
    }

    private void MoveToDead(GameObject dead)
    {
        Destroy(dead);
    }

    private void OnMouseDown()
    {
        if (!gameControl.GetScreenLock())
        {
            CleanAllIndicator();
            turn = gameControl.GetTurn();
            if (turn == side)
            {
                List<(int, int)> moves = cs.GetPossibleMove();
                createBoard.CreateIndicator(moves, transform);
            }
            else
            {
                print("sit");
            }
        }
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
