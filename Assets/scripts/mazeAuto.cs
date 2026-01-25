using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class mazeAuto : MonoBehaviour
{

    [SerializeField] Tilemap tilemapFloor;
    [SerializeField] Tilemap tilemapWall;

    [SerializeField] Tile tileFloor;
    [SerializeField] Tile tileWall;

    [SerializeField] Maze_CA maze;
    Vector3Int pos;
    int[,] mapArray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapArray = new int[320, 140];
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
            { mapArray[i, j] = 4; }
        }
        maze.MazeAwake(189000);
        DrawMaze();

        DrawMap();
    }

    void DrawMaze()
    {
        for (int i = 0; i < 102; i++)
        {
            for (int j = 0; j < 102; j++)
            {
                mapArray[i + 200, j + 20] = maze.mapFinal[i, j];
            }
        }
    }
    void DrawMap()
    {
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
            {
                pos = new Vector3Int(i-10, j-10, 0);
                if (mapArray[i, j] == 0)
                {
                    tilemapFloor.SetTile(pos, tileFloor);

                }
                else if (mapArray[i, j] == 1)
                {
                    tilemapWall.SetTile(pos, tileWall);

                }

                }
            
        }
    }

}
