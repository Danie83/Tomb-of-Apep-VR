using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    [Range(5, 50)]
    private int width = 11;

    [SerializeField]
    [Range(5, 50)]
    private int height = 11;

    [SerializeField]
    private float size = 2f;

    [SerializeField]
    private bool renderCeiling = false;

    [SerializeField]
    private bool renderFloor = true;

    [Header("Prefabs")]
    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

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

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                WallState cell = maze[i, j];

                var position = new Vector3(-width + i * 2 + 1 , 0, -height + j * 2 + 1);
                Debug.Log(position);

                if (renderFloor)
                    Instantiate(floorPrefab, position + Vector3.up * transform.position.y, Quaternion.identity, transform);
                if (renderCeiling)
                    Instantiate(floorPrefab, position + Vector3.up * (transform.position.y + size), Quaternion.identity, transform);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.name = "TOP=>" + "i:" + i + "==" + "j:" + j;
                    topWall.localPosition = position + new Vector3(-size / 2, 0, 0);
                    topWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.name = "LEFT=>" + "i:" + i + "==" + "j:" + j;
                    leftWall.localPosition = position + new Vector3(0, 0, -size / 2);
                }

                if (j == height - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.name = "RIGHT=>" + "i:" + i + "==" + "j:" + j;
                        rightWall.localPosition = position + new Vector3(0, 0, +size / 2);
                    }
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(wallPrefab, transform) as Transform;
                        downWall.name = "DOWN=>" + "i:" + i + "==" + "j:" + j;
                        downWall.localPosition = position + new Vector3(+size / 2, 0, 0);
                        downWall.eulerAngles = new Vector3(0, 90, 0);
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
