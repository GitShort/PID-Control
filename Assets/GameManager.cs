using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    float Clock = 0;
    bool CheckIfRun;
    bool Done;
    [SerializeField]
    List<TriggerCheck> GameObjects = new List<TriggerCheck>();
    [SerializeField]
    GameObject Text;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CheckIfRun = false;
        Done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfRun)
        {
            Clock += Time.deltaTime;
        }
    }

    public void CheckIfAllPlaced()
    {
        int i = 0;
        foreach(TriggerCheck check in GameObjects)
        {
            if(check.CheckObject())
            {
                i++;
            }
        }
        if(i == 3)
        {
            CheckIfRun = false;
            Done = true;
            Text.SetActive(true);
        }
    }

    public void StartClock()
    {
        if(!CheckIfRun && !Done)
        {
            CheckIfRun = true;
        }
    }
}
