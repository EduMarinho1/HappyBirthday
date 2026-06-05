using UnityEngine;

public class RegisterIngredientsScript : MonoBehaviour
{
    private CookScript cookScript;

    void Start()
    {
        GameObject cook = GameObject.Find("Cook");

        if (cook != null)
        {
            cookScript = cook.GetComponent<CookScript>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched: " + other.name);

        if (!other.CompareTag("Ingredient"))
            return;

        if (cookScript == null)
            return;

        switch (other.gameObject.name)
        {

            case "Raddish":
                cookScript.raddish = true;
                Debug.Log("Switch case");
                break;

            case "Garlic":
                cookScript.garlic = true;
                Debug.Log("Switch case");
                break;

            case "GreenOnion":
                cookScript.greenOnion = true;
                Debug.Log("Switch case");
                break;

            case "Cabbage":
                cookScript.cabbage = true;
                Debug.Log("Switch case");
                break;

            case "Onion":
                cookScript.onion = true;
                Debug.Log("Switch case");
                break;

            case "Shrimp":
                cookScript.shrimp = true;
                Debug.Log("Switch case");
                break;

            case "Gochugaru":
                cookScript.gochugaru = true;
                Debug.Log("Switch case");
                break;

            case "Chocolate":
                cookScript.chocolate = true;
                Debug.Log("Switch case");
                break;

            case "Tomato":
                cookScript.tomato = true;
                Debug.Log("Switch case");
                break;

            case "Cucumber":
                cookScript.cucumber = true;
                Debug.Log("Switch case");
                break;

            case "Cheese":
                cookScript.cheese = true;
                Debug.Log("Switch case");
                break;

            default:
                return;
        }

        Destroy(other.gameObject);
    }
}