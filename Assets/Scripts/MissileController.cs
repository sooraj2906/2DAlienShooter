using UnityEngine;

// <summary>
/// This class manages the okayer missile
/// </summary>
public class MissileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    private void OnEnable()
    {
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    // <summary>
    /// Checks collision with enemies
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
        GameManager.PInstance.UpdateScore(1);
        GameManager.PInstance.OnEnemiesDestroyed();
    }

    // <summary>
    /// Checks collision with enemy missiles
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("EnemyMissile")) return;
        other.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}