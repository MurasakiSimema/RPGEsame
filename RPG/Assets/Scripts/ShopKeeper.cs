using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpen;
    public List<string> itemsToSell;
    public string url = "https://apirpgesame.herokuapp.com/items";
    public bool online = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetButtonDown("Jump") && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            if(online)
                StartCoroutine(GetText());
            else
            {
                Shop.instance.itemToSell = itemsToSell.ToArray();
                Shop.instance.OpenShop();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canOpen = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canOpen = false;
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = new UnityWebRequest(url + "?lv=" + GameManager.instance.MaxLv);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(www.error);
            Shop.instance.itemToSell = itemsToSell.ToArray();
            Shop.instance.OpenShop();
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            string output = www.downloadHandler.text;
            List<ItemToSell> items = JsonConvert.DeserializeObject<List<ItemToSell>>(output);

            itemsToSell = new List<string>();
            foreach (ItemToSell item in items)
                itemsToSell.Add(item.Nome);

            Shop.instance.itemToSell = itemsToSell.ToArray();
            Shop.instance.OpenShop();
        }
    }
}

public class ItemToSell
{
    public string Nome { get; set; }
}