	package com.shifa.employee.logger;


import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import com.shifa.employee.logger.R;


import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.telephony.TelephonyManager;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Toast;


public class registration extends Activity {
	 ProgressDialog progressDialog ;
	 Button reg_btn_step1,reg_btn_step2,reg_btn_step3,reg_btn_step4;
	 LinearLayout reg_ln_step1,reg_ln_step2,reg_ln_step3,reg_ln_step4;
	 EditText reg_tv_first,reg_tv_last,reg_tv_email,reg_tv_password;
	 Context ctx;
	 String REQUEST_DATA = "";
	 
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.registration);
        reg_tv_first = (EditText) findViewById(R.id.reg_tv_first);
        reg_tv_last = (EditText) findViewById(R.id.reg_tv_last);
        reg_tv_email= (EditText) findViewById(R.id.reg_tv_emailid);
        reg_tv_password= (EditText) findViewById(R.id.reg_tv_password);
        
        reg_ln_step1 = (LinearLayout) findViewById(R.id.reg_lay_step1);
        reg_ln_step2 = (LinearLayout) findViewById(R.id.reg_lay_step2);
        reg_ln_step3 = (LinearLayout) findViewById(R.id.reg_lay_step3);
        reg_ln_step4 = (LinearLayout) findViewById(R.id.reg_lay_step4);
        
        reg_btn_step1 = (Button) findViewById(R.id.reg_btn_step1);
        reg_btn_step2 = (Button) findViewById(R.id.reg_btn_step2);
        reg_btn_step3 = (Button) findViewById(R.id.reg_btn_step3);
        reg_btn_step4 = (Button) findViewById(R.id.reg_btn_step4);
        
        reg_tv_first.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals(""))
            			reg_btn_step1.setEnabled(false);
            		else 
            			reg_btn_step1.setEnabled(true);
            }

			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				
			}
        });
        
        reg_tv_email.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals("") || s.toString().trim().indexOf("@") == -1 || s.toString().trim().indexOf(".") == -1)
            			reg_btn_step2.setEnabled(false);
            		else 
            			reg_btn_step2.setEnabled(true);
            }

			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				
			}
        });
          
        reg_tv_password.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals(""))
            			reg_btn_step3.setEnabled(false);
            		else 
            			reg_btn_step3.setEnabled(true);
            }

			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				
			}
        });
          
            
            
        reg_btn_step1.setOnClickListener(new View.OnClickListener() {
	        @Override
	        public void onClick(View view) {
	        	
	        	  reg_ln_step1.setVisibility(view.GONE);
	        	  reg_ln_step2.setVisibility(view.VISIBLE);
	        	  reg_tv_email.requestFocus();
	        }
	        });
        
        reg_btn_step2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            	
            	reg_ln_step2.setVisibility(view.GONE);
	        	reg_ln_step3.setVisibility(view.VISIBLE);
	        	reg_tv_password.requestFocus();
            }
            });
        		
        reg_btn_step3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            	reg_ln_step3.setVisibility(view.GONE);
	        	reg_ln_step4.setVisibility(view.VISIBLE);
	        	reg_btn_step4.requestFocus();
            }
            });
        
        reg_btn_step4.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            	REQUEST_DATA = reg_tv_first.getText().toString() + "-$-$-" + reg_tv_last.getText().toString() + "-$-$-" + reg_tv_password.getText().toString() + "-$-$-" + reg_tv_email.getText().toString() + "-$-$-";
            	DownloadWebPageTask task = new DownloadWebPageTask();
        		task.execute(new String[] { "http://kent.nasz.us/elog_php/reg.php"});
            	
            }
            });
    	/*	
      			
      			String web = "http://kent.nasz.us/app_php/app_reg.php?fname=" + fname.getText().toString() + "&" +
      			"lname=" + lname.getText().toString() + "&email=" + email.getText().toString() + "&dob=" + dob.getText().toString() + "&" +
    			"sex=" + sexs + "&country=" + country.getText().toString() + "&city=" + city.getText().toString() + "&" +
    					"occupation=" + occ + "&" +
    	    					"referemailid=" + referemailid.getText().toString()  +
      			 "&password=" + password.getText().toString();
      			progressDialog =  ProgressDialog.show(registration.this, "", 
                        "Loading. Please wait...", true);
      				Log.e("web",web);
      				web = web.replaceAll(" ", "NASSPACENAS");	
      			task.execute(new String[] { web });

        	*/	 

    	}

    	private class DownloadWebPageTask extends AsyncTask<String, Context, String> {
    		protected Context ctx;
    		@Override
    	    protected String doInBackground(String... urls) {
    			
    			Log.e("doInBackground","enter");
    		      String response = "";
    		      String uri = "";
    		      for (String url : urls) {
    		    	  uri = url;
    		    	  Log.e("uri",uri);
    		    	  try {
    				        DefaultHttpClient client = new DefaultHttpClient();
    				        HttpPost httpGet = new HttpPost(url);
    				        try {
    				        	Log.e("HttpPost", "Progress 0");
    				        	  List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(1);
    				        	  Log.e("data",REQUEST_DATA);
    				        	  nameValuePairs.add(new BasicNameValuePair("data", REQUEST_DATA)); 
    					          Log.e("HttpPost", "Progress 1");
    					          httpGet.setEntity(new UrlEncodedFormEntity(nameValuePairs));
    					          Log.e("HttpPost", "Progress 2");
    				          HttpResponse execute = client.execute(httpGet);
    				          Log.e("HttpPost", "Progress 2.5");
    				          InputStream content = execute.getEntity().getContent();
    				          Log.e("HttpPost", "Progress 3");
    				          BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
    				          String s = "";
    				          Log.e("HttpPost", "Progress 4");
    				          while ((s = buffer.readLine()) != null) {
    				            response += s;
    				          }
    			
    				        } catch (Exception e) {
    				        	 Log.e("Error http:", e.toString());
    				        
    				        	 e.printStackTrace();
    				      		return "-999";
    				        }
    				  }
    		    	  catch(Exception ex)
    		    	  {
    		    		  Log.e("Error http:", ex.toString());
    		    		  return "-999";
    		    	  }
    		      }
    	      Log.e("Response",response);
    	     
    	      return response;
    	    }

    	    @Override
    	    protected void onPostExecute(String result) {
    	    	if (result.equals("-999")) 
    	    		{
    	    			Toast.makeText(getApplicationContext(), "Some problem with server or internet connection", Toast.LENGTH_SHORT).show();
    	    			return;
    	    		}
    	    	
    	    	if (result.trim().equals("1001")) // When async Reposonse received update successfully with value 1001 so clear old sych data
        		{
    	    		Toast.makeText(getApplicationContext(), "Congratulation!! Account Created.", Toast.LENGTH_SHORT).show();
        			finish();
        		}
    	    	
    	    }
    	  }
    	
   
    	
    
}
	