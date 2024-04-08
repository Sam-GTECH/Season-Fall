using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
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

    private void HandleTilePlacing(Vector3Int pos)
    {
        //for(int i=-1; i<2; i++)
        //{
        //    for (int j=-1; j<2; j++)
        //    {
        //        pos.x += i;
        //        pos.y += j;
        //        string strTile = map.GetTile<Tile>(map.WorldToCell(pos)).ToString();
        //    }
        //}
        //get player element
        string strTile = map.GetTile<UnityEngine.Tilemaps.Tile>(pos).ToString();
        //Debug.Log(map.GetTile<Tile>(pos).ToString());
        char number = strTile[strTile.Length-29];
        Debug.Log(number);
        int converted = number - '0';
        //converted++;
        //int converted = Convert.ToInt16(number);
        //Debug.Log(converted);
        //converted++;
        //number = Convert.ToChar(converted);
        //strTile = strTile.Remove(strTile.Length - 1, 1) + number;
        map.SetTile(pos, icePalette[converted]);
    }
}
