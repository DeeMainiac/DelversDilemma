using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LnFItem : MonoBehaviour {

    public string itemName;
    public int itemValue;

    GameManager GM;

    // /timer controls
    private bool held = false;
    private float startTime = 0f;
    private float timer = 0f;

    // /times for holding down the dig button
    float holdTimelvl1 = 1.5f;
    float holdTimelvl2 = 1f;
    float holdTimelvl3 = .5f;

    private bool isTouchingPlayer = false;

    // /distance from the player where the treasure is 
    public bool hitShort = false;
    public bool hitMed = false;
    public bool hitLong = false;

    // Start is called before the first frame update
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        GM.assignItemValues(this);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    float GetHoldTime() {
        float hold;

        if (GM.diggingLevel == 2) {
            hold = holdTimelvl2;
        }
        else if (GM.diggingLevel == 3) {
            hold = holdTimelvl3;
        }
        else {
            hold = holdTimelvl1;
        }

        return hold;
    }

    // Update is called once per frame
    void Update() {

        if (isTouchingPlayer) {
            // /https://forum.unity.com/threads/hold-key-down-for-x-amount-of-seconds-how-c.455571/#post-3497526
            if (Input.GetKeyDown(KeyCode.Space)) {
                startTime = Time.time;
                timer = startTime;
                GM.digBarSlider.gameObject.SetActive(true);

                GM.GoDigBar(-(startTime - timer) / GetHoldTime());
            }

            if (Input.GetKey(KeyCode.Space) && !held) {
                timer += Time.deltaTime;

                GM.GoDigBar(-(startTime - timer) / GetHoldTime());

                if (timer > (startTime + GetHoldTime())) {
                    held = true;
                    GM.GoDigBar(0);
                    GM.digBarSlider.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyUp(KeyCode.Space) || isMoving()) {
                held = false;
                GM.GoDigBar(0);
                GM.digBarSlider.gameObject.SetActive(false);
            }
        }

    }

    bool isMoving() {

        if (Input.GetKey(KeyCode.W))
            return true;
        if (Input.GetKey(KeyCode.A))
            return true;
        if (Input.GetKey(KeyCode.S))
            return true;
        if (Input.GetKey(KeyCode.D))
            return true;

        else return false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Revealer")) {
            //  gameObject.GetComponent<SpriteRenderer>().enabled = true;

            if (collision.name == "TempRangeShort")
                hitShort = true;
            else if (collision.name == "TempRangeMed")
                hitMed = true;
            else if (collision.name == "TempRangeLong")
                hitLong = true;
        }

        else if (collision.CompareTag("Player")) {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

     //   if (collision.CompareTag("Revealer")) {
            //  gameObject.GetComponent<SpriteRenderer>().enabled = true;
      //  }

         if (collision.CompareTag("Player") && held) {

            GM.money += itemValue;
            BeforeDestroy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Revealer")) {
           // gameObject.GetComponent<SpriteRenderer>().enabled = false;
            hitShort = false;
            hitMed = false;
            hitLong = false;
        }
        else if (collision.CompareTag("Player")) {
            isTouchingPlayer = false;
        }
    }

    void BeforeDestroy() {

        GameObject temp = Instantiate(GM.sandPile);
        temp.transform.position = gameObject.transform.position;

        GameObject particl = Instantiate(GM.itemFader);
        particl.transform.position = gameObject.transform.position;
        particl.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

        GM.numberOfTreasuresFound++;
        GM.spawnedItems.Remove(gameObject);

        Destroy(gameObject);
    }
}
