using UnityEngine;

public class CandleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameObject cake = GameObject.FindGameObjectWithTag("Cake");

        if (cake != null)
        {
            CakeScript cakeScript = cake.GetComponent<CakeScript>();

            if (cakeScript != null)
            {
                cakeScript.candles++;

                Debug.Log("Candles: " + cakeScript.candles);
            }
        }

        Destroy(gameObject);
    }
}