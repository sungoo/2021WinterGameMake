using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text textScore = null;
    [SerializeField] int increaseScore = 10;
    private int currentScore = 0;

    [SerializeField] float[] weight = null;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        textScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        int t_increaseScore = increaseScore;

        // 가중치 계산
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);

        currentScore += t_increaseScore;
        textScore.text = string.Format("{0:#, ##0}", currentScore);
    }
}
