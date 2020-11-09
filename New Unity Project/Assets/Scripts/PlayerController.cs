/* 
 * logan Ross
 * assignment 7
 * allows for player movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;

    private float forwardInput;

    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public GameManager gm;

    public bool hasPowerup = false;
    private float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
         forwardInput = Input.GetAxis("Vertical");

        //move powerup indicator
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(transform.position.y < -5)
        {
            gm.gameOver = true;
        }
    }

    private void FixedUpdate()
    {
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Player collision w powerup");

            //get local ref to enemy rigidbody
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            //set vector3 w/ a direction away from payer
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            //add force away from player
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
