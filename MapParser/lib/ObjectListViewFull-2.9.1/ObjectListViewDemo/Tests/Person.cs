/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/21/2008 11:09 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/21/2008 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BrightIdeasSoftware.Tests
{
    /// <summary>
    /// Description of Person.
    /// </summary>

    public class Person : INotifyPropertyChanged
    {
        public bool? IsActive = null;

        public Person(string name)
        {
            this.name = name;
        }

        public Person(string name, string occupation, int culinaryRating, DateTime birthDate, double hourlyRate, bool canTellJokes, string photo, string comments)
        {
            this.name = name;
            this.Occupation = occupation;
            this.culinaryRating = culinaryRating;
            this.birthDate = birthDate;
            this.hourlyRate = hourlyRate;
            this.CanTellJokes = canTellJokes;
            this.Comments = comments;
            this.Photo = photo;
        }

        public Person(Person other)
        {
            this.name = other.Name;
            this.Occupation = other.Occupation;
            this.culinaryRating = other.CulinaryRating;
            this.birthDate = other.BirthDate;
            this.hourlyRate = other.GetRate();
            this.CanTellJokes = other.CanTellJokes;
            this.Photo = other.Photo;
            this.Comments = other.Comments;
            this.Parent = other.Parent;
        }

        public Person Parent
        {
            get { return parent; }
            set {
                if (parent == value) return;
                parent = value;
                this.OnPropertyChanged("Parent");
            }
        }
        private Person parent;

        // Allows tests for properties.
        public string Name
        {
            get { return name; }
            set {
                if (name == value) return;
                name = value;
                this.OnPropertyChanged("Name");
            }
        }
        private string name;

        public string Occupation
        {
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
            set {
                if (culinaryRating == value) return;
                culinaryRating = value;
                this.OnPropertyChanged("CulinaryRating");
            }
        }
        private int culinaryRating;

        public DateTime BirthDate {
            get { return birthDate; }
            set {
                if (birthDate == value) return;
                birthDate = value;
                this.OnPropertyChanged("BirthDate");
            }
        }
        private DateTime birthDate;

        public int YearOfBirth
        {
            get { return this.BirthDate.Year; }
            set { this.BirthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
        }

        // Allows tests for methods
        virtual public double GetRate()
        {
            return hourlyRate;
        }
        private double hourlyRate;

        public void SetRate(double value)
        {
            hourlyRate = value;
        }

        // Allow tests on trees
        public IList<Person> Children
        {       
            get { return children; }
            set { children = value; }
        }
        private IList<Person> children = new List<Person>();

        public void AddChild(Person child) {
            Children.Add(child);
            child.Parent = this;
        }

        // Allows tests for fields.
        public string Photo;
        public string Comments;
        public bool CanTellJokes;

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CountNotifyPropertyChangedSubscriptions {
            get { return this.PropertyChanged == null ? 0 : this.PropertyChanged.GetInvocationList().Length; }
        }

        #endregion

        #region Equality members

        protected bool Equals(Person other) {
            return string.Equals(name, other.name);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            Person other = obj as Person;
            return other != null && Equals(other);
        }

        public override int GetHashCode() {
            return (name != null ? name.GetHashCode() : 0);
        }

        #endregion
    }

    // Model class for testing virtual and overridden methods

    public class Person2 : Person
    {
        public Person2(string name, string occupation, int culinaryRating, DateTime birthDate, double hourlyRate, bool canTellJokes, string photo, string comments)
            : base(name, occupation, culinaryRating, birthDate, hourlyRate, canTellJokes, photo, comments)
        {
        }

        public override double GetRate()
        {
            return base.GetRate() * 2;
        }

        new public int CulinaryRating
        {
            get { return base.CulinaryRating * 2; }
            set { base.CulinaryRating = value; }
        }
    }

    public static class PersonDb
    {
        static void InitializeAllPersons()
        {
            sAllPersons = new List<Person>(new Person[] {             
                new Person("name", "occupation", 300, DateTime.Now.AddYears(1), 1.0, true, "  photo  ", "comments"),
                new Person2("name2", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments"),
                new Person(PersonDb.FirstAlphabeticalName, "occupation3", 90, DateTime.Now, 3.0, true, "  photo3  ", "comments3"),
                new Person("name4", "occupation4", 80, DateTime.Now, 4.0, true, "  photo4  ", "comments4"),
                new Person2("name5", "occupation5", 70, DateTime.Now, 5.0, true, "  photo5  ", "comments5"),
                new Person("name6", "occupation6", 65, DateTime.Now, 6.0, true, "  photo6  ", "comments6"),
                new Person("name7", "occupation7", 62, DateTime.Now, 7.0, true, "  photo7  ", "comments7"),
                new Person(PersonDb.LastAlphabeticalName, "occupation6", 60, DateTime.Now.AddYears(-1), 6.0, true, "  photo6  ", LastComment),
            });
            sAllPersons[0].AddChild(sAllPersons[2]);
            sAllPersons[0].AddChild(sAllPersons[3]);
            sAllPersons[1].AddChild(sAllPersons[4]);
            sAllPersons[1].AddChild(sAllPersons[5]);
            sAllPersons[5].AddChild(sAllPersons[6]);
            sAllPersons[6].AddChild(sAllPersons[7]);
        }
        static private List<Person> sAllPersons;

        static public List<Person> All
        {
            get {
                if (sAllPersons == null)
                    InitializeAllPersons();
                return sAllPersons;
            }
        }

        static public void Reset() {
            sAllPersons = null;
        }

        static public string FirstAlphabeticalName
        {
            get { return "aaa First Alphabetical Name"; }
        }

        static public string LastAlphabeticalName {
            get { return "zzz Last Alphabetical Name"; }
        }

        static public string LastComment {
            get { return "zzz Last Comment"; }
        }
    }
}
