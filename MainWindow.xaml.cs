using System;
using System.Windows;

/*
	    Jiwant Singh
		Nihal Ahmed Gesudraz
		Apoorva Solanki
		Kiranpreet Kaur
		Harkirat Kaur
*/

namespace PenniesForPay
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		PopVM pvm = new PopVM();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = pvm;

		}

		private void SelectionChecked(object sender, RoutedEventArgs e)
		{
			pvm.SelectionChecked();
		}

		private void AutoAdd(object sender, EventArgs e)
		{
			pvm.AutoAddRecord();
		}

		private void AutoClear(object sender, EventArgs e)
		{
			pvm.AutoClear();
		}


		private void Add(object sender, EventArgs e)
		{
			pvm.AddRecord();
		}

		private void Clear(object sender, EventArgs e)
		{
			pvm.Clear();
		}

		private void DispWhole(object sender, EventArgs e)
		{
			pvm.DispWhole();
		}

		private void Reset(object sender, EventArgs e)
		{
			pvm.Reset();
		}

		private void Save(object sender, EventArgs e)
		{
			pvm.SaveList();
		}

		private void Open(object sender, EventArgs e)
		{
			pvm.OpenList();
		}

		private void WriteFile(object sender, EventArgs e)
		{
			pvm.WriteToFile();
		}

		private void WritePDFFile(object sender, EventArgs e)
		{
			pvm.WriteToPdfFile();
		}

		private void PaySlip(object sender, EventArgs e)
		{
			pvm.DispPaySlipfunc();
		}
	}
}
