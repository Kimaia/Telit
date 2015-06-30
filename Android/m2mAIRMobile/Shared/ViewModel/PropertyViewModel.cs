using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;
using Shared.Charts;
using System.Drawing;

namespace Shared.ViewModel
{
	public class PropertyViewModel: IChartDataSource
	{
		public delegate void OnSuccess();
		public delegate void OnError(string msg);

		public enum PropertyPresentationMode { History, Continuous }

		private DALManager 					dataManager;
		private Thing						daThing;
		private Property 					daProperty;

		public 	PropertyPresentationMode 	PresentationMode { get; set; }
		private List<TR50PropertyValue> 	displayedRecords;

		// singleton
		private static PropertyViewModel instance;
		public static PropertyViewModel Instance 
		{
			get {
				if (instance == null)
					instance = new PropertyViewModel ();
				return instance; 
			}
		}
		private PropertyViewModel ()
		{
			dataManager = new DALManager();
			displayedRecords = new List<TR50PropertyValue>();
			PresentationMode = PropertyPresentationMode.History; // default value;
		}


		public void GetThingObjectAsync (string key, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					Logger.Debug ("GetThingObject(), Thing key:" + key);
					Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));
					daThing = await dataManager.DBLoadItemAsync<Thing> (predicate);
					onSuccess();
				}
				catch (Exception e)
				{
					onError("Thing Object Unavailable", e.Message);
				}
			});
		}

		public Thing GetThing()
		{
			return daThing;
		}


		public void OnPropertySelected(Property property, OnSuccess onSuccess, OnError onError)
		{
			if (daProperty == null || daProperty.key != property.key) 
			{
				daProperty = property;
				displayedRecords.Clear ();
			}

			switch (PresentationMode) 
			{
			case PropertyPresentationMode.History:
				GetPropertyHistoryAsync (onSuccess, onError);
				break;
			case PropertyPresentationMode.Continuous:
				GetPropertyContinuousAsync (onSuccess, onError);
				break;
			}
		}

		public void SetPresentationMode(PropertyPresentationMode mode)
		{
			Logger.Debug ("SetPresentationMode: " + mode);
			PresentationMode = mode;
			displayedRecords.Clear();
		}


		#region History
		public void GetPropertyHistoryAsync (OnSuccess onSuccess, OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					var command = TR50CommandFactory.Build (M2MCommands.CommandType.Property_History, daThing.key, daProperty.key);
					var historyRecords = await dataManager.M2MLoadListAsync<TR50PropertyHistoryParams> (command);
					if (historyRecords.Params.HasPayload())
					{
						displayedRecords.Clear();
						foreach (TR50PropertyValue pv in historyRecords.Params.values)
							if (pv.HasPayload())
								displayedRecords.Add(pv);

						onSuccess();
					}
					else
					{
						// display error dialog and cleaer the graph display
						displayedRecords.Clear();
						onError("Property has no history records");
					}
				}
				catch (Exception e)
				{
					displayedRecords.Clear();
					onError("Property's records Unavailable: " + e.Message);
				}
			});
		}
		#endregion
			
		#region Continuous
		public void GetPropertyContinuousAsync (OnSuccess onSuccess, OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					#if DEBUG
					var currentRecord = GetCurrentStub();
					#else
//					var command = TR50CommandFactory.Build (M2MCommands.CommandType.Property_Current, daThing.key, daProperty.key);
//					var currentRecord = await dataManager.M2MLoadListAsync<TR50PropertyValue> (command);
					#endif

					if (currentRecord.Params.HasPayload())
					{
						TR50PropertyValue pv = currentRecord.Params;
						if (pv.HasPayload())
							displayedRecords.Add(pv);

						onSuccess();
					}
					else
					{						
						// display error dialog and clear the graph display
						displayedRecords.Clear();
						onError("Property has no history records");
					}
				}
				catch (Exception e)
				{
					displayedRecords.Clear();
					onError("Property's records Unavailable: " + e.Message);
				}
			});
		}
		#endregion

		#region IChartDataSource
		public List<Point> Points ()
		{
			return ScalePoints4Display();
		}

		public string Name ()
		{
			return daProperty.name;
		}

		public string[] Ticks ()
		{
			string[] ticks = new string[2];
			ticks [0] = displayedRecords [0].ts;
			if (displayedRecords.Count > 1)
				ticks [1] = displayedRecords[displayedRecords.Count-1].ts;
			return ticks;
		}
		#endregion

		#region Points scaling
		private List<Point> ScalePoints4Display()
		{
			if (displayedRecords.Count > 0)
				return Convert2PointsAdjusted();
			else
				return null;
		}

		private List<Point> Convert2PointsAdjusted()
		{
			// log the TR50 points
			Logger.Debug ("TR50 Points: " + PointsLog(displayedRecords));

			var points = new List<Point> ();
			long baseMeasureX = TS2Seconds (displayedRecords[0].ts);
			foreach (TR50PropertyValue pv in displayedRecords) 
				points.Insert (0, TR50Point2PointAdjusted(pv, baseMeasureX));  // in Reverse order

			// Log the converted points
			Logger.Debug ("Converted Points: " + PointsLog(points));

			return points;
		}

		private Point TR50Point2PointAdjusted(TR50PropertyValue tr50p, long zeroPoint)
		{
			PointF pf = new PointF (TS2Seconds (tr50p.ts) - zeroPoint, tr50p.value);
			return new Point ((int)pf.X, (int)pf.Y);
		}

		private long TS2Seconds(string ts)
		{
			return (DateTime.Parse (ts)).Ticks / 10000000;
		}

		private string PointsLog(List<TR50PropertyValue> points)
		{
			string log = null;
			foreach (TR50PropertyValue pv in points)
				log += " {" + pv.ts + ',' + pv.value + "} ,";
			return log;
		}

		private string PointsLog(List<Point> points)
		{
			string log = null;
			foreach (Point pv in points)
				log += " {" + pv.X + ',' + pv.Y + "} ,";
			return log;
		}
		#endregion

		#region DEBUG Current Points Stub
		private TR50Response<TR50PropertyValue> GetCurrentStub()
		{
			var response = new TR50Response<TR50PropertyValue> ();
			response.success = true;
			response.Params = new TR50PropertyValue ();
			Random random = new Random ();
			response.Params.value = random.Next (30) + 1;
			response.Params.ts = TextUtils.CurrentTime ();
			return response;
		}
		#endregion
	}
}

