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
        HandleTilePlacing(pos);
    }
    
    void PlaceTile(Tile tile, Vector3Int pos)
    {
        map.SetTile(pos, tile);
    }

    public void HandleTilePlacing(Vector3 pos)
    {
        Vector3Int cellPos = map.WorldToCell(cam.ScreenToWorldPoint(pos));
        Vector3Int tPos = new Vector3Int(0, 0, 0);
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                tPos.x = cellPos.x + i;
                tPos.y = cellPos.y + j;
                string strTile = map.GetTile<UnityEngine.Tilemaps.Tile>(tPos).ToString();
                char number = strTile[strTile.Length - 29];
                Debug.Log(number);
                int converted = number - '0';
                map.SetTile(tPos, icePalette[converted]);
            }
        }
    }
}
