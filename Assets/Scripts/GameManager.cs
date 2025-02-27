using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    
    public Brick[] bricks {get; private set; }
    private void Awake(){
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start(){
        NewGame();
    }

    private void NewGame(){
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);

    }
    
    private void LoadLevel(int level){
        this.level = level;
        if (level > 3){
            GameOver();
        }else{
            SceneManager.LoadScene("Level" + level, LoadSceneMode.Single);
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode){
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();

    }

    public void Hit(Brick brick){
        this.score += brick.points;

        if (Cleared()){
            LoadLevel(this.level + 1);
        }
    }

    private void ResetLevel(){
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver(){
        PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
    public void Hit(MissZone missZone){
        this.lives--;

        if (this.lives > 0){
            ResetLevel();  
        }else{
            GameOver();
        }
    }

    private bool Cleared(){
        for (int i = 0; i < this.bricks.Length; i++){
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable){
                return false;
            }
        }

        return true;

    }

    

}
