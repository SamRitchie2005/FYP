using NavMeshPlus.Components;


using Unity.Mathematics;


using UnityEngine;

using UnityEngine.Tilemaps;

using UnityEngine.UIElements;



public class GridTest1 : MonoBehaviour
{
    [SerializeField] Tilemap tilemapFloor;
    [SerializeField] Tilemap tilemapWall;
    Vector3Int pos;
    [SerializeField] Tile tileFloor;
    [SerializeField] Tile tileWall;
    [SerializeField] Tile tileWater;
    [SerializeField] Tile tileWaterShallow;
    [SerializeField] Tile tileSand;
    [SerializeField] Tile tileGrass;
    [SerializeField] int seed;
    [SerializeField] SeedContainer seedContainer;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject saw;
    [SerializeField] GameObject star;
    [SerializeField] NavMeshSurface surface;
    [SerializeField] int enemyAmount;
    TilemapCollider2D tCollider;
    [SerializeField] int automatonLoops;
    [SerializeField] Maze_CA maze;
    [SerializeField] DungeonGen dungeon;
    [SerializeField] UIDocument UI;
    Label seedLabel;
    int neighbourLive;
    int[,] mapArray;

    private void Awake()
    {
        if (FindAnyObjectByType<SeedContainer>() != null)
        {
            seedContainer = FindAnyObjectByType<SeedContainer>();
            seed = seedContainer.MainSeed;
           
        }
        
        mapArray = new int[320,140];
        tCollider = tilemapWall.GetComponent<TilemapCollider2D>();
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
            {
                float flip = Mathf.PerlinNoise(seed+i/10f+0.1f, seed+j/10f+0.1f);
               // pos = new Vector3Int(i, j, 0);

                // int flip = Random.Range(0, 2);
                if (flip > 0.65f && flip <= 0.7f)
                {
                    //tilemapFloor.SetTile(pos, tileFloor);
                    mapArray[i,j] = 0;
                }
                else if (flip > 0.7f)
                {
                    //tilemapWall.SetTile(pos, tileWall);
                    mapArray[i, j] = 1;
                }
                else if(/*flip >= 0f &&*/ flip <= 0.15f)
                {
                    mapArray[i, j] = 2;
                }
                else if(flip > 0.15f &&flip <= 0.25f)
                {
                    mapArray[i, j] = 3;
                }
                else if (flip > 0.25f && flip <= 0.35f)
                {
                    mapArray[i, j] = 4;
                }
                else if (flip > 0.35f && flip <= 0.65f)
                {
                    mapArray[i, j] = 5;
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
        dungeon.DungeonAwake(seed);
        DrawMaze();
        DrawDungeon();
        DrawWalls();
        DrawMap();
        
        tCollider.enabled = false;
        tCollider.enabled = true;
        UnityEngine.Random.InitState(seed);
        while(enemyAmount>0)
        {
            
            int posx = UnityEngine.Random.Range(0, 320);
            int posy = UnityEngine.Random.Range(0, 140);
            int targetCell = mapArray[posx,posy];
            if((targetCell==0|| targetCell == 3 || targetCell == 4 || targetCell == 5) &&/*!(posx>=99&&posx<=201&& posy >= 99 && posy <= 201)*/posx<200)
            {
                Vector3Int mapPos = new Vector3Int(posx, posy);
                Vector3 v = tilemapFloor.CellToWorld(mapPos);
                quaternion c = new quaternion(0,0,0,0);
                Instantiate(enemy, v,c);
                enemyAmount--;
            }
        }
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
            {
                if(mapArray[i, j] == 9)
                {
                    Vector3Int mapPos = new Vector3Int(2*i+1, 2*j+1);
                    Vector3 v = tilemapFloor.CellToWorld(mapPos)/2;
                    quaternion c = new quaternion(0, 0, 0, 0);
                    Instantiate(saw, v, c);
                }
                if (mapArray[i, j] == 8)
                {
                    Vector3Int mapPos = new Vector3Int(2 * i + 2, 2 * j + 2);
                    Vector3 v = tilemapFloor.CellToWorld(mapPos) / 2;
                    quaternion c = new quaternion(0, 0, 0, 0);
                    Instantiate(star, v, c);
                }
            }
        }
        var root = UI.rootVisualElement;
        seedLabel = root.Q<Label>("SeedLabel");
        seedLabel.text = "Seed:"+ seed.ToString();
        }

    void Start()
    {
        surface.BuildNavMesh();
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
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
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
                else if (mapArray[i, j] == 2)
                {
                    tilemapWall.SetTile(pos, tileWater);
                }
                else if (mapArray[i, j] == 3)
                {
                    tilemapFloor.SetTile(pos, tileWaterShallow);
                }
                else if (mapArray[i, j] == 4)
                {
                    tilemapFloor.SetTile(pos, tileSand);
                }
                else if (mapArray[i, j] == 5)
                {
                    tilemapFloor.SetTile(pos, tileGrass);
                }
                else if (mapArray[i, j] == 9)
                {
                    tilemapFloor.SetTile(pos, tileFloor);
                }
                else if (mapArray[i, j] == 8)
                {
                    tilemapFloor.SetTile(pos, tileFloor);
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
                mapArray[i+200,j+20] = maze.mapFinal[i,j];
            }
        }
    }
    void DrawDungeon()
    {
        dungeon.mapFinal[55, 55] = 8;
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (dungeon.mapFinal[i, j] != 999)
                {
                    mapArray[i+10, j+20] = dungeon.mapFinal[i, j];
                }
            }
        }
        
    }

    void DrawWalls()
    {
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
            {
                if (i < 10 || i >= 310 || j < 10 || j >= 130)
                {
                    mapArray[i, j] = 1;
                }
            }

        }
    }

}
