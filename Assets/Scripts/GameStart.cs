using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {
    public PlayerController player;
    public Text GameStartText;
    Button button;
    Image image;

    public bool startUp;


	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        startUp = true;

	}


    public void StartFade()
    {
        startUp = false;
        StartCoroutine(Fade());
        player.enabled = true;
        player.Reset();

    }

    public void StartAppear()
    {
        StartCoroutine(Appear());
    }

    IEnumerator Fade()
    {
        button.interactable = false;
        GameStartText.enabled = false;
        while(image.color.a>0)
        {
            var tempColor = image.color;
            tempColor.a = image.color.a-0.01f;
            image.color = tempColor;
            yield return null;
        }
        player.enabled = true;


    }

    IEnumerator Appear()
    {
        
        while (image.color.a < 1)
        {
            var tempColor = image.color;
            tempColor.a = image.color.a + 0.04f;
            image.color = tempColor;
            yield return null;
        }
        GameStartText.enabled = true;
        GameStartText.text = "You died, click anywhere to try again";
        button.interactable = true;

    }



}
