using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CalculateScore : MonoBehaviour
{
    [SerializeField]
    TimeMeter timer;
    [SerializeField]
    EnergyMeter energy;
    [SerializeField]
    Dialogue setDialogue;
    [SerializeField]
    Toggle[] question1Options = new Toggle[4];
    [SerializeField]
    Toggle[] question2Options = new Toggle[4];
    [SerializeField]
    Toggle[] question3Options = new Toggle[4];
    [SerializeField]
    Toggle[] question4Options = new Toggle[4];
    [SerializeField]
    Toggle[] question5Options = new Toggle[4];
    [SerializeField]
    Toggle[] question6Options = new Toggle[4];
    [SerializeField]
    Toggle[] question7Options = new Toggle[4];
    [SerializeField]
    Toggle[] question8Options = new Toggle[4];
    [SerializeField]
    Toggle[] question9Options = new Toggle[4];
    [SerializeField]
    Toggle[] question10Options = new Toggle[4];
    [SerializeField]
    Toggle correctAnswer1;
    [SerializeField]
    Toggle correctAnswer2;
    [SerializeField]
    Toggle correctAnswer3;
    [SerializeField]
    Toggle correctAnswer4;
    [SerializeField]
    Toggle correctAnswer5;
    [SerializeField]
    Toggle correctAnswer6;
    [SerializeField]
    Toggle correctAnswer7;
    [SerializeField]
    Toggle correctAnswer8;
    [SerializeField]
    Toggle correctAnswer9;
    [SerializeField]
    Toggle correctAnswer10;
    [SerializeField]
    GameObject scorePanel;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject tryAgainButton;
    [SerializeField]
    GameObject continueButton;

    List<Toggle[]> questions = new List<Toggle[]>();
    List<Toggle> answers = new List<Toggle>();
    List<Toggle> correctAnswers = new List<Toggle>();
    Toggle question1Answer;
    Toggle question2Answer;
    Toggle question3Answer;
    Toggle question4Answer;
    Toggle question5Answer;
    Toggle question6Answer;
    Toggle question7Answer;
    Toggle question8Answer;
    Toggle question9Answer;
    Toggle question10Answer;
    int score;
    bool hasCheckedAnswers;
    bool hasSetQuestionsOff;

    void Start()
    {
        questions.Add(question1Options);
        questions.Add(question2Options);
        questions.Add(question3Options);
        questions.Add(question4Options);
        questions.Add(question5Options);
        questions.Add(question6Options);
        questions.Add(question7Options);
        questions.Add(question8Options);
        questions.Add(question9Options);
        questions.Add(question10Options);

        answers.Add(question1Answer);
        answers.Add(question2Answer);
        answers.Add(question3Answer);
        answers.Add(question4Answer);
        answers.Add(question5Answer);
        answers.Add(question6Answer);
        answers.Add(question7Answer);
        answers.Add(question8Answer);
        answers.Add(question9Answer);
        answers.Add(question10Answer);

        correctAnswers.Add(correctAnswer1);
        correctAnswers.Add(correctAnswer2);
        correctAnswers.Add(correctAnswer3);
        correctAnswers.Add(correctAnswer4);
        correctAnswers.Add(correctAnswer5);
        correctAnswers.Add(correctAnswer6);
        correctAnswers.Add(correctAnswer7);
        correctAnswers.Add(correctAnswer8);
        correctAnswers.Add(correctAnswer9);
        correctAnswers.Add(correctAnswer10);

        score = 0;
        hasCheckedAnswers = false;
        hasSetQuestionsOff = false;
    }

    void Update()
    {
        if (energy.energyLeft <= 0 && !hasSetQuestionsOff)
        {
            foreach (var question in questions)
            {
                foreach (var answer in question)
                {
                    answer.interactable = false;
                }
            }

            setDialogue.SetDialogueText(12);
            hasSetQuestionsOff = true;
        }

        if (timer.timeLeft <= 0 && !hasCheckedAnswers)
        {
            for (int i = 0; i < answers.Count; i++)
            {
                for (int j = 0; j < questions[i].Length; j++)
                {
                    if (questions[i][j].isOn)
                    {
                        answers[i] = questions[i][j];
                    }
                }
            }

            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i] == correctAnswers[i])
                {
                    score++;
                }
            }

            setDialogue.SetDialogueText(13);
            scoreText.text = gradeString(score);
            scorePanel.SetActive(true);
            tryAgainButton.SetActive(true);
            continueButton.SetActive(true);
            hasCheckedAnswers = true;
        }
    }

    string gradeString(int totalScore)
    {
        string letter = "";

        if (totalScore >= 0 && totalScore <= 5)
            letter = "F";
        else if (totalScore == 6)
            letter = "D";
        else if (totalScore == 7)
            letter = "C";
        else if (totalScore == 8)
            letter = "B";
        else if (totalScore >= 9 && totalScore <= 10)
            letter = "A";

        return letter;
    }
}
