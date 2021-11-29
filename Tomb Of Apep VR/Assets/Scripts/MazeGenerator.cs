using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform wallObj;
    [SerializeField]
    private Transform floorObj;
    [SerializeField]
    private float roomSize = 1;
    [SerializeField]
    private int rooms = 5;

    int UP = 1;
    int RIGHT = 2;
    int DOWN = 4;
    int LEFT = 8;


    private cell[,] maze;

    public class cell
    {
        public List<(int, int, int)> neighbours = new List<(int, int, int)>();
        public int walls;
        public bool visited;
    }

    void Start()
    {
        maze = new cell[rooms, rooms];

        for (int i = 0; i < rooms; i++)
        {
            for (int j = 0; j < rooms; j++)
            {
                maze[i, j] = new cell();
                if (i != 0)
                    maze[i, j].neighbours.Add((i - 1, j, DOWN));
                if (i != rooms - 1)
                    maze[i, j].neighbours.Add((i + 1, j, UP));
                if (j != 0)
                    maze[i, j].neighbours.Add((i, j - 1, RIGHT));
                if (j != rooms - 1)
                    maze[i, j].neighbours.Add((i, j + 1, LEFT));
                maze[i, j].walls = 15;
            }
        }

        Stack<(int, int, int)> stack = new Stack<(int, int, int)>();
        var parents = new Dictionary<(int, int), (int, int)>();
        stack.Push((0, 0, 0));
        parents[(0, 0)] = (0, 0);
        while (stack.Count != 0)
        {
            (int i, int j, int k) = stack.Pop();
            (int x, int y) = parents[(i, j)];
            if (!maze[i, j].visited)
            {
                maze[i, j].visited = true;
                maze[i, j].walls -= k;
                if (k > 3)
                    maze[x, y].walls -= k >> 2;
                else
                    maze[x, y].walls -= k << 2;
                while (maze[i, j].neighbours.Count != 0)
                {
                    int rnd = Mathf.FloorToInt(Random.Range(0, maze[i, j].neighbours.Count));
                    int x1 = maze[i, j].neighbours[rnd].Item1;
                    int y1 = maze[i, j].neighbours[rnd].Item2;
                    int v = maze[i, j].neighbours[rnd].Item3;
                    stack.Push((x1, y1, v));
                    parents[(x1, y1)] = (i, j);
                    maze[i, j].neighbours.RemoveAt(rnd);
                }
            }

        }
        var floor1 = Instantiate(this.floorObj, transform);
        floor1.position = new Vector3(rooms * roomSize / 2, floor1.position.y, -rooms * roomSize / 2);
        floor1.localScale = new Vector3(rooms * roomSize * 2 * floor1.localScale.x, rooms * roomSize * floor1.localScale.y * 2, floor1.localScale.z);
        var floor2 = Instantiate(this.floorObj, transform);
        floor2.position = new Vector3(rooms * roomSize / 2, floor1.position.y + 1, -rooms * roomSize / 2);
        floor2.localScale = floor1.localScale;



        for (int i = 0; i < rooms; i++)
        {
            string str = "";
            for (int j = 0; j < rooms; j++)
            {
               
                str += " " + maze[i, j].walls;
                Vector3 position = transform.position + new Vector3(roomSize * j + roomSize / 2, 0, -roomSize * i - roomSize / 2);
                if ((maze[i, j].walls & LEFT) != 0 && (i!=0||j!=0))
                {
                    var wall = Instantiate(this.wallObj, transform);
                    wall.position = position + new Vector3((-roomSize / 2), wall.position.y, 0);
                    wall.localScale = new Vector3(wall.localScale.x, wall.localScale.y * roomSize, wall.localScale.z);
                    wall.eulerAngles = new Vector3(90, 0, 0);
                }
                if ((maze[i, j].walls & UP) != 0)
                {
                    var wall = Instantiate(this.wallObj, transform);
                    wall.position = position + new Vector3(0, wall.position.y, roomSize / 2);
                    wall.localScale = new Vector3(wall.localScale.x, wall.localScale.y * roomSize, wall.localScale.z);

                }
                if (j == rooms - 1)
                {
                    var wall = Instantiate(this.wallObj, transform);
                    wall.position = position + new Vector3(roomSize / 2, wall.position.y, 0); 
                    wall.localScale = new Vector3(wall.localScale.x, wall.localScale.y * roomSize, wall.localScale.z);
                    wall.eulerAngles = new Vector3(90, 0, 0);
                }
                if (i == rooms - 1)
                {
                    var wall = Instantiate(this.wallObj, transform);
                    wall.position = position + new Vector3(0, wall.position.y, -roomSize / 2);
                    wall.localScale = new Vector3(wall.localScale.x, wall.localScale.y * roomSize, wall.localScale.z);
                }
            }
            Debug.Log(str);
        }

        Destroy(GetComponent<Transform>().GetChild(GetComponent<Transform>().childCount - 1).gameObject);

    }

}
