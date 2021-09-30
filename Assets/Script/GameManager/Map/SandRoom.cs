using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<KnightController>().playerSpeed = 128;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<KnightController>().playerSpeed = 160;
        }
    }
}
