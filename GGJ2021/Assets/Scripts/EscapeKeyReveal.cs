using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// just pops open the help menu
/// </summary>
public class EscapeKeyReveal : MonoBehaviour
{
   [SerializeField] GameObject textText;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            EscFunction();
        }

    }

    void EscFunction() {
#pragma warning disable
        textText.SetActive(!textText.active);
    }
}
