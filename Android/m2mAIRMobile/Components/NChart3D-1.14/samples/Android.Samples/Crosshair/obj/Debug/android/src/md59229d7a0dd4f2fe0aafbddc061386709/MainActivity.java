package md59229d7a0dd4f2fe0aafbddc061386709;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.nulana.NChart.NChartSeriesDataSource,
		com.nulana.NChart.NChartCrosshairDelegate
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
			"n_DidBeginMoving:(Lcom/nulana/NChart/NChartCrosshair;)V:GetDidBeginMoving_Lcom_nulana_NChart_NChartCrosshair_Handler:NChart3D_Android.INChartCrosshairDelegateInvoker, NChart3D\n" +
			"n_DidEndMoving:(Lcom/nulana/NChart/NChartCrosshair;)V:GetDidEndMoving_Lcom_nulana_NChart_NChartCrosshair_Handler:NChart3D_Android.INChartCrosshairDelegateInvoker, NChart3D\n" +
			"n_DidMove:(Lcom/nulana/NChart/NChartCrosshair;)V:GetDidMove_Lcom_nulana_NChart_NChartCrosshair_Handler:NChart3D_Android.INChartCrosshairDelegateInvoker, NChart3D\n" +
			"";
		mono.android.Runtime.register ("Crosshair.MainActivity, Crosshair, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("Crosshair.MainActivity, Crosshair, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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


	public void DidBeginMoving (com.nulana.NChart.NChartCrosshair p0)
	{
		n_DidBeginMoving (p0);
	}

	private native void n_DidBeginMoving (com.nulana.NChart.NChartCrosshair p0);


	public void DidEndMoving (com.nulana.NChart.NChartCrosshair p0)
	{
		n_DidEndMoving (p0);
	}

	private native void n_DidEndMoving (com.nulana.NChart.NChartCrosshair p0);


	public void DidMove (com.nulana.NChart.NChartCrosshair p0)
	{
		n_DidMove (p0);
	}

	private native void n_DidMove (com.nulana.NChart.NChartCrosshair p0);

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
