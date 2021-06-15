using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelinquentMover : MonoBehaviour
{
    /*
    delinquents are NPCs in the same boat as the player, and they
    don't actually do anything to them. At one point I wanted them
    to bump into the player and jostle them around, thus delinquent
    being their names, but then I decided to make it non-violent. 
    */

    float waitTimer = 2000f;
    float timer;

    float chatterwaitTimer;
    float chatTimer;

    float targetX;
    float targetY;
    Vector2 targetpos;

    int walkspeed = 3;

    [SerializeField] bool isFemale;
    [SerializeField] LayerMask objLayer;

    [SerializeField] AudioClip[] sounds;
    AudioSource audioSource;

    [SerializeField] GameObject dissItem;

    // Start is called before the first frame update
    void Start()
    {
        waitTimer = waitTimer * Time.deltaTime;
        waitTimer = UnityEngine.Random.Range(waitTimer, 3000 * Time.deltaTime);

        chatterwaitTimer = waitTimer * Time.deltaTime;

        timer = 0;
        chatTimer = 0;

        targetpos = new Vector2(targetX + transform.position.x, targetY + transform.position.y);

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = sounds[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= waitTimer) {
            targetX = UnityEngine.Random.Range(-1, 1.01f);
            targetY = UnityEngine.Random.Range(-1, 1.01f);

            targetpos = new Vector2(targetX + transform.position.x, targetY + transform.position.y);

            timer = 0;
        }
        else {
            timer++;
        }
        if (isWalkable(targetpos))
        transform.position = Vector3.MoveTowards(transform.position, targetpos, walkspeed * Time.deltaTime);
    }

    bool isWalkable(Vector3 targetpos) {

        GetComponent<BoxCollider2D>().enabled = false;

        RaycastHit2D[] linecast = Physics2D.LinecastAll(gameObject.transform.position, targetpos, objLayer);

        if (Physics2D.OverlapCircle(targetpos, .2f, objLayer) != null) {
            GetComponent<BoxCollider2D>().enabled = true;
            return false;
        }
            
        GetComponent<BoxCollider2D>().enabled = true;
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {

            chatTimer++;

            if (chatTimer >= chatterwaitTimer) {

                if (isFemale) {
                    int counter = UnityEngine.Random.Range(0, 2);
                    audioSource.clip = sounds[counter];
                    audioSource.Play();
                }
                else {
                    int counter = UnityEngine.Random.Range(0, 2);
                    audioSource.clip = sounds[counter + 2];
                    audioSource.Play();
                }
                chatTimer = 0;
            }
        }


    }

}
