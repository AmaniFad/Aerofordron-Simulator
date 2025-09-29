using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DroneCrash : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject destroyedDron;
    private DronController controller;
    private GameObject currentDestroyedDronFeedback;
    private CinemachineVirtualCamera virtualCamera;
    private Transform previousTransform;
    private Quaternion previousRotation;
    private Rigidbody dronRb;
    [SerializeField] private CinemachineVirtualCamera thirrdPersonViewCamera;
    [SerializeField] private float speedThreshold;
    private float currentSpeed;
    [SerializeField] private int damage;
    private bool isCrashed;
    void Start()
    {
        controller = GetComponent<DronController>();
        dronRb = GetComponent<Rigidbody>();
        previousRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsCrashed()
    {
        return isCrashed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!controller.IsGrounded() && !currentDestroyedDronFeedback && (currentSpeed > speedThreshold))
        {
            Respawn();
            isCrashed = true;
        }
    }


    public void Respawn()
    {
        SwitchToFullView.instance.ExitFullView();
        PlayerInputController.Instance.SetFullView(false);
        currentDestroyedDronFeedback = Instantiate(destroyedDron);
        currentDestroyedDronFeedback.transform.position = transform.position;
        gameObject.transform.rotation = previousRotation;
        gameObject.transform.position = spawnPoint.position;
        if(this.gameObject.GetComponent<HealthBehaviour>() != null )
        {
            gameObject.GetComponent<HealthBehaviour>().Damage(damage);
        }
        virtualCamera = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        virtualCamera.Priority = 10;
        thirrdPersonViewCamera.Priority = 12;
        previousTransform = virtualCamera.Follow;
        InteractionZone.Instance.gameObject.SetActive(false);
        thirrdPersonViewCamera.Follow = currentDestroyedDronFeedback.transform;
        thirrdPersonViewCamera.LookAt = currentDestroyedDronFeedback.transform;
        controller.StopMovingDron();
        StartCoroutine(RecoverCamera(4));
        StartCoroutine(DestroyFeedback());
    }
    public void GoToFirstPosition()
    {
        SwitchToFullView.instance.ExitFullView();
        PlayerInputController.Instance.SetFullView(false);
        gameObject.transform.rotation = previousRotation;
        gameObject.transform.position = spawnPoint.position;

        virtualCamera = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        StartCoroutine(RecoverCamera(4));
    }

    private IEnumerator RecoverCamera(float time)
    {
        yield return new WaitForSeconds(time);
        InteractionZone.Instance.gameObject.SetActive(true);
        thirrdPersonViewCamera.Priority = 10;

        virtualCamera.Priority = 12;
        thirrdPersonViewCamera.LookAt = transform;
        thirrdPersonViewCamera.Follow = previousTransform;

        controller.StartMovingDron();
        isCrashed = false;
    }

    private IEnumerator DestroyFeedback()
    {
        yield return new WaitForSeconds(6);
        Destroy(currentDestroyedDronFeedback);
    }

    private void LateUpdate()
    {
        currentSpeed = dronRb.linearVelocity.magnitude * (60 * 60) / 100;
    }
}
