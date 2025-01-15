using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class DroneSelection : MonoBehaviour
{
    [SerializeField] GameObject drones;
    [SerializeField] GameObject droneModelPlace;
    [SerializeField] GameObject dronImagesPlace;
    [SerializeField] GameObject currentUIDrone;
    [SerializeField] GridLayoutGroup gridContainer;
    [SerializeField] GameObject finalDronePlace;

    [System.Serializable]
    class SelectionItem
    {
        [SerializeField] public Sprite itemImage;
        [SerializeField] public GameObject droneModel;
    }
    [SerializeField] SelectionItem[] items;
    void Start()
    {
        foreach (SelectionItem i in items)
        {
            GameObject imageContainer = new GameObject();
            imageContainer.AddComponent<Image>();
            imageContainer.AddComponent<CanvasRenderer>();
            imageContainer.GetComponent<Image>().sprite = i.itemImage;
            imageContainer.AddComponent<DroneModelContainer>();
            imageContainer.GetComponent<DroneModelContainer>().SetDroneModel( i.droneModel);
            imageContainer.AddComponent<Button>();
            imageContainer.AddComponent<GraphicRaycaster>();
            imageContainer.AddComponent<TrackedDeviceGraphicRaycaster>();
            imageContainer.GetComponent<Button>().onClick.AddListener(() => DisplayDroneModel(imageContainer.GetComponent<DroneModelContainer>().GetDroneModel()));
            imageContainer.name = "dronImage";
            imageContainer.transform.parent = dronImagesPlace.transform;
            imageContainer.transform.localScale = Vector3.one;
            StartCoroutine(SetImagePosition(imageContainer));
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDroneModel(GameObject dronModel)
    {
        print("DisplayDroneModel");
        Destroy(currentUIDrone);
        currentUIDrone = Instantiate(dronModel, droneModelPlace.transform);
        currentUIDrone.transform.localRotation = Quaternion.Euler(Vector3.zero);
        currentUIDrone.name = "dronModel";
    }

    private IEnumerator SetImagePosition(GameObject image)
    {
        yield return new WaitForEndOfFrame();
        Vector3 imagePos = image.transform.localPosition;
        Vector3 position = new Vector3(imagePos.x,imagePos.y,0);
        image.transform.localPosition = position;
        image.transform.localRotation = Quaternion.Euler(Vector3.zero);


    }

    public void SelectDrone()
    {
        if (currentUIDrone)
        {
            Instantiate(currentUIDrone, finalDronePlace.transform);
            this.gameObject.SetActive(false);
        }
    }
}
