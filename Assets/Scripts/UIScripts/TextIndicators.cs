using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIndicators : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        //healthCounter = GetComponent<TextMesh>();
    }

    // Update is called once per frame

    public void SetTextValue(string text, int value)
    {
        textMesh.text = text + value;
    }

    public void SetSkillTreeValues(string healthTxt, int health, string expTxt, int exp, string movementTxt, float movementSpeed, string jumpTxt, int jumpCount)
    {
        textMesh.text = healthTxt + ": " + health + "\n" +
            expTxt + ": " + exp + "\n" +
            movementTxt + ": " + movementSpeed + "\n" +
            jumpTxt + ": " + jumpCount;
    }
}
