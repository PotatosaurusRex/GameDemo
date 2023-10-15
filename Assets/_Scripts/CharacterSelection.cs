using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int index = 0;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        characterList = new GameObject[transform.childCount];

        // fills array with three characters
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // make inactive chars invisible
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }
        
        characterList[index].SetActive(true);
        /*
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
        */
    }

    public void ToggleLeft()
    {

        characterList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }

        characterList[index].SetActive(true);
    }

    public void ConfirmButton()
    {
        string type = "";
        if (index == 0)
        {
            type = "Water";
        } else if (index == 1)
        {
            type = "Grass";
        }
        else
        {
            type = "Fire";
        }
        PlayerPrefs.SetString("Type", type);
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("SampleScene");
    }
}
