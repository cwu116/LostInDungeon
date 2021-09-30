using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<String, GameObject> itemDict;
    Dictionary<float, float> positionDict;
    void Start()
    {
        itemDict = new Dictionary<String, GameObject>();
        positionDict = new Dictionary<float, float>();
        itemDict.Add("RedPot", Resources.Load("RedPot") as GameObject);
        itemDict.Add("GoldPot", Resources.Load("GoldPot") as GameObject);
        itemDict.Add("GreyPot", Resources.Load("GreyPot") as GameObject);
        GenerateItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateItem()
    {
       

        int randNum = UnityEngine.Random.Range(1, 101); 
        if( randNum <= 10)
        {
            randNum = 1;
        }
        else if (randNum > 10 && randNum <= 40 )
        {
            randNum = 2;
        }
        else if (randNum > 40 && randNum <= 75)
        {
            randNum = 3;
        }
        else if (randNum > 75 && randNum <= 85)
        {
            randNum = 4;
        }
        else if (randNum > 85 && randNum <= 90)
        {
            randNum = 5;
        }
        else if (randNum > 90 && randNum <= 100)
        {
            randNum = 0;
        }


        for (int i = 0; i < randNum; i++)
        {
            int rand = UnityEngine.Random.Range(1, 31);
            float randPositionX = 0;
            float randPositionY = 0;
            do
            {
                randPositionX = UnityEngine.Random.Range(-85 + transform.position.x * 10, 86 + transform.position.x * 10);
                randPositionY = UnityEngine.Random.Range(-40 + transform.position.y * 10, 41 + transform.position.y * 10);
                if (Mathf.Abs(randPositionX) % 10 == 0)
                {
                    randPositionX = randPositionX / 10 + 0.5f;

                }
                else if (Mathf.Abs(randPositionX) % 5 == 0)
                {
                    randPositionX = randPositionX / 10;
                }
                else
                {
                    randPositionX = Mathf.Round(randPositionX / 10) + 0.5f;
                }

                if (Mathf.Abs(randPositionY) % 5 == 0)
                {
                    randPositionY = randPositionY / 10;
                }
                else
                {
                    randPositionY = Mathf.Round(randPositionY / 10);
                }
                if(!positionDict.ContainsKey(randPositionX))
                positionDict.Add(randPositionX, randPositionY);
            } while (positionDict.ContainsKey(randPositionX) && positionDict[randPositionX] == randPositionY);

            if (rand <= 10)
            {
                GameObject item = Instantiate(itemDict["RedPot"], transform);
                item.transform.position = new Vector3(randPositionX, randPositionY, item.transform.position.z);
            }

            else if (rand > 10 && rand <= 20)
            {
                GameObject item = Instantiate(itemDict["GoldPot"], transform);
                item.transform.position = new Vector3(randPositionX, randPositionY, item.transform.position.z);
            }

            else if (rand > 20 && rand <= 30)
            {
                GameObject item = Instantiate(itemDict["GreyPot"], transform);
                item.transform.position = new Vector3(randPositionX, randPositionY, item.transform.position.z);
            }

        }




    }
}
