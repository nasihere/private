package shifa.nasz.locally;


import android.app.TabActivity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.TabHost;
import android.widget.TabHost.TabSpec;

public class activity_tabviewer extends TabActivity {
    /** Called when the activity is first created. */
	 String SearchKeyWord = "";
	 TabHost tabHost;
	 
	 String[] StrAutoCompleteMap = null;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.tab_materia_medica);

//      bundle read parameter boeriek data


        
	//	intent.putExtra("allen_data", sDataAllen);
	//	intent.putExtra("kent_data", sDataKent);
		
		
		
        /** TabHost will have Tabs */
         tabHost = (TabHost)findViewById(android.R.id.tabhost);
 
        /** TabSpec used to create a new tab.
         * By using TabSpec only we can able to setContent to the tab.
         * By using TabSpec setIndicator() we can set name to tab. */
 
        /** tid1 is firstTabSpec Id. Its used to access outside. */
        TabSpec firstTabSpec = tabHost.newTabSpec("tab_id1");
        TabSpec secondTabSpec = tabHost.newTabSpec("tab_id2");
        TabSpec thirdTabSpec = tabHost.newTabSpec("tab_id3");


        Intent IntentKent = new Intent(activity_tabviewer.this,
                activity_kent.class);

        Intent IntentGoogleMap = new Intent(activity_tabviewer.this,
                activity_googleMap.class);


        Intent IntentURL = new Intent(activity_tabviewer.this,
                activity_event.class);
        IntentURL.putExtra("url", "http://kent.nasz.us/angframe/index.html?city=Mumbra&category=Classes&Mobile=9833983933");


        firstTabSpec.setIndicator("List").setContent(IntentKent);
        secondTabSpec.setIndicator("Map" ).setContent(IntentGoogleMap);
        thirdTabSpec.setIndicator("Free Listing" ).setContent(IntentURL);


        /** Add tabSpec to the TabHost to display. */
        	tabHost.addTab(firstTabSpec);
        	tabHost.addTab(secondTabSpec);
        tabHost.addTab(thirdTabSpec);
        tabHost.setCurrentTab(0);







    }
 
}
