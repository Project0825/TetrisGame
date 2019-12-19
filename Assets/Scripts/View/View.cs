using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour {
    private Ctrl ctrl;

    private RectTransform logoName;
    private RectTransform menuUI;
    private RectTransform gameUI;
    private GameObject reStartButtton;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject rankUI;

    private Text score;
    private Text highScore;
    private Text gameOverScore;

    private GameObject mute;
    private Text rankScore;
    private Text rankHighScore;
    private Text rankNumber;

    // Use this for initialization
    void Awake () {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
        logoName = transform.Find("Canvas/TitleName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        reStartButtton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;

        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScorelabel/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Score").GetComponent<Text>();

        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/Text").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighLabel/Text").GetComponent<Text>();
        rankNumber = transform.Find("Canvas/RankUI/GameNumberLabel/Text").GetComponent<Text>();
    }
    public void ShowMenu()
    {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-121.9f, 1);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(61, 1);
    }
    public void HideMenu()
    {
        logoName.DOAnchorPosY(168f, 1).OnComplete(delegate { logoName.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-86, 1).OnComplete(()=>{ menuUI.gameObject.SetActive(false); });
    }
    public void ShowGameUI(int score=0,int highScore=0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();

        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-129.39f, 1);
    }
    public void UpdataGameUI(int score,int highScore)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(148f, 1).OnComplete(()=>{ gameUI.gameObject.SetActive(false);});
    }
    public void ShowRestartButton()
    {
        reStartButtton.SetActive(true);
    }
    public void ShowGameOverUI(int score = 0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("jiazai");
    }
    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }
    public void SetMuteActive(bool b)
    {
        ctrl.audioManager.PlayCursor();
        mute.SetActive(b);
    }
    public void OnSettingUIClick ()
    {
        settingUI.SetActive(false);
    }
    
    public void ShowRankUI(int s,int hs,int c)
    {
        rankScore.text = s.ToString();
        rankHighScore.text = hs.ToString();
        rankNumber.text = c.ToString();
        rankUI.SetActive(true);
    }
    public void OnRankUIClick()
    {
        rankUI.SetActive(false);
    }
}
