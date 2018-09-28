using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //单利
    private static GameManager _instance;
    public static GameManager Instantce {
        get {

            return _instance;
        }    
    }


    //get all objects;
    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject startCountPrefab;
    public GameObject gameOverPrefab;
    public GameObject winPrefab;
    public AudioClip startClip;
    public GameObject startPanel;
    public GameObject gamePanel;


    public GameObject text00;
    public Text remainText;
    public Text nowText;
    public Text scoreText;

    public bool isSuperPacman = false;

    private int pacdotCount = 0;
    private int nowEat = 0;
    public int score = 0;

    private List<GameObject> pacdots = new List<GameObject>();

    public List<int> orderList = new List<int>();
    public List<int> randomList = new List<int>();

    //private List<GameObject> pacdotGos = new List<GameObject>();


    private void Awake()
    {
        _instance = this;
        Screen.SetResolution(1024,768,false);

        //find all pacdots
        foreach (Transform t in GameObject.Find("Maze").transform)
        {
            pacdots.Add(t.gameObject);

        }
        //init orderList
        int temp = blinky.GetComponent<EnemyMove>().ways.Length;
        Debug.Log("way.Length:" + temp);
        for (int i = 0; i < temp; i++) {

            orderList.Add(i);

        }


        //初始randomList

        int tempCount = orderList.Count;
        for (int i = 0; i < tempCount; i++) {

            int temp1 = Random.Range(0, orderList.Count);
            randomList.Add(orderList[temp1]);
            orderList.RemoveAt(temp1);
        }

        pacdotCount = GameObject.Find("Maze").transform.childCount;
    }


    private void Start()
    {
        SetGameState(false);

    }

    public void OnStartButton() {

        // SetGameState(true);
        StartCoroutine(PlayStartCountDown());
        AudioSource.PlayClipAtPoint(startClip, Vector3.zero);
        startPanel.SetActive(false);


    }
    public void OnExitButton()
    {
        Application.Quit();
    }

    IEnumerator PlayStartCountDown() {

        GameObject go = Instantiate(startCountPrefab);

        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);
        Invoke("CreateSuperDot", 10f);
        gamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }


    public void OnEatDot(GameObject go) {
        nowEat++;
        score += 10;
        pacdots.Remove(go);
    }

    public void OnEatSuperPacdot() {


        score += 20;
        Invoke("CreateSuperDot", 10f);
            
        
        isSuperPacman = true;
        FreezeEnemy();
        //Invoke("RecoveryEnemy", 3f);
        StartCoroutine(RecoveryEnemy());

    }

    private void Update()
    {

        if (nowEat == pacdotCount&&pacman.GetComponent<PacManMove>().enabled != false) {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
            text00.SetActive(true);
            GetComponent<NewBehaviourScript>().enabled = true;
        }
        if (nowEat == pacdotCount) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);

            }
        }

        if (gamePanel.activeInHierarchy) {

            remainText.text = "Remain:\n\n" + (pacdotCount - nowEat);
            nowText.text = "Eaten:\n\n" + nowEat;

            scoreText.text = "Score:\n\n" + score;

        }

    }

   

    IEnumerator RecoveryEnemy() {

        yield return new WaitForSeconds(3f);
        DisFreezeEnemy();
        isSuperPacman = false;


    }
    //create superdot
    public void CreateSuperDot() {

        if (pacdots.Count < 10) {
            return;
        }

        int temp = Random.Range(0, pacdots.Count);
        pacdots[temp].transform.localScale = new Vector3(3, 3, 3);
        pacdots[temp].GetComponent<BoxCollider2D>().size = new Vector2(0.75f,0.75f);

        pacdots[temp].GetComponent<Pacdot>().isSuperDot = true;


    }

    private void FreezeEnemy() {

        blinky.GetComponent<EnemyMove>().enabled = false;
        clyde.GetComponent<EnemyMove>().enabled = false;

        inky.GetComponent<EnemyMove>().enabled = false;

        pinky.GetComponent<EnemyMove>().enabled = false;

        blinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);

        inky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);

        pinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);

    }


    private void DisFreezeEnemy()
    {

        blinky.GetComponent<EnemyMove>().enabled = true;
        clyde.GetComponent<EnemyMove>().enabled = true;

        inky.GetComponent<EnemyMove>().enabled = true;

        pinky.GetComponent<EnemyMove>().enabled = true;

        blinky.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        inky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        pinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }

    private void SetGameState(bool state) {


        pacman.GetComponent<PacManMove>().enabled = state;
        blinky.GetComponent<EnemyMove>().enabled = state;
        clyde.GetComponent<EnemyMove>().enabled = state;
        inky.GetComponent<EnemyMove>().enabled = state;
        pinky.GetComponent<EnemyMove>().enabled = state;

    }



}
