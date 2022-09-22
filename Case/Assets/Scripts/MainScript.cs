using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    GameObject[] find;
    public GameObject FinishPanel;
    public GameObject LoadingCanvas;
    public GameObject PausePanel;
    public Slider LoadingSlider;
    bool isPaused = false;
    public TMP_Text time_text, finish_text, pause_text;
    float time = 30;


    void Update()
    {
        find = GameObject.FindGameObjectsWithTag("player");
        time -= Time.deltaTime;
        time_text.text = "SÜRE: " + ((int)time).ToString();

        if(find.Length == 1){
            time = 0;
            Finish();
        }
       if((int)time < 0){
            FinishTime();
            time = 0;
        }
        if(Input.GetTouch(0).phase == TouchPhase.Began){
           PauseGame();
       }


    }
    void Finish(){
        FinishPanel.SetActive(true);
        finish_text.text = "Oyunu Kazanan: " + find[0].name;
        StartCoroutine(Loading(0));
    }
    void FinishTime(){
        FinishPanel.SetActive(true);
        finish_text.text = "Oyunu Kazananlar" + "\n";
        for (int i = 0; i < find.Length; i++)
        {
            finish_text.text += find[i].name + "\n";
        }
        StartCoroutine(Loading(0));
    }
    public void PauseGame(){
        if(isPaused){
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }else{
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
    IEnumerator Loading(int SceneIndex)
    {
        yield return new WaitForSeconds (1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        LoadingCanvas.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadingSlider.value = progress;
            yield return null;
        }
    }
}
