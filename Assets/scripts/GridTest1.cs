using UnityEngine;
using UnityEngine.Tilemaps;
public class GridTest1 : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    Vector3Int pos;
    [SerializeField] Tile tile;

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                pos = new Vector3Int(i, j, 0);
                int flip = Random.Range(0, 2);
                if (flip == 0)
                {
                    tilemap.SetTile(pos, tile);
                
            }
      
            }
        }
    }
}
