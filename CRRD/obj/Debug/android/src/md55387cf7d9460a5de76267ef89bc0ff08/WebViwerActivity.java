package md55387cf7d9460a5de76267ef89bc0ff08;


public class WebViwerActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CRRD.Resources.Activities.WebViwerActivity, CRRD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WebViwerActivity.class, __md_methods);
	}


	public WebViwerActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == WebViwerActivity.class)
			mono.android.TypeManager.Activate ("CRRD.Resources.Activities.WebViwerActivity, CRRD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
