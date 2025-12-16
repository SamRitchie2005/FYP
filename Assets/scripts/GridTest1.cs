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
    int neighbourLive;

    private void Awake()
    {
        tCollider = tilemapWall.GetComponent<TilemapCollider2D>();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                int flip = (int)Mathf.Round(Mathf.PerlinNoise(seed+i+0.1f, seed+j+0.1f));
                pos = new Vector3Int(i, j, 0);

                // int flip = Random.Range(0, 2);
                if (flip == 0)
                {
                    tilemapFloor.SetTile(pos, tileFloor);
                }
                else if (flip == 1)
                {
                    tilemapWall.SetTile(pos, tileWall);
                }
                for (int k = 0; k < automatonLoops; k++)
                {
                    CellularAutomaton();
                }
     
            }
        }

        tCollider.enabled = false;
        tCollider.enabled = true;

    }
    void CellularAutomaton()
    {
        for (int i = 45; i < 55; i++)
        {
            for (int j = 45; j < 55; j++)
            {
                neighbourLive = 0;
                if (tilemapWall.GetTile(new Vector3Int(i-1, j-1, 0))==tileWall){
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i - 1, j , 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i - 1, j + 1, 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i , j - 1, 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i , j + 1, 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i + 1, j - 1, 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i + 1, j , 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (tilemapWall.GetTile(new Vector3Int(i + 1, j + 1, 0)) == tileWall)
                {
                    neighbourLive++;
                }
                if (neighbourLive == 3) { 
                    tilemapWall.SetTile(new Vector3Int(i , j , 0), tileWall); 
                    tilemapFloor.SetTile(new Vector3Int(i, j, 0), null);
                }
                if (neighbourLive <1||neighbourLive>5)
                {
                    tilemapWall.SetTile(new Vector3Int(i, j, 0), null);
                    tilemapFloor.SetTile(new Vector3Int(i, j, 0), tileFloor);
                }
            }
        }
    }

}
