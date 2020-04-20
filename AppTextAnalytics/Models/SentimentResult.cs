using System;
using System.Collections.Generic;
using System.Text;

namespace AppTextAnalytics.Models
{
    public class SentimentResult
    {
        public Document[] Documents { get; set; }
        public Error[] Errors { get; set; }

        public class Document
        {
            public float Score { get; set; }
            public string Id { get; set; }
            public string Sentiment { get; set; }
            public Documentscores Documentscores { get; set; }
            public List<Sentence> Sentences { get; set; }
        }
    }
    public class Error
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
    public class Sentence
    {
        public string Sentiment { get; set; }
        public Sentencescores Sentencescores { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
    }
    public class Documentscores
    {
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }
    }

    public class Sentencescores
    {
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }
    }
}
