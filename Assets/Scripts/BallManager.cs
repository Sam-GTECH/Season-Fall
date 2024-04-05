using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private readonly float increase = 0.01f;
    public float boom = 0;
    public GameObject scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb.IsAwake())
                rb.Sleep();
            GetComponent<Transform>().position += new Vector3(0, increase, 0);
            scale.GetComponent<Transform>().localScale += new Vector3(boom, 0, 0);
            boom += 0.35f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb.IsSleeping())
                rb.WakeUp();
            rb.AddForce(new Vector2(boom, boom/4));
            boom = 0;
        }
    }
}
