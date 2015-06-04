package md59afb182b026bd01187441571485899c4;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.nulana.NChart.NChartSeriesDataSource,
		com.nulana.NChart.NChartSizeAxisDataSource
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
			"n_maxSize:(Lcom/nulana/NChart/NChartSizeAxis;)F:GetMaxSize_Lcom_nulana_NChart_NChartSizeAxis_Handler:NChart3D_Android.INChartSizeAxisDataSourceInvoker, NChart3D\n" +
			"n_maxValue:(Lcom/nulana/NChart/NChartSizeAxis;)Ljava/lang/Number;:GetMaxValue_Lcom_nulana_NChart_NChartSizeAxis_Handler:NChart3D_Android.INChartSizeAxisDataSourceInvoker, NChart3D\n" +
			"n_minSize:(Lcom/nulana/NChart/NChartSizeAxis;)F:GetMinSize_Lcom_nulana_NChart_NChartSizeAxis_Handler:NChart3D_Android.INChartSizeAxisDataSourceInvoker, NChart3D\n" +
			"n_minValue:(Lcom/nulana/NChart/NChartSizeAxis;)Ljava/lang/Number;:GetMinValue_Lcom_nulana_NChart_NChartSizeAxis_Handler:NChart3D_Android.INChartSizeAxisDataSourceInvoker, NChart3D\n" +
			"";
		mono.android.Runtime.register ("FloatingColor.MainActivity, FloatingColor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("FloatingColor.MainActivity, FloatingColor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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


	public float maxSize (com.nulana.NChart.NChartSizeAxis p0)
	{
		return n_maxSize (p0);
	}

	private native float n_maxSize (com.nulana.NChart.NChartSizeAxis p0);


	public java.lang.Number maxValue (com.nulana.NChart.NChartSizeAxis p0)
	{
		return n_maxValue (p0);
	}

	private native java.lang.Number n_maxValue (com.nulana.NChart.NChartSizeAxis p0);


	public float minSize (com.nulana.NChart.NChartSizeAxis p0)
	{
		return n_minSize (p0);
	}

	private native float n_minSize (com.nulana.NChart.NChartSizeAxis p0);


	public java.lang.Number minValue (com.nulana.NChart.NChartSizeAxis p0)
	{
		return n_minValue (p0);
	}

	private native java.lang.Number n_minValue (com.nulana.NChart.NChartSizeAxis p0);

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
