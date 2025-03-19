using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookTowardsItem : MonoBehaviour
{
    private GameObject lastItem;
    [SerializeField] private float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (lastItem != EventSystem.current.currentSelectedGameObject)
        {

            LookTowards(EventSystem.current.currentSelectedGameObject);
            lastItem = EventSystem.current.currentSelectedGameObject;


        }
    }

    public void LookTowards(GameObject item)
    {

        Vector3 direction = item.transform.position - Camera.main.transform.position; 
        Quaternion toRotation = Quaternion.LookRotation(direction);
        StartCoroutine(DoRotation(toRotation));
    }

    private IEnumerator DoRotation(Quaternion rotation)
    {
        float timer = 0;
        while(timer < 0.2f)
        {
            print(Camera.main.transform.rotation);
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, rotation, rotationSpeed*timer);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
