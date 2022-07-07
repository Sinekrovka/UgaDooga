using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class GridController : MonoBehaviour
{

    private GridLayoutGroup _gridLayoutGroup;
    private Vector2 _gridSize;
    
    private int startPositionX;
    private int startPositionY;

    private InputController _input;

    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        RectTransform rect = GetComponent<RectTransform>();
        _input = FindObjectOfType<InputController>();
        _input.gridParams += ReCreateGrid;
        _gridSize = new Vector2(Screen.width, Screen.height)+rect.sizeDelta;
    }

    private void GenerateNewGrid()
    {
        int count = startPositionX * startPositionY;

        GameObject startTile = transform.GetChild(0).gameObject;
        
        for (int i = 1; i < count; ++i)
        {
            Instantiate(startTile, transform);
        }
    }

    private void ReCreateGrid(int x, int y, bool spawn)
    {
        if (!spawn)
        {
            startPositionX = x;
            startPositionY = y;

            int countChildren = transform.childCount-1;
            if (countChildren > 0)
            {
                for (int i = countChildren; i > 1; --i)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            GenerateNewGrid();
            RecalculateSizeGrid();
        }
    }

    private void RecalculateSizeGrid()
    {
        float CellSizeX = (_gridSize.x-_gridLayoutGroup.spacing.x*startPositionX - 
                           _gridLayoutGroup.padding.left -_gridLayoutGroup.padding.right) / startPositionX;
        float CellSizeY = (_gridSize.y- _gridLayoutGroup.spacing.y*startPositionY-
                           _gridLayoutGroup.padding.top-_gridLayoutGroup.padding.bottom) / startPositionY;
        
        _gridLayoutGroup.cellSize = new Vector2(CellSizeX, CellSizeY);
        _input.gridParams?.Invoke(startPositionX, startPositionY, true);
    }

    public Vector2 CellSize => _gridLayoutGroup.cellSize;

    public List<RectTransform> GetPoints()
    {
        return transform.GetComponentsInChildren<RectTransform>().ToList();
    }
}
