using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyBinds : MonoBehaviour
{
     private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, down, left, right, Dash, Interact, Talk, Attack, Weapon_1, Weapon_2, Weapon_3, Weapon_4, Weapon_5;

    private GameObject currentKey;

    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(43, 134, 170, 255);


    void Start ()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Dash", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dash", "Space")));
        keys.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E")));
        keys.Add("Talk", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Talk", "Mouse 1")));
        keys.Add("Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "Mouse 2")));
        keys.Add("Weapon_1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Weapon_1", "1")));
        keys.Add("Weapon_2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Weapon_2", "2")));
        keys.Add("Weapon_3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Weapon_3", "3")));
        keys.Add("Weapon_4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Weapon_4", "4")));
        keys.Add("Weapon_5", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Weapon_5", "5")));
    
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        Dash.text = keys["Dash"].ToString();
        Interact.text = keys["Interact"].ToString();
        Talk.text = keys["Talk"].ToString();
        Attack.text = keys["Attack"].ToString();
        Weapon_1.text = keys["Weapon_1"].ToString();
        Weapon_2.text = keys["Weapon_2"].ToString();
        Weapon_3.text = keys["Weapon_3"].ToString();
        Weapon_4.text = keys["Weapon_4"].ToString();
        Weapon_5.text = keys["Weapon_5"].ToString();
    


    }

    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Dash"]))
        {
            Debug.Log("Dash");
        }
        if (Input.GetKeyDown(keys["Interact"]))
        {
            Debug.Log("Interact");
        }
        if (Input.GetKeyDown(keys["Talk"]))
        {
            Debug.Log("Talk");
        }
        if (Input.GetKeyDown(keys["Attack"]))
        {
            Debug.Log("Attack");
        }
        if (Input.GetKeyDown(keys["Weapon_1"]))
        {
            Debug.Log("Weapon_1");
        }
        if (Input.GetKeyDown(keys["Weapon_2"]))
        {
            Debug.Log("Weapon_2");
        }
        if (Input.GetKeyDown(keys["Weapon_3"]))
        {
            Debug.Log("Weapon_3");
        }
        if (Input.GetKeyDown(keys["Weapon_4"]))
        {
            Debug.Log("Weapon_4");
        }
        if (Input.GetKeyDown(keys["Weapon_5"]))
        {
            Debug.Log("Weapon_5");
        }
    }
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if(currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}

