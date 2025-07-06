using TMPro;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    //population, wood, gold, food, stone, iron, tools
    [SerializeField] private int days = 0; //rjeseno
    [SerializeField] private int population = 0; //rjeseno
    [SerializeField] private int workers = 0; // rjeseno
    [SerializeField] private int unemployed = 0; // rjeseno
    [SerializeField] private int wood = 0; //rjeseno
    [SerializeField] private int food = 0; //rjeseno
    [SerializeField] private int iron = 0;
    // farm, house, iron mine, gold mine, lumberjack, blacksmith, quarry
    [SerializeField] private int farm = 0; // rjeseno
    [SerializeField] private int house = 0; // rjeseno
    [SerializeField] private int woodcutter = 0; //rjeseno
    [SerializeField] private TMP_Text daysText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text populationText;
    [SerializeField] private TMP_Text farmText;
    [SerializeField] private TMP_Text houseText;
    [SerializeField] private TMP_Text woodCutterText;
    private float timer;


    void Update()
    {
        TimePassed();

    }
    private void TimePassed()
    {
        timer += Time.deltaTime;
        if (timer >= 7)
        {
            if (food <= 0 && Population() > 0)
            {
                StopAllCoroutines();
                    GameManager.Instance.GameOverPanel.SetActive(true);
                    GameManager.Instance.Buildings.SetActive(false);
                    GameManager.Instance.StartGamePanel.SetActive(false);
                    GameManager.Instance.StartGame.SetActive(false);
                    GameManager.Instance.PauseGamePanel.SetActive(false);
                    GameManager.Instance.MainMenuPanel.SetActive(false);

                Debug.Log("Game Over! No food left.");
                 return;     //ako hrana padne na 0, game over

            }
            days++;
            WoodProduction();
            FoodGathering();
            FoodProduction();
            FoodConsumption(1);
            IncreasePopulation();
            timer = 0f;
            UpdateText();
            // Update UI or other game elements here if needed
        }
    }
    private void FoodProduction() // each farm produces 2 food per day
    {
        food += farm * 4;
    }
    private void FoodGathering()
    {
        food += unemployed / 2;
    } //each unemployed worker produces 0.5 food per day, rounded down
    private int Population()
    {
        return workers + unemployed;
    } // vraca populaciju
    private void IncreasePopulation()
    {
        if (days % 2 == 0)
        {
            if (GetMaxPopulation() > Population())
            {
                unemployed += house; // Increase population by the number of houses which can house 4 people each
                Debug.Log("Population increased to: " + population);
                if (GetMaxPopulation() < Population())
                {
                    unemployed = GetMaxPopulation() - workers;
                }
            }
            else
            {
                Debug.Log("Max population reached: " + GetMaxPopulation());
            }
        }
    } // Increase population by the number of houses which can house 4 people each
    private void FoodConsumption(int foodConsumed)
    {
        food -= foodConsumed * Population();
    } // food umanjen za populaciju
    private int GetMaxPopulation() //number of max house*4
    {
        int maxPopulation = house * 4;
        return maxPopulation;
    }

    public void BuildFarm() // cost of farm is 10 wood and requires 2 unemployed workers to be available but doesnt consume them
    {
        if (wood >= 10 && CanAssignWorker(2))
        {
            wood -= 10;
            farm++;
            UpdateText();
        }
        else
        {
            Debug.Log("Not enough resources");
        }
    }
    public void BuildWoodCutter() // cost of woodcutter is 5 wood and requires 1 unemployed worker
    {
        if (wood >= 5 && CanAssignWorker(1) && iron > 0)
        {
            iron--;
            wood -= 5;
            woodcutter++;
            unemployed--;
            workers++;
            UpdateText();
        }
    }
    public void BuildHouse()
    {
        if (CanAssignWorker(1) && wood >= 2)
        {

            wood -= 2;
            house++;
            UpdateText();
        }
    }
    public void WoodProduction() // each woodcutter produces 2 wood per day
    {
        wood += woodcutter * 2;
    }
    public void BuildCost(int woodCost, int stoneCost, int workerAssign)
    {
        if (wood >= woodCost && unemployed >= workerAssign)
        {
            wood -= woodCost;
            unemployed -= workerAssign;
            workers += workerAssign;
        }
    }
    public void WorkerAssign(int amount)
    {
        if (unemployed > amount)
        {
            unemployed -= amount;
            workers += amount;
        }
    } // number of workers we assign
    private bool CanAssignWorker(int amount)
    {
        return unemployed >= amount;
    } //check if we can assign workers
    private void UpdateText()
    {
        //resources
        populationText.text = $"Population={Population()}/{GetMaxPopulation()}\n Workers: {workers}\n     Unemployed:{unemployed}";
        foodText.text = $"Food: {food}";
        woodText.text = $"Wood: {wood}";        //buildings
        farmText.text = $"Farm: {farm}";
        houseText.text = $"House: {house}";
        woodCutterText.text = $"Woodcutter: {woodcutter}";
    }

}
