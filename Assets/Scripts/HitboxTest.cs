using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitboxTest : MonoBehaviour
{
    [SerializeField] private Transform attackerRoot;
    
    [SerializeField] private bool alwaysHot = true; //having weird issue where ybot doesnt react anymore

    // Runtime-configured per attack
    private AttackData attack;
    private bool isActive;
    private HashSet<Transform> hitThisSwing = new HashSet<Transform>();
    private AudioSource audioSource;

    void Awake()
    {
        if (attackerRoot)
        {
            //if i want to add sound later
            audioSource = attackerRoot.GetComponent<AudioSource>();
            if (!audioSource) audioSource = attackerRoot.gameObject.AddComponent<AudioSource>();
        }
        if (alwaysHot) isActive = true;
    }

    public void SetAttacker(Transform attacker) { attackerRoot = attacker; }

    public void ConfigureForAttack(AttackData data)
    {
        attack = data;
        hitThisSwing.Clear();
    }

    public void ActivateHitbox()
    {
        isActive = true;
        hitThisSwing.Clear();
    }

    public void DeactivateHitbox() => isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
    {
        // Debug.Log("[Hitbox] Ignored hit: not active");
        isActive = true;
    }
        if (other.gameObject.layer != LayerMask.NameToLayer("Hurtbox")) return;
        if (attack == null || attackerRoot == null) return;

        Transform defenderRoot = other.transform.root;
        if (!hitThisSwing.Add(defenderRoot)) return;

        var health = defenderRoot.GetComponent<HealthAndStun>();
        if (!health) return;

        float dirX = Mathf.Sign(defenderRoot.position.x - attackerRoot.position.x);

        // Apply attack stats
        health.TakeHit(attack.damage, dirX * attack.knockback);

        // FX
        if (attack.hitSparkPrefab)
            Instantiate(attack.hitSparkPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
        if (attack.hitSfx && audioSource)
            audioSource.PlayOneShot(attack.hitSfx);

        // Hitstop
        StartCoroutine(HitStop(attack.hitStop));
    }

    private IEnumerator HitStop(float duration)
    {
        if (duration <= 0f) yield break;
        float prev = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = prev;
    }
}
