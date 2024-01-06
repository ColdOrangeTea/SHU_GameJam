using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject Banana; // player
    [Header("釘子產生")]
    public Transform[] NailPos;
    public GameObject NailPrefabs;
    private IEnumerator spawnNail;
    public static bool IsGameStart = false;
    public static bool canSpawn = true;
    public float SpawnInterval = 1f;
    [Header("計時")]
    private float Timer = 0;
    public float timeLimit = 10f;
    [Header("分數")]
    public const int ADDSCORE = 1;
    public static int TotalScore = 0;
    [Header("遊戲UI顯示控制")]
    public GameObject StartButton;    
    public GameObject StartText;
    public GameObject EndButton;
    public GameObject EndText;
    public GameObject QuitButton;
    [Header("敲擊次數")]
    public static int KnockCounts = 0;

    // Start is called before the first frame update
    void Start()
    {
        Banana.SetActive(false);
        StartButton.SetActive(true);
        EndButton.SetActive(false);
        StartText.SetActive(true);
        EndText.SetActive(false);
        QuitButton.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (!canSpawn)
        {
            if(Banana.activeInHierarchy)
            {
                Banana.SetActive(false); 
            }
                if (!IsGameStart) return;
                DisplayGameOverUI();
        }

        if (!GameControl.IsGameStart) return;
        ChangeSpawnInterval();
    }
    
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

    }
    public void GameStart()
    {
        KnockCounts = 0;
        Timer = 0;
        SpawnInterval = 1f;
        IsGameStart = true;
        canSpawn = true;
        Banana.SetActive(true);
        StartButton.SetActive(false);
        StartText.SetActive(false);
        EndButton.SetActive(false);
        EndText.SetActive(false);
        QuitButton.SetActive(false);
        spawnNail = RandomSpawnNail();
        StartCoroutine(spawnNail);
    }
    public void GameOver()
    {
        StartButton.SetActive(true);
        StartText.SetActive(true);
        EndButton.SetActive(false);
        EndText.SetActive(false);
        QuitButton.SetActive(true);
        DamageControl control =GameObject.Find("DamageControl").GetComponent<DamageControl>();
        control.GameReset();
        Timer = 0;
        SpawnInterval = 1f;
    }
    public void DisplayGameOverUI()
    {
        IsGameStart = false;
        EndButton.SetActive(true);
        EndText.SetActive(true);
        QuitButton.SetActive(true);
    }

    void ChangeSpawnInterval()
    { 
        Timer += Time.deltaTime;
        if (Timer >= timeLimit)
        {
            Timer = 0;
            SpawnInterval -= 0.15f;
        }
    }


    IEnumerator RandomSpawnNail()
    {
        while (canSpawn)
        {
            int r = Random.Range(0, 2); // 0上 1下 
            if (r == 0)
                Instantiate(NailPrefabs, NailPos[0].transform.position, Quaternion.identity);
            else
                Instantiate(NailPrefabs, NailPos[1].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
