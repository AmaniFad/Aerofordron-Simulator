using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private GridInputManager gridInputManager;
    [SerializeField] private Grid grid;

    [SerializeField] private ObjectDatabaseSO database;
    private int selectedObjIndex = -1;
    [SerializeField] private GameObject gridVisualitation;

    private GridData floorData, furnitureData;
    private Renderer previewRenderer;
    private List<GameObject> placeGameObject = new();

    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        furnitureData = new();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>(); 
    }
    private void Update()
    {
        //feedback de donde estamos econ el raton encima del terreno  de cosntuccion
        if(selectedObjIndex < 0)
        {
            return;
        }
        Vector3 mousePosition = gridInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
    public void StartPlacement(int ID)
    {
        //esta funcion va atada a los botones, es para seleccionar el obj que vas a poner 
        StopPlacement();
        selectedObjIndex = database.objectsData.FindIndex(data  => data.ID == ID);
        if(selectedObjIndex < 0)
        {
            Debug.LogError("No ID found");
            return;
        }
        gridVisualitation.SetActive(true);
        cellIndicator.SetActive(true);
        gridInputManager.OnClicked += PlaceStructure;
        gridInputManager.OnExit += StopPlacement;
    }
    private void StopPlacement()
    {
        // para dejar la zona de crafteo
        selectedObjIndex = -1;
        gridVisualitation.SetActive(false);
        cellIndicator.SetActive(false);
        gridInputManager.OnClicked -= PlaceStructure;
        gridInputManager.OnExit -= StopPlacement;
    }
    private void PlaceStructure()
    {
        // para intanciar el obj en donde los quieres de la zona 
        if (gridInputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = gridInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjIndex);
        if (placementValidity == false)
        {
            return;
        }

        GameObject gameObj = Instantiate(database.objectsData[selectedObjIndex].Prefab);
        gameObj.transform.position = grid.CellToWorld(gridPosition);
        placeGameObject.Add(gameObj);

        GridData selectedData = database.objectsData[selectedObjIndex].ID == 0 ? floorData : furnitureData;
        selectedData.AddObjAt(gridPosition, database.objectsData[selectedObjIndex].Size, database.objectsData[selectedObjIndex].ID,placeGameObject.Count - 1);
    }
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjIndex)
    {
        GridData selectedData = database.objectsData[selectedObjIndex].ID == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjAt(gridPosition, database.objectsData[selectedObjIndex].Size);

    }
}
