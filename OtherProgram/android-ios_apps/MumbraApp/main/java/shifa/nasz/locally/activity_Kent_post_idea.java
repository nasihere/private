package shifa.nasz.locally;



import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.TextView;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;

public class activity_Kent_post_idea extends Activity {
 Context ctx;
 Super_Library_AppClass SLAc;
 String id_web = "";
 String SessionID = "";
 String SessionName = "";
@Override
public void onCreate(Bundle savedInstanceState) {
    super.onCreate(savedInstanceState);
    setContentView(R.layout.activity_kent_idea_post);
    ctx = this;
    this.SLAc = new Super_Library_AppClass(ctx);
    
    
    
    
    Bundle extras = getIntent().getExtras();
    if (extras == null) {
    	finish();
    }
    id_web = extras.getString("id_web"); // important line to save in the database
    SessionID  = SLAc.RestoreSessionIndexID("session_id");
    final EditText EditTextPostData = (EditText) findViewById(R.id.editVIdeaReadMe);
    
    
    TextView button1 = (TextView) findViewById(R.id.txtVBtnIdeaPost);
    button1.setOnClickListener(new OnClickListener() {

		@Override
		public void onClick(View v) {
			String Data = EditTextPostData.getText().toString();
			if (Data != ""){

                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(3);
                nameValuePairs.add(new BasicNameValuePair("id_web", id_web));
                nameValuePairs.add(new BasicNameValuePair("comment", Data));
                nameValuePairs.add(new BasicNameValuePair("mobile", SessionID));

                Super_Library_URL SLU = new Super_Library_URL( "http://kent.nasz.us/mumbra/php/comment.php",  nameValuePairs, ((Activity)ctx)  );
                finish();

			}
		}
	});
    
    TextView button2 = (TextView) findViewById(R.id.txtVBtnIdeaCancel);
    button2.setOnClickListener(new OnClickListener() {

		@Override
		public void onClick(View v) {
			finish();
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

  public String GetPreferenceValue(String StringName)
    {
    	SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		String restoredText = prefs.getString(StringName, null);
		if (restoredText != null) 
		{
			return restoredText;
		}
		return "0";
    }
}
