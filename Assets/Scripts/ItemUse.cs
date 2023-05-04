using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    private GameObject firstSlot;
    private GameObject secondSlot;
    private GameObject thirdSlot;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        firstSlot = inventory.slots[0];
        secondSlot = inventory.slots[1];
        thirdSlot = inventory.slots[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.isFull[0])
        {
            var item = firstSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[0] = false;
            var gameObj = new GameObject();

            AddComponents(gameObj, item);

            Destroy(item.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.W) && inventory.isFull[1])
        {
            var item = secondSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[1] = false;
            var gameObj = new GameObject();

            AddComponents(gameObj, item);

            Destroy(item.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.E) && inventory.isFull[2])
        {
            var item = thirdSlot.transform.GetChild(0).GetComponent<Image>();
            inventory.isFull[2] = false;
            var gameObj = new GameObject();

            AddComponents(gameObj, item);

            Destroy(item.gameObject);
        }
    }

    private void AddComponents(GameObject gameObj, Image item)
    {
        if (item.name.Contains("Chest"))
        {
            gameObj.AddComponent<CasketScript>();
            gameObj.GetComponent<CasketScript>().CasketImage = item.sprite;
        }

        if (item.name.Contains("Bottle"))
        {
            gameObj.AddComponent<BottleScript>();
            gameObj.GetComponent<BottleScript>().BottleImage = item.sprite;
        }

        //if (item.name.Contains("Apple"))
        //{
        //    gameObj.AddComponent<CasketScript>();
        //    gameObj.GetComponent<CasketScript>().CasketImage = item.sprite;
        //}
    }
}
