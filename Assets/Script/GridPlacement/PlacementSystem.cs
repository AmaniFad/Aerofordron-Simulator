using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GridInputManager gridInputManager;
    [SerializeField] private Grid grid;

    [SerializeField] private ObjectDatabaseSO database;
    private int selectedObjIndex = -1;
    [SerializeField] private GameObject gridVisualitation;

    private GridData floorData, furnitureData;
    

    [SerializeField] private PreviewSystem preview;
    [SerializeField] private ObjectPlacer objPlacer;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        furnitureData = new();
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

        if(lastDetectedPosition != gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjIndex);

            mouseIndicator.transform.position = mousePosition;
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDetectedPosition = gridPosition;
        }
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
        preview.StartShowingPlacementPreview(database.objectsData[selectedObjIndex].Prefab, database.objectsData[selectedObjIndex].Size);
        gridInputManager.OnClicked += PlaceStructure;
        gridInputManager.OnExit += StopPlacement;
    }
    private void StopPlacement()
    {
        // para dejar la zona de crafteo
        selectedObjIndex = -1;
        gridVisualitation.SetActive(false);
        preview.StopShowingPreview();
        gridInputManager.OnClicked -= PlaceStructure;
        gridInputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
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

        int index = objPlacer.PlaceObj(database.objectsData[selectedObjIndex].Prefab, grid.CellToWorld(gridPosition));

        GridData selectedData = database.objectsData[selectedObjIndex].ID == 0 ? floorData : furnitureData;
        selectedData.AddObjAt(gridPosition, database.objectsData[selectedObjIndex].Size, database.objectsData[selectedObjIndex].ID, index);

        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjIndex)
    {
        GridData selectedData = database.objectsData[selectedObjIndex].ID == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjAt(gridPosition, database.objectsData[selectedObjIndex].Size);

    }
}
