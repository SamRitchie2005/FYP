using TreeEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridTest1 : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    Vector3Int pos;
    [SerializeField] Tile tile;
    [SerializeField] int seed;

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                int flip = (int)Mathf.Round(Mathf.PerlinNoise(seed+i+0.1f, seed+j+0.1f));
                pos = new Vector3Int(i, j, 0);
              
               // int flip = Random.Range(0, 2);
                if (flip == 0)
                {
                    tilemap.SetTile(pos, tile);
                
            }
     
            }
        }
    }
}
