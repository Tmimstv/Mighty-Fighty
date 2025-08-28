using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    [Header("Attacks")]
    public AttackData light;
    public AttackData medium;
    public AttackData heavy;

    [Header("Refs")]
    public HitboxTest hitbox;
    public Transform attackerRoot;

    private Animator anim;
    private bool isAttacking = false;
    private AttackData current;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (hitbox) hitbox.SetAttacker(attackerRoot);
    }

    void Update()
    {
        // SIMPLE INPUT
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.J)) StartAttack(light);
            else if (Input.GetKeyDown(KeyCode.K)) StartAttack(medium);
            else if (Input.GetKeyDown(KeyCode.L)) StartAttack(heavy);
        }
        else
        {
            // Basic chaining: allow cancels during the latter half of active → early recovery
            if (!CanChain()) return;

            if (Input.GetKeyDown(KeyCode.J) && current.canChainToLight) StartAttack(light);
            else if (Input.GetKeyDown(KeyCode.K) && current.canChainToMedium) StartAttack(medium);
            else if (Input.GetKeyDown(KeyCode.L) && current.canChainToHeavy) StartAttack(heavy);
        }
    }

    void StartAttack(AttackData data)
    {
        current = data;
        isAttacking = true;

        // Tell the hitbox what stats to use for THIS swing
        hitbox.ConfigureForAttack(current);

        // Drive Animator
        anim.CrossFadeInFixedTime(current.animatorStateName, 0.05f);

        // Drive the simple timeline (startup → active → recovery)
        StopAllCoroutines();
        StartCoroutine(AttackTimeline(current));
    }

    IEnumerator AttackTimeline(AttackData data)
    {
        // Startup
        hitbox.DeactivateHitbox();
        yield return new WaitForSeconds(data.startup);

        // Active
        hitbox.ActivateHitbox();
        yield return new WaitForSeconds(data.active);

        // Recovery
        hitbox.DeactivateHitbox();
        yield return new WaitForSeconds(data.recovery);

        isAttacking = false;
    }

    bool CanChain()
    {
        // Optional: use animator normalizedTime for precision.
        // Quick version: allow chaining once startup+half active has passed.
        float elapsedGate = current.startup + (current.active * 0.5f);
        // We don't track absolute elapsed here; timeline gating happens via inputs in Update
        // If you want stricter control, track a float timer incremented in AttackTimeline and expose it.
        return true;
    }
}