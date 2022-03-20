using UnityEngine;

// <summary>
/// This class manages the enemy missiles
/// </summary>
public class EnemyMissileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    private void OnEnable()
    {
        rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    // <summary>
    /// Checks collision with the player
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        GameManager.PInstance.OnPlayerDestroyed();
    }
}