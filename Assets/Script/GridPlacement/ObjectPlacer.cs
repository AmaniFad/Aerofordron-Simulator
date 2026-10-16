using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placeGameObjects = new();

    public int PlaceObj(GameObject prefab, Vector3 position)
    {
        GameObject newObj = Instantiate(prefab);
        newObj.transform.position = position;
        placeGameObjects.Add(newObj);

        return placeGameObjects.Count - 1;
    }
}
