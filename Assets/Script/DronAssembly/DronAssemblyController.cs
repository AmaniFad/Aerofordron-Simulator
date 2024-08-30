using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DronAssemblyController : MonoBehaviour
{
    [SerializeField] private GameObject normalDronPartsContainer;
    [SerializeField] private GameObject transparentDronPartsContainer;
    [SerializeField] private float assemblyDistance;
    [SerializeField] private Transform[] normalDronPartList;
    [SerializeField] private Transform[] transparentDronPartsList;
    [SerializeField] private GameObject incorrectPartMessage;
    [SerializeField] private int currentPart;
    [SerializeField] private GameObject onMountFeedback;
    public UnityEvent OnMount;
    // Start is called before the first frame update
    private void Start()
    {
        transform.rotation = Quaternion.identity;
        currentPart = 0;
        normalDronPartList[currentPart].GetComponent<Outline>().OutlineWidth = 10;
        transparentDronPartsList[currentPart].GetComponent<Outline>().OutlineWidth = 10;
        FindEqual(new GameObject());
    }

    private void Update()
    {
        if (currentPart == normalDronPartList.Length)
        {
            if (MountDronController.instance)
            {
                MountDronController.instance.NextInstruction();
            }
            OnMount.Invoke();
        }
    }
    public bool CheckIfClose(GameObject dronPart)
    {
        bool aux = false;
        int i = FindEqual(dronPart);
        if (Vector3.Distance(dronPart.transform.position, transparentDronPartsList[i].transform.position) < assemblyDistance)
        {
            MountPart(dronPart, transparentDronPartsList[i].gameObject);
            normalDronPartList[i].GetComponent<Outline>().OutlineWidth = 0;
            if (i+ 1 < normalDronPartList.Length)
            {
                normalDronPartList[i + 1].GetComponent<Outline>().OutlineWidth = 10;
                transparentDronPartsList[i + 1].GetComponent<Outline>().OutlineWidth = 10;
            }
            transparentDronPartsList[i].GetComponent<Outline>().OutlineWidth = 0;

            //dronPart.GetComponent<Collider>().isTrigger = true;
            //dronPart.GetComponent<Rigidbody>().useGravity = false;
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
                if (!normalDronPartList[i].GetComponent<DronPartInteract>().IsMounted())
                    {

                }


                aux = i;
            }
            else
            {
                if (i != 0)
                {
                }

            }
        }
        return aux;
    }

    public void MountPart(GameObject grabbedPart, GameObject targetPart)
    {
        grabbedPart.GetComponent<Collider>().isTrigger = false;
        int i = FindEqual(grabbedPart);
        print(i);
        if (i == currentPart)
        {
            print("hola1");
            transparentDronPartsList[i].gameObject.SetActive(false);
            normalDronPartList[currentPart].GetComponent<DronPartInteract>().Mounted();
            StartCoroutine(PutPartInPlace(grabbedPart, targetPart, 0.5f));
        }
        else
        {
            incorrectPartMessage.SetActive(true);
            StartCoroutine(_DeactivateIncorrectMessage(incorrectPartMessage));
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
    public IEnumerator PutPartInPlace(GameObject grabbedPart, GameObject targetPart, float duration)
    {
        // Deactivate feedback for the current part

        // Ensure the coroutine waits for the end of frame
        yield return new WaitForEndOfFrame();

        // Initialize the start position and rotation
        Vector3 startPosition = grabbedPart.transform.position;
        Quaternion startRotation = grabbedPart.transform.rotation;

        // Target position and rotation
        Vector3 targetPosition = targetPart.transform.position;
        Quaternion targetRotation = targetPart.transform.rotation;

        // Ensure the Rigidbody has zero velocity
        //Rigidbody grabbedRigidbody = grabbedPart.GetComponent<Rigidbody>();
        //if (grabbedRigidbody != null)
        //{
        //    grabbedRigidbody.velocity = Vector3.zero;
        //}

        // Interpolate over the specified duration
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            grabbedPart.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            grabbedPart.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null; 
        }

        grabbedPart.transform.position = targetPosition;
        grabbedPart.transform.rotation = targetRotation;

        // Deactivate feedback for the transparent part

        // Increment the current part index
        currentPart++;
    }

}
