using System;

namespace PopupDictionairy.App.Model
{
    public class Translation : IEquatable<Translation>
    {
        private string fromLanguage;
        private string toLanguage;
        private int correctAnswers;
        private DateTime lastCorrectAnswer;

        public Translation()
        {
        }

        public Translation(string fromLanguage, string toLanguage)
        {
            this.fromLanguage = fromLanguage;
            this.toLanguage = toLanguage;
        }

        public string FromLanguage
        {
            get { return fromLanguage; }
            set { fromLanguage = value; }
        }

        public string ToLanguage
        {
            get { return toLanguage; }
            set { toLanguage = value; }
        }

        public int CorrectAnswers
        {
            get { return correctAnswers; }
            set { correctAnswers = value; }
        }

        public DateTime LastCorrectAnswer
        {
            get { return lastCorrectAnswer; }
            set { lastCorrectAnswer = value; }
        }

        public bool Equals(Translation other)
        {
            return string.Concat(fromLanguage, toLanguage) == string.Concat(other.FromLanguage, other.ToLanguage);
        }
    }
}