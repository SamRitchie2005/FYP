using TreeEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridTest1 : MonoBehaviour
{
    [SerializeField] Tilemap tilemapFloor;
    [SerializeField] Tilemap tilemapWall;
    Vector3Int pos;
    [SerializeField] Tile tileFloor;
    [SerializeField] Tile tileWall;
    [SerializeField] int seed;
    TilemapCollider2D tCollider;
    [SerializeField] int automatonLoops;
    [SerializeField] Maze_CA maze;
    int neighbourLive;
    int[,] mapArray;

    private void Awake()
    {
        mapArray = new int[250,250];
        tCollider = tilemapWall.GetComponent<TilemapCollider2D>();
        for (int i = 0; i < 250; i++)
        {
            for (int j = 0; j < 250; j++)
            {
                int flip = (int)Mathf.Round(Mathf.PerlinNoise(seed+i/10f+0.1f, seed+j/10f+0.1f));
               // pos = new Vector3Int(i, j, 0);

                // int flip = Random.Range(0, 2);
                if (flip == 0)
                {
                    //tilemapFloor.SetTile(pos, tileFloor);
                    mapArray[i,j] = 0;
                }
                else if (flip == 1)
                {
                    //tilemapWall.SetTile(pos, tileWall);
                    mapArray[i, j] = 1;
                }
                //for (int k = 0; k < automatonLoops; k++)
                //{
                //   CellularAutomaton();
                //}
     
            }
        }
        // for (int k = 0; k < automatonLoops; k++)
         //{
           // CellularAutomaton();
        // }
        maze.MazeAwake(seed);
        DrawMaze();
        DrawMap();
        
        tCollider.enabled = false;
        tCollider.enabled = true;

    }
    void CellularAutomaton()
    {
        for (int i = 1; i < 99; i++)
        {
            for (int j = 1; j < 99; j++)
            {
                //neighbourLive = 0;
                //if (tilemapWall.GetTile(new Vector3Int(i-1, j-1, 0))==tileWall){
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i - 1, j , 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i - 1, j + 1, 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i , j - 1, 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i , j + 1, 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i + 1, j - 1, 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i + 1, j , 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (tilemapWall.GetTile(new Vector3Int(i + 1, j + 1, 0)) == tileWall)
                //{
                //    neighbourLive++;
                //}
                //if (neighbourLive == 3) { 
                //    tilemapWall.SetTile(new Vector3Int(i , j , 0), tileWall); 
                //    tilemapFloor.SetTile(new Vector3Int(i, j, 0), null);
                //}
                //if (neighbourLive <1||neighbourLive>5)
                //{
                //    tilemapWall.SetTile(new Vector3Int(i, j, 0), null);
                //    tilemapFloor.SetTile(new Vector3Int(i, j, 0), tileFloor);
                neighbourLive = 0;
                if (mapArray[i-1,j-1]==1)
                {
                    neighbourLive++;
                }
                if (mapArray[i, j-1] == 1)
                {
                    neighbourLive++;
                }
                if (mapArray[i+1, j - 1] == 1)
                {
                    neighbourLive++;
                }
                if (mapArray[i-1, j] == 1)
                {
                    neighbourLive++;
                }
                if (mapArray[i+1, j] == 1)
                {
                    neighbourLive++;
                }
                if (mapArray[i - 1, j+1] == 1)
                {
                    neighbourLive++;
                }
                if (mapArray[i, j+1] == 1)
                {
                    neighbourLive++;
                }
                if (mapArray[i+1, j+1] == 1)
                {
                    neighbourLive++;
                }
                if (neighbourLive == 3)
                {
                    mapArray[i, j] = 1;
                }
                if (neighbourLive < 1 || neighbourLive >4)
                {
                    mapArray[i, j] = 0;
                }
            }
        }
    }


    void DrawMap()
    {
        for (int i = 0; i < 250; i++)
        {
            for (int j = 0; j < 250; j++)
            {
                pos = new Vector3Int(i, j, 0);
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

    void DrawMaze()
    {
        for (int i = 0; i < 102; i++)
        {
            for (int j = 0; j < 102; j++)
            {
                mapArray[i,j] = maze.mapFinal[i,j];
            }
        }
    }
}
