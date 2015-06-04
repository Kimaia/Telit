package md515ff3835b5172702943c24b3cb56edf8;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.nulana.NChart.NChartSeriesDataSource,
		com.nulana.NChart.NChartDelegate
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_image:(Lcom/nulana/NChart/NChartSeries;)Landroid/graphics/Bitmap;:GetImage_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_name:(Lcom/nulana/NChart/NChartSeries;)Ljava/lang/String;:GetName_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_points:(Lcom/nulana/NChart/NChartSeries;)[Lcom/nulana/NChart/NChartPoint;:GetPoints_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_didEndAnimating:(Lcom/nulana/NChart/NChart;Ljava/lang/Object;Lcom/nulana/NChart/NChartAnimationType;)V:GetDidEndAnimating_Lcom_nulana_NChart_NChart_Ljava_lang_Object_Lcom_nulana_NChart_NChartAnimationType_Handler:NChart3D_Android.INChartDelegateInvoker, NChart3D\n" +
			"n_pointSelected:(Lcom/nulana/NChart/NChart;Lcom/nulana/NChart/NChartPoint;)V:GetPointSelected_Lcom_nulana_NChart_NChart_Lcom_nulana_NChart_NChartPoint_Handler:NChart3D_Android.INChartDelegateInvoker, NChart3D\n" +
			"n_timeIndexChanged:(Lcom/nulana/NChart/NChart;D)V:GetTimeIndexChanged_Lcom_nulana_NChart_NChart_DHandler:NChart3D_Android.INChartDelegateInvoker, NChart3D\n" +
			"";
		mono.android.Runtime.register ("Tooltips.MainActivity, Tooltips, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("Tooltips.MainActivity, Tooltips, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public android.graphics.Bitmap image (com.nulana.NChart.NChartSeries p0)
	{
		return n_image (p0);
	}

	private native android.graphics.Bitmap n_image (com.nulana.NChart.NChartSeries p0);


	public java.lang.String name (com.nulana.NChart.NChartSeries p0)
	{
		return n_name (p0);
	}

	private native java.lang.String n_name (com.nulana.NChart.NChartSeries p0);


	public com.nulana.NChart.NChartPoint[] points (com.nulana.NChart.NChartSeries p0)
	{
		return n_points (p0);
	}

	private native com.nulana.NChart.NChartPoint[] n_points (com.nulana.NChart.NChartSeries p0);


	public void didEndAnimating (com.nulana.NChart.NChart p0, java.lang.Object p1, com.nulana.NChart.NChartAnimationType p2)
	{
		n_didEndAnimating (p0, p1, p2);
	}

	private native void n_didEndAnimating (com.nulana.NChart.NChart p0, java.lang.Object p1, com.nulana.NChart.NChartAnimationType p2);


	public void pointSelected (com.nulana.NChart.NChart p0, com.nulana.NChart.NChartPoint p1)
	{
		n_pointSelected (p0, p1);
	}

	private native void n_pointSelected (com.nulana.NChart.NChart p0, com.nulana.NChart.NChartPoint p1);


	public void timeIndexChanged (com.nulana.NChart.NChart p0, double p1)
	{
		n_timeIndexChanged (p0, p1);
	}

	private native void n_timeIndexChanged (com.nulana.NChart.NChart p0, double p1);

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
