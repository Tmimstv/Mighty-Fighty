using UnityEngine;
using UnityEngine.UI;

public class HealthAndStun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Stats")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Slider healthBar;
    
    [SerializeField] private float knockback = 4f;
    [SerializeField] private float verticalPop = 1.5f;
    [SerializeField] private float hitstun = 0.25f;
    
    [Header("Refs")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Renderer[] renderersToFlash;
    
    
    
    
    private Animator animator;
    
    private int hp;
    private float stunTimer;

    public void Awake()
    {
        hp = maxHP;
        healthBar.minValue = 0;
        healthBar.maxValue = maxHP;
        healthBar.value = maxHP;
    }
    
    
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    public void TakeHit(int dmg, float dirX)
    {
        hp = Mathf.Max(0, hp - dmg);
        stunTimer = hitstun;
        healthBar.value = hp;
        

        // knockback away from attacker
        // var v = rb.linearVelocity;
        // v.x = dirX * knockback;
        // v.y = Mathf.Max(v.y, verticalPop);
        // rb.linearVelocity = v;
        
        if (hp == 0)
        {
            // simple KO for now
            Debug.Log($"{name} KO!");
            
            //to play knockout, make animation and add animator.SetTrigger("Knockout")
            animator.SetTrigger("Knockout");
            return;
        }
        
        animator.SetTrigger("Hit");

        StartCoroutine(Flash());
        
        


    }

    public bool IsStunned => stunTimer > 0f;
    // Update is called once per frame
    private void Update()
    {
        if (stunTimer > 0f) stunTimer -= Time.deltaTime; 
    }
    
    System.Collections.IEnumerator Flash()
    {
        // quick white flash using emission keyword (works with URP/Lit too if emissive)
        foreach (var r in renderersToFlash) r.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.06f);
        foreach (var r in renderersToFlash) r.material.DisableKeyword("_EMISSION");
    }
    
}
