using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoomInMap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform wall;
    public Transform wall2;
    void Start()
    {
        wall = transform.Find("Map_Rooms_1");
        wall2 = transform.Find("Grid");
        wall.gameObject.SetActive(false);
        wall2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wall.gameObject.SetActive(true);
            wall2.gameObject.SetActive(true);
        }
    }
}
