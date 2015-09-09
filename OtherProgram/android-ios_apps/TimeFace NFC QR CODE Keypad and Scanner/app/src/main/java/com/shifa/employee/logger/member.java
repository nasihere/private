	package com.shifa.employee.logger;


import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.Toast;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;


public class member extends Activity {
	 ProgressDialog progressDialog ;

	 Button emp_btn_step1,emp_btn_step2,emp_btn_step3,emp_btn_step4,emp_btn_step5;
	 ScrollView emp_src_step1;
	 LinearLayout emp_lay_step2,emp_lay_step3,emp_lay_step4,emp_lay_step5;
	 EditText emp_tv_first,emp_tv_emailid,emp_tv_emp_id,emp_tv_perhour,emp_tv_code,emp_tv_admin_emailid,emp_tv_admin_password, emp_tv_planned_letin, emp_tv_planned_letout;
	 Context ctx;
	 String REQUEST_DATA = "";
	 public View ctxView;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.member);
        
        emp_tv_first = (EditText) findViewById(R.id.emp_tv_first);
        emp_tv_emailid = (EditText) findViewById(R.id.emp_tv_emailid);
		emp_tv_emp_id  = (EditText) findViewById(R.id.emp_tv_emp_id);
		emp_tv_perhour= (EditText) findViewById(R.id.emp_tv_perhour);
		emp_tv_code = (EditText) findViewById(R.id.emp_tv_code);
		emp_tv_admin_emailid = (EditText) findViewById(R.id.emp_tv_admin_emailid);
		emp_tv_admin_password = (EditText) findViewById(R.id.reg_tv_admin_password);
        emp_tv_planned_letin = (EditText) findViewById(R.id.edtLetInPlanned);
        emp_tv_planned_letout = (EditText) findViewById(R.id.edtLetOutPlanned);


        emp_src_step1 = (ScrollView) findViewById(R.id.emp_src_step1);
		emp_lay_step2 = (LinearLayout) findViewById(R.id.emp_lay_step2);
		emp_lay_step3 = (LinearLayout) findViewById(R.id.emp_lay_step3);
		emp_lay_step4 = (LinearLayout) findViewById(R.id.emp_lay_step4);
        emp_lay_step5 = (LinearLayout) findViewById(R.id.emp_lay_step5);

        emp_btn_step1 = (Button) findViewById(R.id.emp_btn_step1);
        emp_btn_step2 = (Button) findViewById(R.id.emp_btn_step2);
        emp_btn_step3 = (Button) findViewById(R.id.emp_btn_step3);
        emp_btn_step4 = (Button) findViewById(R.id.emp_btn_step4);
        emp_btn_step5 = (Button) findViewById(R.id.emp_btn_step5);
        
        emp_tv_first.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals(""))
            			emp_btn_step1.setEnabled(false);
            		else 
            			emp_btn_step1.setEnabled(true);
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
        
        emp_tv_emailid.addTextChangedListener(new TextWatcher() {
            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals("") || s.toString().trim().indexOf("@") == -1 || s.toString().trim().indexOf(".") == -1)
            			emp_btn_step1.setEnabled(false);
            		else 
            			emp_btn_step1.setEnabled(true);
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
        emp_tv_perhour.addTextChangedListener(new TextWatcher() {
            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals(""))
            		{
            			
            			emp_btn_step1.setEnabled(false);
            		}
            		else 
            			emp_btn_step1.setEnabled(true);
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
        
          
        emp_tv_code.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {
            		if (s.toString().trim().equals(""))
            		{
            			emp_btn_step2.setEnabled(false);
            			
            		}
            		else if (s.toString().trim().substring(0,1).equals("0"))
        				emp_btn_step2.setEnabled(false);
            		else 
            			emp_btn_step2.setEnabled(true);
            		
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


        emp_btn_step5.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                emp_lay_step5.setVisibility(view.GONE);
                emp_lay_step2.setVisibility(view.VISIBLE);
                emp_tv_code.requestFocus();
            }
        });
            
        emp_btn_step1.setOnClickListener(new View.OnClickListener() {
	        @Override
	        public void onClick(View view) {
	        	
	        	  emp_src_step1.setVisibility(view.GONE);
                ShowPlannedTimeForm();
	        }
	        });
        
        emp_btn_step2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            	
            	emp_lay_step2.setVisibility(view.GONE);
            	emp_lay_step4.setVisibility(view.VISIBLE);
	        	emp_btn_step4.requestFocus();
            }
            });
        		
        emp_btn_step4.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            	ctxView = view;
            	REQUEST_DATA = emp_tv_first.getText().toString() + "-$-$-" + emp_tv_emailid.getText().toString() + "-$-$-" + emp_tv_emp_id.getText().toString() + "-$-$-" + emp_tv_perhour.getText().toString() + "-$-$-"+ emp_tv_code.getText().toString() + "-$-$-"+ emp_tv_admin_emailid.getText().toString() + "-$-$-"+ emp_tv_admin_password.getText().toString() + "-$-$-"+ emp_tv_planned_letin.getText().toString() + "-$-$-"+ emp_tv_planned_letout.getText().toString() + "-$-$-";
            	Log.e("REQUEST_DATA ",REQUEST_DATA );
            	DownloadWebPageTask task = new DownloadWebPageTask();
        		task.execute(new String[] { "http://kent.nasz.us/elog_php/member.php"});
        		
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

    private void ShowPlannedTimeForm(){
        emp_lay_step5.setVisibility(View.VISIBLE);
        emp_tv_planned_letin.requestFocus();
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
    	    	
    	    	if (result.trim().length() >= 5)
    	    	{
	    	    	if (result.trim().substring(0,4).equals("1001")) // When async Reposonse received update successfully with value 1001 so clear old sych data
	        		{
	    	    		result =result.substring(4);
	    	    		try
	    	    		{
	    	    			
	  			    	  
		    	    		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		    	    		String Session_data  = prefs.getString("session_data", null);
		    	    		if (Session_data == null) Session_data = "";
		    	    		Log.e("session_data before ",Session_data );
		    	    		if (Session_data != null) 
		    	    		{
		    	    			if (!Session_data.trim().equals(""))
		    	    			{
		    	    				  if (Session_data.indexOf("@#@#@#") == 0) Session_data = ""; 	

				    	    		  SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
				    	    		  Session_data  = Session_data + result;
				    	    		  editor.putString("session_data", Session_data);
				    		    	  
				    		    	  editor.commit();
				    		    	  Log.e("session_data after ",Session_data );
				    	    			
		    	    			}
		    	    		}
	    	    		
		    	    		Intent intent = new Intent(member.this, home_menu.class);
		    	    		
							startActivity(intent);
				      		finish();
				      		
	    	    		}
	    	    		catch(Exception ex){}
	    	    		
	    				return;
	        		}
	    	    	
    	    	}
    	    	 if (result.trim().equals("505")) // in incase if admin login is correct but member could'nt account created
        		{
    	    		
    	    		Toast.makeText(getApplicationContext(), "Please try again.. or Contact Device Team to help you or Device not able to communicate with database." , Toast.LENGTH_SHORT).show();
    	    		
        		}
    	    	else if (result.trim().equals("405")) // admin right is correct but that code is duplicate
        		{
    	    		
    	    		Toast.makeText(getApplicationContext(), "Unfortunately! The Code is not available. Someone already registered this code.", Toast.LENGTH_SHORT).show();
    	    		emp_lay_step4.setVisibility(ctxView.GONE);
                	emp_lay_step2.setVisibility(ctxView.VISIBLE);            	
    	        	
	    		
    	    		
        		}
    	    	else if (result.trim().equals("404")) // in incase if admin login is correct but member could'nt account created
        		{
    	    		
    	    		Toast.makeText(getApplicationContext(), "Admin Credential is not correct!", Toast.LENGTH_SHORT).show();
    	    		
        		}
    	    	
    	    }
    	  }
    	
    	@Override
    	public boolean onKeyDown(int keyCode, KeyEvent event) {
    		   if (keyCode == KeyEvent.KEYCODE_BACK) {
    	    		finish();
    		        return true;
    		    }
    	    return super.onKeyDown(keyCode, event);
    	}
    	
    
}
	