using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager for the game events and such. Meat and potatoes and what not.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int money = 0;
    public int[] inventory;
    public int dowsingLevel;
    public int diggingLevel;
    public Sprite[] playerSprites;
    float startTime;
    [HideInInspector] public int numberOfTreasuresFound;

    [Header("Event Related")]
    public bool diggingTime;

    [Header("Item Spawning")]
    [SerializeField] GameObject item;
    int maxItemsOnField = 10;
    public List<GameObject> spawnedItems;

    float maxXCoord = 10f;
    float maxYCoord = 7f;

    [Header("Item Stuff")]
    [SerializeField] int[] itemValues;
    [SerializeField] Sprite[] itemSprites;

    [Header("UI Stuff")]
    [SerializeField] TextMeshProUGUI moneyText;
    public Slider digBarSlider;
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winScreenText;

    [Header("Prefabs")]
    public GameObject sandPile;
    public GameObject itemFader;

    GameObject player;

    AudioSource playerAudio;

    bool hasWon = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        startTime = Time.time;
        inventory = new int[8];
        player = GameObject.FindGameObjectWithTag("Player");

        playerAudio = player.GetComponent<AudioSource>();

        diggingLevel = 1;
        dowsingLevel = 1;

        numberOfTreasuresFound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (money >= 500 && !hasWon) {
            Win();
        }

        if (diggingTime) {

            if (diggingTime && spawnedItems.Count < maxItemsOnField) {
                placeItems();
            }

            if (Input.GetKey(KeyCode.Mouse0)) {
                DetectItems();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            playerAudio.Stop();

        updateUI();
    }
    /// <summary>
    /// spawns items in the world determined by the max coordinates
    /// </summary>
    void placeItems() {

        GameObject temp = Instantiate(item);
        spawnedItems.Add(temp);

        float xCoord = UnityEngine.Random.Range(-maxXCoord, maxXCoord);
        float yCoord = UnityEngine.Random.Range(-maxYCoord, maxYCoord);

        temp.transform.position = new Vector3(xCoord, yCoord, 0);
    }

    void updateUI() {

        moneyText.text = $"Ducats: {money}";

    }

    void DetectItems() {
        bool highTone = false;
        bool medTone = false;
        bool lowTone = false;


        foreach (GameObject g in spawnedItems) {

            // /determines what tone should be used depending on the distance between the player and treasure
            if (g.GetComponent<LnFItem>().hitShort) {
                highTone = true;
                break;
            }
            if (g.GetComponent<LnFItem>().hitMed) 
                medTone = true;
            
            if (g.GetComponent<LnFItem>().hitLong) 
                lowTone = true;            
        }

        if (highTone || medTone || lowTone) {
            PlayBeeps(lowTone, medTone, highTone);
        }
        else {
            playerAudio.Stop();
        }



    }

    public void GoDigBar(float valOutOfOne) {

       digBarSlider.value = valOutOfOne;
    }

    void PlayBeeps(bool low, bool med, bool high) {

        if (!playerAudio.isPlaying)
            playerAudio.Play();

        if (high) {
            playerAudio.pitch = 2f;
#if UNITY_EDITOR
            Debug.Log("High Tone! " + Time.deltaTime);
#endif
        }
        else if (med) {
            playerAudio.pitch = 1f;
#if UNITY_EDITOR
            Debug.Log("Medium Tone! " + Time.deltaTime);
#endif
        }
        else if (low) {
            playerAudio.pitch = .5f;
#if UNITY_EDITOR
            Debug.Log("Low Tone! " + Time.deltaTime);
#endif
        }
    }

    public void assignItemValues(LnFItem item) {

        if (dowsingLevel == 1) {
            item.itemValue = itemValues[UnityEngine.Random.Range(0, 8)];
        }
        else if (dowsingLevel == 2) {
            item.itemValue = itemValues[UnityEngine.Random.Range(2, 10)];
        }
        else if (dowsingLevel == 3) {
            item.itemValue = itemValues[UnityEngine.Random.Range(6, itemValues.Length)];
        }
        else {
            item.itemValue = itemValues[UnityEngine.Random.Range(0, itemValues.Length)];
        }


        item.GetComponent<SpriteRenderer>().sprite = itemSprites[UnityEngine.Random.Range(0, itemSprites.Length)];

    }

    void Win() {

        hasWon = true;

        playerAudio.Stop();

        winScreen.SetActive(true);

        float endTime = Time.time;


        float totTime = -(startTime - endTime);

        int newLvl = dowsingLevel + diggingLevel -2;

        winScreenText.text  = $"You finished in {TimeSpan.FromSeconds(totTime).Hours}:{TimeSpan.FromSeconds(totTime).Minutes}:{TimeSpan.FromSeconds(totTime).Seconds}. Delightful!\n";
        winScreenText.text += $"You collected {numberOfTreasuresFound} treasures. Daring!\n";

        if (newLvl == 0)
            winScreenText.text += $"You improved your tools 0 times? Disappointing!";
        else
        winScreenText.text += $"You improved your tools {newLvl} times. Devoted!";
    }

    public void ExitGame() {
        SceneManager.LoadScene(0);
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
}
