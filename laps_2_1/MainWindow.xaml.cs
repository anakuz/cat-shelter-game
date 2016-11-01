using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using laps_2_1.Annotations;

namespace laps_2_1
{
    public partial class MainWindow
    {
        private readonly ShelterViewModel _viewModel = new ShelterViewModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        public ShelterViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }














    public class ShelterViewModel
    {
        private readonly IEnumerable<Cat> _cats;

        private DateTime _lastUpdateTime = DateTime.Now;
        private readonly CatShop _selectedCatShop = new CatShop();
        private Cat _selectedCat;

        public ShelterViewModel()
        {
            _cats = new[]
            {
                new Cat(CatType.Cat1),
                new Cat(CatType.Cat2),
            };
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var nowTime = DateTime.Now;
            var delta = nowTime - _lastUpdateTime;
            foreach (var cat in Cats)
                cat.Update(delta);
            _lastUpdateTime = nowTime;
        }

        public IEnumerable<Cat> Cats
        {
            get { return _cats; }
        }

        public Cat SelectedCat
        {
            get { return _selectedCat; }
            set
            {
                _selectedCat = value;
                _selectedCatShop.Cat = value;
            }
        }

        public CatShop SelectedCatShop
        {
            get { return _selectedCatShop; }
        }
    }

    public class CatShop : INotifyPropertyChanged
    {
        private Cat _cat;
        private IEnumerable<CatShopItem> _items = new 
        CatShopItem []
        {
            new ClewShopItem(), 
            new ShieldShopItem()
        };

        public IEnumerable<CatShopItem> ItemsForBuying
        {
            get { return _items; }
        }

        public Cat Cat
        {
            get { return _cat; }
            set
            {
                if (_cat == value) 
                    return;
                _cat = value;
                OnPropertyChanged();
                OnPropertyChanged("IsCatSelected");
            }
        }

        public bool IsCatSelected { get { return Cat != null; } }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class CatShopItem
    {
        public abstract string Name { get; }

        public abstract decimal Cost { get; }
    }

    public class ShieldShopItem : CatShopItem
    {
        public override decimal Cost
        {
            get { return 100; }
        }

        public override string Name
        {
            get { return "shield"; }
        }
    }

    public class ClewShopItem : CatShopItem
    {
        public override string Name
        {
            get { return "clew"; }
        }

        public override decimal Cost
        {
            get { return 50; }
        }
    }




    public enum CatType
    {
        Cat1,
        Cat2,
        Cat3
    }



    public class Cat : INotifyPropertyChanged
    {
        private TimeSpan _age;
        public CatType Type { get; set; }

        public string Name
        {
            get { return Type.ToString(); }
        }

        public TimeSpan Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
                OnPropertyChanged("AgeString");
            }
        }

        public string AgeString
        {
            get { return string.Format("{0} : {1}", (int)Age.TotalDays, Age.Hours); }
        }

        public IEnumerable<Dog> AffectingDogs
        {
            get
            {
                return null;
                throw new NotImplementedException();
            }
        }

        public bool UnderShiled
        {
            get { return true; }
        }

        public Cat(CatType type)
        {
            Type = type;
        }

        public void Update(TimeSpan delta)
        {
            Age = Age.Add(TimeSpan.FromHours(1));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Dog
    {
    }
}
