using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronAssemblyController : MonoBehaviour
{
    [SerializeField] private GameObject normalDronPartsContainer;
    [SerializeField] private GameObject transparentDronPartsContainer;
    [SerializeField] private float assemblyDistance;
    private Transform[] normalDronPartList;
    private Transform[] transparentDronPartsList;
    // Start is called before the first frame update
    private void Start()
    {
        normalDronPartList = normalDronPartsContainer.GetComponentsInChildren<Transform>();
        transparentDronPartsList = transparentDronPartsContainer.GetComponentsInChildren<Transform>();
    }
    public bool CheckIfClose(GameObject dronPart)
    {
        bool aux = false;
        int i = FindEqual(dronPart);
        Debug.Log("FoundEqual");
        Debug.Log(Vector3.Distance(dronPart.transform.position, transparentDronPartsList[i].transform.position));
        if (Vector3.Distance(dronPart.transform.position, transparentDronPartsList[i].transform.position) < assemblyDistance)
        {
            Debug.Log("Found");
            MountPart(dronPart, transparentDronPartsList[i].gameObject);
            dronPart.GetComponent<Collider>().isTrigger = true;
            dronPart.GetComponent<Rigidbody>().useGravity = false;
            aux = true;
        }

        return aux;
    }

    public int FindEqual(GameObject dronPart)
    {
        int aux = 0;
        for (int i = 0; i < normalDronPartList.Length; i++)
        {
            if (normalDronPartList[i].gameObject == dronPart)
            {
                aux = i;
            }
        }
        return aux;
    }
    public void MountPart(GameObject grabbedPart, GameObject targetPart)
    {
        int i = FindEqual(grabbedPart);
        transparentDronPartsList[i].gameObject.SetActive(false);


        StartCoroutine(PutPartInPlace(grabbedPart,targetPart));
    }

    public IEnumerator PutPartInPlace(GameObject grabbedPart, GameObject targetPart)
    {
        yield return new WaitForEndOfFrame();
        grabbedPart.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabbedPart.transform.position = targetPart.transform.position;
        grabbedPart.transform.rotation = targetPart.transform.rotation;
    }

}
