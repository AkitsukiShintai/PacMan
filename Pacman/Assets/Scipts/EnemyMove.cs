using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{


    //存储路径

    public GameObject[] ways;
    //存储一条路径中所有点的位置，在完成一条路径后归零并重新获取
    private List<Vector3> currentPath = new List<Vector3>();
    //public Transform[] wayPoints;
    public float speed = 0.05f;
    //标记当前需要移动的位置
    private int index = 0;

    private Vector3 startPosition;



    private void Start()
    {
        startPosition = transform.position + new Vector3(0, 3, 0);
        int count = Random.Range(0, ways.Length);
        //foreach (Transform t in ways[Random.Range(0, count)].transform)
        //{
        //    wayPoints.Add(t.position);

        //}
        ////GetTransform(ways[Random.Range(0, ways.Length)]);
        Debug.Log(count);
        LoadPath(ways[GameManager.Instantce.randomList[GetComponent<SpriteRenderer>().sortingOrder - 2]]);
    }

    private void FixedUpdate()
    {
        //怪物移动
        if (transform.position != currentPath[index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, currentPath[index], speed);

            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        else
        {
            index++;
            if (index >= currentPath.Count)
            {

                index = 0;
                LoadPath(ways[Random.Range(0, ways.Length)]);
            }
        }
        Vector2 dir = currentPath[index] - transform.position;
        //动画状态机状态改变
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            if (GameManager.Instantce.isSuperPacman)
            {

                transform.position = startPosition - new Vector3(0, 3, 0);
                index = 0;
                GameManager.Instantce.score += 50;
            }
            else {

                // Destroy(collision.gameObject);
                collision.gameObject.SetActive(false);
                GameManager.Instantce.gamePanel.SetActive(false);
                Instantiate(GameManager.Instantce.gameOverPrefab);
                Invoke("Restart", 3f);
            }
        }
    }


    private void Restart() {

        SceneManager.LoadScene(0);
    }

    private void LoadPath(GameObject go)
    {
        currentPath.Clear();
        foreach (Transform t in go.transform)
        {
            currentPath.Add(t.position);
        }

        currentPath.Insert(0, startPosition);
        currentPath.Add(startPosition);
    }


}
