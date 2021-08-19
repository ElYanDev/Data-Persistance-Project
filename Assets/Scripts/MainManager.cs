using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text playerText;
    public Text recordText;
    public GameObject GameOverText;
    public GameObject CongratsText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private bool isFinished = false;
    private int recordScore;
    private string recordName;
    private string namePlayer;
    private int totalBricks;

    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("record", 0);
        //PlayerPrefs.SetString("recordName", "Papi");
        try
        {
            PlayerPrefs.GetInt("record");
        }
        catch
        {
            PlayerPrefs.SetInt("record", 0);
        }

        Debug.Log(PlayerPrefs.GetString("recordName"));

        recordName = PlayerPrefs.GetString("recordName");

        //Debug.Log("start with:"+PlayerPrefs.GetString("name"));
        namePlayer = PlayerPrefs.GetString("name");
        playerText.text = PlayerPrefs.GetString("name");
        recordScore = PlayerPrefs.GetInt("record");
        recordText.text = "Best Score " + recordName + " : "+ recordScore;
        totalBricks = 0;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                totalBricks++;
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !(isFinished || m_GameOver))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver || isFinished)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > recordScore)
        {
            recordScore = m_Points;
            PlayerPrefs.SetInt("record", recordScore);
            PlayerPrefs.SetString("recordName", namePlayer);
            recordText.text = "Best Score " + namePlayer + " : " + recordScore;
        }
        totalBricks--;
        if (totalBricks <= 0)
        {
            Congratulations();    
        }
    }
    public void Congratulations()
    {
        isFinished = true;
        Destroy(Ball.gameObject);
        CongratsText.SetActive(true);
    }
    public void GameOver()
    {
        m_GameOver = true;
        MuestraGameOverText();
    }
    public void MuestraGameOverText()
    {
        if (!isFinished)  GameOverText.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}