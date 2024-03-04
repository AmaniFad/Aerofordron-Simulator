using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] GameObject sceneTransition;
    private Animator sceneAnimator;
    void Start()
    {
        sceneAnimator = sceneTransition.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition()
    {
        sceneTransition.SetActive(true);
        PlayLeaveTransition();
    }
    public void PlayLeaveTransition()
    {
        sceneAnimator.SetTrigger("leaveTransition");
        StartCoroutine(_WaitForAnimationEnd(sceneAnimator.GetCurrentAnimatorClipInfo(0).Length));
    }

    public void PlayEnterTransition()
    {
        sceneTransition.GetComponent<Animator>().SetTrigger("enterTransition");

    }

    private IEnumerator _WaitForAnimationEnd(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        PlayEnterTransition();
        StartCoroutine(_EndTransition(sceneAnimator.GetCurrentAnimatorClipInfo(0).Length));
    }

    private IEnumerator _EndTransition(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        PlayEnterTransition();
    }


}
