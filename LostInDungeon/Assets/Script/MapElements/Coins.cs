using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    KnightController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (gameObject.tag) {

                case "Copper":
                    player.coinNum += 1;
                    break;
                case "Sliver":
                    player.coinNum += 5;
                    break;
                case "Gold":
                    player.coinNum += 10;
                    break;

            }
            Destroy(this.gameObject);
        }
    }
}
