package md5e8d59294a1eba115706b4d0c51730c5c;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.nulana.NChart.NChartSeriesDataSource,
		com.nulana.NChart.NChartValueAxisDataSource
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
			"n_doubleToString:(DLcom/nulana/NChart/NChartValueAxis;)Ljava/lang/String;:GetDoubleToString_DLcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"n_length:(Lcom/nulana/NChart/NChartValueAxis;)Ljava/lang/Number;:GetLength_Lcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"n_max:(Lcom/nulana/NChart/NChartValueAxis;)Ljava/lang/Number;:GetMax_Lcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"n_min:(Lcom/nulana/NChart/NChartValueAxis;)Ljava/lang/Number;:GetMin_Lcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"n_name:(Lcom/nulana/NChart/NChartValueAxis;)Ljava/lang/String;:GetName_Lcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"n_step:(Lcom/nulana/NChart/NChartValueAxis;)Ljava/lang/Number;:GetStep_Lcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"n_ticks:(Lcom/nulana/NChart/NChartValueAxis;)[Ljava/lang/String;:GetTicks_Lcom_nulana_NChart_NChartValueAxis_Handler:NChart3D_Android.INChartValueAxisDataSourceInvoker, NChart3D\n" +
			"";
		mono.android.Runtime.register ("SecondaryAxis.MainActivity, SecondaryAxis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("SecondaryAxis.MainActivity, SecondaryAxis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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


	public java.lang.String doubleToString (double p0, com.nulana.NChart.NChartValueAxis p1)
	{
		return n_doubleToString (p0, p1);
	}

	private native java.lang.String n_doubleToString (double p0, com.nulana.NChart.NChartValueAxis p1);


	public java.lang.Number length (com.nulana.NChart.NChartValueAxis p0)
	{
		return n_length (p0);
	}

	private native java.lang.Number n_length (com.nulana.NChart.NChartValueAxis p0);


	public java.lang.Number max (com.nulana.NChart.NChartValueAxis p0)
	{
		return n_max (p0);
	}

	private native java.lang.Number n_max (com.nulana.NChart.NChartValueAxis p0);


	public java.lang.Number min (com.nulana.NChart.NChartValueAxis p0)
	{
		return n_min (p0);
	}

	private native java.lang.Number n_min (com.nulana.NChart.NChartValueAxis p0);


	public java.lang.String name (com.nulana.NChart.NChartValueAxis p0)
	{
		return n_name (p0);
	}

	private native java.lang.String n_name (com.nulana.NChart.NChartValueAxis p0);


	public java.lang.Number step (com.nulana.NChart.NChartValueAxis p0)
	{
		return n_step (p0);
	}

	private native java.lang.Number n_step (com.nulana.NChart.NChartValueAxis p0);


	public java.lang.String[] ticks (com.nulana.NChart.NChartValueAxis p0)
	{
		return n_ticks (p0);
	}

	private native java.lang.String[] n_ticks (com.nulana.NChart.NChartValueAxis p0);

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
