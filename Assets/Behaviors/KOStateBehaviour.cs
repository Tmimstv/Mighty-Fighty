using UnityEngine;

public class KOStateBehaviour : StateMachineBehaviour
{
    public bool useTriggerForDowned = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var switcher = animator.GetComponentInParent<ColliderProfileSwitcher>();
        if (switcher)
        {
            switcher.downedAsTrigger = useTriggerForDowned;
            switcher.SetDowned(true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var switcher = animator.GetComponentInParent<ColliderProfileSwitcher>();
        if (switcher) switcher.SetDowned(false);
    }
}