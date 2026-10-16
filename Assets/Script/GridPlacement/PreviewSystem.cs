using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previweYOffset = 0.6f;

    [SerializeField] private GameObject cellIndicator;
    private GameObject previewObj;

    [SerializeField] private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;
    void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();   
    }
    void Update()
    {
        
    }
    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObj = Instantiate(prefab);
        PreparePreavie(previewObj);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }
    private void PrepareCursor(Vector2Int size)
    {
        if(size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicatorRenderer.GetComponentInChildren<Renderer>().material.mainTextureScale = size;
        }
    }
    private void PreparePreavie(GameObject previewObj)
    {
        Renderer[] renderers = previewObj.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for(int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }
    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        Destroy(previewObj);
    }
    public void UpdatePosition(Vector3 position, bool validity)
    {
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedBack(validity);
    }

    private void MovePreview(Vector3 position)
    {
        previewObj.transform.position = new Vector3(position.x, position.y + previweYOffset, position.z);
    }
    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }
    private void ApplyFeedBack(bool validity)
    {
        // cambiamos el color dependiendo si se puede colocar el objeto o no
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
        previewMaterialInstance.color = c;
    }

}
