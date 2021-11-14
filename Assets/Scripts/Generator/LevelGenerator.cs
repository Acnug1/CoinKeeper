using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private float _spawnRadiusX;
    [SerializeField] private float _spawnRadiusY;
    [SerializeField] private float _cellSize;
    [SerializeField] private int _capacityLevelContainer;

    private Camera _camera;
    private List<Vector2Int> _collisionsMatrix = new List<Vector2Int>();
    private List<GridObject> _gridObjects = new List<GridObject>();
    private int _cellCountAxisX;
    private int _cellCountAxisY;

    private void Start()
    {
        _camera = Camera.main;
        _collisionsMatrix.Add(new Vector2Int(0, 0));
        _cellCountAxisX = ReturnCellCountAxis(_spawnRadiusX, _cellSize);
        _cellCountAxisY = ReturnCellCountAxis(_spawnRadiusY, _cellSize);
    }

    private int ReturnCellCountAxis(float spawnRadius, float cellSize)
    {
        return (int)(spawnRadius / cellSize);
    }

    private void Update()
    {
        FillRadius(_camera.transform.position, _cellCountAxisX, _cellCountAxisY);
    }

    private void FillRadius(Vector2 center, int cellCountAxisX, int cellCountAxisY)
    {
        var fillAreaCenter = WorldToGridPosition(center);

        for (int x = -cellCountAxisX; x <= cellCountAxisX; x++)
        {
            for (int y = -cellCountAxisY; y <= cellCountAxisY; y++)
            {
                TryCreateRandomObjectOnLayer(GridLayer.Ground, fillAreaCenter + new Vector2Int(x, y));
                TryCreateRandomObjectOnLayer(GridLayer.OnGround, fillAreaCenter + new Vector2Int(x, y));
            }
        }
    }

    private void TryCreateRandomObjectOnLayer(GridLayer layer, Vector2Int gridPosition)
    {
        ClearCollisionsMatrix();

        gridPosition.y += (int)layer;

        if (_collisionsMatrix.Contains(gridPosition))
            return;
        else
            _collisionsMatrix.Add(gridPosition);

        var template = GetRandomTemplate(layer);

        if (template == null)
            return;

        var position = GridToWorldPosition(gridPosition);

        var gridObject = Instantiate(template, position, Quaternion.identity, transform);
        _gridObjects.Add(gridObject);

        ClearLevelContainer();
    }

    private GridObject GetRandomTemplate(GridLayer layer)
    {
        var variants = _templates.Where(template => template.Layer == layer);

        foreach (var template in variants)
        {
            if (template.Chance > Random.Range(0, 100))
            {
                return template;
            }
        }

        return null;
    }

    private void ClearLevelContainer()
    {
        if (_gridObjects.Count > _capacityLevelContainer)
        {
            var firstElementGridObjects = _gridObjects[0];
            _gridObjects.Remove(firstElementGridObjects);

            Destroy(firstElementGridObjects.gameObject);
        }
    }

    private void ClearCollisionsMatrix()
    {
        if (_collisionsMatrix.Count > _capacityLevelContainer * 10)
            _collisionsMatrix.Remove(_collisionsMatrix[0]);
    }

    private Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector2(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize);
    }

    private Vector2Int WorldToGridPosition(Vector2 worldPosition)
    {
        return new Vector2Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize));
    }
}
