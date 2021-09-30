using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
   
    public Transform prefabManager;
    [Header("房间信息")]
    public Direction direction;
    public int roomNumber;
    public Color startColor, endColor;
    
    [Header("位置控制")]
    public Transform curPosition;
    public float xOffset, yOffset;
    bool FindEmptyPosition = false;
    public Room[,] rooms = new Room[11,11];
    public int curRow = 5;
    public int curCol = 5;

    [Header("最远房间")]
    public List<Room> finalRooms = new List<Room>();  //候选成特殊房间的房间列表
    public Dictionary<string ,GameObject> walls = new Dictionary<string, GameObject>();

    [Header("特殊房间")]
    public Room finalRoom;
    public Room treasureRoom;
    public Room shopRoom;
    public Room thronesRoom;

    [Header("房间预制体")]
    public GameObject roomPrefab;
    public GameObject startRoomPrefab;
    public GameObject finalRoomPrefab;
    public GameObject shopRoomPrefab;
    public GameObject treasureRoomPrefab;
    public GameObject treasureRoomPrefab1;
    public GameObject thronesRoomPrefab;

    public WallType wall;
    // Start is called before the first frame update

    void Start()
    {
 
        GenerateRooms();
        foreach (var room in rooms)
        {

            if (room != null)
            {
                SetDoorDirection(room);
                room.SetDoor();
                room.CountStep();
                SetWalls(room);
            }
        }
        SelectSpecialRoom(finalRoom,"final");
        SelectSpecialRoom(treasureRoom, "treasure");
        SelectSpecialRoom(treasureRoom, "treasure1");
        SelectSpecialRoom(thronesRoom, "thrones");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(0);
        }
    }

    public enum Direction
    {
       Left,Right, Up, Down
    }
    private void GenerateRooms()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            if (curRow == 5 && curCol == 5)
            {
                rooms[curRow, curCol] = (Instantiate(startRoomPrefab, curPosition.position, Quaternion.identity, prefabManager)).GetComponent<Room>();
            }
            else
            {
                rooms[curRow, curCol] = (Instantiate(roomPrefab, curPosition.position, Quaternion.identity, prefabManager)).GetComponent<Room>();

            }
            rooms[curRow, curCol].row = curRow;
            rooms[curRow, curCol].col = curCol;
            rooms[curRow, curCol].x = curPosition.position.x;
            rooms[curRow, curCol].y = curPosition.position.y;
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
      
        while (!FindEmptyPosition)
        {
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.Left:
                    if (curCol - 1 >= 0)
                    {
                        if (rooms[curRow, curCol - 1] == null)
                        {
                            curPosition.position += new Vector3(-xOffset, 0, 0);
                            curCol --;
                            FindEmptyPosition = true;
                        }
                    }
                    break;

                case Direction.Right:
                    if (curCol + 1 < rooms.GetLength(0))
                    {
                        if (rooms[curRow, curCol + 1] == null)
                        {
                            curPosition.position += new Vector3(xOffset, 0, 0);
                            curCol ++;
                            FindEmptyPosition = true;
                        }
                    }
                    break;

                case Direction.Up:
                    if (curRow - 1 >= 0)
                    {
                        if (rooms[curRow - 1, curCol] == null) {
                            curPosition.position += new Vector3(0, yOffset, 0);
                            curRow --;
                            FindEmptyPosition = true;
                        }
                    }
                    break;

                case Direction.Down:
                    if (curRow + 1 < rooms.GetLength(0))
                    {
                        if (rooms[curRow + 1, curCol] == null)
                        {
                            curPosition.position += new Vector3(0, -yOffset, 0);
                            curRow ++;
                            FindEmptyPosition = true;
                        }
                    }
                    break;
            }
        }

        FindEmptyPosition = false;
    }

    private void SetDoorDirection(Room room)
    {
       if(room.col - 1 >= 0 && rooms[room.row, room.col - 1] != null)
        {
            room.roomLeft = true;
        }
     

        if (room.row - 1 >= 0 && rooms[room.row - 1, room.col] != null)
        {
            room.roomUp = true;
        }
       

        if (room.col + 1 < rooms.GetLength(0) && rooms[room.row, room.col + 1] != null)
        {
            room.roomRight = true;
        }
        

        if (room.row + 1 < rooms.GetLength(0) && rooms[room.row + 1, room.col ] != null)
        {
            room.roomDown= true;
        }
        
    }

    private void SelectSpecialRoom(Room specialRoom, string roomType)
    {
        foreach (var room in rooms)
        {

            if (room != null && room.stepToStart >= 3)
            {
                finalRooms.Add(room);
            }
        }
        int count = 0;
        while (true || count < 1000)
        {
            count++;
            int index = Random.Range(0, finalRooms.Count);
            specialRoom = finalRooms[index];
            if(specialRoom.row - 1 >= 0 && rooms[specialRoom.row - 1, specialRoom.col] == null)
            {
                finalRooms.Remove(specialRoom);
                Vector3 finalPosition = new Vector3(specialRoom.x, specialRoom.y + yOffset, 0);
                Room finalRoomNext;
                if (roomType == "final")
                {
                    finalRoomNext = Instantiate(finalRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure")
                {
                     finalRoomNext = Instantiate(treasureRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure1")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab1, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "thrones")
                {
                    finalRoomNext = Instantiate(thronesRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else
                {
                     finalRoomNext = Instantiate(roomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                specialRoom.roomUp = true;
                GameObject.Destroy(walls[specialRoom.row + "-" + specialRoom.col]);
                walls.Remove(specialRoom.row + "-" + specialRoom.col);
                specialRoom.doorCount = 0;
                specialRoom.SetDoor();
                SetWalls(specialRoom);
                finalRoomNext.roomDown = true;
                finalRoomNext.SetDoor();
                finalRoomNext.row = specialRoom.row - 1;
                finalRoomNext.col = specialRoom.col;
                finalRoomNext.x = finalPosition.x;
                finalRoomNext.y = finalPosition.y;
                rooms[finalRoomNext.row, finalRoomNext.col] = finalRoomNext;
                SetWalls(finalRoomNext);
                specialRoom = finalRoomNext;
                break;
            }

            if (specialRoom.row + 1 < rooms.GetLength(0) && rooms[specialRoom.row + 1, specialRoom.col] == null)
            {
                finalRooms.Remove(specialRoom);
                Vector3 finalPosition = new Vector3(specialRoom.x, specialRoom.y - yOffset, 0);
                Room finalRoomNext;
                if (roomType == "final")
                {
                    finalRoomNext = Instantiate(finalRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure1")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab1, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "thrones")
                {
                    finalRoomNext = Instantiate(thronesRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else
                {
                    finalRoomNext = Instantiate(roomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                specialRoom.roomDown = true;
                GameObject.Destroy(walls[specialRoom.row + "-" + specialRoom.col]);
                walls.Remove(specialRoom.row + "-" + specialRoom.col);
                specialRoom.doorCount = 0;
                specialRoom.SetDoor();
                SetWalls(specialRoom);
                finalRoomNext.roomUp = true;
                finalRoomNext.SetDoor();
                finalRoomNext.row = specialRoom.row + 1;
                finalRoomNext.col = specialRoom.col;
                finalRoomNext.x = finalPosition.x;
                finalRoomNext.y = finalPosition.y;
                rooms[finalRoomNext.row, finalRoomNext.col] = finalRoomNext;
                SetWalls(finalRoomNext);
                specialRoom = finalRoomNext;
                break;
            }

            if (specialRoom.col + 1 < rooms.GetLength(0) && rooms[specialRoom.row, specialRoom.col + 1] == null)
            {
                finalRooms.Remove(specialRoom);
                Vector3 finalPosition = new Vector3(specialRoom.x + xOffset, specialRoom.y, 0);
                Room finalRoomNext;
                if (roomType == "final")
                {
                    finalRoomNext = Instantiate(finalRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure1")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab1, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "thrones")
                {
                    finalRoomNext = Instantiate(thronesRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else
                {
                    finalRoomNext = Instantiate(roomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                specialRoom.roomRight = true;
                GameObject.Destroy(walls[specialRoom.row + "-" + specialRoom.col]);
                walls.Remove(specialRoom.row + "-" + specialRoom.col);
                specialRoom.doorCount = 0;
                specialRoom.SetDoor();
                SetWalls(specialRoom);
                finalRoomNext.roomLeft = true;
                finalRoomNext.SetDoor();
                finalRoomNext.row = specialRoom.row;
                finalRoomNext.col = specialRoom.col + 1;
                finalRoomNext.x = finalPosition.x;
                finalRoomNext.y = finalPosition.y;
                rooms[finalRoomNext.row, finalRoomNext.col] = finalRoomNext;
                SetWalls(finalRoomNext);
                specialRoom = finalRoomNext;
                break;
            }

            if (specialRoom.col - 1 >= rooms.GetLength(0) && rooms[specialRoom.row, specialRoom.col - 1] == null)
            {
                finalRooms.Remove(specialRoom);
                Vector3 finalPosition = new Vector3(specialRoom.x - xOffset, specialRoom.y, 0);
                Room finalRoomNext;
                if (roomType == "final")
                {
                    finalRoomNext = Instantiate(finalRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "treasure1")
                {
                    finalRoomNext = Instantiate(treasureRoomPrefab1, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else if (roomType == "thrones")
                {
                    finalRoomNext = Instantiate(thronesRoomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                else
                {
                    finalRoomNext = Instantiate(roomPrefab, finalPosition, Quaternion.identity, prefabManager).GetComponent<Room>();
                }
                specialRoom.roomLeft = true;
                GameObject.Destroy(walls[specialRoom.row + "-" + specialRoom.col]);
                walls.Remove(specialRoom.row + "-" + specialRoom.col);
                specialRoom.doorCount = 0;
                specialRoom.SetDoor();
                SetWalls(specialRoom);
                finalRoomNext.roomRight = true;
                finalRoomNext.SetDoor();
                finalRoomNext.row = specialRoom.row;
                finalRoomNext.col = specialRoom.col - 1;
                finalRoomNext.x = finalPosition.x;
                finalRoomNext.y = finalPosition.y;
                rooms[finalRoomNext.row, finalRoomNext.col] = finalRoomNext;
                SetWalls(finalRoomNext);
                specialRoom = finalRoomNext;
                break;
            }
        }
    }

    private void SetWalls(Room room) { 
    switch (room.doorCount)
        {
            case 1:
                if (room.roomUp)
                    walls.Add(room.row + "-" + room.col,Instantiate(wall.singleUp, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.singleDown, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomLeft)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.singleLeft, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomRight)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.singleRight, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                break;

            case 2:
                if (room.roomUp && room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.doubleUD, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomUp && room.roomLeft)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.doubleLU, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomUp && room.roomRight)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.doubleUR, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomLeft && room.roomRight)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.doubleLR, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomLeft && room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.doubleLD, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomRight && room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.doubleRD, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                break;

            case 3:
                if (room.roomUp && room.roomLeft && room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.tripleULD, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomUp && room.roomLeft && room.roomRight)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.tripleULR, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomUp && room.roomRight && room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.tripleURD, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                if (room.roomRight && room.roomLeft && room.roomDown)
                    walls.Add(room.row + "-" + room.col, Instantiate(wall.tripleLRD, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                break;

            case 4:
                walls.Add(room.row + "-" + room.col, Instantiate(wall.fourDoor, new Vector2(room.x, room.y), Quaternion.identity, prefabManager));
                break;



        }
    }
}


[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleDown,
                      doubleLR, doubleLU, doubleLD, doubleUR, doubleUD, doubleRD,
                      tripleULR, tripleULD, tripleURD, tripleLRD,
                      fourDoor;
}