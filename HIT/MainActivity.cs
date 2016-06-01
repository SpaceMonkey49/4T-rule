using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace HIT
{
    [Activity(Label = "HIT", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Quiz Quiz4TRule;
        private int CurrentQuestionId; //TODO - Remove this !

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button BtnStart = FindViewById<Button>(Resource.Id.BtnStart);

            BtnStart.Click += delegate { Start4T(); };

        }


        void Start4T()
        {
            Quiz4TRule = new Quiz();

            //Question 1
            Question Question1 = new Question();
            Question1.Issue = "Platelet nadir ?";
            Question1.Choices.Add(new Choice("≥20 G/L", 1));
            Question1.Choices.Add(new Choice("10-19 G/L", 1));
            Question1.Choices.Add(new Choice("<10 G/L", 0));
            Quiz4TRule.Questions.Add(Question1);

            //Question 2
            Question Question2 = new Question();
            Question2.Issue = "Platelet count fall ?";
            Question2.Choices.Add(new Choice("> 50 %", 1));
            Question2.Choices.Add(new Choice("< 50 %", 0));
            Quiz4TRule.Questions.Add(Question2);

            //Question 3
            Question Question3 = new Question();
            Question3.Issue = "Timing of platelet count fall ?";
            Question3.Choices.Add(new Choice("Clear onset between days 5-10 without recent heparin exposure", 2));
            Question3.Choices.Add(new Choice("≤1 day with prior heparin exposure within 30 days", 2));
            Question3.Choices.Add(new Choice("Consistent with day 5-10 fall but not clear (missing platelet count)", 1));
            Question3.Choices.Add(new Choice("Onset after day 10", 1));
            Question3.Choices.Add(new Choice("Fall ≤1 day prior exposure 30-100 days", 1));
            Question3.Choices.Add(new Choice("<4 days without recent heparin exposure", 0));
            Quiz4TRule.Questions.Add(Question3);

            //Question 4
            Question Question4 = new Question();
            Question4.Issue = "Thrombosis or other sequelae ?";
            Question4.Choices.Add(new Choice("New thrombosis confirmed", 2));
            Question4.Choices.Add(new Choice("Skin necronis at heparin injection sites", 2));
            Question4.Choices.Add(new Choice("Acute systemic reaction after intravenous heparin bolus", 2));
            Question4.Choices.Add(new Choice("Progressive or recurrent thrombosis", 1));
            Question4.Choices.Add(new Choice("Non-necrotising (erythematous) skin lesions", 1));
            Question4.Choices.Add(new Choice("Suspected thrombosis (not proven)", 1));
            Question4.Choices.Add(new Choice("None", 0));
            Quiz4TRule.Questions.Add(Question4);

            //Question 5
            Question Question5 = new Question();
            Question5.Issue = "Other cause for thrombocytopenia ?";
            Question5.Choices.Add(new Choice("None apparent", 2));
            Question5.Choices.Add(new Choice("Possible", 1));
            Question5.Choices.Add(new Choice("Definite", 0));
            Quiz4TRule.Questions.Add(Question5);

            CurrentQuestionId = 1;
            DisplayQuestion();

        }

        void DisplayQuestion()
        {
            Question CurrentQuestion;
            if (CurrentQuestionId > 0 && CurrentQuestionId <= Quiz4TRule.Questions.Count)
            {
                //TODO : Remettre un peu d'ordre ! Je pense que ce traitement n'a rien à faire ici (encore que?)
                if (CurrentQuestionId == 2 && Quiz4TRule.Questions[0].SelectedChoice!=0)
                {
                    Quiz4TRule.Questions[CurrentQuestionId - 1].SelectChoice(-1);
                    CurrentQuestionId++;
                }

                CurrentQuestion = Quiz4TRule.Questions[CurrentQuestionId - 1];
            }
            else
            {
                DisplayResult();
                return;
            }

            SetContentView(Resource.Layout.Question);
            TextView IssueTextView = FindViewById<TextView>(Resource.Id.TextViewQuestion);
            ListView ChoicesListView = FindViewById<ListView>(Resource.Id.ListViewChoices);


            IssueTextView.Text = CurrentQuestion.Issue;
            ArrayAdapter ChoicesListViewAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, CurrentQuestion.GetChoicesTexts());
            ChoicesListView.Adapter = ChoicesListViewAdapter;

            ChoicesListView.ItemClick += ChoiceClick;
        }

        void ChoiceClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Quiz4TRule.Questions[CurrentQuestionId-1].SelectChoice(e.Position);
            CurrentQuestionId++;
            DisplayQuestion();
        }


        void DisplayResult()
        {
            SetContentView(Resource.Layout.Result);
            TextView ScoreTextView = FindViewById<TextView>(Resource.Id.TextViewFinalScore);
            TextView TextViewFinalScoreDetail = FindViewById<TextView>(Resource.Id.TextViewFinalScoreDetail);
            Button BtnRestart = FindViewById<Button>(Resource.Id.BtnRestart);
            Button BtnExit = FindViewById<Button>(Resource.Id.BtnExit);

            ScoreTextView.Text = Quiz4TRule.GetScore().ToString();

            String Result;
            if (Quiz4TRule.GetScore() <= 3)
            {
                Result = "Hit is unlikely. No change in heparin treatment.";
            }
            else if (Quiz4TRule.GetScore() <= 5)
            {
                Result = "The risk of HIT is intermediate. Ask for an immunoassay.";
            }
            else
            {
                Result = "The risk of HIT is high. Ask for an immunoassay.";
            }
            TextViewFinalScoreDetail.Text = Result;

            BtnRestart.Click += delegate { Start4T(); };
            BtnExit.Click += delegate { System.exit(0); };
        }
    }
}

