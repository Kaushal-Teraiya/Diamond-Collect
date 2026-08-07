using System;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int maxDiamondsCount = 45;
    [SerializeField] private int minDiamondsCount = 25;
    [SerializeField][Range(0.25f, 0.40f)] private float lavaChance = 0.25f;
    [SerializeField] private int rows = 8;
    [SerializeField] private int columns = 16;
    [SerializeField] private GameObject lavaPrefab, safeIslandPrefab, diamondPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private float tileSize;
    [SerializeField] private Vector3 girdParentScale;
    [SerializeField] private Vector2 gridParentPosition;
    public enum TerrainType
    {
        Lava,
        Island
    }

    void Start()
    {
        GenerateGrid();
        gridParent.localScale = girdParentScale;
        gridParent.position = gridParentPosition;
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                //spawn tile
                var tilePosition = new Vector2
                (
                    gridParent.transform.position.x + j * tileSize,
                    gridParent.transform.position.y - i * tileSize
                );

                var tile = GetTilePrefab(GetRandomTile());
                Instantiate(tile, tilePosition, Quaternion.identity, gridParent);
            }
        }
    }

    private TerrainType GetRandomTile()
    {
        var chance = UnityEngine.Random.value;
        return chance <= lavaChance ? TerrainType.Lava : TerrainType.Island;
    }

    private GameObject GetTilePrefab(TerrainType tileType)
    {
        switch (tileType)
        {
            case TerrainType.Lava:
                return lavaPrefab;
            case TerrainType.Island:
                return safeIslandPrefab;
            default:
                throw new ArgumentOutOfRangeException(nameof(tileType));
        }
    }
}
