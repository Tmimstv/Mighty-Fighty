using UnityEngine;

public class ColliderProfileSwitcher : MonoBehaviour
{
    [Header("Standing vs Downed")]
    public Collider standingCollider;  
    public Collider downedCollider;
    public bool downedAsTrigger = false;

    public void SetDowned(bool down)
    {
        if (standingCollider) standingCollider.enabled = !down;
        if (downedCollider)
        {
            downedCollider.enabled = down;
            downedCollider.isTrigger = downedAsTrigger && down;
        }
    }
}
