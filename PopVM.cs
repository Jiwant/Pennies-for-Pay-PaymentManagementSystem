using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PenniesForPay
{
	class PopVM : INotifyPropertyChanged
	{
		//Initializing Internal Variables of MVVM
		bool autoMode=false;
		bool manMode = false;
		bool automaticEnable=false;
		bool manualEnable=false;
		string autodayNum="1";
		int dayNum=1;
		bool pres=false;
		int ovrTime;
		String dispPaySlip;
		DayRecList drc = new DayRecList();

		public String AutoMode
		{
			get { return autoMode.ToString(); }
			set { autoMode = bool.Parse(value);OnPropertyChanged(); }
		}

		public String ManMode
		{
			get { return manMode.ToString(); }
			set { manMode = bool.Parse(value); OnPropertyChanged(); }
		}

		public String AutomaticEnable
		{
			get { return automaticEnable.ToString(); }
			set { automaticEnable = bool.Parse(value);OnPropertyChanged(); }
		}

		public String ManualEnable
		{
			get { return manualEnable.ToString(); }
			set { manualEnable = bool.Parse(value); OnPropertyChanged(); }
		}

		public String AutoDayNum
		{
			get { return autodayNum; }
			set { autodayNum = value;OnPropertyChanged(); }
		}

		public String DayNum
		{
			get	{return dayNum.ToString();}
			set {dayNum = int.Parse(value);OnPropertyChanged();}
		}

		public bool Pres
		{ 
			get	{return pres;}
			set	{pres = value;OnPropertyChanged();}
		}

		public String OvrTime
		{
			get	{return ovrTime.ToString();}
			set	{ovrTime = int.Parse(value);OnPropertyChanged();}
		}

		public String DispPaySlip
		{
			get	{return dispPaySlip;}
			set	{dispPaySlip = value;OnPropertyChanged();}
		}

		public void SelectionChecked()
		{
			if(autoMode==true)
			{
				AutomaticEnable = true.ToString();
				ManualEnable = false.ToString();
			}
			else if(autoMode==false)
			{
				AutomaticEnable = false.ToString();
				ManualEnable = true.ToString();
			}
		}

		public void AutoAddRecord()
		{
			drc.AddRecordAutomatic(int.Parse(AutoDayNum));
		}

		public void AutoClear()
		{
			AutoDayNum = "1";
		}
		public void AddRecord()
		{
			drc.AddRecord(int.Parse(DayNum), Pres, ovrTime);
			DayNum = (int.Parse(DayNum) + 1).ToString();
		}

		public void Clear()
		{
			DayNum = "1";
			Pres = true;
			OvrTime = "0";
		}

		public void Reset()
		{
			drc.Reset();
		}
		public void SaveList()
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fsout = new FileStream(@"test.binary", FileMode.Create, FileAccess.Write, FileShare.None);
			try
			{
					bf.Serialize(fsout, drc);
			}

			catch {}
		}

		public void OpenList()
		{
			BinaryFormatter bf = new BinaryFormatter();

			FileStream fsin = new FileStream(@"test.binary", FileMode.Open, FileAccess.Read, FileShare.None);
			try
			{
				drc = (DayRecList)bf.Deserialize(fsin);
			}
			catch {}
		}

		public void WriteToFile()
		{
			File.WriteAllText(@"test.txt", drc.DispPaySlip()+Environment.NewLine+Environment.NewLine+Environment.NewLine+ drc.DispWholeList());
		}

		public void WriteToPdfFile()
		{
			drc.GeneratePdf(true);
		}

		public void DispWhole()
		{
			DispPaySlip = drc.DispWholeList();
		}

		public void DispPaySlipfunc()
		{
			DispPaySlip = drc.DispPaySlip();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] String propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}