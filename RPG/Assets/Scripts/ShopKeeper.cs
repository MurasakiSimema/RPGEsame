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
    public string url = "http://127.0.0.1/items";
    public string user = "username";
    public string pass = "password1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetButtonDown("Jump") && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            StartCoroutine(GetText());
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
        UnityWebRequest www = new UnityWebRequest("https://apirpgesame.herokuapp.com/items?lv=" + GameManager.instance.MaxLv);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(www.error);
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
    string authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }
}

public class ItemToSell
{
    public string Nome { get; set; }
}