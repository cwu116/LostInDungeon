using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    Transform player;
    Transform playerCollect;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCollect = GameObject.FindGameObjectWithTag("PlayerCollectUI").transform;
    }

    // Update is called once per frame
    void Update()   
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenPosition.z;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        transform.position = mousePositionInWorld;
        if(player.position.x < transform.position.x && player.localScale.x == -1)
        {
            player.localScale = new Vector3(1,1,1);
            playerCollect.localScale = new Vector3(1, 1, 1);
        }

        if (player.position.x > transform.position.x && player.localScale.x == 1)
        {
            player.localScale = new Vector3(-1, 1, 1);
            playerCollect.localScale = new Vector3(1, 1, -1);
        }
    }
}
