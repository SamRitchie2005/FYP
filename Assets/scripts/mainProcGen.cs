using NavMeshPlus.Components;


using Unity.Mathematics;
using UnityEngine.SceneManagement;

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
           //looks for the object holding the random seed and assigns it locally
        }
        
        mapArray = new int[320,140]; //initialise the array the the map will be built from
        tCollider = tilemapWall.GetComponent<TilemapCollider2D>();
        for (int i = 0; i < 320; i++)
        {
            for (int j = 0; j < 140; j++)
            {
                float perlinValue = Mathf.PerlinNoise(seed+i/10f+0.1f, seed+j/10f+0.1f);
                //for each cell in the main array, gets an associate value rfom a perlin noise function

            
                if (perlinValue > 0.65f && perlinValue <= 0.7f)
                {
                    //set value for dirt floor in array
                    mapArray[i,j] = 0;
                }
                else if (perlinValue > 0.7f)
                {
                    //set value for wall in array
                    mapArray[i, j] = 1;
                }
                else if(perlinValue <= 0.15f)
                {
                    //set value for deep water in array
                    mapArray[i, j] = 2;
                }
                else if(perlinValue > 0.15f &&perlinValue <= 0.25f)
                {
                    //set value for shallow water in array
                    mapArray[i, j] = 3;
                }
                else if (perlinValue > 0.25f && perlinValue <= 0.35f)
                {
                    //set value for sand in array
                    mapArray[i, j] = 4;
                }
                else if (perlinValue > 0.35f && perlinValue <= 0.65f)
                {
                    //set value for grass in array
                    mapArray[i, j] = 5;
                }
           

            }
        }

        maze.MazeAwake(seed); //runs maze generator for maze array
        dungeon.DungeonAwake(seed); // runs dungeon generator for dungeon array
        DrawMaze();  //overwrites parts of the main array with the maze array
        DrawDungeon(); //overwrites parts of the main array with the dungeon array
        DrawWalls(); // overwrites parts of the main array with border walls
        DrawMap(); // converts the main array into a tilemap
        
        tCollider.enabled = false;
        tCollider.enabled = true; //regenerate tilemap collider
        UnityEngine.Random.InitState(seed); 
        while(enemyAmount>0)
        {
            //until every enemy is placed, check random cells to see if they are valid spawn areas. place enemy if true, retry if false
            //valid spawns are anywhere not in the wall layer and not in the maze area
            int posx = UnityEngine.Random.Range(0, 320);
            int posy = UnityEngine.Random.Range(0, 140);
            int targetCell = mapArray[posx,posy];
            bool closeToPlayer = (math.abs(posx-150)<10 && math.abs(posy-70)<10); 
            if ((targetCell==0|| targetCell == 3 || targetCell == 4 || targetCell == 5) &&(posx<200) && !closeToPlayer )
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
                    //place a saw object in every cell designated
                    Vector3Int mapPos = new Vector3Int(2*i+1, 2*j+1);
                    Vector3 v = tilemapFloor.CellToWorld(mapPos)/2;
                    quaternion c = new quaternion(0, 0, 0, 0);
                    Instantiate(saw, v, c);
                }
                if (mapArray[i, j] == 8)
                {
                    //place a star object in every cell designated
                    Vector3Int mapPos = new Vector3Int(2 * i + 2, 2 * j + 2);
                    Vector3 v = tilemapFloor.CellToWorld(mapPos) / 2;
                    quaternion c = new quaternion(0, 0, 0, 0);
                    Instantiate(star, v, c);
                }
            }
        }
        var root = UI.rootVisualElement; //adds the seed to the ui
        seedLabel = root.Q<Label>("SeedLabel");
        seedLabel.text = "Seed:"+ seed.ToString();
        }

    void Start()
    {
        surface.BuildNavMesh(); //build the navmesh 
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
                    //sets dirt floor tile on tilemap
                }
                else if (mapArray[i, j] == 1)
                {
                    tilemapWall.SetTile(pos, tileWall);
                    //sets wall tile on tilemap
                }
                else if (mapArray[i, j] == 2)
                {
                    tilemapWall.SetTile(pos, tileWater);
                    //sets deep water tile on tilemap
                }
                else if (mapArray[i, j] == 3)
                {
                    tilemapFloor.SetTile(pos, tileWaterShallow);
                    //sets shallow water tile on tilemap
                }
                else if (mapArray[i, j] == 4)
                {
                    tilemapFloor.SetTile(pos, tileSand);
                    //sets sand tile on tilemap
                }
                else if (mapArray[i, j] == 5)
                {
                    tilemapFloor.SetTile(pos, tileGrass);
                    //sets grass tile on tilemap
                }
                else if (mapArray[i, j] == 9)
                {
                    tilemapFloor.SetTile(pos, tileFloor);
                    //sets dirt floor tile on tilemap (marked for saw)
                }
                else if (mapArray[i, j] == 8)
                {
                    tilemapFloor.SetTile(pos, tileFloor);
                    //sets dirt floor tile on tilemap (marked for star)
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
                mapArray[i + 200, j + 20] = maze.mapFinal[i, j]; //maps maze array onto main array
            }
        }
        int mazeChecker = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(maze.mapFinal[i + 20, j + 20] == 0)
                {
                    mazeChecker++;
                }
            }
        }
        if(mazeChecker == 100)
        {
            
            seedContainer.MainSeed = seedContainer.MainSeed + 1;
            SceneManager.LoadScene("ProcGen");
        }
        Debug.Log("checsk"+mazeChecker);
    }
    void DrawDungeon()
    {
        dungeon.mapFinal[55, 55] = 8;
        for (int i = 52; i < 58; i++)
        {
            for (int j = 52; j < 58; j++)
            {
                if(i==55&& j == 55)
                {
                    continue;
                }
                dungeon.mapFinal[i, j] = 0;
            }
        }
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (dungeon.mapFinal[i, j] != 999)
                {
                    mapArray[i+10, j+20] = dungeon.mapFinal[i, j]; //maps every non null cell in the dungeon array onto the main array 
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
                    mapArray[i, j] = 1; //replaces every cell in the outer layers of the main array with wall values
                }
            }

        }
    }

}
