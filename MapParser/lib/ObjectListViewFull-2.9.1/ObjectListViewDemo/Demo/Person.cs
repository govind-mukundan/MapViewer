using System;
using System.ComponentModel;
using System.Drawing;
using BrightIdeasSoftware;

namespace ObjectListViewDemo {

    public enum MaritalStatus
    {
        Single,
        Married,
        Divorced,
        Partnered
    }

    public class Person : INotifyPropertyChanged
    {
        public bool IsActive = true;

        public Person(string name) {
            this.name = name;
        }

        public Person(string name, string occupation, int culinaryRating, DateTime birthDate, double hourlyRate, bool canTellJokes, string photo, string comments) {
            this.name = name;
            this.Occupation = occupation;
            this.culinaryRating = culinaryRating;
            this.birthDate = birthDate;
            this.hourlyRate = hourlyRate;
            this.CanTellJokes = canTellJokes;
            this.Comments = comments;
            this.Photo = photo;
        }

        public Person(Person other) {
            this.name = other.Name;
            this.Occupation = other.Occupation;
            this.culinaryRating = other.CulinaryRating;
            this.birthDate = other.BirthDate;
            this.hourlyRate = other.GetRate();
            this.CanTellJokes = other.CanTellJokes;
            this.Photo = other.Photo;
            this.Comments = other.Comments;
            this.MaritalStatus = other.MaritalStatus;
        }

        [OLVIgnore]
        public Image ImageAspect {
            get {
                return Resource1.folder16;
            }
        }

        [OLVIgnore]
        public string ImageName {
            get {
                return "user";
            }
        }

        // Allows tests for properties.
        [OLVColumn(ImageAspectName = "ImageName")]
        public string Name {
            get { return name; }
            set {
                if (name == value) return;
                name = value;
                this.OnPropertyChanged("Name");
            }
        }
        private string name;

        [OLVColumn(ImageAspectName = "ImageName")]
        public string Occupation {
            get { return occupation; }
            set {
                if (occupation == value) return;
                occupation = value;
                this.OnPropertyChanged("Occupation");
            }
        }
        private string occupation;

        public int CulinaryRating {
            get { return culinaryRating; }
            set { culinaryRating = value; }
        }
        private int culinaryRating;

        public DateTime BirthDate {
            get { return birthDate; }
            set { birthDate = value; }
        }
        private DateTime birthDate;

        public int YearOfBirth {
            get { return this.BirthDate.Year; }
            set { this.BirthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
        }

        // Allow tests for methods
        public double GetRate() {
            return hourlyRate;
        }
        private double hourlyRate;

        public void SetRate(double value) {
            hourlyRate = value;
        }

        // Allows tests for fields.
        public string Photo;
        public string Comments;
        public int serialNumber;
        public bool? CanTellJokes;

        // Allow tests for enums
        public MaritalStatus MaritalStatus = MaritalStatus.Single;

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}