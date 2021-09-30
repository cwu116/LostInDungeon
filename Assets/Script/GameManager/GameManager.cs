using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public MSKEffect mskEffect;
    bool playerDead;
    // Start is called before the first frame update
    void Start()
    {
        mskEffect.TileSize = 80;
        playerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDead)
        {
            if (mskEffect.TileSize > 1)
            {
                mskEffect.TileSize--;
            }
        }
        else
        {
            if (mskEffect.TileSize < 100)
            {
                mskEffect.TileSize++;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

       
    }

    public void PlayerDead()
    {
        playerDead = true;
    }
}
