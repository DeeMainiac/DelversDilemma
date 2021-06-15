using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// moves the player accordingly.
/// </summary>
public class PlayerMover : MonoBehaviour {
    [SerializeField] int movespeed = 5;
    [SerializeField] LayerMask objLayer;

    [SerializeField] GameObject animatedWalk;

    [SerializeField] Sprite idle;
    [SerializeField] Sprite sit;

    float walkspeed;
    Vector2 input;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start() {
        walkspeed = movespeed / 4f;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero) {

            GetComponent<SpriteRenderer>().enabled = false;
            animatedWalk.SetActive(true);

            Vector3 targetpos = transform.position;
            targetpos.x += input.x;
            targetpos.y += input.y;

            walkspeed = movespeed / 4f;


            if (isWalkable(targetpos)) {
                if (Input.GetKey(KeyCode.Mouse0))
                    transform.position = Vector3.MoveTowards(transform.position, targetpos, walkspeed * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, targetpos, movespeed * Time.deltaTime);
            }
        }
        else {
            GetComponent<SpriteRenderer>().enabled = true;
            animatedWalk.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (GetComponent<SpriteRenderer>().sprite == sit) 
                GetComponent<SpriteRenderer>().sprite = idle;
            else if (GetComponent<SpriteRenderer>().sprite == idle)
                GetComponent<SpriteRenderer>().sprite = sit;

        }
        
    }

    bool isWalkable(Vector3 targetpos) {

        RaycastHit2D[] linecast = Physics2D.LinecastAll(gameObject.transform.position, targetpos, objLayer);

        if (Physics2D.OverlapCircle(targetpos, .2f, objLayer) != null)
            return false;

        else return true;
    }
}
