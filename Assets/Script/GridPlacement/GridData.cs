using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using System;

public class GridData : MonoBehaviour
{
    Dictionary<Vector3Int, PlacementData> placedObj = new();
    //este script se guarda la info de loq ue hemos puesto en la escena para no poner objetos encima de otros
    public void AddObjAt(Vector3Int gridPosition, Vector2Int objSize, int ID, int placedObjIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositons(gridPosition, objSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjIndex);
        foreach (var pos in positionToOccupy)
        {
            if(placedObj.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position{pos}");
            }
            placedObj[pos] = data;
        }
    }
    public List<Vector3Int> CalculatePositons(Vector3Int gridPosition, Vector2Int objSize)
    {
        List<Vector3Int> returnVal = new();
        for(int x = 0; x < objSize.x; x++)
        {
            for(int y = 0; y < objSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0,y));
            }
        }
        return returnVal;
    }
    public bool CanPlaceObjAt(Vector3Int gridPosition, Vector2Int objSize)
    {
        List<Vector3Int> positionsToOccupy= CalculatePositons(gridPosition,objSize);
        foreach (var pos in positionsToOccupy)
        {
            if (placedObj.ContainsKey(pos))
            {
                return false;   
            }
        }
        return true;
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPosition;
    public int ID {  get; private set; }
    public int PlacedObjIndex { get; private set; }

    public PlacementData (List<Vector3Int> occupiedPosition, int iD, int placedObjIndex)
    {
        this.occupiedPosition = occupiedPosition;
        ID = iD;
        PlacedObjIndex = placedObjIndex;
    }
}
