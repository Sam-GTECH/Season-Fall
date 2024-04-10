using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetManager : MonoBehaviour
{
    public bool isGrounded = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "front")
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "front")
            isGrounded = false;
    }
}
