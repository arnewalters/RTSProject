using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    public string playerName;

    private string[] firstnames = { "John", "Maria", "Dirk", "Cash", "Boy", "Dude"};
    private string[] lastnames = {"Dill", "Rap", "Nap", "Noob", "Rude", "Sup"};

    private void Awake()
    {
        #region Validate PlayerManager instance
        if (instance == null)
        {
            instance = this;
            playerName = GenerateRandomName();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transform.gameObject);
        #endregion 
    }

    private string GenerateRandomName()
    {
        return firstnames[Random.Range(0, firstnames.Length)] + " " + lastnames[Random.Range(0, lastnames.Length)];
    }
}
