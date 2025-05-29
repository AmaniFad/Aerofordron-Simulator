using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookTowardsItem : MonoBehaviour
{
    private GameObject lastItem;
    [SerializeField] private float rotationSpeed;
    CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        virtualCamera = Camera.main.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (lastItem != EventSystem.current.currentSelectedGameObject)
        {

            //LookTowards(EventSystem.current.currentSelectedGameObject);
            //lastItem = EventSystem.current.currentSelectedGameObject;


        }
    }

    public void LookTowards(GameObject item)
    {
        print(item.transform);
        //Camera.main.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera..GetComponent<CinemachineVirtualCamera>().LookAt = item.transform;
    }


}
