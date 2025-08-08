using UnityEngine;
using System.Collections; // Required for Coroutines

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Speed at which the player moves horizontally
    [SerializeField] private float jumpForce = 10f; // Force applied when the player jumps
    [SerializeField] private Transform groundCheck; // A transform marking the ground position (for checking if grounded)
    [SerializeField] private LayerMask groundLayer; // The layer(s) considered "ground"

    [Header("Combat Settings")]
    [SerializeField] private Transform attackPoint; // The point from which attacks originate
    [SerializeField] private float attackRange = 0.5f; // The radius of the attack
    [SerializeField] private LayerMask enemyLayers; // The layer(s) that enemies are on
    [SerializeField] private int attackDamage = 20; // Damage dealt per attack
    [SerializeField] private float attackRate = 2f; // Attacks per second
    private float nextAttackTime = 0f; // Time when the next attack is allowed

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component
    private Animator animator; // Reference to the player's Animator component (if you have one)
    private bool isGrounded; // True if the player is currently on the ground
    private bool facingRight = true; // True if the player is facing right

    // Called when the script instance is being loaded
    void Awake()
    {
        // Get references to components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator is optional, remove if not used

        // Initialize current health
        currentHealth = maxHealth;
    }

    // Called once per frame for physics updates
    void FixedUpdate()
    {
        // Check if the player is grounded using a circle cast
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Set the "IsGrounded" parameter in the Animator if it exists
        if (animator != null)
        {
            animator.SetBool("IsGrounded", isGrounded);
        }

        // Get horizontal input (A/D or Left/Right Arrow keys)
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player horizontally
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Set the "Speed" parameter in the Animator for walking/running animation
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        // Flip the player's sprite if changing direction
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    // Called once per frame for general updates
    void Update()
    {
        // Jumping input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // Trigger the "Jump" animation if you have one
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        // Attack input
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // Left mouse click
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // Flips the player's sprite horizontally
    void Flip()
    {
        facingRight = !facingRight; // Toggle the boolean
        Vector3 scaler = transform.localScale; // Get current scale
        scaler.x *= -1; // Flip the x-component of the scale
        transform.localScale = scaler; // Apply the new scale
    }

    // Handles the player's attack action
    void Attack()
    {
        // Play an attack animation if you have one
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            // Assuming enemies have a script with a 'TakeDamage' method
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);
            }
            // Optional: Debugging to see what was hit
            Debug.Log("Hit " + enemy.name);
        }
    }

    // Method for the player to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);

        // Optional: Play a hit animation or sound effect
        // animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method when the player's health reaches zero
    void Die()
    {
        Debug.Log("Player has died!");
        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Disable player controls
        this.enabled = false; // Disables this script
        GetComponent<Collider2D>().enabled = false; // Disable collider (optional, depends on desired effect)
        rb.gravityScale = 0; // Stop gravity (optional)
        rb.velocity = Vector2.zero; // Stop all movement (optional)

        // TODO: Implement game over, reload scene, etc.
        // Destroy(gameObject); // Optionally destroy the player object
    }

    // Visualizes the attack range in the Unity editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

// --- IMPORTANT: You will also need a simple Enemy script like this ---
// Create a new C# script named "Enemy.cs" and attach it to your enemy GameObjects.
/*
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damageToPlayer = 10; // How much damage this enemy deals

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(transform.name + " took " + damage + " damage. Current Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(transform.name + " died!");
        Destroy(gameObject); // Destroy the enemy GameObject when it dies
    }

    // Example: If enemy collides with player and deals damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damageToPlayer);
        }
    }
}
*/