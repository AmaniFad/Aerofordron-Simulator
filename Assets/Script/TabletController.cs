using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletController : MonoBehaviour
{

    private Animator tabletAnimator;
    private float animationTimer;
    private void Start()
    {
        tabletAnimator = GetComponent<Animator>();
    }
    public void LeaveTablet()
    {
        tabletAnimator.SetTrigger("dropTablet");
        animationTimer = tabletAnimator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(_DeactivateTablet());
        PlayerStateController.instance.ResumeMoving();
        PlayerStateController.instance.ResumeCameraMovement();
        Cursor.visible = false;
    }

    private IEnumerator _DeactivateTablet()
    {
        yield return new WaitForSeconds(animationTimer);
        gameObject.SetActive(false);
    }
}
