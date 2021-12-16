using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Windows.Input;
using ASDF.Database;
using ASDF.Model;
using System.Data;

namespace ASDF.ViewModel
{
    public class Payment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Window ownerWindow = null;


        public Payment(Window win)
        {
            ownerWindow = win;
        }

        ment _pa = new ment();
        // MainWindow 객체 선언


        public string Name
        {
            get { return _pa.Name; }
            set
            {
                if (value != _pa.Name)
                {
                    _pa.Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public string Doctor
        {
            get { return _pa.Doctor; }
            set
            {
                if (value != _pa.Doctor)
                {
                    _pa.Doctor = value;
                    this.OnPropertyChanged("Doctor");
                }
            }
        }


        public string Group
        {
            get { return _pa.Group; }
            set
            {
                if (value != _pa.Group)
                {
                    _pa.Group = value;
                    this.OnPropertyChanged("Group");
                }
            }
        }

        public string Date
        {
            get { return _pa.Date; }
            set
            {
                if (value != _pa.Date)
                {
                    _pa.Date = value;
                    this.OnPropertyChanged("Date");
                }
            }
        }




        /*
        public Payment(string name, string doctor, string group, string str_date)
        {
            _pa.Name = name;
            this.doctor = doctor;
            this.group = group;
            this.str_date = str_date;
        }
        */
        /*
        ObservableCollection<patient> _sampleDatas = null;
        public ObservableCollection<patient> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<patient>();
                }
                return _sampleDatas;
            }
            set
            { _sampleDatas = value; }
        }*/
        ObservableCollection<ment> _sampleDatas = null;
        public ObservableCollection<ment> SampleDatas
        {

            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<ment>();
                    DataSet ds = new DataSet();
                    string query = @" SELECT PT_NAME, PT_REGNUM, PT_ADDR, PT_PHONE FROM PATIENT ";
                    OracleDBManager.Instance.ExecuteDsQuery(ds, query);

                    for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                    {
                        ment obj = new ment()
                        {
                            Name = ds.Tables[0].Rows[idx]["PT_NAME"].ToString(),
                            Doctor = ds.Tables[0].Rows[idx]["PT_REGNUM"].ToString(),
                            Group = ds.Tables[0].Rows[idx]["PT_ADDR"].ToString(),
                            Date = ds.Tables[0].Rows[idx]["PT_PHONE"].ToString(),

                        };
                        SampleDatas.Add(obj);

                        
                    }
                }
                return _sampleDatas;
            }
            set
            { _sampleDatas = value; OnPropertyChanged("_sampleDatas"); }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }



        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return (this.connectCommand) ?? (this.connectCommand = new DelegateCommand(Connect));
            }
        }

        private ICommand selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                return (this.selectCommand) ?? (this.selectCommand = new DelegateCommand(DataSearch));
            }
        }
     
  
        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return (this.loadedCommand) ?? (this.loadedCommand = new DelegateCommand(LoadEvent));
            }
        }

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return (this.nextCommand) ?? (this.nextCommand = new DelegateCommand(ButtonShow));
            }
        }

        private void ButtonShow()
        {
            ASDF.prescription prescription = new ASDF.prescription();
            prescription.ShowDialog();
        }

        private void LoadEvent()
        { //Connect to DB
            if (OracleDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }
        }
        
        private void DataSearch()
        {
            DataSet ds = new DataSet();
            string query = @" SELECT PT_NAME, PT_REGNUM, PT_ADDR, PT_PHONE FROM PATIENT ";
            OracleDBManager.Instance.ExecuteDsQuery(ds, query);

            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                ment obj = new ment()
                {
                    Name = ds.Tables[0].Rows[idx]["PT_NAME"].ToString(),
                    Doctor = ds.Tables[0].Rows[idx]["PT_REGNUM"].ToString(),
                    Group = ds.Tables[0].Rows[idx]["PT_ADDR"].ToString(),
                    Date = ds.Tables[0].Rows[idx]["PT_PHONE"].ToString(),

                };
                SampleDatas.Add(obj);
            }
        }

        public void Connect()
        { //Connect to DB
            if (OracleDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }
        }


    }

 
    /*
    public class Payment_pays : ViewModelBase
    {
        private ObservableCollection<Payment> payments;

        public ObservableCollection<Payment> Payments
        {
            get
            {
                if (this.payments == null)
                {
                    this.payments = this.CreatePayments();
                }

                return this.payments;
            }
        }

        private ObservableCollection<Payment> CreatePayments()
        {
            ObservableCollection<Payment> payments = new ObservableCollection<Payment>();
            Payment payment;

            payment = new Payment("name", "doctor", "group", DateTime.Now.ToString("MM/dd/yyyy"));
            payments.Add(payment);
            payment = new Payment("이순자", "김철수", "치과", DateTime.Now.ToString("MM/dd/yyyy"));
            payments.Add(payment);


            return payments;
        }
    }*/

}
