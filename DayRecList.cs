using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PenniesForPay
{
	[Serializable]
	class DayRecList
	{
		List<DayRec> LstDayRec = new List<DayRec>();
		int dct = 0;
		int dtot = 0;
		Decimal CumSal = 0;

		public void AddRecordAutomatic(int d)
		{
			for (int i=1;i<=d;i++)
			{
				AddRecord(dct+1, true, 0);
			}
		}

		public void AddRecord(int d, bool p, int o)
		{
			Decimal ds, ods;
			dtot++;
			if (p == true)
			{
				dct++;
				ods = (decimal)(Math.Pow(2, dct - 4) * o);
				ds = (decimal)Math.Pow(2, dct - 1) + ods;
			}

			else
			{
				ds = 0;
			}
			CumSal = CumSal + ds;
			LstDayRec.Add(new DayRec(d, p, o, ds, CumSal));//To Enter into Collection Framework
		}

		public String DispPaySlip()
		{
			return "Number of Days Worked: " + dct.ToString() + Environment.NewLine +
				"Number of Holiday: " + (dtot - dct).ToString() + Environment.NewLine +
				"Total Number of Days: " + dtot.ToString() + Environment.NewLine +
				"Total Pay:" + CumSal.ToString();
		}

		public String DispWholeList()
		{
			string str = "";
			foreach (DayRec day in LstDayRec)
			{
				str = str + day.ToString();
				str = str + Environment.NewLine;
			}
			return str;
		}
		
		//true: Generates the PDF; false: Resets the PDF.
		public void GeneratePdf(bool generatePdf)
		{
			int marginTop = 50;
			int marginLeft = 10;
			int marginRight = 10;
			int marginBottom = 50;

			Document doc = new Document(iTextSharp.text.PageSize.A4, marginLeft, marginRight, marginTop, marginBottom);
			PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Susan's Payslip.pdf", FileMode.OpenOrCreate));
			Paragraph p = new Paragraph();
			BaseFont timesNewRoman = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
			//Two Columns
			PdfPTable table = new PdfPTable(5);
			PdfPCell dayValue = new PdfPCell(new Phrase("Day Worked"));
			PdfPCell dayAttendance = new PdfPCell(new Phrase("Attendance"));
			PdfPCell dayOvertime = new PdfPCell(new Phrase("Overtime"));
			PdfPCell daySalary = new PdfPCell(new Phrase("Day's Salary"));
			PdfPCell amountEarned = new PdfPCell(new Phrase("Total Amount Earned (Pennies) "));
			PdfPCell totalDays = new PdfPCell(new Phrase("Total Calculated Earnings"));
			PdfPCell totalEarned;

			//Centers the table header values
			dayValue.HorizontalAlignment = 1;
			dayAttendance.HorizontalAlignment = 1;
			dayOvertime.HorizontalAlignment = 1;
			daySalary.HorizontalAlignment = 1;
			amountEarned.HorizontalAlignment = 1;
			doc.Open();

			p.Alignment = Element.ALIGN_CENTER;
			p.Font = new Font(timesNewRoman, 24);
			p.Add("Payslip: Susan");
			doc.Add(p);

			//Line breaks for formatting
			doc.Add(new Paragraph(" "));
			doc.Add(new Paragraph(" "));

			//Table Headers
			table.AddCell(dayValue);
			table.AddCell(dayAttendance);
			table.AddCell(dayOvertime);
			table.AddCell(daySalary);
			table.AddCell(amountEarned);

			if (generatePdf == true)
			{
				foreach (DayRec day in LstDayRec)
				{
					table.AddCell(day.DayNum.ToString());
					table.AddCell(day.Pres ? "Present" : "Absent");
					table.AddCell(day.DayOvr.ToString());
					table.AddCell(day.DaySal.ToString());
					CumSal = CumSal + Decimal.Parse(Math.Pow(2, day.DayNum).ToString());
					table.AddCell(day.DayCumSal.ToString());
				}
			}

			totalEarned = new PdfPCell(new Phrase(CumSal.ToString()));
			table.AddCell(totalDays);
			table.AddCell(totalEarned);
			doc.Add(table);
			doc.Close();
		}

		public void Reset()
		{
			//Reset List Variable
			dct = 0;
			dtot = 0;
			CumSal = 0;
			LstDayRec.Clear();

			//Code to Empty Serial File
				BinaryFormatter bf = new BinaryFormatter();
				FileStream fsout = new FileStream(@"test.binary", FileMode.Create, FileAccess.Write, FileShare.None);
				try
				{
					bf.Serialize(fsout, LstDayRec);
				}
				catch { }

			//Code to Empty Text File
			File.WriteAllText(@"test.txt","");

			//Code to Empty PDF File
			GeneratePdf(false);
		}
	}
}