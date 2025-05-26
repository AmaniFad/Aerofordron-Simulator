using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JoystickAnimation : MonoBehaviour
{
    [SerializeField] private PlaySounds soundPlayer;
    [SerializeField] private Animator leftStickAnimator;
    [SerializeField] private Animator rightStickAnimator;
    private Coroutine checker;
    private AnimatorClipInfo[] currentAnimation;
    // Start is called before the first frame update
    void Start()
    {
        currentAnimation = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        AnimationsLeftStick();
        AnimationsRightStick();

    }



    private void AnimationsLeftStick()
    {
        float leftStickYInput = DisplayInputData.leftControllerDirection.y;
        float leftStickXInput = DisplayInputData.leftControllerDirection.x;
        if (leftStickYInput > 0f)
        {
            leftStickAnimator.SetBool("LUp", true);
            leftStickAnimator.SetBool("LDown", false);
        }
        else if (leftStickYInput < 0f)
        {
            leftStickAnimator.SetBool("LDown", true);
            leftStickAnimator.SetBool("LUp", false);

        }
        else
        {

            leftStickAnimator.SetBool("LUp", false);
            leftStickAnimator.SetBool("LDown", false);
        }

        if (leftStickXInput > 0)
        {
            leftStickAnimator.SetBool("LRight", true);
            leftStickAnimator.SetBool("LLeft", false);

        }
        else if (leftStickXInput < 0)
        {
            leftStickAnimator.SetBool("LLeft", true);
            leftStickAnimator.SetBool("LRight", false);
        }
        else
        {
            leftStickAnimator.SetBool("LRight", false);
            leftStickAnimator.SetBool("LLeft", false);

        }
    }

    private void AnimationsRightStick()
    {
        float leftStickYInput = DisplayInputData.rightControllerDirection.y;
        float leftStickXInput = DisplayInputData.rightControllerDirection.x;
        if (leftStickYInput > 0f)
        {
            rightStickAnimator.SetBool("LUp", true);
            rightStickAnimator.SetBool("LDown", false);
        }
        else if (leftStickYInput < 0f)
        {
            rightStickAnimator.SetBool("LDown", true);
            rightStickAnimator.SetBool("LUp", false);

        }
        else
        {

            rightStickAnimator.SetBool("LUp", false);
            rightStickAnimator.SetBool("LDown", false);
        }

        if (leftStickXInput > 0)
        {
            rightStickAnimator.SetBool("LRight", true);
            rightStickAnimator.SetBool("LLeft", false);

        }
        else if (leftStickXInput < 0)
        {
            rightStickAnimator.SetBool("LLeft", true);
            rightStickAnimator.SetBool("LRight", false);
        }
        else
        {
            rightStickAnimator.SetBool("LRight", false);
            rightStickAnimator.SetBool("LLeft", false);

        }
    }

    public void CallOneShot( )
    {
        StudioEventEmitter emitter = GetComponent<StudioEventEmitter>();
        emitter.Play();
        //FMODUnity.RuntimeManager.PlayOneShot(eventRoute);
    }

    private IEnumerator GraceTimeToReturnStickToPlace()
    {
        yield return new WaitForSeconds(0.3f);
        float leftStickXInput = DronInputController.Instance.GetDirectionInput().x;
        if (leftStickXInput == 0)
        {

        }
        checker = null;
    }
}
