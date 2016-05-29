using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HIT
{

    class Quiz
    {
        public List<Question> Questions;
        private Result Result;

        public Quiz()
        {
            Questions = new List<Question>();
            Result = new Result();
        }

        public Quiz(List<Question> questions)
        {
            
            Questions = new List<Question>();
            foreach (Question question in questions)
            {
                Questions.Add(question);
            }
            Result = new Result();
        }

        public int GetScore()
        {
            Result.Score = 0;
            foreach (Question q in Questions)
            {
                if (q.SelectedChoice != -1)
                {
                    Result.Score += q.Choices[q.SelectedChoice].Score;
                }
                
            }
            return Result.Score;
        }

        
        public String GetResultDetail()
        {
            return Result.Detail;
        }



    }

    class Question
    {
        public String Issue;
        public List<Choice> Choices;
        public int SelectedChoice;

        public Question()
        {
            Issue = "DefaultIssue";
            Choices = new List<Choice>();
        }

        public Question(String issue, List<Choice> choices)
        {
            Issue = issue;
            Choices = new List<Choice>();
            foreach (Choice choice in choices)
            {
                Choices.Add(choice);
            }
        }


        public List<String> GetChoicesTexts()
        {
            List<String> ChoicesTexts = new List<String>();
            foreach(Choice Choice in Choices)
            {
                ChoicesTexts.Add(Choice.Answer);
            }
            return (ChoicesTexts);
        }

        public void SelectChoice (int choice)
        {
            SelectedChoice = choice;
        }

    }

    class Choice
    {
        public String Answer;
        public int Score;

        public Choice()
        {
            this.Answer = "DefaultAnswer";
            this.Score = 0;
        }

        public Choice(String answer, int score)
        {
            this.Answer = answer;
            this.Score = score;
        }

    }

    class Result
    {
        public int Score;
        public String Detail;

        public Result()
        {
            Score = 0;
            Detail = "DefaultDetail";
        }
    }
}