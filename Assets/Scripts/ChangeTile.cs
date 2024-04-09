using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using static UnityEditor.PlayerSettings;
using System.Linq;
//using UnityEngine.WSA;

public class ChangeTile : MonoBehaviour
{
    public Tilemap map;
    public Camera cam;
    //public GameObject player;
    public TileBase[] firePalette;
    public TileBase[] icePalette;
    public TileBase[] windPalette;
    public TileBase[] groundPalette;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = map.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));
    }

    public void HandleTilePlacing(Vector3 pos)
    {
        Debug.Log("precompiled pos  " + pos);
        Vector3Int cellPos = map.WorldToCell(cam.ScreenToWorldPoint(pos));
        Debug.Log("postcompiled pos  " + pos);
        Vector3Int tPos = new Vector3Int(0, 0, 0);
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                tPos.x = cellPos.x +i;
                tPos.y = cellPos.y +j;
                if (map.GetTile<UnityEngine.Tilemaps.Tile>(tPos))
                {
                    string number = "";
                    string strTile = map.GetTile<UnityEngine.Tilemaps.Tile>(tPos).ToString();
                    if(strTile.Length == 37)
                    {
                        number+=strTile[strTile.Length - 29];
                    }else if(strTile.Length == 38)
                    {
                        number+=strTile[strTile.Length - 30];
                        number+=strTile[strTile.Length - 29];
                    }
                    int converted = Convert.ToInt32(number.ToString());
                    map.SetTile(tPos, icePalette[converted]);
                }
                else { Debug.Log("miss tile"); }
                
            }
        }
    }
}
