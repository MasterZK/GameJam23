using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private GameObject point;
    [SerializeField] private float force;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MovemenOffOn(collision.gameObject.GetComponent<PlayerController>()));
            collision.gameObject.transform.position = point.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * force, ForceMode2D.Impulse);
        }
    }

    IEnumerator MovemenOffOn(PlayerController player)
    {
        player.ToggleMovement();
        yield return new WaitForSeconds(0.3f);
        player.ToggleMovement();
    }
}
