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

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                WallState cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (i == 0 || i == width - 1)
                {
                    float sign = i > 0 ? 1 : -1;
                    buildMarginWall(position, new Vector3(sign * size / 2, 0, 0), true);
                }

                if (j == 0 || j == height - 1)
                {
                    float sign = j > 0 ? 1 : -1;
                    buildMarginWall(position, new Vector3(0, 0, sign * size / 2), false);
                }

                if ((i == 1 && j == height - 2) || (i == width - 2 && j == 1) || (i == width / 2 && j == height / 2))
                {
                    var roomPosition = position;
                    roomPosition.y = -0.5f;
                    Instantiate(roomPrefab, roomPosition, Quaternion.identity);
                }
            }
        }

        int randomTreasureTrove1 = Random.Range(1, width / 2 - 1);
        int randomTreasureTrove2 = Random.Range(width/2 + 1, width - 1);

        Instantiate(roomPrefab, new Vector3(-width / 2 + randomTreasureTrove1, -0.5f, -height / 2 + Random.Range(randomTreasureTrove1, width / 2 - 1)), Quaternion.identity);
        Instantiate(roomPrefab, new Vector3(-width / 2 + randomTreasureTrove2, -0.5f, -height / 2 + Random.Range(randomTreasureTrove2, width - 1)), Quaternion.identity);

        //for (int i = 0; i < width; ++i)
        //{
        //    for (int j = 0; j < height; ++j)
        //    {
        //        WallState cell = maze[i, j];
        //        var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

        //        if (cell.HasFlag(WallState.UP))
        //        {
        //            var topWall = Instantiate(wallPrefab, transform) as Transform;
        //            topWall.localPosition = position + new Vector3(0, 0, size / 2);
        //            topWall.localScale = new Vector3(5, topWall.localScale.y, topWall.localScale.z);
        //        }

        //        if (cell.HasFlag(WallState.LEFT))
        //        {
        //            var leftWall = Instantiate(wallPrefab, transform) as Transform;
        //            leftWall.localPosition = position + new Vector3(-size / 2, 0, 0);
        //            leftWall.localScale = new Vector3(5, leftWall.localScale.y, leftWall.localScale.z);
        //            leftWall.eulerAngles = new Vector3(90, 0, 0);
        //        }

        //        if (i == width - 1)
        //        {
        //            if (cell.HasFlag(WallState.RIGHT))
        //            {
        //                var rightWall = Instantiate(wallPrefab, transform) as Transform;
        //                rightWall.localPosition = position + new Vector3(+size / 2, 0, 0);
        //                rightWall.localScale = new Vector3(5, rightWall.localScale.y, rightWall.localScale.z);
        //                rightWall.eulerAngles = new Vector3(90, 0, 0);
        //            }
        //        }

        //        if (j == 0)
        //        {
        //            if (cell.HasFlag(WallState.DOWN))
        //            {
        //                var downWall = Instantiate(wallPrefab, transform) as Transform;
        //                downWall.localPosition = position + new Vector3(0, 0, -size / 2);
        //                downWall.localScale = new Vector3(5, downWall.localScale.y, downWall.localScale.z);
        //            }
        //        }
        //    }

        //}

    }

    // Update is called once per frame
    void Update()
    {

    }
}
