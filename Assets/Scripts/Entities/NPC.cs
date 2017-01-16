using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NPC : CharacterEntity {
    public enum NPCType
    {
        Strength,
        Agility,
        Health
    }

    public NPCType npcType;
    Player player;

    bool isPlayerNear;
    public Text actionText;

    public int cost;

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    public override void Attack()
    {
        throw new NotImplementedException();
    }

    public override void Death()
    {
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other");
        if (other.tag == "Player")
        {
            isPlayerNear = true;
            player = other.GetComponent<Player>();
            player.currentNPC = this;
            player.isNearNPC = true;
            actionText.DOFade(1, 0.1f);

            if (npcType == NPCType.Agility)
            {
                actionText.text = "Press E to buy 1 agility for " + cost + " rubis";
            }
            else if (npcType == NPCType.Strength)
            {
                actionText.text = "Press E to buy 1 strength for " + cost + " rubis";
            }
            else if (npcType == NPCType.Health)
            {
                actionText.text = "Press E to buy 1 life for " + cost + " rubis";
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = false;
            player.isNearNPC = false;
            player = null;
            actionText.DOFade(0, 0.1f);
        }
    }


    public void AddStat()
    {
        if(player != null && player.currentNbOfGold >= cost)
        {
            if (npcType == NPCType.Agility)
            {
                player.agility += 1;
            }
            else if (npcType == NPCType.Strength)
            {
                player.strength += 1;
                player.maxLife += 1;
            }
            else if (npcType == NPCType.Health)
            {
                player.HealDamage(1);
            }
            player.currentNbOfGold -= cost;
        }
    }
}
