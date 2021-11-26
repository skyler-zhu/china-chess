using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    private List<List<(int s, string t)>> board;
    internal GameControl gameControl;
    private Dictionary<(int, int), (int, string)> data;
    public GameObject chessMan1;
    public GameObject chessMan2;
    public GameObject indicator;
    public GameObject Side1Folder;
    public GameObject Side2Folder;
    
    internal logic logicObj;
    // Start is called before the first frame update

    internal void CreateIndicator(List<(int, int)> moves, Transform cm)
    {
        foreach (var (x, z) in moves)
        {
            Vector3 p = new Vector3(x, 2, z);
            Quaternion r = Quaternion.identity;
            GameObject indi;
            indi = Instantiate(indicator, p, r, cm);
        }
    }

    internal void CreateChessMan()
    {
        foreach (KeyValuePair<(int, int), (int, string)> item in data)
        {
            (int x, int z) key = item.Key;
            (int s, string t) value = item.Value;
            Vector3 sp = new Vector3(key.x, 2, key.z);
            Quaternion q = Quaternion.identity;
            GameObject cm;
            if (value.s == 0)
            {
                chessMan1.GetComponent<move>().SetType(value.t);
                chessMan1.GetComponent<move>().SetSide(true);
                cm = Instantiate(chessMan1, sp, q, Side1Folder.transform);
            }
            else
            {
                chessMan2.GetComponent<move>().SetType(value.t);
                chessMan2.GetComponent<move>().SetSide(false); 
                cm = Instantiate(chessMan2, sp, q, Side2Folder.transform);
            }
            cm.name = key.x.ToString() + " " + key.z.ToString();
        }
    }

    void Start()
    {
        logicObj = GameObject.Find("Logic").GetComponent<logic>();
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        data = gameControl.GetData();
        CreateChessMan();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
