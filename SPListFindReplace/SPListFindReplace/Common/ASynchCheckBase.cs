using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.Common
{
	/// <summary>
	/// Summary description for ASynchCheckBase.
	/// </summary>
	public abstract class ASynchComponentBase
	{
		public event ComponentEventHandler Complete, Abort;
		public event ComponentErrorEventHandler Error, Trace;

		protected eCheckStatus m_CurrentStatus;
		protected int m_ComponentIndex;
		
		protected bool m_IsCanceling = false;
		protected bool m_IsCanceled = false;

		private delegate void DoWorkDelegate(int componentIndex);

		/// <summary>
		/// Public constructor
		/// </summary>
		public ASynchComponentBase(int componentIndex)
		{	
			m_ComponentIndex = componentIndex;
			m_CurrentStatus = eCheckStatus.Failed;
		}

		/// <summary>
		/// Represent index(id) of specified component
		/// </summary>
		public int ComponentIndex
		{
			get { return m_ComponentIndex; }
		}

		/// <summary>
		/// Readonly property represents current status of the components
		/// </summary>
		public eCheckStatus CurrentStatus
		{
			get
			{
				return m_CurrentStatus;
			}
		}

		/// <summary>
		/// Start asyncronous check.
		/// </summary>
		public void Start ()
		{
			try
			{
				DoWorkDelegate WorkerThread = new DoWorkDelegate(DoWork);
				WorkerThread.BeginInvoke(m_ComponentIndex, new AsyncCallback(DoWorkComplete), WorkerThread);
			}
			catch(Exception ex)
			{
				OnError(ex, "ASynchCheckBase.Start");
			}
		}

		public void Stop ()
		{
			m_IsCanceling = true;
		}

		/// <summary>
		/// Start of asynchronous operation.
		/// Override this function by derived class to implement check process for a specified component.
		/// </summary>
		/// <param name="componentIndex"></param>
		public virtual void DoWork(int componentIndex)
		{
		}

		/// <summary>
		/// End of asynchronous operation.
		/// </summary>
		/// <param name="workID"></param>
		public virtual void DoWorkComplete (IAsyncResult workID)
		{
			if ( m_IsCanceled )
				OnAbort(m_ComponentIndex);
			else
				OnComplete(m_ComponentIndex, m_CurrentStatus);
		}	

		/// <summary>
		/// Helper function to fire Complete event
		/// </summary>
		/// <param name="conponentIndex"></param>
		/// <param name="currentStatus"></param>
		protected void OnComplete (int conponentIndex, eCheckStatus currentStatus)
		{
			if ( Complete != null )
			{
				Complete(this, new ComponentEventArgs(conponentIndex, currentStatus));
			}
		}

		/// <summary>
		/// Helper function to fire Abort event
		/// </summary>
		protected void OnAbort(int conponentIndex)
		{
			if ( Abort != null )
			{
				Abort(this, new ComponentEventArgs(conponentIndex, eCheckStatus.Failed));
			}
		}

		/// <summary>
		/// Helper function ot fire Error event
		/// </summary>
		/// <param name="originalException"></param>
		/// <param name="sourceName"></param>
		protected void OnError (Exception originalException, string sourceName)
		{
			if ( Error != null )
			{
				Error(this, new ComponentErrorEventArgs(originalException, sourceName));
			}
		}

		/// <summary>
		/// Helper function ot fire Trace event
		/// </summary>
		/// <param name="originalException"></param>
		/// <param name="sourceName"></param>
		protected void OnTrace (string message, string sourceName)
		{
			if ( Trace != null )
			{
				Trace(this, new ComponentErrorEventArgs(new Exception(message), sourceName));
			}
		}
	}

	/// <summary>
	/// Delegate for define event handlers for asynchronous component check functionality
	/// </summary>
	public delegate void ComponentEventHandler (object sender, ComponentEventArgs e);
	
	/// <summary>
	/// Delegate for define event handlers error for asynchronous component check functionality
	/// </summary>
	public delegate void ComponentErrorEventHandler(object sender, ComponentErrorEventArgs e);

	/// <summary>
	/// Event argument class for ComponentEventHandler delegate
	/// </summary>
	public class ComponentEventArgs : EventArgs
	{
		private int m_ComponentIndex;
		private eCheckStatus m_CheckStatus;

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="componentIndex"></param>
		/// <param name="checkStatus"></param>
		public ComponentEventArgs(int componentIndex, eCheckStatus checkStatus)
		{
			m_ComponentIndex = componentIndex;
			m_CheckStatus = checkStatus;
		}

		/// <summary>
		/// Component index property
		/// </summary>
		public int ComponentIndex
		{
			get{ return m_ComponentIndex; }
		}

		/// <summary>
		/// Current status property

		/// </summary>
		public eCheckStatus CurrentStatus
		{
			get { return m_CheckStatus; }
			set { m_CheckStatus = value; }
		}
	}

	/// <summary>
	/// Event argument class for ComponentErrorEventHandler delegate
	/// </summary>
	public class ComponentErrorEventArgs : EventArgs
	{
		private Exception m_InnerException;
		private string m_SourceName;		

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="ex"></param>
		/// <param name="sourceName"></param>
		public ComponentErrorEventArgs(Exception ex, string sourceName)
		{
			m_InnerException = ex;
			m_SourceName = sourceName;
		}

		/// <summary>
		/// SourceName property
		/// </summary>
		public string SourceName
		{
			get{return m_SourceName;}
		}

		/// <summary>
		/// Inner exception property of original exception
		/// </summary>
		public Exception InnerException
		{
			get{return m_InnerException;}
		}
	}

	/// <summary>
	/// Enum with statuses of check
	/// </summary>
	public enum eCheckStatus
	{
		Wait,
		Passed,
		Warning,
		Failed
	}
}
