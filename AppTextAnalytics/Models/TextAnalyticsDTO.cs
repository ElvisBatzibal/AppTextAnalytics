using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Microcharts;

namespace AppTextAnalytics.Models
{
    public class TextAnalyticsDTO : CarouselItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Captions { get; set; }

        public string TextAnalyse { get; set; }
        public string Language { get; set; }
        public string JsonResultLanguage { get; set; }
        /// <summary>
        /// Frases claves
        /// </summary>
        public string KeyPhrases { get; set; }
        /// <summary>
        /// Json by Key Phrases
        /// </summary>
        public string JsonResultKeyPhrases { get; set; }
        public string OpinionGeneral { get; set; }
        public string OpinionGeneralPositiva { get; set; }
        public string OpinionGeneralNegativa { get; set; }
        public string OpinionGeneralNeutral { get; set; }
        public string FrasePositiva { get; set; }
        public string FraseNegativa { get; set; }
        public string FraseNeutral { get; set; }
        public string JsonOpinion { get; set; }
        public string EntidadesNombre { get; set; }
        public string JsonEntidades { get; set; }
        public string Color { get; set; }
        public string CreateDateString { get; set; }
        public DateTime CreateDate { get; set; }
        public string DetectedFace { get; set; }

        [Ignore]
        public  DonutChart DataChart { get; set; }
        public TextAnalyticsDTO()
        {
            CreateDate = DateTime.Now;
            CreateDateString = DateTime.Now.ToString();
            Color = "#FF6119";
            DataChart = new DonutChart();
        }
    }
}
