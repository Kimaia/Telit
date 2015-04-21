package android.source.screens;


public class ThingsActivity
	extends android.source.screens.BaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Android.Source.Screens.ThingsActivity, m2mAIRMobile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ThingsActivity.class, __md_methods);
	}


	public ThingsActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ThingsActivity.class)
			mono.android.TypeManager.Activate ("Android.Source.Screens.ThingsActivity, m2mAIRMobile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
