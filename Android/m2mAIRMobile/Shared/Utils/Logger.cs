using System;
using System.Collections.Generic;

namespace Shared.Utils
{
	enum Level { Debug, Info, Warn, Error };

	public class LogParameters
	{
		public class Parameter
		{
			public string Name {get; set;}
			public object Value {get;set;}
		}

		private readonly List<Parameter> parameters;

		public LogParameters()
		{
			parameters = new List<Parameter> ();
		}

		public LogParameters Add(string name, object value)
		{
			parameters.Add (new Parameter { Name = name, Value = value});
			return this;
		}

		public IReadOnlyList<Parameter> Parameters()
		{
			return parameters;
		}
	}

	public static class Logger
	{
		public static LogParameters Params()
		{
			var p = new LogParameters ();
			return p;
		}

		public static LogParameters Params(string name, object value)
		{
			var p = new LogParameters ().Add(name, value);
			return p;
		}

		public static void Debug(string message, LogParameters parameters = null)
		{
			Message(Level.Debug, message, parameters);
		}

		public static void Info(string message, LogParameters parameters = null)
		{
			Message (Level.Info, message, parameters);
		}

		public static void Warn(string message, LogParameters parameters = null)
		{
			Message (Level.Warn, message, parameters);
		}

		public static void Warn(string message, Exception e, LogParameters parameters = null)
		{
			Message (Level.Warn, message, e, parameters);
		}

		public static void Error(string message, LogParameters parameters = null)
		{
			Message (Level.Warn, message, parameters);
		}

		public static void Error(string message, Exception e, LogParameters parameters = null)
		{
			Message (Level.Warn, message, e, parameters);
		}

		private static void Message(Level level, string message, LogParameters parameters = null)
		{
			var now = DateTime.Now;
			Console.WriteLine ("[{0}] [{1}]: {2}", level, now, message);
			if (parameters == null)
				return;
			foreach (var p in parameters.Parameters()) {
				Console.WriteLine ("\t{0}={1}", p.Name, p.Value);
			}
		}

		private static void Message(Level level, string message, Exception e, LogParameters parameters = null)
		{
			message = message + ": " + e.Message;
			parameters = parameters ?? new LogParameters ();
			parameters.Add ("Exception", e);
			Message (level, message, parameters);
		}
	
	}
}

