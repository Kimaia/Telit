package md5aa0e609ec1a7b8f4c54b28698359c708;


public class LocksListAdapter
	extends android.widget.BaseAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemId:(I)J:GetGetItemId_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItem:(I)Ljava/lang/Object;:GetGetItem_IHandler\n" +
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"";
		mono.android.Runtime.register ("com.telit.lock_and_safe.LocksListAdapter, LockAndSafe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocksListAdapter.class, __md_methods);
	}


	public LocksListAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LocksListAdapter.class)
			mono.android.TypeManager.Activate ("com.telit.lock_and_safe.LocksListAdapter, LockAndSafe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public LocksListAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == LocksListAdapter.class)
			mono.android.TypeManager.Activate ("com.telit.lock_and_safe.LocksListAdapter, LockAndSafe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public long getItemId (int p0)
	{
		return n_getItemId (p0);
	}

	private native long n_getItemId (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public java.lang.Object getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native java.lang.Object n_getItem (int p0);


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
