using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previweYOffset = 0.6f;
    [SerializeField] private GameObject cellIndicator;
    private GameObject previewObj;
    [SerializeField] private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
    }
    void Update()
    {
        
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObj = Instantiate(prefab);
        PreparePreavie(previewObj);
        PrepareCursor(size);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if(size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicator.GetComponentInChildren<Renderer>().material.mainTextureScale = size;
        }
    }

    private void PreparePreavie(GameObject previewObj)
    {
        throw new NotImplementedException();
    }
}
