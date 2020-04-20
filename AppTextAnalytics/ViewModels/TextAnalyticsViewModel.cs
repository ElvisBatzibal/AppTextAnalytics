using AppTextAnalytics.Models;
using AppTextAnalytics.Services;
using GalaSoft.MvvmLight.Command;
using Microcharts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppTextAnalytics.ViewModels
{
    public class TextAnalyticsViewModel : BaseViewModel
    {
        private readonly TextAnalyticsService _textAnalyticsService = new TextAnalyticsService(Helpers.CognitiveServices.TextAnalytics);
        private string _errorMessage;
        private bool _isBusy;
        private string _textanalyse = "Buenos días señor, compré un Somy Smart TV de su compañía el mes pasado. Lamentablemente no estoy satisfecho con el artículo que ordené. Ha habido un malentendido con mi pedido.";
        private LanguageResult _languageResult;
        private KeyPhrasesResult _keyPhrasesResult;
        private SentimentResult _sentimentResult;
        private EntitiesResult _entityResult;
        private TextAnalyticsDTO _resultAnalyse;
        private ObservableCollection<TextAnalyticsDTO> _listAnalyse;
        private bool _isVisiblePreview;
        private bool _isVisibleAnalyse;
        public bool _isVisibleEndPreview;

        public string TextAnalyse
        {
            get => this._textanalyse;
            set
            {
                this.SetValue(ref this._textanalyse, value);
                if (value.Length > 0)
                {

                    this.IsVisibleAnalyse = true;
                }
                else
                {
                    this.IsVisibleAnalyse = false;
                }
            }
        }
        public bool IsVisibleEndPreview
        {
            get => this._isVisibleEndPreview;
            set => this.SetValue(ref this._isVisibleEndPreview, value);
        }
        public bool IsVisibleAnalyse
        {
            get => this._isVisibleAnalyse;
            set => this.SetValue(ref this._isVisibleAnalyse, value);
        }
        public bool IsVisiblePreview
        {
            get => this._isVisiblePreview;
            set => this.SetValue(ref this._isVisiblePreview, value);

        }
        public ObservableCollection<TextAnalyticsDTO> ListAnalyse
        {
            get => this._listAnalyse;
            set => this.SetValue(ref this._listAnalyse, value);
        }
        public TextAnalyticsDTO ResultAnalyse
        {
            get => this._resultAnalyse;
            set => this.SetValue(ref this._resultAnalyse, value);
        }
        public LanguageResult LanguageResult
        {
            get => this._languageResult;
            set => this.SetValue(ref this._languageResult, value);

        }

        public KeyPhrasesResult KeyPhrasesResult
        {
            get => this._keyPhrasesResult;
            set => this.SetValue(ref this._keyPhrasesResult, value);

        }

        public SentimentResult SentimentResult
        {
            get => this._sentimentResult;
            set => this.SetValue(ref this._sentimentResult, value);
        }

        public EntitiesResult EntityResult
        {
            get => this._entityResult;
            set => this.SetValue(ref this._entityResult, value);
        }

        public string ErrorMessage
        {
            get => this._errorMessage;
            set => this.SetValue(ref this._errorMessage, value);

        }

        public bool IsBusy
        {
            get => this._isBusy;
            set => this.SetValue(ref this._isBusy, value);

        }
        public TextAnalyticsViewModel()
        {
            TextAnalyse = string.Empty;
            IsVisiblePreview = false;
            IsVisibleAnalyse = false;
            IsVisibleEndPreview = false;
            LoadTopAnalisy();
        }
        public ICommand DetectLanguageFromTextCommand => new RelayCommand(DetectLanguageFromText);
        public async void DetectLanguageFromText()
        {
            IsBusy = true;
            try
            {
                LanguageResult = await _textAnalyticsService.DetectLanguageAsync(_textanalyse);
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
            }
            IsBusy = false;
        }
        public ICommand DetectKeyPhrasesCommand => new RelayCommand(DetectKeyPhrases);
        public async void DetectKeyPhrases()
        {
            IsBusy = true;
            try
            {
                KeyPhrasesResult = await _textAnalyticsService.DetectKeyPhrasesFromTextAsync(_textanalyse);
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
            }
            IsBusy = false;
        }
        public ICommand DetectSentimentCommand => new RelayCommand(DetectSentiment);
        public async void DetectSentiment()
        {
            IsBusy = true;
            try
            {
                SentimentResult = await _textAnalyticsService.DetectSentimentFromTextAsync(_textanalyse);
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
            }
            IsBusy = false;
        }

        public ICommand AnalyseAllTextCommand => new RelayCommand(AnalyseAllText);
        public async void AnalyseAllText()
        {
            IsBusy = true;
            try
            {
                IsVisibleEndPreview = true;
                IsVisiblePreview = true;
                IsVisibleAnalyse = false;

                SentimentResult = await _textAnalyticsService.DetectSentimentFromTextAsync(_textanalyse);
                LanguageResult = await _textAnalyticsService.DetectLanguageAsync(_textanalyse);
                KeyPhrasesResult = await _textAnalyticsService.DetectKeyPhrasesFromTextAsync(_textanalyse);
                EntityResult = await _textAnalyticsService.DetectEntityFromTextAsync(_textanalyse);

                TextAnalyticsDTO Analyse = new TextAnalyticsDTO();
                Analyse.TextAnalyse = _textanalyse;
                var Languaje = LanguageResult.Documents.FirstOrDefault().DetectedLanguages.FirstOrDefault();
                Analyse.Language = Languaje.Name + " (" + (Languaje.Score * 100).ToString("0.00") + " %)";
                Analyse.JsonResultLanguage = JsonConvert.SerializeObject(LanguageResult);
                Analyse.KeyPhrases = KeyPhrasesResult.ToString();
                Analyse.JsonResultKeyPhrases = JsonConvert.SerializeObject(KeyPhrasesResult);
                var OpinionDoc = SentimentResult.Documents.FirstOrDefault();
                Analyse.OpinionGeneral = OpinionDoc.Sentiment;
                Analyse.OpinionGeneralPositiva = (OpinionDoc.Documentscores.Positive * 100).ToString("0.00") + " %";
                Analyse.OpinionGeneralNegativa = (OpinionDoc.Documentscores.Negative * 100).ToString("0.00") + " %";
                Analyse.OpinionGeneralNeutral = (OpinionDoc.Documentscores.Neutral * 100).ToString("0.00") + " %";
                Analyse.JsonOpinion = JsonConvert.SerializeObject(SentimentResult);

                Analyse.EntidadesNombre = EntityResult.ToString();
                Analyse.JsonEntidades = JsonConvert.SerializeObject(EntityResult);

                var Id = DataService.Instance.AddItem(Analyse);
                this.TextAnalyse = String.Empty;
                this.ResultAnalyse = Analyse;
                this.IsVisiblePreview = true;
                LoadTopAnalisy();
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
            }
            IsBusy = false;
        }

        public void LoadTopAnalisy()
        {
            var List = DataService.Instance.AccessDBNotAsync.Table<TextAnalyticsDTO>().ToList();
            if (List.Count == 0)
            {
                var LineDefault = new TextAnalyticsDTO
                {
                    TextAnalyse = "Buenos días señor, compré un Somy Smart TV de su compañía el mes pasado. Lamentablemente no estoy satisfecho con el artículo que ordené. Ha habido un malentendido con mi pedido.",
                    Color = "#FF6119",
                    Language = "Español (100%)",
                    KeyPhrases = "Somy Smart TV, compañía, buenos días señor, mes, malentendido, pedido, artículo",
                    CreateDate = DateTime.Now,
                    CreateDateString = DateTime.Now.ToString(),
                    OpinionGeneral = "MIXED",
                    OpinionGeneralPositiva = "33",
                    OpinionGeneralNegativa = "67",
                    OpinionGeneralNeutral = "0",
                    EntidadesNombre = "Somy Smart TV [Organization] last month[DateTime - DateRange]"
                };

                List.Add(LineDefault);



        }

            this.ListAnalyse = new ObservableCollection<TextAnalyticsDTO>(List);
            LoadDataChart();
        }

        public void LoadDataChart()
        {

            foreach (var LineDefault in ListAnalyse)
            {
                List<Microcharts.Entry> DataEntries = new List<Microcharts.Entry>();
                DataEntries.Add(new Microcharts.Entry(float.Parse(LineDefault.OpinionGeneralPositiva)) { Label = "Positivo", ValueLabel = LineDefault.OpinionGeneralPositiva, Color = SkiaSharp.SKColor.Parse("#89c402") });
                DataEntries.Add(new Microcharts.Entry(float.Parse(LineDefault.OpinionGeneralNeutral)) { Label = "Neutral", ValueLabel = LineDefault.OpinionGeneralNeutral, Color = SkiaSharp.SKColor.Parse("#0078d4") });
                DataEntries.Add(new Microcharts.Entry(float.Parse(LineDefault.OpinionGeneralNegativa)) { Label = "Negativo", ValueLabel = LineDefault.OpinionGeneralNegativa, Color = SkiaSharp.SKColor.Parse("#a51419") });


                LineDefault.DataChart = new DonutChart()
                {
                    Entries = DataEntries

                };
            }
            return;
        }

        public ICommand EndAnalyseImageStreamCommand => new RelayCommand(EndAnalyseImage);
    public void EndAnalyseImage()
    {
        IsVisiblePreview = false;
        IsVisibleEndPreview = false;
        IsVisibleAnalyse = false;
        TextAnalyse = String.Empty;
        LoadTopAnalisy();
    }
}
}
