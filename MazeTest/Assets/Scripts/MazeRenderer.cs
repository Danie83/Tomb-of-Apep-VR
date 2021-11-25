using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{

    [SerializeField]
    [Range(5, 50)]
    private int width = 11;

    [SerializeField]
    [Range(5, 50)]
    private int height = 11;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    private Transform roomPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGen.Generate(width, height);
        Draw(maze);
    }
    public void buildMarginWall(Vector3 referencePosition, Vector3 position, bool angle)
    {
        var obj = Instantiate(wallPrefab, transform) as Transform;
        obj.localPosition = referencePosition + position;
        obj.localScale = new Vector3(5, obj.localScale.y, obj.localScale.z);
        if (angle)
        {
            obj.eulerAngles = new Vector3(90, 0, 0);
        }
    }
    private void Draw(WallState[,] maze)
    {
        var floor = Instantiate(floorPrefab, transform);
        floor.localPosition += new Vector3(0, -0.5f, 0);
        if (width % 2 == 0)
        {
            floor.localPosition += new Vector3(-0.5f, 0, -0.5f);
        }
        floor.localScale = new Vector3(width * floor.localScale.x * 2, height * floor.localScale.y * 2, floor.localScale.z);

        //for (int i = 0; i < width; ++i)
        //{
        //    for (int j = 0; j < height; ++j)
        //    {
        //        WallState cell = maze[i, j];
        //        var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

        //        if (i == 0 || i == width - 1)
        //        {
        //            float sign = i > 0 ? 1 : -1;
        //            buildMarginWall(position, new Vector3(sign * size / 2, 0, 0), true);
        //        }

        //        if (j == 0 || j == height - 1)
        //        {
        //            float sign = j > 0 ? 1 : -1;
        //            buildMarginWall(position, new Vector3(0, 0, sign * size / 2), false);
        //        }

        //        if ((i == 1 && j == height - 2) || (i == width - 2 && j == 1) || (i == width / 2 && j == height / 2))
        //        {
        //            var roomPosition = position;
        //            roomPosition.y = -0.5f;
        //            Instantiate(roomPrefab, roomPosition, Quaternion.identity);
        //        }
        //    }
        //}

        //int randomTreasureTrove1 = Random.Range(2, width / 2 - 1);
        //int randomTreasureTrove11 = Random.Range(randomTreasureTrove1, width / 2 - 1);
        //int randomTreasureTrove2 = Random.Range(width / 2 + 2, width - 1);
        //int randomTreasureTrove22 = Random.Range(randomTreasureTrove2, width - 1);

        ////Random.Range(0, 3); // is inclusive
        //int rotation1 = Random.Range(0, 3);
        //var room1 = Instantiate(roomPrefab, new Vector3(-width / 2 + randomTreasureTrove1,
        //    -0.5f, -height / 2 + randomTreasureTrove11),
        //    Quaternion.identity);
        
        //room1.eulerAngles = new Vector3(0, rotation1 * 90, 0);

        //int rotation2 = Random.Range(0, 3);
        //var room2 = Instantiate(roomPrefab, new Vector3(-width / 2 + randomTreasureTrove2,
        //    -0.5f, -height / 2 + randomTreasureTrove22),
        //    Quaternion.identity);
        //room2.eulerAngles = new Vector3(0, rotation2 * 90, 0);
        //maze[randomTreasureTrove2, randomTreasureTrove22] = WallState.ALLROUND;

        //Debug.Log(maze[randomTreasureTrove1 - 1, randomTreasureTrove11]);
        //maze[randomTreasureTrove1 - 1, randomTreasureTrove11] &= ~WallState.DOWN;
        //Debug.Log(-width / 2 + randomTreasureTrove1 - 1);
        //Debug.Log(-height / 2 + randomTreasureTrove11);
        //Debug.Log(maze[randomTreasureTrove1 - 1, randomTreasureTrove11]);
        //maze[randomTreasureTrove1, randomTreasureTrove11 - 1] &= ~WallState.RIGHT;
        
        //maze[randomTreasureTrove1 + 1, randomTreasureTrove11] &= ~WallState.UP;
        //maze[randomTreasureTrove1, randomTreasureTrove11 + 1] &= ~WallState.LEFT;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                WallState cell = maze[i, j];
                
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if ((i == 1 && j == height - 2) || (i == width - 2 && j == 1) || (i == width / 2 && j == height / 2))
                {
                    var roomPosition = position;
                    roomPosition.y = -0.5f;
                    Instantiate(roomPrefab, roomPosition, Quaternion.identity);
                    
                    maze[i - 1, j] &= ~WallState.DOWN;
                    maze[i + 1, j] &= ~WallState.UP;
                    maze[i, j - 1] &= ~WallState.RIGHT;
                    maze[i, j +1] &= ~WallState.LEFT;
                    continue;
                }

                //if ((i == 1 && j == height - 2)
                //    || (i == width - 2 && j == 1)
                //    || (i == width / 2 && j == height / 2)
                ////    //|| (i == randomTreasureTrove1 && j == randomTreasureTrove11)
                ////    //|| (i == randomTreasureTrove2 && j == randomTreasureTrove22)
                //    )
                //    continue;

                if (cell.HasFlag(WallState.UP))
                {
                 
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.name = "TOP=>" + "i:" + i + "==" + "j:" + j;
                    topWall.localPosition = position + new Vector3(-size / 2, 0, 0);
                    topWall.localScale = new Vector3(5, topWall.localScale.y, topWall.localScale.z);
                    topWall.eulerAngles = new Vector3(90, 0, 0);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.name = "LEFT=>" + "i:" + i + "==" + "j:" + j;
                    leftWall.localPosition = position + new Vector3(0, 0, -size / 2);
                    leftWall.localScale = new Vector3(5, leftWall.localScale.y, leftWall.localScale.z);
                   

                }

                if (j == height - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.name = "RIGHT=>" + "i:" + i + "==" + "j:" + j;
                        rightWall.localPosition = position + new Vector3(0, 0, +size / 2);
                        rightWall.localScale = new Vector3(5, rightWall.localScale.y, rightWall.localScale.z);
                        
                    }
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(wallPrefab, transform) as Transform;
                        downWall.name = "DOWN=>" + "i:" + i + "==" + "j:" + j;
                        downWall.localPosition = position + new Vector3(+size/2, 0, 0);
                        downWall.localScale = new Vector3(5, downWall.localScale.y, downWall.localScale.z);
                        downWall.eulerAngles = new Vector3(90, 0, 0);
                    }
                }

            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
