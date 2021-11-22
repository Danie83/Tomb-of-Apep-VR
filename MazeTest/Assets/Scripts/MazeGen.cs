using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum WallState
{
    LEFT = 1, // 0001
    RIGHT = 2, // 0010
    UP = 4, // 0100
    DOWN = 8, // 1000
    ALLROUND = 15, // 1111

    VISITED = 128,
}

public struct Position
{
    public int xpos;
    public int ypos;
}

public struct Neighbour
{
    public Position position;
    public WallState sharedWall;
}

public static class MazeGen
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }
    public static WallState[,] RecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        var position = new Position { xpos = rng.Next(0, width), ypos = rng.Next(0, height) };

        maze[position.xpos, position.ypos] |= WallState.VISITED;
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = getUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.position;
                maze[current.xpos, current.ypos] &= ~randomNeighbour.sharedWall;
                maze[nPosition.xpos, nPosition.ypos] &= ~GetOppositeWall(randomNeighbour.sharedWall);
                maze[nPosition.xpos, nPosition.ypos] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }
    public static List<Neighbour> getUnvisitedNeighbours(Position pos, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();
        if (pos.xpos > 0) // left
        {
            if (!maze[pos.xpos - 1,pos.ypos].HasFlag(WallState.VISITED)) {
                list.Add(new Neighbour{
                    position = new Position 
                    { 
                        xpos = pos.xpos - 1, 
                        ypos = pos.ypos 
                    },
                    sharedWall = WallState.LEFT
                });
            }
        }

        if (pos.ypos > 0) // down
        {
            if (!maze[pos.xpos , pos.ypos - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        xpos = pos.xpos,
                        ypos = pos.ypos - 1
                    },
                    sharedWall = WallState.DOWN
                });
            }
        }

        if (pos.ypos < height - 1) // up
        {
            if (!maze[pos.xpos, pos.ypos + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        xpos = pos.xpos,
                        ypos = pos.ypos + 1
                    },
                    sharedWall = WallState.UP
                });
            }
        }

        if (pos.xpos < width - 1) // right
        {
            if (!maze[pos.xpos + 1, pos.ypos].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        xpos = pos.xpos + 1,
                        ypos = pos.ypos
                    },
                    sharedWall = WallState.RIGHT
                });
            }
        }
        return list;
    }
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];

        WallState initial = WallState.LEFT | WallState.RIGHT | WallState.UP | WallState.DOWN;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i, j] = initial; // 1111
            }
        }

        return RecursiveBacktracker(maze,width,height);
    }
}
