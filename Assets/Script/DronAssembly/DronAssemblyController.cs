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
    [SerializeField] private GameObject incorrectPartMessage;
    [SerializeField] private int currentPart;
    // Start is called before the first frame update
    private void Start()
    {
        currentPart = 0;
        normalDronPartList[currentPart].GetComponent<DronPartFeedback>().ActivateFeedback();
        FindEqual(new GameObject());
    }

    private void Update()
    {
    }
    public bool CheckIfClose(GameObject dronPart)
    {
        Debug.Log("CheckIfClose");
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
        Debug.Log("FindEqual");
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
        Debug.Log("MountPart");
        int i = FindEqual(grabbedPart);
        if (i == currentPart)
        {
            transparentDronPartsList[i].gameObject.SetActive(false);
            normalDronPartList[currentPart].GetComponent<DronPartFeedback>().DeactivateFeedback();
            StartCoroutine(PutPartInPlace(grabbedPart, targetPart));
        }
        else
        {
            Debug.Log("Current Part + " + currentPart + " Dron Part " + i);
            incorrectPartMessage.SetActive(true);
            StartCoroutine(_DeactivateIncorrectMessage(grabbedPart));
        }

    }

    public bool IsCurrentPart(GameObject part)
    {
        int i = FindEqual(part);
        bool aux = false;
        if (i == currentPart)
        {
            aux = true;
        }
        return aux;
    }
    private IEnumerator _DeactivateIncorrectMessage(GameObject incorrectMessage)
    {
        yield return new WaitForSeconds(2);
        incorrectMessage.SetActive(false);
    }
    public IEnumerator PutPartInPlace(GameObject grabbedPart, GameObject targetPart)
    {
        Debug.Log("PutPartInPlace");
        yield return new WaitForEndOfFrame();
        grabbedPart.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabbedPart.transform.position = targetPart.transform.position;
        grabbedPart.transform.rotation = targetPart.transform.rotation;
        currentPart++;
    }

}
