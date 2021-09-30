using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject up, down, left, right;
    public bool roomLeft, roomRight, roomUp, roomDown;
    public int col, row;
    public float x, y;
    public int stepToStart;
    public Text step;
    public int doorCount = 0;
    public string roomType;

    public Image fade;
    public float fadeTime;
    public float apha;

    public GameObject background;
    // Start is called before the first frame update
    void Awake()
    {
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
    }
    private void Update()
    {
        if(apha > 0)
        {
            apha -= Time.deltaTime;
            fade.color = new Color(0, 0, 0, apha);
        }
    }
    public void SetDoor()
    {
        up.SetActive(roomUp);
        down.SetActive(roomDown);
        left.SetActive(roomLeft);
        right.SetActive(roomRight);
        if (roomUp)
        {
            doorCount++;
        }
        if (roomDown)
        {
            doorCount++;
        }
        if (roomLeft)
        {
            doorCount++;
        }
        if (roomRight)
        {
            doorCount++;
        }
    }

    public void CountStep()
    {
        stepToStart = Mathf.Abs(col - 5) + Mathf.Abs(row - 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            CameraMove.cameramove.target = this.transform;
            apha = 1;
            fade.color = new Color(0, 0, 0, apha);
            background.SetActive(true);
        }
    }

  


}
