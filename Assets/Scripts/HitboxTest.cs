using UnityEngine;

public class HitboxTest : MonoBehaviour
{
    
    [SerializeField] private Transform attackerRoot;
    [SerializeField] private int damage = 10;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hurtbox"))
            Debug.Log("Hit landed on: " + other.name);
        
        // Find the defender root (assumes Hurtbox is under FighterRoot)
        Transform defenderRoot = other.transform.root;
        var health = defenderRoot.GetComponent<HealthAndStun>();
        if (!health) return;

        float dirX = Mathf.Sign(defenderRoot.position.x - attackerRoot.position.x); // push away from attacker
        health.TakeHit(damage, dirX);
    }
    
}
