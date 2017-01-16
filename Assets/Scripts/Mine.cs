using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Mine : MonoBehaviour {

    bool isPlayerNear;
    Player player;

    public Text actionText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other");
        if(other.tag == "Player")
        {
            isPlayerNear = true;
            player = other.GetComponent<Player>();
            player.isNearMine = true;
            actionText.DOFade(1, 0.1f);
            actionText.text = "Press E to mine";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = false;
            player.isNearMine = false;
            player = null;
            actionText.DOFade(0, 0.1f);
        }
    }
}
