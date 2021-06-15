using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StorePopup : MonoBehaviour {
    GameManager GM;

    [Header("Dowsing Panel")]
    [SerializeField] GameObject leftPanel;
   [SerializeField] TextMeshProUGUI DowsingLevel;
   [SerializeField] Image DowsingSprite;
   [SerializeField] TextMeshProUGUI DowsingText;
   [SerializeField] Button dowsingButton;

    [Header("Digging Panel")]
    [SerializeField] GameObject rightPanel;
    [SerializeField] TextMeshProUGUI DiggingLevel;
    [SerializeField] Image DiggingSprite;
    [SerializeField] TextMeshProUGUI DiggingText;
    [SerializeField] Button diggingButton;

    [Header(" ")]
  [SerializeField]  Sprite[] dowsingSprites;
  [SerializeField]  Sprite[] diggingSprites;
  [SerializeField] Sprite idleSprite;

    // /Alliteration!

    // /old shovel. Disburse upgrades dig speed
    string Diglvl1text = "Dilapidated, delicate dirt driver. Disburse drives dig dashing.";

    // /modern shovel. dig speed is yeetin'
    string Diglvl2text = "Delightful dirt displacer. Drives dirt dashingly.";

    // /Fully upgraded, demonic looking shovel. Best in the business at it's job
    string Diglvl3text = "Devilishly direct dirt deliverer. Dominates dirt dislocation.";


    // /Dowsing rod stick that beeps like a metal detector. finds treasures in the ground. Disburse increases the value of found treasures
    string Dowlvl1text = "Dull, delightful device! Dowser droves drone’s dinning disturbance. Disburse drives derived Ducats.";

    // /Next level dowsing rod. pretty ok at its job
    string Dowlvl2text = "Directing durianwood dowser describes delightful darlings downwards.";

    // /Fully upgraded, demonic looking stick. gets that phat cash for dearest, the player.
    string Dowlvl3text = "Demonic devilwood diviner drives dearest’s deliveries devilishly.";

    int lvl2cost = 50;
    int lvl3cost = 175;


    // Start is called before the first frame update
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {

            leftPanel.SetActive(true);
            rightPanel.SetActive(true);

            GetComponent<Animator>().enabled = true;

            RefreshLeftMenu();
            RefreshRightMenu();

            GM.diggingTime = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {

            leftPanel.SetActive(false);
            rightPanel.SetActive(false);

            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = idleSprite;

            GM.diggingTime = true;
        }
    }

    public void LevelUpDowsing() {
        
        // /theres 3 levels

        if (GM.dowsingLevel == 1 && GM.money >= lvl2cost) {
            GM.money -= lvl2cost;
            GM.dowsingLevel = 2;
            GM.spawnedItems.Clear();
        }
        else if (GM.dowsingLevel == 2 && GM.money >= lvl3cost) {
            GM.money -= lvl3cost;
            GM.dowsingLevel = 3;
            GM.spawnedItems.Clear();
        }
        RefreshLeftMenu();
        
    }

    public void LevelUpDigging() {

        // /theres 3 levels

        if (GM.diggingLevel == 1 && GM.money >= lvl2cost) {
            GM.money -= lvl2cost;
            GM.diggingLevel = 2;
        }
        else if (GM.diggingLevel == 2 && GM.money >= lvl3cost) {
            GM.money -= lvl3cost;
            GM.diggingLevel = 3;
        }
        RefreshRightMenu();

    }

    // /UI for the dowsing store panel
    void RefreshLeftMenu() {

        DowsingLevel.text = $"Dowsing {GM.dowsingLevel} ";

        if (GM.dowsingLevel == 1) {
            DowsingText.text = Dowlvl1text;
            DowsingSprite.sprite = dowsingSprites[0];
            dowsingButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Disburse: {lvl2cost }";
        }

       else if (GM.dowsingLevel == 2) {
            DowsingText.text = Dowlvl2text;
            DowsingSprite.sprite = dowsingSprites[1];
            dowsingButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Disburse: {lvl3cost }";
        }
       else if (GM.dowsingLevel == 3) {
            DowsingText.text = Dowlvl3text;
            DowsingSprite.sprite = dowsingSprites[2];
            dowsingButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Done";
        }

        GameObject.Find("Dowser").GetComponent<SpriteRenderer>().sprite = DowsingSprite.sprite;
    }

    // /UI for the shovel store panel
    void RefreshRightMenu() {

        DiggingLevel.text = $"Digging {GM.diggingLevel} ";

        if (GM.diggingLevel == 1) {
            DiggingText.text = Diglvl1text;
            DiggingSprite.sprite = diggingSprites[0];
            diggingButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Disburse: {lvl2cost }";
        }

       else if (GM.diggingLevel == 2) {
            DiggingText.text = Diglvl2text;
            DiggingSprite.sprite = diggingSprites[1];
            diggingButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Disburse: {lvl3cost }";
        }
        else if (GM.diggingLevel == 3) {
            DiggingText.text = Diglvl3text;
            DiggingSprite.sprite = diggingSprites[2];
            diggingButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Done";
        }
    }
}