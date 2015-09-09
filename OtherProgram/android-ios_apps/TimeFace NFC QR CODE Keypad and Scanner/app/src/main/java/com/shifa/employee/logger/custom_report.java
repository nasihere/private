	package com.shifa.employee.logger;



import com.shifa.employee.logger.R;


import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class custom_report extends Activity {
	public String SessionID = "";
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        	setContentView(R.layout.custom_report);
        	SessionID = LoggedIn();
	        final Button button = (Button) findViewById(R.id.btnWebMore);
	        button.setOnClickListener(new View.OnClickListener() {

	        	  @Override
	        	  public void onClick(View view) {
	        		  
	        		  //openURL("")
	        		  openURL("http://kent.nasz.us/elog_php/moreaboutproduct.php?session_id="+SessionID);
	        		 
	        	  }

	        	});
	        
	        
    	}

    	
    	@Override
    	public boolean onKeyDown(int keyCode, KeyEvent event) {
    		   if (keyCode == KeyEvent.KEYCODE_BACK) {
    	    		
    				finish();
    		        return true;
    		    }
    	    return super.onKeyDown(keyCode, event);
    	}
    	private void openURL(String URL)
    	{
    		
    		  Intent intent = new Intent(custom_report.this, activity_events.class);
    		  intent.putExtra("url", URL);
    		  startActivity(intent);
    		  
    	}
    	private String LoggedIn()
    	{

    		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
    		String restoredText = prefs.getString("session_id", null);
    		if (restoredText != null) 
    		{
    			return restoredText;
    		}
    		return "";

    	}
}
	