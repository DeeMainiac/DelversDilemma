using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fade effect for treasures, counts down to their doom.
/// </summary>
public class timedDetonation : MonoBehaviour
{
    [SerializeField] bool isMovable;

    SpriteRenderer selfSprite;

    Vector2 velocity = Vector2.zero;
    Vector2 destination = new Vector2 (1, 8);

    // Start is called before the first frame update
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        float newAlpha = selfSprite.color.a - (.4f * Time.deltaTime);

        selfSprite.color = new Color(selfSprite.color.r, selfSprite.color.b, selfSprite.color.g, newAlpha);

        if (selfSprite.color.a <= 0) {
            Destroy(gameObject);
        }

        if (isMovable) {

            transform.position = Vector2.SmoothDamp(transform.position, destination,ref velocity, .9f);

        }

    }
}
