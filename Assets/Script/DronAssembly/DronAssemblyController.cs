using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronAssemblyController : MonoBehaviour
{
    [SerializeField] private GameObject normalDronPartsContainer;
    [SerializeField] private GameObject transparentDronPartsContainer;
    [SerializeField] private float assemblyDistance;
    [SerializeField]private Transform[] normalDronPartList;
    [SerializeField]private Transform[] transparentDronPartsList;
    private int currentPart;
    // Start is called before the first frame update
    private void Start()
    {
        currentPart = 0;
        FindEqual(new GameObject());
    }

    private void Update()
    {
        if (!normalDronPartList[currentPart].GetComponent<DronPartInteract>().interacted)
        {
            normalDronPartList[currentPart].GetComponent<DronPartFeedback>().ActivateFeedback();
        }
        else
        {
            normalDronPartList[currentPart].GetComponent<DronPartFeedback>().DeactivateFeedback();

        }
    }
    public bool CheckIfClose(GameObject dronPart)
    {
        bool aux = false;
        int i = FindEqual(dronPart);
        Debug.Log(Vector3.Distance(dronPart.transform.position, transparentDronPartsList[i].transform.position));
        if (Vector3.Distance(dronPart.transform.position, transparentDronPartsList[i].transform.position) < assemblyDistance)
        {
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
                transparentDronPartsList[i].GetComponent<DronPartFeedback>().ActivateFeedback();
                Debug.Log("FoundEqual");

                aux = i;
            }
            else
            {
                if (i != 0)
                {
                    Debug.Log(transparentDronPartsList[i].name + " Index: " + i);
                    transparentDronPartsList[i].GetComponent<DronPartFeedback>().DeactivateFeedback();
                }

            }
        }
        return aux;
    }

    public void MountPart(GameObject grabbedPart, GameObject targetPart)
    {
        int i = FindEqual(grabbedPart);
        transparentDronPartsList[i].gameObject.SetActive(false);
        normalDronPartList[currentPart].GetComponent<DronPartFeedback>().DeactivateFeedback();
        currentPart++;
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
