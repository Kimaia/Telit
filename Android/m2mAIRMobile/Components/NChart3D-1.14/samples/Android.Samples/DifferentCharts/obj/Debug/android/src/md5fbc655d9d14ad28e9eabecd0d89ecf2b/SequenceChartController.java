package md5fbc655d9d14ad28e9eabecd0d89ecf2b;


public class SequenceChartController
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.nulana.NChart.NChartSeriesDataSource
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_image:(Lcom/nulana/NChart/NChartSeries;)Landroid/graphics/Bitmap;:GetImage_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_name:(Lcom/nulana/NChart/NChartSeries;)Ljava/lang/String;:GetName_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_points:(Lcom/nulana/NChart/NChartSeries;)[Lcom/nulana/NChart/NChartPoint;:GetPoints_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"";
		mono.android.Runtime.register ("DifferentCharts.SequenceChartController, DifferentCharts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SequenceChartController.class, __md_methods);
	}


	public SequenceChartController () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SequenceChartController.class)
			mono.android.TypeManager.Activate ("DifferentCharts.SequenceChartController, DifferentCharts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public SequenceChartController (com.nulana.NChart.NChartView p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == SequenceChartController.class)
			mono.android.TypeManager.Activate ("DifferentCharts.SequenceChartController, DifferentCharts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "NChart3D_Android.NChartView, NChart3D, Version=1.14.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


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
