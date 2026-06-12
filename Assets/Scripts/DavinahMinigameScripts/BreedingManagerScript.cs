using System.Collections.Generic;
using UnityEngine;

public class BreedingManagerScript : MonoBehaviour
{
    public static BreedingManagerScript Instance;

    private FoodTriggerScript currentFood;

    private Dictionary<string, string> correctFood = new Dictionary<string, string>()
    {
        { "BreedPanda", "Bamboo" },
        { "BreedRabbit", "Carrot" },
        { "BreedHorse", "GoldenApple" },
        { "BreedTurtle", "SeaGrass" },
        { "BreedLlama", "HayBale" },
        { "BreedFox", "Berries" },
        { "BreedFrog", "SlimeBall" },
        { "BreedCamel", "Cactus" }
    };

    private void Awake()
    {
        Instance = this;
    }

    public void SelectFood(FoodTriggerScript food)
    {
        if (currentFood != null)
        {
            currentFood.SetSelected(false);
        }

        currentFood = food;
        currentFood.SetSelected(true);

        Debug.Log("Selected Food: " + food.gameObject.name);
    }

    public void TryBreed(string breedButtonName, GameObject breedButton)
    {
        if (currentFood == null)
        {
            Debug.Log("No food selected.");
            return;
        }

        bool correct =
            correctFood.ContainsKey(breedButtonName) &&
            correctFood[breedButtonName] == currentFood.gameObject.name;

        string leftAnimalName =
            breedButtonName.Replace("Breed", "") + "_Left";

        string rightAnimalName =
            breedButtonName.Replace("Breed", "") + "_Right";

        Transform leftAnimal =
            transform.Find("Animals/" + leftAnimalName);

        Transform rightAnimal =
            transform.Find("Animals/" + rightAnimalName);

        if (correct)
        {
            Debug.Log("Correct!");

            if (leftAnimal != null)
            {
                Vector3 scale = leftAnimal.localScale;
                scale.x *= -1f;
                leftAnimal.localScale = scale;

                leftAnimal.position += new Vector3(2f, 0f, 0f);
            }

            if (rightAnimal != null)
            {
                rightAnimal.position += new Vector3(0f, 0.5f, 0f);

                rightAnimal.rotation = Quaternion.Euler(0f, 0f, -45f);
            }
        }
        else
        {
            Debug.Log("Wrong!");

            if (leftAnimal != null)
            {
                Vector3 scale = leftAnimal.localScale;
                scale.x *= -1f;
                leftAnimal.localScale = scale;
            }

            if (rightAnimal != null)
            {
                Vector3 scale = rightAnimal.localScale;
                scale.x *= -1f;
                rightAnimal.localScale = scale;
            }
        }

        Destroy(currentFood.gameObject);
        Destroy(breedButton);

        currentFood = null;
    }
}