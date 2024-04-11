using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    public bool followX = false;
    public bool followY = false;

    public float maxX = 1f;
    public float minX = 51f;

    public float maxY = 4.4f;
    public float minY = -3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followX)
        {
            Vector3 pos = transform.position;
            Debug.Log(GameObject.FindGameObjectWithTag("main").transform.position.x +"\t"+ minX + "\t" + minY + "\t" + Mathf.Clamp(GameObject.FindGameObjectWithTag("main").transform.position.x, minX, maxX));
            pos.x = Mathf.Clamp(GameObject.FindGameObjectWithTag("main").transform.position.x, minX, maxX);
            Debug.Log(pos.x);
            transform.position = pos;
        }
        if (followY)
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(GameObject.FindGameObjectWithTag("main").transform.position.y, minY, maxY);
            transform.position = pos;
        }
    }
}
