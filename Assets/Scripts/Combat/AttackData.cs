using UnityEngine;


[CreateAssetMenu(menuName = "MightyFighty/Attack Data")]

public class AttackData : UnityEngine.ScriptableObject
{
    [Header("Gameplay")]
    public int damage = 10;
    public float knockback = 2.5f;
    public float hitStop = 0.05f;

    [Header("Timing (sec)")]
    public float startup = 0.08f;
    public float active = 0.10f;
    public float recovery = 0.18f;

    [Header("Cancel Windows")]
    public bool canChainToLight;
    public bool canChainToMedium;
    public bool canChainToHeavy;

    [Header("Animation & FX")]
    public string animatorStateName; // e.g., "Light"
    public AudioClip hitSfx;
    public GameObject hitSparkPrefab;
}
