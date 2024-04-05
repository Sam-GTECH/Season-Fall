using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage");

        //Inflict damage to player
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}